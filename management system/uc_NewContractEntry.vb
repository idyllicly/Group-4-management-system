Imports MySql.Data.MySqlClient

Public Class uc_NewContractEntry

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    Private Sub uc_NewContractEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LoadDropdowns()
        CalculateEndDate() ' Init label
    End Sub

    ' === 1. LOAD DATA ===
    Private Sub LoadDropdowns()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' Clients
                Dim daC As New MySqlDataAdapter("SELECT ClientID, ClientName FROM tbl_Clients", conn)
                Dim dtC As New DataTable()
                daC.Fill(dtC)
                cmbClient.DataSource = dtC
                cmbClient.DisplayMember = "ClientName"
                cmbClient.ValueMember = "ClientID"

                ' Services
                Dim daS As New MySqlDataAdapter("SELECT ServiceID, ServiceName FROM tbl_Services", conn)
                Dim dtS As New DataTable()
                daS.Fill(dtS)
                cmbService.DataSource = dtS
                cmbService.DisplayMember = "ServiceName"
                cmbService.ValueMember = "ServiceID"

                ' [FIX 2] Simplify Payment Terms Options
                ' Instead of confusing text, we limit this to the Logic Drivers
                cmbPaymentTerms.Items.Clear()
                cmbPaymentTerms.Items.Add("Full Payment")
                cmbPaymentTerms.Items.Add("Installment")
                cmbPaymentTerms.SelectedIndex = 0 ' Default to Full Payment

                ' Update Pay Intervals (Added Quarterly)
                cmbPayInterval.Items.Clear()
                cmbPayInterval.Items.AddRange(New Object() {"Weekly", "Bi-Weekly", "Monthly", "Quarterly"})
                cmbPayInterval.SelectedIndex = 2 ' Default Monthly

                cmbFrequency.SelectedIndex = 0 ' Default Monthly

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
        Dim endDate As Date = startDate.AddMonths(duration)

        Dim totalAmt As Decimal = Convert.ToDecimal(txtAmount.Text)
        Dim freq As String = cmbFrequency.Text ' e.g., "Monthly"

        ' [FIX 3] Use Payment Terms as the source of truth
        Dim terms As String = cmbPaymentTerms.Text

        ' Determine Days Interval based on Dropdown
        Dim dayInterval As Integer = 30 ' Default
        If freq = "Monthly" Then dayInterval = 30
        If freq = "Quarterly" Then dayInterval = 90
        If freq = "Bi-Monthly" Then dayInterval = 60
        If freq = "One-Time" Then dayInterval = 0

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim trans As MySqlTransaction = conn.BeginTransaction()

            Try
                ' A. INSERT CONTRACT
                Dim sqlCon As String = "INSERT INTO tbl_Contracts " &
                                       "(ClientID, ServiceID, StartDate, DurationMonths, TotalAmount, " &
                                       "ServiceFrequency, PaymentStatus, PaymentTerms, BalanceRemaining, VisitsCompleted) " &
                                       "VALUES (@cid, @sid, @start, @dur, @amt, @freq, 'With Balance', @term, @amt, 0); " &
                                       "SELECT LAST_INSERT_ID();"

                Dim cmdCon As New MySqlCommand(sqlCon, conn, trans)
                cmdCon.Parameters.AddWithValue("@cid", clientID)
                cmdCon.Parameters.AddWithValue("@sid", serviceID)
                cmdCon.Parameters.AddWithValue("@start", startDate)
                cmdCon.Parameters.AddWithValue("@dur", duration)
                cmdCon.Parameters.AddWithValue("@amt", totalAmt)
                cmdCon.Parameters.AddWithValue("@freq", freq)
                cmdCon.Parameters.AddWithValue("@term", terms)

                Dim newContractID As Integer = Convert.ToInt32(cmdCon.ExecuteScalar())

                ' B. GENERATE SCHEDULE LOOP (Service Visits)
                Dim nextDate As Date = startDate
                Dim visitCount As Integer = 0

                If freq = "One-Time" Then
                    visitCount = 1
                Else
                    If dayInterval > 0 Then
                        visitCount = Math.Ceiling((duration * 30) / dayInterval)
                    Else
                        visitCount = 1 ' Fallback
                    End If
                End If

                For i As Integer = 1 To visitCount
                    Dim sqlJob As String = "INSERT INTO tbl_JobOrders (ContractID, VisitNumber, ScheduledDate, Status, JobType) " &
                                           "VALUES (@conID, @visNum, @sDate, 'Pending', 'Service')"

                    Dim cmdJob As New MySqlCommand(sqlJob, conn, trans)
                    cmdJob.Parameters.AddWithValue("@conID", newContractID)
                    cmdJob.Parameters.AddWithValue("@visNum", i)
                    cmdJob.Parameters.AddWithValue("@sDate", nextDate)
                    cmdJob.ExecuteNonQuery()

                    If dayInterval > 0 Then nextDate = nextDate.AddDays(dayInterval)
                Next

                ' === C. GENERATE PAYMENT SCHEDULE ===
                ' [FIX 4] Logic now depends on cmbPaymentTerms
                Dim payIntervalDays As Integer = 0
                Dim isInstallment As Boolean = (cmbPaymentTerms.Text = "Installment")

                If isInstallment Then
                    If cmbPayInterval.Text = "Weekly" Then payIntervalDays = 7
                    If cmbPayInterval.Text = "Bi-Weekly" Then payIntervalDays = 14
                    If cmbPayInterval.Text = "Monthly" Then payIntervalDays = 30
                    If cmbPayInterval.Text = "Quarterly" Then payIntervalDays = 90 ' Added Quarterly
                End If

                ' Determine Count and Amount
                Dim payCount As Integer = 1
                Dim payAmount As Decimal = totalAmt

                If isInstallment Then
                    payCount = Convert.ToInt32(numInstallments.Value)
                    payAmount = totalAmt / payCount
                End If

                Dim nextPayDate As Date = dtpFirstPayment.Value

                For p As Integer = 1 To payCount
                    Dim sqlPaySched As String = "INSERT INTO tbl_PaymentSchedule (ContractID, DueDate, AmountDue, Status, InstallmentNumber) " &
                                                "VALUES (@cid, @due, @amt, 'Pending', @num)"

                    Dim cmdPay As New MySqlCommand(sqlPaySched, conn, trans)
                    cmdPay.Parameters.AddWithValue("@cid", newContractID)
                    cmdPay.Parameters.AddWithValue("@due", nextPayDate)
                    cmdPay.Parameters.AddWithValue("@amt", payAmount)
                    cmdPay.Parameters.AddWithValue("@num", p)

                    cmdPay.ExecuteNonQuery()

                    If payIntervalDays > 0 Then nextPayDate = nextPayDate.AddDays(payIntervalDays)
                Next

                trans.Commit()
                MessageBox.Show("Contract Saved! " & visitCount & " visits and " & payCount & " payments generated.")

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error saving: " & ex.Message)
            End Try
        End Using
    End Sub

    ' === VISUAL: Show/Hide Installment Options ===
    ' [FIX 5] This event now handles cmbPaymentTerms instead of the deleted Mode combo
    Private Sub cmbPaymentTerms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPaymentTerms.SelectedIndexChanged
        Dim isInstallment As Boolean = (cmbPaymentTerms.Text = "Installment")

        ' Show controls only if Installment is selected
        lblTerms.Visible = isInstallment
        numInstallments.Visible = isInstallment
        lblInterval.Visible = isInstallment
        cmbPayInterval.Visible = isInstallment
        lblInstallmentAmt.Visible = isInstallment
        lblFirstDue.Visible = True ' Always show first due date
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
        ' Only use the number box if we are in Installment mode
        If cmbPaymentTerms.Text = "Installment" Then
            terms = Convert.ToInt32(numInstallments.Value)
        End If

        If terms < 1 Then terms = 1

        Dim perGive As Decimal = total / terms
        lblInstallmentAmt.Text = "Amount Per Give: " & perGive.ToString("N2")
    End Sub

End Class