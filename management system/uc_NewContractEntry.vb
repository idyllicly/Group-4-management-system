Imports MySql.Data.MySqlClient

Public Class uc_NewContractEntry

    ' Connection String
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    ' Variables to hold data passed from the Quotation Manager
    Private _pfClientID As Integer = 0
    Private _pfServiceID As Integer = 0
    Private _pfAmount As Decimal = 0
    Private _pfQuoteID As Integer = 0 ' To track which quote generated this contract

    Private Sub uc_NewContractEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDropdowns()

        ' === APPLY PRE-FILLED DATA (If coming from a Quote) ===
        If _pfClientID > 0 Then cmbClient.SelectedValue = _pfClientID
        If _pfServiceID > 0 Then cmbService.SelectedValue = _pfServiceID
        If _pfAmount > 0 Then txtAmount.Text = _pfAmount.ToString("0.00")

        CalculateEndDate()
    End Sub

    ' ==========================================
    ' 0. PRE-FILL METHOD (Called by Quote Manager)
    ' ==========================================
    Public Sub PreFillData(clientID As Integer, serviceID As Integer, amount As Decimal, quoteID As Integer)
        _pfClientID = clientID
        _pfServiceID = serviceID
        _pfAmount = amount
        _pfQuoteID = quoteID
    End Sub

    ' === 1. LOAD DATA ===
    Private Sub LoadDropdowns()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' --- CLIENTS ---
                Dim sqlClients As String = "SELECT ClientID, CONCAT(ClientFirstName, ' ', ClientLastName) AS ClientName FROM tbl_clients"
                Dim daC As New MySqlDataAdapter(sqlClients, conn)
                Dim dtC As New DataTable()
                daC.Fill(dtC)
                cmbClient.DataSource = dtC
                cmbClient.DisplayMember = "ClientName"
                cmbClient.ValueMember = "ClientID"

                ' --- SERVICES ---
                Dim daS As New MySqlDataAdapter("SELECT ServiceID, ServiceName FROM tbl_services", conn)
                Dim dtS As New DataTable()
                daS.Fill(dtS)
                cmbService.DataSource = dtS
                cmbService.DisplayMember = "ServiceName"
                cmbService.ValueMember = "ServiceID"

                ' --- PAYMENT TERMS ---
                cmbPaymentTerms.Items.Clear()
                cmbPaymentTerms.Items.Add("Full Payment")
                cmbPaymentTerms.Items.Add("Installment")
                cmbPaymentTerms.SelectedIndex = 0

                ' --- INTERVALS ---
                cmbPayInterval.Items.Clear()
                cmbPayInterval.Items.AddRange(New Object() {"Weekly", "Bi-Weekly", "Monthly", "Quarterly"})
                cmbPayInterval.SelectedIndex = 2

                cmbFrequency.SelectedIndex = 0

            Catch ex As Exception
                MessageBox.Show("Error loading data: " & ex.Message)
            End Try
        End Using
    End Sub

    ' === 2. AUTO-CALCULATE END DATE ===
    Private Sub dtpStart_ValueChanged(sender As Object, e As EventArgs) Handles dtpStart.ValueChanged, numDuration.ValueChanged
        CalculateEndDate()
    End Sub

    Private Sub CalculateEndDate()
        Dim months As Integer = Convert.ToInt32(numDuration.Value)
        Dim endDate As Date = dtpStart.Value.AddMonths(months)
        lblEndDate.Text = endDate.ToString("MMMM dd, yyyy")
    End Sub

    ' === 3. SAVE & GENERATE ===
    Private Sub btnSaveContract_Click(sender As Object, e As EventArgs) Handles btnSaveContract.Click

        ' Validation
        If cmbClient.SelectedIndex = -1 Or cmbService.SelectedIndex = -1 Then
            MessageBox.Show("Please select Client and Service.")
            Exit Sub
        End If
        If txtAmount.Text = "" Then
            MessageBox.Show("Please enter the Total Price.")
            Exit Sub
        End If

        ' Gather Data
        Dim clientID As Integer = Convert.ToInt32(cmbClient.SelectedValue)
        Dim serviceID As Integer = Convert.ToInt32(cmbService.SelectedValue)
        Dim startDate As Date = dtpStart.Value
        Dim duration As Integer = Convert.ToInt32(numDuration.Value)

        Dim totalAmt As Decimal = Convert.ToDecimal(txtAmount.Text)
        Dim freq As String = cmbFrequency.Text
        Dim terms As String = cmbPaymentTerms.Text

        ' Determine Days Interval for Visits
        Dim dayInterval As Integer = 30
        If freq = "Monthly" Then dayInterval = 30
        If freq = "Quarterly" Then dayInterval = 90
        If freq = "Bi-Monthly" Then dayInterval = 60
        If freq = "One-Time" Then dayInterval = 0

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim trans As MySqlTransaction = conn.BeginTransaction()

            Try
                ' A. INSERT CONTRACT (UPDATED: Added QuoteID)
                ' We must include QuoteID in the INSERT because your DB requires it.
                Dim sqlCon As String = "INSERT INTO tbl_contracts " &
                                       "(ClientID, ServiceID, QuoteID, StartDate, DurationMonths, TotalAmount, ServiceFrequency, PaymentTerms, ContractStatus) " &
                                       "VALUES (@cid, @sid, @qid, @start, @dur, @amt, @freq, @term, 'Active');" &
                                       "SELECT LAST_INSERT_ID();"

                Dim cmdCon As New MySqlCommand(sqlCon, conn, trans)
                cmdCon.Parameters.AddWithValue("@cid", clientID)
                cmdCon.Parameters.AddWithValue("@sid", serviceID)

                ' LOGIC: If _pfQuoteID > 0 (came from quote), save it. Else, save NULL.
                If _pfQuoteID > 0 Then
                    cmdCon.Parameters.AddWithValue("@qid", _pfQuoteID)
                Else
                    cmdCon.Parameters.AddWithValue("@qid", DBNull.Value)
                End If

                cmdCon.Parameters.AddWithValue("@start", startDate)
                cmdCon.Parameters.AddWithValue("@dur", duration)
                cmdCon.Parameters.AddWithValue("@amt", totalAmt)
                cmdCon.Parameters.AddWithValue("@freq", freq)
                cmdCon.Parameters.AddWithValue("@term", terms)

                Dim newContractID As Integer = Convert.ToInt32(cmdCon.ExecuteScalar())

                ' B. GENERATE JOB ORDERS (Visits)
                Dim nextDate As Date = startDate
                Dim visitCount As Integer = 0

                If freq = "One-Time" Then
                    visitCount = 1
                Else
                    If dayInterval > 0 Then
                        visitCount = Math.Ceiling((duration * 30) / dayInterval)
                    Else
                        visitCount = 1
                    End If
                End If

                For i As Integer = 1 To visitCount
                    Dim sqlJob As String = "INSERT INTO tbl_joborders (ClientID, ContractID, VisitNumber, ScheduledDate, Status, JobType) " &
                                           "VALUES (@cid, @conID, @visNum, @sDate, 'Pending', 'Service')"

                    Dim cmdJob As New MySqlCommand(sqlJob, conn, trans)
                    cmdJob.Parameters.AddWithValue("@cid", clientID)
                    cmdJob.Parameters.AddWithValue("@conID", newContractID)
                    cmdJob.Parameters.AddWithValue("@visNum", i)
                    cmdJob.Parameters.AddWithValue("@sDate", nextDate)
                    cmdJob.ExecuteNonQuery()

                    If dayInterval > 0 Then nextDate = nextDate.AddDays(dayInterval)
                Next

                ' C. GENERATE PAYMENT SCHEDULE
                Dim payIntervalDays As Integer = 0
                Dim isInstallment As Boolean = (cmbPaymentTerms.Text = "Installment")

                If isInstallment Then
                    If cmbPayInterval.Text = "Weekly" Then payIntervalDays = 7
                    If cmbPayInterval.Text = "Bi-Weekly" Then payIntervalDays = 14
                    If cmbPayInterval.Text = "Monthly" Then payIntervalDays = 30
                    If cmbPayInterval.Text = "Quarterly" Then payIntervalDays = 90
                End If

                Dim payCount As Integer = 1
                Dim payAmount As Decimal = totalAmt

                If isInstallment Then
                    payCount = Convert.ToInt32(numInstallments.Value)
                    payAmount = totalAmt / payCount
                End If

                Dim nextPayDate As Date = dtpFirstPayment.Value

                For p As Integer = 1 To payCount
                    Dim sqlPaySched As String = "INSERT INTO tbl_paymentschedule (ContractID, DueDate, AmountDue, InstallmentNumber) " &
                                                "VALUES (@cid, @due, @amt, @num)"

                    Dim cmdPay As New MySqlCommand(sqlPaySched, conn, trans)
                    cmdPay.Parameters.AddWithValue("@cid", newContractID)
                    cmdPay.Parameters.AddWithValue("@due", nextPayDate)
                    cmdPay.Parameters.AddWithValue("@amt", payAmount)
                    cmdPay.Parameters.AddWithValue("@num", p)

                    cmdPay.ExecuteNonQuery()

                    If payIntervalDays > 0 Then nextPayDate = nextPayDate.AddDays(payIntervalDays)
                Next

                ' === D. UPDATE QUOTE STATUS (If this came from a Quote) ===
                If _pfQuoteID > 0 Then
                    Dim sqlUpdateQuote As String = "UPDATE tbl_quotations SET Status = 'Approved' WHERE QuoteID = @qid"
                    Dim cmdUpdate As New MySqlCommand(sqlUpdateQuote, conn, trans)
                    cmdUpdate.Parameters.AddWithValue("@qid", _pfQuoteID)
                    cmdUpdate.ExecuteNonQuery()
                End If

                trans.Commit()
                MessageBox.Show("Contract Created Successfully!" & vbCrLf & "Generated " & visitCount & " visits and " & payCount & " payment schedules.")

                ' Optional: Close after save
                ' Me.ParentForm.Close() 

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error saving: " & ex.Message)
            End Try
        End Using
    End Sub

    ' === VISUAL: Show/Hide Installment Options ===
    Private Sub cmbPaymentTerms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPaymentTerms.SelectedIndexChanged
        Dim isInstallment As Boolean = (cmbPaymentTerms.Text = "Installment")

        lblTerms.Visible = isInstallment
        numInstallments.Visible = isInstallment
        lblInterval.Visible = isInstallment
        cmbPayInterval.Visible = isInstallment
        lblInstallmentAmt.Visible = isInstallment
        lblFirstDue.Visible = True
        dtpFirstPayment.Visible = True

        CalculateInstallmentAmount()
    End Sub

    Private Sub numInstallments_ValueChanged(sender As Object, e As EventArgs) Handles numInstallments.ValueChanged, txtAmount.TextChanged
        CalculateInstallmentAmount()
    End Sub

    Private Sub CalculateInstallmentAmount()
        Dim total As Decimal = 0
        If Not Decimal.TryParse(txtAmount.Text, total) Then Exit Sub

        Dim terms As Integer = 1
        If cmbPaymentTerms.Text = "Installment" Then
            terms = Convert.ToInt32(numInstallments.Value)
        End If

        If terms < 1 Then terms = 1

        Dim perGive As Decimal = total / terms
        lblInstallmentAmt.Text = "Amount Per Give: " & perGive.ToString("N2")
    End Sub

End Class