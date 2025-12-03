Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class newUcBilling

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    ' Variables to track selection
    Private _selectedContractID As Integer = 0
    Private _selectedScheduleID As Integer = 0
    Private _currentBalance As Decimal = 0

    Private Sub newUcBilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize Search Box Style
        txtSearchBilling.Text = "Search Client..."
        txtSearchBilling.ForeColor = Color.Gray

        LoadBillingData("")
    End Sub

    ' ==========================================
    ' 1. MASTER LIST (Safe Version)
    ' ==========================================
    Private Sub LoadBillingData(search As String)
        Dim previouslySelectedID As Integer = _selectedContractID

        ' Handle Placeholder Text logic for the Query
        If search.Trim() = "Search Client..." Then
            search = ""
        End If

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' SQL joins Contracts with Clients to allow searching by ClientName
                Dim sql As String = "SELECT Con.ContractID, Cli.ClientName, Ser.ServiceName, Con.ServiceFrequency, " &
                                    "Con.PaymentStatus, Con.BalanceRemaining, Con.NextVisitDate " &
                                    "FROM tbl_Contracts Con " &
                                    "JOIN tbl_Clients Cli ON Con.ClientID = Cli.ClientID " &
                                    "JOIN tbl_Services Ser ON Con.ServiceID = Ser.ServiceID " &
                                    "WHERE Cli.ClientName LIKE @s ORDER BY Con.NextVisitDate ASC"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@s", "%" & search & "%")
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvBilling.DataSource = dt

                ' --- FIX: Safety Checks Added Here ---
                If dgvBilling.Columns("ContractID") IsNot Nothing Then
                    dgvBilling.Columns("ContractID").Visible = False
                End If

                If dgvBilling.Columns("BalanceRemaining") IsNot Nothing Then
                    dgvBilling.Columns("BalanceRemaining").DefaultCellStyle.Format = "N2"
                End If

                If dgvBilling.Columns("NextVisitDate") IsNot Nothing Then
                    dgvBilling.Columns("NextVisitDate").DefaultCellStyle.Format = "MMM dd, yyyy"
                End If
                ' -------------------------------------

                ' --- RESTORE SELECTION (SAFE LOOP) ---
                If previouslySelectedID > 0 Then
                    Dim found As Boolean = False
                    For Each row As DataGridViewRow In dgvBilling.Rows
                        If row.IsNewRow Then Continue For

                        Dim cellVal As Object = row.Cells("ContractID").Value
                        If cellVal IsNot Nothing AndAlso IsNumeric(cellVal) Then
                            If Convert.ToInt32(cellVal) = previouslySelectedID Then
                                row.Selected = True
                                ' Safety check to prevent crash if cell 1 doesn't exist
                                If row.Cells.Count > 1 Then
                                    dgvBilling.CurrentCell = row.Cells(1)
                                End If
                                SelectContract(row)
                                found = True
                                Exit For
                            End If
                        End If
                    Next
                    If Not found Then ClearRightPanel()
                End If

            Catch ex As Exception
                MessageBox.Show("Error Loading Data: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 2. HELPER: SELECT CONTRACT & UPDATE UI
    ' ==========================================
    Private Sub SelectContract(row As DataGridViewRow)
        ' Safety check for value existence
        If row.Cells("ContractID").Value IsNot Nothing Then
            _selectedContractID = Convert.ToInt32(row.Cells("ContractID").Value)
        End If

        If row.Cells("BalanceRemaining").Value IsNot Nothing Then
            _currentBalance = Convert.ToDecimal(row.Cells("BalanceRemaining").Value)
        End If

        ' Update Labels
        lblContractInfo.Text = row.Cells("ClientName").Value.ToString()
        lblBalance.Text = _currentBalance.ToString("N2")

        ' Clear previous payment inputs
        txtPayAmount.Clear()
        _selectedScheduleID = 0

        ' Load the Schedule Grid
        LoadPaymentSchedule(_selectedContractID)
    End Sub

    Private Sub ClearRightPanel()
        _selectedContractID = 0
        lblContractInfo.Text = "---"
        lblBalance.Text = "0.00"
        dgvSchedule.DataSource = Nothing
    End Sub

    ' ==========================================
    ' 3. CLICK EVENTS
    ' ==========================================
    Private Sub dgvBilling_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBilling.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvBilling.Rows(e.RowIndex)
            SelectContract(row)
        End If
    End Sub

    Private Sub dgvSchedule_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSchedule.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvSchedule.Rows(e.RowIndex)

            If row.Cells("Status").Value.ToString() = "Paid" Then
                MessageBox.Show("This installment is already paid.")
                txtPayAmount.Clear()
                _selectedScheduleID = 0
                Exit Sub
            End If

            _selectedScheduleID = Convert.ToInt32(row.Cells("ScheduleID").Value)
            Dim amt As Decimal = Convert.ToDecimal(row.Cells("AmountDue").Value)
            txtPayAmount.Text = amt.ToString("0.00")
        End If
    End Sub

    ' ==========================================
    ' 4. SAVE PAYMENT
    ' ==========================================
    Private Sub btnSavePayment_Click(sender As Object, e As EventArgs) Handles btnSavePayment.Click
        If _selectedContractID = 0 Then
            MessageBox.Show("Please select a contract first.")
            Exit Sub
        End If

        Dim amountPay As Decimal = 0
        If Not Decimal.TryParse(txtPayAmount.Text, amountPay) Then
            MessageBox.Show("Invalid Amount.")
            Exit Sub
        End If

        ' Save current search text to use for refresh later
        Dim currentSearchTerm As String = txtSearchBilling.Text

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim trans As MySqlTransaction = conn.BeginTransaction()

            Try
                ' A. Record Payment
                Dim sqlPay As String = "INSERT INTO tbl_Payments (ContractID, AmountPaid, PaymentDate) VALUES (@cid, @amt, @date)"
                Dim cmdPay As New MySqlCommand(sqlPay, conn, trans)
                cmdPay.Parameters.AddWithValue("@cid", _selectedContractID)
                cmdPay.Parameters.AddWithValue("@amt", amountPay)
                cmdPay.Parameters.AddWithValue("@date", DateTime.Now)
                cmdPay.ExecuteNonQuery()

                ' B. Update Schedule Status
                If _selectedScheduleID > 0 Then
                    Dim sqlSched As String = "UPDATE tbl_PaymentSchedule SET Status = 'Paid' WHERE ScheduleID = @sid"
                    Dim cmdSched As New MySqlCommand(sqlSched, conn, trans)
                    cmdSched.Parameters.AddWithValue("@sid", _selectedScheduleID)
                    cmdSched.ExecuteNonQuery()
                End If

                ' C. Update Parent Contract
                Dim sqlBal As String = "SELECT (TotalAmount - (SELECT COALESCE(SUM(AmountPaid),0) FROM tbl_Payments WHERE ContractID = @cid)) FROM tbl_Contracts WHERE ContractID = @cid"
                Dim cmdBal As New MySqlCommand(sqlBal, conn, trans)
                cmdBal.Parameters.AddWithValue("@cid", _selectedContractID)
                Dim newBalance As Decimal = Convert.ToDecimal(cmdBal.ExecuteScalar())

                Dim newStatus As String = If(newBalance <= 0, "Fully Paid", "With Balance")

                Dim sqlUp As String = "UPDATE tbl_Contracts SET BalanceRemaining = @bal, PaymentStatus = @stat WHERE ContractID = @cid"
                Dim cmdUp As New MySqlCommand(sqlUp, conn, trans)
                cmdUp.Parameters.AddWithValue("@bal", newBalance)
                cmdUp.Parameters.AddWithValue("@stat", newStatus)
                cmdUp.Parameters.AddWithValue("@cid", _selectedContractID)
                cmdUp.ExecuteNonQuery()

                trans.Commit()
                MessageBox.Show("Payment Success!")

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error: " & ex.Message)
                Exit Sub ' Stop here if error, do not reload grid
            End Try
        End Using

        ' D. REFRESH EVERYTHING
        LoadBillingData(currentSearchTerm)
    End Sub

    ' ==========================================
    ' 5. HELPERS (Schedule & Colors)
    ' ==========================================
    Private Sub LoadPaymentSchedule(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            Dim sql As String = "SELECT ScheduleID, InstallmentNumber AS 'Give #', DueDate, AmountDue, Status " &
                                "FROM tbl_PaymentSchedule WHERE ContractID = @cid ORDER BY DueDate ASC"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@cid", contractID)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvSchedule.DataSource = dt

            ' --- FIX: Safety Checks Added Here ---
            If dgvSchedule.Columns("ScheduleID") IsNot Nothing Then
                dgvSchedule.Columns("ScheduleID").Visible = False
            End If

            If dgvSchedule.Columns("DueDate") IsNot Nothing Then
                dgvSchedule.Columns("DueDate").DefaultCellStyle.Format = "MMM dd, yyyy"
            End If

            If dgvSchedule.Columns("AmountDue") IsNot Nothing Then
                dgvSchedule.Columns("AmountDue").DefaultCellStyle.Format = "N2"
            End If
            ' -------------------------------------

            ColorScheduleRows()
        End Using
    End Sub

    Private Sub dgvBilling_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvBilling.DataBindingComplete
        CheckBalances()
    End Sub

    Private Sub CheckBalances()
        For Each row As DataGridViewRow In dgvBilling.Rows
            If row.IsNewRow Then Continue For

            ' Safety check to ensure column exists before accessing it
            If dgvBilling.Columns.Contains("BalanceRemaining") Then
                Dim cellVal As Object = row.Cells("BalanceRemaining").Value
                If cellVal IsNot Nothing AndAlso IsNumeric(cellVal) Then
                    Dim bal As Decimal = Convert.ToDecimal(cellVal)
                    If bal > 0 Then
                        row.DefaultCellStyle.BackColor = Color.MistyRose
                        row.DefaultCellStyle.SelectionBackColor = Color.Red
                    Else
                        row.DefaultCellStyle.BackColor = Color.LightGreen
                        row.DefaultCellStyle.SelectionBackColor = Color.Green
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub ColorScheduleRows()
        For Each row As DataGridViewRow In dgvSchedule.Rows
            ' Safety check to ensure columns exist
            If dgvSchedule.Columns.Contains("Status") AndAlso dgvSchedule.Columns.Contains("DueDate") Then
                Dim status As String = row.Cells("Status").Value.ToString()
                Dim dueDate As Date = Convert.ToDateTime(row.Cells("DueDate").Value)

                If status = "Paid" Then
                    row.DefaultCellStyle.BackColor = Color.LightGreen
                ElseIf dueDate < DateTime.Now And status = "Pending" Then
                    row.DefaultCellStyle.BackColor = Color.LightSalmon
                End If
            End If
        Next
    End Sub

    ' ==========================================
    ' 6. SEARCH & PLACEHOLDER LOGIC (NEW)
    ' ==========================================

    ' Trigger Live Search when typing
    Private Sub txtSearchBilling_TextChanged(sender As Object, e As EventArgs) Handles txtSearchBilling.TextChanged
        LoadBillingData(txtSearchBilling.Text)
    End Sub

    ' Remove placeholder when user clicks inside
    Private Sub txtSearchBilling_Enter(sender As Object, e As EventArgs) Handles txtSearchBilling.Enter
        If txtSearchBilling.Text = "Search Client..." Then
            txtSearchBilling.Text = ""
            txtSearchBilling.ForeColor = Color.Black
        End If
    End Sub

    ' Restore placeholder if user leaves it empty
    Private Sub txtSearchBilling_Leave(sender As Object, e As EventArgs) Handles txtSearchBilling.Leave
        If String.IsNullOrWhiteSpace(txtSearchBilling.Text) Then
            txtSearchBilling.Text = "Search Client..."
            txtSearchBilling.ForeColor = Color.Gray
        End If
    End Sub

End Class