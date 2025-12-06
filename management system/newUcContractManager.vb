Imports MySql.Data.MySqlClient

Public Class newUcContractManager
    Public Property PresetSearchID As Integer = 0
    ' Database Connection String 
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedContractID As Integer = 0

    ' 1. Load Data when the Form opens
    Private Sub newUcContractManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Setup UI visually
        SetupSortControls()

        ' Load the Service Dropdown dynamically from DB
        LoadServicesCombo()

        If PresetSearchID > 0 Then
            ' Get Client Name to pre-fill search
            Using conn As New MySqlConnection(connString)
                conn.Open()
                Dim cmd As New MySqlCommand("SELECT C.ClientName FROM tbl_contracts Con JOIN tbl_clients C ON Con.ClientID = C.ClientID WHERE Con.ContractID = @id", conn)
                cmd.Parameters.AddWithValue("@id", PresetSearchID)
                Dim name = cmd.ExecuteScalar()
                If name IsNot Nothing Then txtSearch.Text = name.ToString()
            End Using
        End If

        ' Load the main grid
        LoadContractList()
    End Sub

    ' 1.1 Setup the Sort ComboBox manually if you didn't do it in Designer
    Private Sub SetupSortControls()
        ' Clear and add items to Sort Order Combo
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
                ' Get all unique service names from tbl_services
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

    ' 2. Function to Load the Master List with SORTING and FILTERING
    ' UPDATED: Accepts optional search, but pulls sort/filter directly from controls
    ' 2. Master Function to Load List with ALL Filters
    Private Sub LoadContractList()
        Dim searchTerm As String = txtSearch.Text.Trim()

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' Base Query
                Dim sql As String = "SELECT " &
                                    "   Con.ContractID, " &
                                    "   Cl.ClientName, " &
                                    "   S.ServiceName, " &
                                    "   V.PaymentStatus, " &
                                    "   Con.StartDate, " &
                                    "   Con.TotalAmount, " &
                                    "   V.BalanceRemaining " &
                                    "FROM tbl_contracts Con " &
                                    "LEFT JOIN tbl_clients Cl ON Con.ClientID = Cl.ClientID " &
                                    "LEFT JOIN tbl_services S ON Con.ServiceID = S.ServiceID " &
                                    "LEFT JOIN view_contract_details V ON Con.ContractID = V.ContractID " &
                                    "WHERE Cl.ClientName LIKE @search "

                ' --- 1. SERVICE FILTER ---
                If cboServiceFilter.SelectedValue IsNot Nothing AndAlso IsNumeric(cboServiceFilter.SelectedValue) Then
                    If Convert.ToInt32(cboServiceFilter.SelectedValue) > 0 Then
                        sql &= " AND Con.ServiceID = @serviceID "
                    End If
                End If

                ' --- 2. DATE RANGE FILTER ---
                ' Only apply if the checkbox is checked
                If chkDateFilter.Checked Then
                    sql &= " AND Con.StartDate BETWEEN @dateFrom AND @dateTo "
                End If

                ' --- 3. SORTING ---
                Select Case cboSortOrder.SelectedIndex
                    Case 0 ' Newest Date First
                        sql &= " ORDER BY Con.StartDate DESC"
                    Case 1 ' Oldest Date First
                        sql &= " ORDER BY Con.StartDate ASC"
                    Case 2 ' Client Name A-Z
                        sql &= " ORDER BY Cl.ClientName ASC"
                    Case Else
                        sql &= " ORDER BY Con.ContractID DESC"
                End Select

                Dim cmd As New MySqlCommand(sql, conn)

                ' Add Parameters
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")

                ' Service Param
                If cboServiceFilter.SelectedValue IsNot Nothing AndAlso IsNumeric(cboServiceFilter.SelectedValue) Then
                    If Convert.ToInt32(cboServiceFilter.SelectedValue) > 0 Then
                        cmd.Parameters.AddWithValue("@serviceID", cboServiceFilter.SelectedValue)
                    End If
                End If

                ' Date Params
                If chkDateFilter.Checked Then
                    ' .Date ensures we get the start of the day (00:00:00)
                    cmd.Parameters.AddWithValue("@dateFrom", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@dateTo", dtpTo.Value.Date)
                End If

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvContracts.DataSource = dt

                ' Hide ID column if exists
                If dgvContracts.Columns("ContractID") IsNot Nothing Then
                    dgvContracts.Columns("ContractID").Visible = False
                End If

            Catch ex As Exception
                ' Silent fail or log
            End Try
        End Using
    End Sub

    ' --- EVENT HANDLERS ---

    ' Trigger load when Checkbox is checked/unchecked
    Private Sub chkDateFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkDateFilter.CheckedChanged
        ' Optional: Enable/Disable the pickers visually
        dtpFrom.Enabled = chkDateFilter.Checked
        dtpTo.Enabled = chkDateFilter.Checked

        LoadContractList()
    End Sub

    ' Trigger load when Dates change (Only if filter is active)
    Private Sub DateFilter_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged, dtpTo.ValueChanged
        If chkDateFilter.Checked Then
            LoadContractList()
        End If
    End Sub

    ' 3. Search Button
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadContractList()
    End Sub

    ' 3.1 NEW: Event when Filtering/Sorting Changes
    ' Link this event to BOTH cboServiceFilter and cboSortOrder in Designer Properties -> Events
    Private Sub Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboServiceFilter.SelectedIndexChanged, cboSortOrder.SelectedIndexChanged
        LoadContractList()
    End Sub

    ' 4. WHEN USER CLICKS A ROW -> LOAD DETAILS TABS
    Private Sub dgvContracts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvContracts.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvContracts.Rows(e.RowIndex)
            _selectedContractID = Convert.ToInt32(row.Cells("ContractID").Value)

            LoadOverview(row)
            LoadSchedule(_selectedContractID)
            LoadJobHistory(_selectedContractID)
            LoadPaymentHistory(_selectedContractID)
        End If
    End Sub

    ' ... (Keep your existing LoadOverview, LoadSchedule, LoadJobHistory, LoadPaymentHistory logic exactly as is) ...

    Private Sub LoadOverview(row As DataGridViewRow)
        lblClientName.Text = "Client: " & row.Cells("ClientName").Value.ToString()
        lblService.Text = "Service: " & row.Cells("ServiceName").Value.ToString()
        lblStatus.Text = "Status: " & row.Cells("PaymentStatus").Value.ToString()
        lblTotalAmount.Text = "Total: " & row.Cells("TotalAmount").Value.ToString()
        lblBalance.Text = "Balance: " & row.Cells("BalanceRemaining").Value.ToString()
    End Sub

    Private Sub LoadSchedule(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            Dim sql As String = "SELECT InstallmentNumber, DueDate, AmountDue FROM tbl_paymentschedule WHERE ContractID = @id ORDER BY DueDate ASC"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", contractID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvSchedule.DataSource = dt
        End Using
    End Sub

    Private Sub LoadJobHistory(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            Dim sql As String = "SELECT VisitNumber, ScheduledDate, J.Status, T.FullName as Tech " &
                                "FROM tbl_joborders J " &
                                "LEFT JOIN tbl_users T ON J.TechnicianID = T.UserID " &
                                "WHERE J.ContractID = @id ORDER BY VisitNumber ASC"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", contractID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvJobHistory.DataSource = dt
        End Using
    End Sub

    Private Sub LoadPaymentHistory(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            Dim sql As String = "SELECT PaymentDate, AmountPaid, OR_Number FROM tbl_payments WHERE ContractID = @id ORDER BY PaymentDate DESC"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", contractID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvPayments.DataSource = dt
        End Using
    End Sub

    ' 5. Create New Contract Button
    Private Sub btnNewContract_Click(sender As Object, e As EventArgs) Handles btnNewContract.Click
        Dim main As frm_Main = CType(Application.OpenForms("frm_Main"), frm_Main)
        If main IsNot Nothing Then
            main.LoadPage(New uc_NewContractEntry(), "Create New Contract")
        End If
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub tpOverview_Click(sender As Object, e As EventArgs) Handles tpOverview.Click

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class