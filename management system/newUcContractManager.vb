Imports MySql.Data.MySqlClient

Public Class newUcContractManager
    Public Property PresetClientID As Integer = 0
    Public Property PresetSearchID As Integer = 0
    ' Database Connection String - Ensure this matches your XAMPP/MySQL config
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;Convert Zero Datetime=True;"
    Private _selectedContractID As Integer = 0

    ' 1. Load Data when the Form opens
    Private Sub newUcContractManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup UI visually
        SetupSortControls()

        ' Load the Service Dropdown dynamically from DB
        LoadServicesCombo()

        ' Pre-fill search if opened from another screen (Dashboard/Client List)
        If PresetSearchID > 0 Then
            Using conn As New MySqlConnection(connString)
                Try
                    conn.Open()
                    ' Safe concatenation for names
                    Dim sql As String = "SELECT CONCAT(IFNULL(ClientFirstName,''), ' ', IFNULL(ClientLastName,'')) " &
                                        "FROM tbl_contracts Con " &
                                        "JOIN tbl_clients C ON Con.ClientID = C.ClientID " &
                                        "WHERE Con.ContractID = @id"
                    Dim cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", PresetSearchID)
                    Dim name = cmd.ExecuteScalar()
                    If name IsNot Nothing Then txtSearch.Text = name.ToString()
                Catch ex As Exception
                    ' Ignore error on preset load, just continue
                End Try
            End Using
        End If

        ' Load the main grid
        LoadContractList()
    End Sub

    ' 1.1 Setup the Sort ComboBox manually
    Private Sub SetupSortControls()
        cboSortOrder.Items.Clear()
        cboSortOrder.Items.Add("Newest Date First") ' Index 0
        cboSortOrder.Items.Add("Oldest Date First") ' Index 1
        cboSortOrder.Items.Add("Client Name A-Z")   ' Index 2
        cboSortOrder.SelectedIndex = 0 ' Default to Newest
    End Sub

    ' 1.2 Load Services into the Filter ComboBox
    Private Sub LoadServicesCombo()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim sql As String = "SELECT ServiceID, ServiceName FROM tbl_services ORDER BY ServiceName ASC"
                Dim cmd As New MySqlCommand(sql, conn)
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                ' Add a dummy row for "All Services"
                Dim row As DataRow = dt.NewRow()
                row("ServiceID") = 0
                row("ServiceName") = "All Services"
                dt.Rows.InsertAt(row, 0)

                ' Bind to ComboBox
                cboServiceFilter.DisplayMember = "ServiceName"
                cboServiceFilter.ValueMember = "ServiceID"
                cboServiceFilter.DataSource = dt
            Catch ex As Exception
                MessageBox.Show("Error loading services: " & ex.Message)
            End Try
        End Using
    End Sub

    ' 2. Master Function to Load List with ALL Filters
    ' In newUcContractManager.vb

    Private Sub LoadContractList()
        Dim searchTerm As String = txtSearch.Text.Trim()

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' 1. START SQL QUERY
                Dim sql As String = "SELECT " &
                                "   Con.ContractID, " &
                                "   CONCAT(IFNULL(Cl.ClientFirstName,''), ' ', IFNULL(Cl.ClientLastName,'')) AS ClientName, " &
                                "   IFNULL(S.ServiceName, 'N/A') AS ServiceName, " &
                                "   CASE " &
                                "       WHEN (IFNULL(Con.TotalAmount,0) - IFNULL((SELECT SUM(AmountPaid) FROM tbl_payments WHERE ContractID = Con.ContractID), 0)) <= 0 THEN 'Paid' " &
                                "       ELSE IFNULL(Con.ContractStatus, 'Active') " &
                                "   END AS PaymentStatus, " &
                                "   Con.StartDate, " &
                                "   IFNULL(Con.TotalAmount, 0) AS TotalAmount, " &
                                "   (IFNULL(Con.TotalAmount,0) - IFNULL((SELECT SUM(AmountPaid) FROM tbl_payments WHERE ContractID = Con.ContractID), 0)) AS BalanceRemaining " &
                                "FROM tbl_contracts Con " &
                                "LEFT JOIN tbl_clients Cl ON Con.ClientID = Cl.ClientID " &
                                "LEFT JOIN tbl_services S ON Con.ServiceID = S.ServiceID "

                ' 2. DETERMINE FILTER LOGIC
                If PresetSearchID > 0 Then
                    ' Mode A: Searching for one specific contract
                    sql &= " WHERE Con.ContractID = @presetID "

                ElseIf PresetClientID > 0 Then
                    ' Mode B: Searching for ALL contracts for one Client (NEW)
                    sql &= " WHERE Con.ClientID = @presetClientID "

                Else
                    ' Mode C: Normal Text Search
                    sql &= " WHERE (Cl.ClientFirstName LIKE @search OR Cl.ClientLastName LIKE @search) "
                End If

                ' --- SERVICE FILTER (Only apply if NOT in preset mode) ---
                If PresetSearchID = 0 AndAlso cboServiceFilter.SelectedValue IsNot Nothing AndAlso IsNumeric(cboServiceFilter.SelectedValue) Then
                    If Convert.ToInt32(cboServiceFilter.SelectedValue) > 0 Then
                        sql &= " AND Con.ServiceID = @serviceID "
                    End If
                End If

                ' --- DATE RANGE FILTER (Only apply if NOT in preset mode) ---
                If PresetSearchID = 0 AndAlso chkDateFilter.Checked Then
                    sql &= " AND Con.StartDate BETWEEN @dateFrom AND @dateTo "
                End If

                ' --- SORTING ---
                Select Case cboSortOrder.SelectedIndex
                    Case 0 ' Newest Date First
                        sql &= " ORDER BY Con.StartDate DESC"
                    Case 1 ' Oldest Date First
                        sql &= " ORDER BY Con.StartDate ASC"
                    Case 2 ' Client Name A-Z
                        sql &= " ORDER BY ClientName ASC"
                    Case Else
                        sql &= " ORDER BY Con.ContractID DESC"
                End Select

                Dim cmd As New MySqlCommand(sql, conn)

                ' 3. ADD PARAMETERS
                ' 3. ADD PARAMETERS
                If PresetSearchID > 0 Then
                    cmd.Parameters.AddWithValue("@presetID", PresetSearchID)

                ElseIf PresetClientID > 0 Then
                    ' (NEW) Add the Client ID parameter
                    cmd.Parameters.AddWithValue("@presetClientID", PresetClientID)

                Else
                    ' Normal Search Params
                    cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")
                    ' ... keep your existing Date/Service filter logic here ...
                End If

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvContracts.DataSource = dt

                ' ... (Rest of your formatting code stays the same) ...

            Catch ex As Exception
                MessageBox.Show("Error Loading List: " & ex.Message)
            End Try
        End Using
    End Sub

    ' --- EVENT HANDLERS ---

    Private Sub chkDateFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkDateFilter.CheckedChanged
        dtpFrom.Enabled = chkDateFilter.Checked
        dtpTo.Enabled = chkDateFilter.Checked
        LoadContractList()
    End Sub

    Private Sub DateFilter_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged, dtpTo.ValueChanged
        If chkDateFilter.Checked Then
            LoadContractList()
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ' Clear the preset so we can search freely
        PresetSearchID = 0
        LoadContractList()
    End Sub

    Private Sub Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboServiceFilter.SelectedIndexChanged, cboSortOrder.SelectedIndexChanged
        LoadContractList()
    End Sub

    ' 4. WHEN USER CLICKS A ROW -> LOAD DETAILS TABS
    Private Sub dgvContracts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvContracts.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvContracts.Rows(e.RowIndex)
            ' Safety check for DBNull
            If row.Cells("ContractID").Value IsNot Nothing AndAlso IsNumeric(row.Cells("ContractID").Value) Then
                _selectedContractID = Convert.ToInt32(row.Cells("ContractID").Value)

                LoadOverview(row)
                LoadSchedule(_selectedContractID)
                LoadJobHistory(_selectedContractID)
                LoadPaymentHistory(_selectedContractID)
            End If
        End If
    End Sub

    Private Sub LoadOverview(row As DataGridViewRow)
        ' Using checking to prevent crashes on empty/null cells
        lblClientName.Text = "Client: " & If(IsDBNull(row.Cells("ClientName").Value), "N/A", row.Cells("ClientName").Value.ToString())
        lblService.Text = "Service: " & If(IsDBNull(row.Cells("ServiceName").Value), "N/A", row.Cells("ServiceName").Value.ToString())
        lblStatus.Text = "Status: " & If(IsDBNull(row.Cells("PaymentStatus").Value), "-", row.Cells("PaymentStatus").Value.ToString())

        Dim totalVal = row.Cells("TotalAmount").Value
        lblTotalAmount.Text = "Total: " & If(IsNumeric(totalVal), Convert.ToDecimal(totalVal).ToString("N2"), "0.00")

        Dim balVal = row.Cells("BalanceRemaining").Value
        lblBalance.Text = "Balance: " & If(IsNumeric(balVal), Convert.ToDecimal(balVal).ToString("N2"), "0.00")
    End Sub

    Private Sub LoadSchedule(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            ' Linked to tbl_paymentschedule [cite: 33]
            Dim sql As String = "SELECT InstallmentNumber, DueDate, AmountDue FROM tbl_paymentschedule WHERE ContractID = @id ORDER BY DueDate ASC"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", contractID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvSchedule.DataSource = dt

            ' Format
            If dgvSchedule.Columns("DueDate") IsNot Nothing Then dgvSchedule.Columns("DueDate").DefaultCellStyle.Format = "MMM dd, yyyy"
            If dgvSchedule.Columns("AmountDue") IsNot Nothing Then dgvSchedule.Columns("AmountDue").DefaultCellStyle.Format = "N2"
        End Using
    End Sub

    Private Sub LoadJobHistory(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            conn.Open() ' Explicitly open the connection

            ' FIXED SQL: Replaced 'T.FullName' with CONCAT(T.FirstName, ' ', T.LastName)
            Dim sql As String = "SELECT VisitNumber, ScheduledDate, J.Status, " &
                                "CONCAT(IFNULL(T.FirstName,''), ' ', IFNULL(T.LastName,'')) as Tech " &
                                "FROM tbl_joborders J " &
                                "LEFT JOIN tbl_users T ON J.TechnicianID = T.UserID " &
                                "WHERE J.ContractID = @id ORDER BY VisitNumber ASC"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", contractID)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()

            ' The crash was happening here because of the bad SQL
            da.Fill(dt)

            dgvJobHistory.DataSource = dt

            ' Format Date Column
            If dgvJobHistory.Columns("ScheduledDate") IsNot Nothing Then
                dgvJobHistory.Columns("ScheduledDate").DefaultCellStyle.Format = "MMM dd, yyyy"
            End If
        End Using
    End Sub

    Private Sub LoadPaymentHistory(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            ' Linked to tbl_payments [cite: 38]
            Dim sql As String = "SELECT PaymentDate, AmountPaid FROM tbl_payments WHERE ContractID = @id ORDER BY PaymentDate DESC"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", contractID)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvPayments.DataSource = dt

            ' Format
            If dgvPayments.Columns("PaymentDate") IsNot Nothing Then dgvPayments.Columns("PaymentDate").DefaultCellStyle.Format = "MMM dd, yyyy"
            If dgvPayments.Columns("AmountPaid") IsNot Nothing Then dgvPayments.Columns("AmountPaid").DefaultCellStyle.Format = "N2"
        End Using
    End Sub

    ' 5. Create New Contract Button
    Private Sub btnNewContract_Click(sender As Object, e As EventArgs) Handles btnNewContract.Click
        Dim main As frm_Main = CType(Application.OpenForms("frm_Main"), frm_Main)
        If main IsNot Nothing Then
            ' Ensure uc_NewContractEntry exists in your project
            main.LoadPage(New uc_NewContractEntry(), "Create New Contract")
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class