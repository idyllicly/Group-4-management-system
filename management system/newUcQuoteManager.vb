Imports MySql.Data.MySqlClient

Public Class newUcQuoteManager

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedQuoteID As Integer = 0

    ' ==========================================
    ' 1. FORM LOAD & SETUP
    ' ==========================================
    Private Sub newUcQuoteManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Setup UI Controls
        SetupSortControls()
        LoadServicesCombo()

        ' 2. Run Logic
        UpdateAutoExpire() ' Check for expired quotes immediately
        LoadQuotations() ' Load the list with default filters
        FormatGrid()
    End Sub

    Private Sub SetupSortControls()
        cboSortOrder.Items.Clear()
        cboSortOrder.Items.Add("Newest Date First") ' Index 0
        cboSortOrder.Items.Add("Oldest Date First") ' Index 1
        cboSortOrder.Items.Add("Highest Price First") ' Index 2
        cboSortOrder.Items.Add("Status (Grouped)")    ' Index 3
        cboSortOrder.SelectedIndex = 0
    End Sub

    Private Sub LoadServicesCombo()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' Fetch services from DB
                Dim sql As String = "SELECT ServiceID, ServiceName FROM tbl_services ORDER BY ServiceName ASC"
                Dim cmd As New MySqlCommand(sql, conn)
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                ' Add "All Services" option
                Dim row As DataRow = dt.NewRow()
                row("ServiceID") = 0
                row("ServiceName") = "All Services"
                dt.Rows.InsertAt(row, 0)

                cboServiceFilter.DisplayMember = "ServiceName"
                cboServiceFilter.ValueMember = "ServiceID"
                cboServiceFilter.DataSource = dt
            Catch ex As Exception
                ' Silent fail or simple log
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 2. AUTO-EXPIRE LOGIC (Existing)
    ' ==========================================
    Private Sub UpdateAutoExpire()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' SQL Update: Set Status to 'Unresponsive' if Pending > 30 days
                Dim sql As String = "UPDATE tbl_quotations " &
                                    "SET Status = 'Unresponsive' " &
                                    "WHERE Status = 'Pending' AND DATEDIFF(NOW(), DateCreated) >= 30"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Console.WriteLine("Auto-expire check failed: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 3. LOAD DATA (Refined with Filters)
    ' ==========================================
    Private Sub LoadQuotations()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' --- BUILD QUERY ---
                ' UPDATED: Concatenate First and Last Name for ClientName
                Dim sql As String = "SELECT " &
                                    "   Q.QuoteID, " &
                                    "   Q.DateCreated, " &
                                    "   CONCAT(C.ClientFirstName, ' ', C.ClientLastName) AS ClientName, " &
                                    "   S.ServiceName AS 'Proposed Package', " &
                                    "   Q.AreaSize_Sqm, " &
                                    "   Q.InfestationLevel, " &
                                    "   Q.QuotedPrice, " &
                                    "   Q.Remarks, " &
                                    "   Q.Status " &
                                    "FROM tbl_quotations Q " &
                                    "LEFT JOIN tbl_clients C ON Q.ClientID = C.ClientID " &
                                    "LEFT JOIN tbl_services S ON Q.ProposedServiceID = S.ServiceID " &
                                    "WHERE 1=1 " ' Dummy clause to make appending easier

                ' --- FILTER 1: SERVICE ---
                If cboServiceFilter.SelectedValue IsNot Nothing AndAlso IsNumeric(cboServiceFilter.SelectedValue) Then
                    If Convert.ToInt32(cboServiceFilter.SelectedValue) > 0 Then
                        sql &= " AND Q.ProposedServiceID = @serviceID "
                    End If
                End If

                ' --- FILTER 2: DATE RANGE ---
                If chkDateFilter.Checked Then
                    sql &= " AND Q.DateCreated BETWEEN @dateFrom AND @dateTo "
                End If

                ' --- SORTING ---
                Select Case cboSortOrder.SelectedIndex
                    Case 0 ' Newest
                        sql &= " ORDER BY Q.DateCreated DESC"
                    Case 1 ' Oldest
                        sql &= " ORDER BY Q.DateCreated ASC"
                    Case 2 ' Highest Price
                        sql &= " ORDER BY Q.QuotedPrice DESC"
                    Case 3 ' Status
                        sql &= " ORDER BY Q.Status ASC, Q.DateCreated DESC"
                    Case Else
                        sql &= " ORDER BY Q.QuoteID DESC"
                End Select

                Dim cmd As New MySqlCommand(sql, conn)

                ' --- PARAMETERS ---
                ' Service
                If cboServiceFilter.SelectedValue IsNot Nothing AndAlso IsNumeric(cboServiceFilter.SelectedValue) Then
                    If Convert.ToInt32(cboServiceFilter.SelectedValue) > 0 Then
                        cmd.Parameters.AddWithValue("@serviceID", cboServiceFilter.SelectedValue)
                    End If
                End If

                ' Date
                If chkDateFilter.Checked Then
                    ' Use .Date to capture the whole day
                    cmd.Parameters.AddWithValue("@dateFrom", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@dateTo", dtpTo.Value.Date.AddDays(1).AddSeconds(-1)) ' End of day
                End If

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvQuotes.DataSource = dt

            Catch ex As Exception
                MessageBox.Show("Error loading quotes: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 4. FILTER EVENTS (Triggers)
    ' ==========================================
    Private Sub Filters_Changed(sender As Object, e As EventArgs) Handles cboServiceFilter.SelectedIndexChanged, cboSortOrder.SelectedIndexChanged
        LoadQuotations()
    End Sub

    Private Sub chkDateFilter_CheckedChanged(sender As Object, e As EventArgs) Handles chkDateFilter.CheckedChanged
        dtpFrom.Enabled = chkDateFilter.Checked
        dtpTo.Enabled = chkDateFilter.Checked
        LoadQuotations()
    End Sub

    Private Sub DatePickers_ValueChanged(sender As Object, e As EventArgs) Handles dtpFrom.ValueChanged, dtpTo.ValueChanged
        If chkDateFilter.Checked Then LoadQuotations()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        UpdateAutoExpire()
        LoadQuotations()
    End Sub

    ' ==========================================
    ' 5. GRID FORMATTING & SELECTION
    ' ==========================================
    Private Sub FormatGrid()
        If dgvQuotes.Columns("QuoteID") IsNot Nothing Then dgvQuotes.Columns("QuoteID").Visible = False

        If dgvQuotes.Columns("QuotedPrice") IsNot Nothing Then
            dgvQuotes.Columns("QuotedPrice").DefaultCellStyle.Format = "N2"
            dgvQuotes.Columns("QuotedPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        dgvQuotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvQuotes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvQuotes.ReadOnly = True
    End Sub

    Private Sub dgvQuotes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvQuotes.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvQuotes.Rows(e.RowIndex)
            _selectedQuoteID = Convert.ToInt32(row.Cells("QuoteID").Value)
        End If
    End Sub

    Private Sub dgvQuotes_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvQuotes.CellFormatting
        If dgvQuotes.Columns(e.ColumnIndex).Name = "Status" Then
            Dim status As String = Convert.ToString(e.Value)
            If status = "Approved" Then
                e.CellStyle.BackColor = Color.LightGreen
                e.CellStyle.ForeColor = Color.DarkGreen
            ElseIf status = "Pending" Then
                e.CellStyle.BackColor = Color.LightYellow
                e.CellStyle.ForeColor = Color.Orange
            ElseIf status = "Unresponsive" Then
                e.CellStyle.BackColor = Color.LightGray
                e.CellStyle.ForeColor = Color.DimGray
            End If
        End If
    End Sub

    ' ==========================================
    ' 6. APPROVE & CREATE CONTRACT (Existing)
    ' ==========================================
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If _selectedQuoteID = 0 Then
            MessageBox.Show("Please select a quotation to approve.")
            Exit Sub
        End If

        If MessageBox.Show("Client Accepted?" & vbCrLf & "Proceed to create contract?", "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            ' A. FETCH QUOTE DATA
            Dim clientID As Integer = 0
            Dim serviceID As Integer = 0
            Dim price As Decimal = 0

            Using conn As New MySqlConnection(connString)
                Try
                    conn.Open()
                    Dim sqlGet As String = "SELECT ClientID, ProposedServiceID, QuotedPrice FROM tbl_quotations WHERE QuoteID = @qid"
                    Dim cmdGet As New MySqlCommand(sqlGet, conn)
                    cmdGet.Parameters.AddWithValue("@qid", _selectedQuoteID)

                    Dim dt As New DataTable()
                    Dim da As New MySqlDataAdapter(cmdGet)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        clientID = If(IsDBNull(row("ClientID")), 0, Convert.ToInt32(row("ClientID")))
                        serviceID = If(IsDBNull(row("ProposedServiceID")), 0, Convert.ToInt32(row("ProposedServiceID")))
                        price = Convert.ToDecimal(row("QuotedPrice"))
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error fetching quote: " & ex.Message)
                    Exit Sub
                End Try
            End Using

            ' B. NAVIGATE TO CONTRACT ENTRY
            Dim mainForm As frm_Main = TryCast(Application.OpenForms("frm_Main"), frm_Main)

            If mainForm IsNot Nothing Then
                Dim newContractPage As New uc_NewContractEntry()
                ' Pass the data!
                newContractPage.PreFillData(clientID, serviceID, price, _selectedQuoteID)
                ' Switch Screen
                mainForm.LoadPage(newContractPage, "Create Contract (From Quote)")
            Else
                MessageBox.Show("Error: Cannot find Main Form to navigate.")
            End If

        End If
    End Sub

End Class