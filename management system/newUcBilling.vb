Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class newUcBilling
    ' ==========================================
    ' CONFIGURATION & VARIABLES
    ' ==========================================
    Public Property PresetContractID As Integer = 0
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' State Tracking
    Private _selectedContractID As Integer = 0
    Private _selectedScheduleID As Integer = 0
    Private _currentContractBalance As Decimal = 0

    ' ==========================================
    ' 1. FORM LOAD
    ' ==========================================
    Private Sub newUcBilling_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize Search Box Style
        txtSearchBilling.Text = "Search Client..."
        txtSearchBilling.ForeColor = Color.Gray

        LoadBillingData("")

        ' Handle Deep Linking (if opened from another control)
        If PresetContractID > 0 Then
            For Each row As DataGridViewRow In dgvBilling.Rows
                If row.Cells("ContractID").Value IsNot Nothing AndAlso Convert.ToInt32(row.Cells("ContractID").Value) = PresetContractID Then
                    row.Selected = True
                    SelectContract(row)
                    dgvBilling.FirstDisplayedScrollingRowIndex = row.Index
                    Exit For
                End If
            Next
        End If
    End Sub

    ' ==========================================
    ' 2. MASTER LIST (LEFT PANEL)
    ' ==========================================
    Private Sub LoadBillingData(search As String)
        Dim previouslySelectedID As Integer = _selectedContractID

        ' Handle Placeholder Text
        If search.Trim() = "Search Client..." Then search = ""

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' SQL: Calculates Paid, Balance, and determines Status (Paid/Partial/Pending)
                Dim sql As String = "SELECT " &
                                    "   Con.ContractID, " &
                                    "   CONCAT(Cli.ClientFirstName, ' ', Cli.ClientLastName) AS ClientName, " &
                                    "   Ser.ServiceName, " &
                                    "   Con.TotalAmount, " &
                                    "   (Con.TotalAmount - COALESCE((SELECT SUM(AmountPaid) FROM tbl_payments WHERE ContractID = Con.ContractID), 0)) AS BalanceRemaining, " &
                                    "   CASE " &
                                    "       WHEN (Con.TotalAmount - COALESCE((SELECT SUM(AmountPaid) FROM tbl_payments WHERE ContractID = Con.ContractID), 0)) <= 0 THEN 'Paid' " &
                                    "       WHEN (Con.TotalAmount - COALESCE((SELECT SUM(AmountPaid) FROM tbl_payments WHERE ContractID = Con.ContractID), 0)) < Con.TotalAmount THEN 'Partial' " &
                                    "       ELSE 'Pending' " &
                                    "   END AS PaymentStatus " &
                                    "FROM tbl_contracts Con " &
                                    "LEFT JOIN tbl_clients Cli ON Con.ClientID = Cli.ClientID " &
                                    "LEFT JOIN tbl_services Ser ON Con.ServiceID = Ser.ServiceID " &
                                    "WHERE (Cli.ClientFirstName LIKE @s OR Cli.ClientLastName LIKE @s) " &
                                    "ORDER BY Con.ContractID DESC"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@s", "%" & search & "%")
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvBilling.DataSource = dt

                ' --- UI CLEANUP ---
                If dgvBilling.Columns("ContractID") IsNot Nothing Then dgvBilling.Columns("ContractID").Visible = False

                If dgvBilling.Columns("BalanceRemaining") IsNot Nothing Then
                    dgvBilling.Columns("BalanceRemaining").DefaultCellStyle.Format = "N2"
                    dgvBilling.Columns("BalanceRemaining").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
                If dgvBilling.Columns("TotalAmount") IsNot Nothing Then
                    dgvBilling.Columns("TotalAmount").DefaultCellStyle.Format = "N2"
                    dgvBilling.Columns("TotalAmount").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If



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

    Private Sub ColorMasterList()
        For Each row As DataGridViewRow In dgvBilling.Rows
            If row.Cells("PaymentStatus").Value IsNot Nothing Then
                Dim status As String = row.Cells("PaymentStatus").Value.ToString()
                If status = "Paid" Then
                    row.DefaultCellStyle.BackColor = Color.LightGreen
                    row.DefaultCellStyle.SelectionBackColor = Color.SeaGreen
                ElseIf status = "Partial" Then
                    row.DefaultCellStyle.BackColor = Color.LightYellow
                    row.DefaultCellStyle.SelectionBackColor = Color.Gold
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            End If
        Next
    End Sub

    ' ==========================================
    ' 3. CONTRACT SELECTION LOGIC
    ' ==========================================
    Private Sub dgvBilling_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvBilling.CellClick
        If e.RowIndex >= 0 Then
            SelectContract(dgvBilling.Rows(e.RowIndex))
        End If
    End Sub

    Private Sub SelectContract(row As DataGridViewRow)
        If row.Cells("ContractID").Value IsNot Nothing Then
            _selectedContractID = Convert.ToInt32(row.Cells("ContractID").Value)
        End If

        ' Get Balance from the calculated column
        If row.Cells("BalanceRemaining").Value IsNot Nothing Then
            _currentContractBalance = Convert.ToDecimal(row.Cells("BalanceRemaining").Value)
        End If

        lblContractInfo.Text = row.Cells("ClientName").Value.ToString()
        lblBalance.Text = _currentContractBalance.ToString("N2")

        txtPayAmount.Clear()
        _selectedScheduleID = 0

        LoadPaymentSchedule(_selectedContractID)
    End Sub

    ' ==========================================
    ' 4. WATERFALL SCHEDULE LOGIC (RIGHT PANEL)
    ' ==========================================
    Private Sub LoadPaymentSchedule(contractID As Integer)
        ' A. Get Total Paid for this Contract from DB
        Dim totalPaidSoFar As Decimal = GetTotalPaidForContract(contractID)

        ' B. Get Raw Schedule List (Ordered by Date)
        Dim dtRaw As DataTable = GetRawScheduleList(contractID)

        ' C. Create Display Table
        Dim dtDisplay As New DataTable()
        dtDisplay.Columns.Add("ScheduleID", GetType(Integer))
        dtDisplay.Columns.Add("Installment", GetType(Integer))
        dtDisplay.Columns.Add("DueDate", GetType(Date))
        dtDisplay.Columns.Add("AmountDue", GetType(Decimal))
        dtDisplay.Columns.Add("PaidAmount", GetType(Decimal)) ' Calculated
        dtDisplay.Columns.Add("Balance", GetType(Decimal))    ' Calculated
        dtDisplay.Columns.Add("Status", GetType(String))

        ' D. WATERFALL CALCULATION
        Dim remainingMoneyToDistribute As Decimal = totalPaidSoFar

        For Each row As DataRow In dtRaw.Rows
            Dim schedID As Integer = Convert.ToInt32(row("ScheduleID"))
            Dim instNum As Integer = Convert.ToInt32(row("InstallmentNumber"))
            Dim dueDate As Date = Convert.ToDateTime(row("DueDate"))
            Dim amountDue As Decimal = Convert.ToDecimal(row("AmountDue"))

            Dim amountPaidForThisRow As Decimal = 0
            Dim rowStatus As String = "Pending"

            If remainingMoneyToDistribute >= amountDue Then
                ' Fully Paid
                amountPaidForThisRow = amountDue
                remainingMoneyToDistribute -= amountDue
                rowStatus = "Paid"
            ElseIf remainingMoneyToDistribute > 0 Then
                ' Partially Paid
                amountPaidForThisRow = remainingMoneyToDistribute
                remainingMoneyToDistribute = 0
                rowStatus = "Partial"
            Else
                ' Not Paid at all
                amountPaidForThisRow = 0
                rowStatus = "Pending"
            End If

            Dim rowBalance As Decimal = amountDue - amountPaidForThisRow
            dtDisplay.Rows.Add(schedID, instNum, dueDate, amountDue, amountPaidForThisRow, rowBalance, rowStatus)
        Next

        ' E. Bind & Format
        dgvSchedule.DataSource = dtDisplay
        FormatScheduleGrid()
        ColorScheduleRows()
    End Sub

    Private Function GetTotalPaidForContract(cid As Integer) As Decimal
        Dim total As Decimal = 0
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim sql As String = "SELECT COALESCE(SUM(AmountPaid), 0) FROM tbl_payments WHERE ContractID = @cid"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@cid", cid)
                total = Convert.ToDecimal(cmd.ExecuteScalar())
            End Using
        End Using
        Return total
    End Function

    Private Function GetRawScheduleList(cid As Integer) As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim sql As String = "SELECT * FROM tbl_paymentschedule WHERE ContractID = @cid ORDER BY DueDate ASC"
            Using da As New MySqlDataAdapter(sql, conn)
                da.SelectCommand.Parameters.AddWithValue("@cid", cid)
                da.Fill(dt)
            End Using
        End Using
        Return dt
    End Function

    Private Sub FormatScheduleGrid()
        If dgvSchedule.Columns("ScheduleID") IsNot Nothing Then dgvSchedule.Columns("ScheduleID").Visible = False
        If dgvSchedule.Columns("DueDate") IsNot Nothing Then dgvSchedule.Columns("DueDate").DefaultCellStyle.Format = "MMM dd, yyyy"

        Dim moneyCols As String() = {"AmountDue", "PaidAmount", "Balance"}
        For Each col As String In moneyCols
            If dgvSchedule.Columns(col) IsNot Nothing Then
                dgvSchedule.Columns(col).DefaultCellStyle.Format = "N2"
                dgvSchedule.Columns(col).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        Next
    End Sub

    Private Sub ColorScheduleRows()
        For Each row As DataGridViewRow In dgvSchedule.Rows
            Dim status As String = row.Cells("Status").Value.ToString()
            Dim dueDate As Date = Convert.ToDateTime(row.Cells("DueDate").Value)

            If status = "Paid" Then
                row.DefaultCellStyle.BackColor = Color.LightGreen
                row.DefaultCellStyle.SelectionBackColor = Color.SeaGreen
            ElseIf status = "Partial" Then
                row.DefaultCellStyle.BackColor = Color.LightYellow
                row.DefaultCellStyle.SelectionBackColor = Color.Gold
            ElseIf status = "Pending" AndAlso dueDate < DateTime.Now Then
                row.DefaultCellStyle.BackColor = Color.LightSalmon
                row.DefaultCellStyle.SelectionBackColor = Color.Red
            Else
                row.DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    ' ==========================================
    ' 5. SMART CLICK (Schedule Grid)
    ' ==========================================
    Private Sub dgvSchedule_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSchedule.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvSchedule.Rows(e.RowIndex)

            _selectedScheduleID = Convert.ToInt32(row.Cells("ScheduleID").Value)
            Dim rowBalance As Decimal = Convert.ToDecimal(row.Cells("Balance").Value)
            Dim status As String = row.Cells("Status").Value.ToString()

            If status = "Paid" Then
                MessageBox.Show("This installment is fully paid.")
                txtPayAmount.Clear()
            Else
                ' Auto-fill the exact remaining balance for this installment
                txtPayAmount.Text = rowBalance.ToString("0.00")
            End If
        End If
    End Sub

    ' ==========================================
    ' 6. ROBUST SAVE PAYMENT
    ' ==========================================
    Private Sub btnSavePayment_Click(sender As Object, e As EventArgs) Handles btnSavePayment.Click
        ' Validation: Contract Selected
        If _selectedContractID = 0 Then
            MessageBox.Show("Please select a contract first.")
            Exit Sub
        End If

        ' Validation: Valid Amount
        Dim amountToPay As Decimal = 0
        If Not Decimal.TryParse(txtPayAmount.Text, amountToPay) OrElse amountToPay <= 0 Then
            MessageBox.Show("Please enter a valid amount greater than 0.")
            Exit Sub
        End If

        ' Validation: Do not allow overpayment of the TOTAL Contract
        If amountToPay > _currentContractBalance Then
            MessageBox.Show($"You are attempting to pay {amountToPay:N2}, but the total remaining balance for this contract is only {_currentContractBalance:N2}." & vbCrLf &
                            "Please enter an amount equal to or less than the remaining balance.")
            Exit Sub
        End If

        Dim currentSearchTerm As String = txtSearchBilling.Text

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim trans As MySqlTransaction = conn.BeginTransaction()

            Try
                ' Insert Payment
                ' Note: We save ScheduleID if selected for reference, but calculations use the Waterfall method
                Dim sqlPay As String = "INSERT INTO tbl_payments (ContractID, ScheduleID, AmountPaid, PaymentDate) VALUES (@cid, @sid, @amt, @date)"
                Dim cmdPay As New MySqlCommand(sqlPay, conn, trans)

                cmdPay.Parameters.AddWithValue("@cid", _selectedContractID)

                If _selectedScheduleID > 0 Then
                    cmdPay.Parameters.AddWithValue("@sid", _selectedScheduleID)
                Else
                    cmdPay.Parameters.AddWithValue("@sid", DBNull.Value)
                End If

                cmdPay.Parameters.AddWithValue("@amt", amountToPay)
                cmdPay.Parameters.AddWithValue("@date", DateTime.Now)

                cmdPay.ExecuteNonQuery()
                trans.Commit()

                MessageBox.Show("Payment Saved Successfully!")

                ' Refresh Data
                LoadBillingData(currentSearchTerm)
                ' Refresh Schedule (Right Panel)
                LoadPaymentSchedule(_selectedContractID)

                ' Clear Input
                txtPayAmount.Clear()
                _selectedScheduleID = 0

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error processing payment: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 7. SEARCH BAR LOGIC
    ' ==========================================
    Private Sub txtSearchBilling_TextChanged(sender As Object, e As EventArgs) Handles txtSearchBilling.TextChanged
        ' Don't search if it's the placeholder text
        If txtSearchBilling.Text <> "Search Client..." Then
            LoadBillingData(txtSearchBilling.Text)
        End If
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
            LoadBillingData("") ' Reset list
        End If
    End Sub

    Private Sub dgvBilling_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvBilling.RowPrePaint
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvBilling.Rows(e.RowIndex)

            ' Safety check: ensure the column exists and value is not null
            If row.Cells("PaymentStatus").Value IsNot Nothing Then
                Dim status As String = row.Cells("PaymentStatus").Value.ToString()

                If status = "Paid" Then
                    row.DefaultCellStyle.BackColor = Color.LightGreen
                    row.DefaultCellStyle.SelectionBackColor = Color.SeaGreen

                ElseIf status = "Partial" Then
                    row.DefaultCellStyle.BackColor = Color.LightYellow
                    row.DefaultCellStyle.SelectionBackColor = Color.Gold

                Else
                    ' Default for Pending or others
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            End If
        End If
    End Sub

End Class