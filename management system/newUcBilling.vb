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
    ' 1. MASTER LIST (Updated for Normalized DB)
    ' ==========================================
    Private Sub LoadBillingData(search As String)
        Dim previouslySelectedID As Integer = _selectedContractID

        ' Handle Placeholder Text
        If search.Trim() = "Search Client..." Then search = ""

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' UPDATED SQL:
                ' 1. Removed NextVisitDate (It's not in the contract table anymore).
                ' 2. Joined 'view_contract_details' (V) to get Balance and Status.
                Dim sql As String = "SELECT " &
                                    "   Con.ContractID, " &
                                    "   Cli.ClientName, " &
                                    "   Ser.ServiceName, " &
                                    "   Con.ServiceFrequency, " &
                                    "   V.PaymentStatus, " &
                                    "   V.BalanceRemaining " &
                                    "FROM tbl_contracts Con " &
                                    "LEFT JOIN tbl_clients Cli ON Con.ClientID = Cli.ClientID " &
                                    "LEFT JOIN tbl_services Ser ON Con.ServiceID = Ser.ServiceID " &
                                    "LEFT JOIN view_contract_details V ON Con.ContractID = V.ContractID " &
                                    "WHERE Cli.ClientName LIKE @s " &
                                    "ORDER BY Con.ContractID DESC"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@s", "%" & search & "%")
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvBilling.DataSource = dt

                ' --- UI CLEANUP ---
                If dgvBilling.Columns("ContractID") IsNot Nothing Then
                    dgvBilling.Columns("ContractID").Visible = False
                End If

                If dgvBilling.Columns("BalanceRemaining") IsNot Nothing Then
                    dgvBilling.Columns("BalanceRemaining").DefaultCellStyle.Format = "N2"
                End If
                ' ------------------

                ' --- RESTORE SELECTION ---
                If previouslySelectedID > 0 Then
                    For Each row As DataGridViewRow In dgvBilling.Rows
                        If row.Cells("ContractID").Value IsNot Nothing AndAlso Convert.ToInt32(row.Cells("ContractID").Value) = previouslySelectedID Then
                            row.Selected = True
                            SelectContract(row)
                            Exit For
                        End If
                    Next
                End If

            Catch ex As Exception
                MessageBox.Show("Error Loading Data: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 2. HELPER: SELECT CONTRACT
    ' ==========================================
    Private Sub SelectContract(row As DataGridViewRow)
        If row.Cells("ContractID").Value IsNot Nothing Then
            _selectedContractID = Convert.ToInt32(row.Cells("ContractID").Value)
        End If

        ' Get Balance from the VIEW column
        If row.Cells("BalanceRemaining").Value IsNot Nothing Then
            _currentBalance = Convert.ToDecimal(row.Cells("BalanceRemaining").Value)
        End If

        lblContractInfo.Text = row.Cells("ClientName").Value.ToString()
        lblBalance.Text = _currentBalance.ToString("N2")

        txtPayAmount.Clear()
        _selectedScheduleID = 0

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
            SelectContract(dgvBilling.Rows(e.RowIndex))
        End If
    End Sub

    Private Sub dgvSchedule_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSchedule.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvSchedule.Rows(e.RowIndex)

            ' Prevent paying already paid items
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
    ' 4. SAVE PAYMENT (Simplified for Logic)
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

        Dim currentSearchTerm As String = txtSearchBilling.Text

        Using conn As New MySqlConnection(connString)
            conn.Open()
            ' We don't need a transaction for just one insert anymore
            Try
                ' UPDATED SQL:
                ' 1. Insert Payment linked to the ScheduleID
                ' 2. We DO NOT update tbl_contracts (Balance is auto-calculated).
                ' 3. We DO NOT update tbl_paymentschedule status (It's auto-calculated).

                Dim sqlPay As String = "INSERT INTO tbl_payments (ContractID, ScheduleID, AmountPaid, PaymentDate) VALUES (@cid, @sid, @amt, @date)"

                Dim cmdPay As New MySqlCommand(sqlPay, conn)
                cmdPay.Parameters.AddWithValue("@cid", _selectedContractID)

                ' Handle cases where no specific schedule is selected (General Payment)
                If _selectedScheduleID > 0 Then
                    cmdPay.Parameters.AddWithValue("@sid", _selectedScheduleID)
                Else
                    cmdPay.Parameters.AddWithValue("@sid", DBNull.Value)
                End If

                cmdPay.Parameters.AddWithValue("@amt", amountPay)
                cmdPay.Parameters.AddWithValue("@date", DateTime.Now)

                cmdPay.ExecuteNonQuery()

                MessageBox.Show("Payment Saved Successfully!")

                ' Refresh Data
                LoadBillingData(currentSearchTerm)
                ' Reload Schedule to show the new "Paid" status
                LoadPaymentSchedule(_selectedContractID)

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 5. HELPERS (Schedule & Colors)
    ' ==========================================
    Private Sub LoadPaymentSchedule(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            ' UPDATED SQL:
            ' We removed the 'Status' column from the table.
            ' We now CALCULATE 'Status' by checking if a Payment exists for that ScheduleID.

            Dim sql As String = "SELECT " &
                                "   S.ScheduleID, " &
                                "   S.InstallmentNumber AS 'Inst #', " &
                                "   S.DueDate, " &
                                "   S.AmountDue, " &
                                "   CASE " &
                                "       WHEN SUM(P.AmountPaid) >= S.AmountDue THEN 'Paid' " &
                                "       ELSE 'Pending' " &
                                "   END AS Status " &
                                "FROM tbl_paymentschedule S " &
                                "LEFT JOIN tbl_payments P ON S.ScheduleID = P.ScheduleID " &
                                "WHERE S.ContractID = @cid " &
                                "GROUP BY S.ScheduleID " &
                                "ORDER BY S.DueDate ASC"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@cid", contractID)

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvSchedule.DataSource = dt

            ' Formatting
            If dgvSchedule.Columns("ScheduleID") IsNot Nothing Then
                dgvSchedule.Columns("ScheduleID").Visible = False
            End If
            If dgvSchedule.Columns("DueDate") IsNot Nothing Then
                dgvSchedule.Columns("DueDate").DefaultCellStyle.Format = "MMM dd, yyyy"
            End If
            If dgvSchedule.Columns("AmountDue") IsNot Nothing Then
                dgvSchedule.Columns("AmountDue").DefaultCellStyle.Format = "N2"
            End If

            ColorScheduleRows()
        End Using
    End Sub

    Private Sub dgvBilling_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs) Handles dgvBilling.DataBindingComplete
        CheckBalances()
    End Sub

    Private Sub CheckBalances()
        For Each row As DataGridViewRow In dgvBilling.Rows
            If row.IsNewRow Then Continue For

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
            If dgvSchedule.Columns.Contains("Status") AndAlso dgvSchedule.Columns.Contains("DueDate") Then
                Dim status As String = row.Cells("Status").Value.ToString()
                ' Handle DBNull safely
                Dim dueDate As Date = DateTime.Now
                If Not IsDBNull(row.Cells("DueDate").Value) Then
                    dueDate = Convert.ToDateTime(row.Cells("DueDate").Value)
                End If

                If status = "Paid" Then
                    row.DefaultCellStyle.BackColor = Color.LightGreen
                ElseIf dueDate < DateTime.Now And status = "Pending" Then
                    row.DefaultCellStyle.BackColor = Color.LightSalmon
                End If
            End If
        Next
    End Sub

    ' ==========================================
    ' 6. SEARCH & PLACEHOLDER LOGIC
    ' ==========================================
    Private Sub txtSearchBilling_TextChanged(sender As Object, e As EventArgs) Handles txtSearchBilling.TextChanged
        LoadBillingData(txtSearchBilling.Text)
    End Sub

    Private Sub txtSearchBilling_Enter(sender As Object, e As EventArgs) Handles txtSearchBilling.Enter
        If txtSearchBilling.Text = "Search Client..." Then
            txtSearchBilling.Text = ""
            txtSearchBilling.ForeColor = Color.Black
        End If
    End Sub

    Private Sub txtSearchBilling_Leave(sender As Object, e As EventArgs) Handles txtSearchBilling.Leave
        If String.IsNullOrWhiteSpace(txtSearchBilling.Text) Then
            txtSearchBilling.Text = "Search Client..."
            txtSearchBilling.ForeColor = Color.Gray
        End If
    End Sub

End Class