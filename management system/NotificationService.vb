Imports MySql.Data.MySqlClient

Public Class NotificationService
    Private Shared connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' 1. ADD NOTIFICATION 
    ' FIXED: Changed 'relatedID' to specific optional IDs to match database columns [cite: 41, 42]
    Public Shared Sub AddNotification(title As String, message As String, category As String, Optional jobID As Integer = 0, Optional contractID As Integer = 0, Optional paymentID As Integer = 0)
        Using conn As New MySqlConnection(connString)
            conn.Open()

            ' A. PREVENT DUPLICATES (Don't alert twice in one day for the same item)
            ' We build the query dynamically based on which ID is provided
            Dim checkSql As String = "SELECT COUNT(*) FROM tbl_notifications WHERE Category=@cat AND DATE(DateCreated) = CURDATE() "

            If jobID > 0 Then
                checkSql &= " AND JobID = @id"
            ElseIf contractID > 0 Then
                checkSql &= " AND ContractID = @id"
            ElseIf paymentID > 0 Then
                checkSql &= " AND PaymentID = @id"
            Else
                ' If no ID is provided, just check title/message to prevent spam
                checkSql &= " AND Title = @title"
            End If

            Using cmdCheck As New MySqlCommand(checkSql, conn)
                cmdCheck.Parameters.AddWithValue("@cat", category)
                ' Use the relevant ID for the check
                Dim checkID As Integer = 0
                If jobID > 0 Then checkID = jobID
                If contractID > 0 Then checkID = contractID
                If paymentID > 0 Then checkID = paymentID

                If checkID > 0 Then
                    cmdCheck.Parameters.AddWithValue("@id", checkID)
                Else
                    cmdCheck.Parameters.AddWithValue("@title", title)
                End If

                Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                If count > 0 Then Exit Sub ' Already notified today
            End Using

            ' B. INSERT NEW NOTIFICATION
            ' FIXED: Insert into specific columns (JobID, ContractID, PaymentID) instead of RelatedID [cite: 44]
            Dim sql As String = "INSERT INTO tbl_notifications (Title, Message, Category, JobID, ContractID, PaymentID, IsRead, DateCreated) " &
                                "VALUES (@t, @m, @c, @jid, @cid, @pid, 0, NOW())"

            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@t", title)
                cmd.Parameters.AddWithValue("@m", message)
                cmd.Parameters.AddWithValue("@c", category)

                ' Handle Nullables for Database
                cmd.Parameters.AddWithValue("@jid", If(jobID > 0, jobID, DBNull.Value))
                cmd.Parameters.AddWithValue("@cid", If(contractID > 0, contractID, DBNull.Value))
                cmd.Parameters.AddWithValue("@pid", If(paymentID > 0, paymentID, DBNull.Value))

                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' 2. RUN SYSTEM CHECKS (Contracts & Billing)
    Public Shared Sub RunSystemChecks()
        Using conn As New MySqlConnection(connString)
            conn.Open()

            ' ---------------------------------------------------------
            ' A. CHECK EXPIRING CONTRACTS (7 Days warning)
            ' ---------------------------------------------------------
            ' FIXED: Uses ClientFirstName/LastName and DurationMonths calculation [cite: 46, 47]
            Dim sqlContract As String = "SELECT c.ContractID, CONCAT(cl.ClientFirstName, ' ', cl.ClientLastName) as ClientName, " &
                                        "DATE_ADD(c.StartDate, INTERVAL c.DurationMonths MONTH) as EndDate " &
                                        "FROM tbl_contracts c " &
                                        "JOIN tbl_clients cl ON c.ClientID = cl.ClientID " &
                                        "WHERE DATE_ADD(c.StartDate, INTERVAL c.DurationMonths MONTH) BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY)"

            Using cmd As New MySqlCommand(sqlContract, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim endDate As DateTime = Convert.ToDateTime(reader("EndDate"))
                        Dim msg As String = $"Contract for {reader("ClientName")} expires on {endDate:MMM dd}."

                        ' FIXED: Pass to contractID parameter
                        AddNotification("Contract Expiring", msg, "Contract", contractID:=Convert.ToInt32(reader("ContractID")))
                    End While
                End Using
            End Using

            ' ---------------------------------------------------------
            ' B. CHECK PAYMENTS DUE (3 Days warning)
            ' ---------------------------------------------------------
            ' FIXED: Links to tbl_paymentschedule and notifies based on ContractID [cite: 51, 52]
            Dim sqlPayment As String = "SELECT S.ScheduleID, S.ContractID, CONCAT(CL.ClientFirstName, ' ', CL.ClientLastName) AS ClientName, S.DueDate, S.AmountDue " &
                                       "FROM tbl_paymentschedule S " &
                                       "JOIN tbl_contracts C ON S.ContractID = C.ContractID " &
                                       "JOIN tbl_clients CL ON C.ClientID = CL.ClientID " &
                                       "WHERE S.DueDate BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 3 DAY)"

            Using cmdP As New MySqlCommand(sqlPayment, conn)
                Using reader As MySqlDataReader = cmdP.ExecuteReader()
                    While reader.Read()
                        Dim dDate As DateTime = Convert.ToDateTime(reader("DueDate"))
                        Dim amt As Decimal = Convert.ToDecimal(reader("AmountDue"))
                        Dim msg As String = $"Payment of {amt:N2} for {reader("ClientName")} is due on {dDate:MMM dd}."

                        ' FIXED: Link to ContractID so the user can view the contract details
                        AddNotification("Payment Due", msg, "Billing", contractID:=Convert.ToInt32(reader("ContractID")))
                    End While
                End Using
            End Using

            ' ---------------------------------------------------------
            ' C. CHECK UPCOMING JOBS (Next 7 Days)
            ' ---------------------------------------------------------
            ' FIXED: Joins tbl_joborders to tbl_clients [cite: 55, 56]
            Dim sqlJobs As String = "SELECT J.JobID, J.ScheduledDate, J.JobType, " &
                                    "CONCAT(Cl.ClientFirstName, ' ', Cl.ClientLastName) AS ClientName " &
                                    "FROM tbl_joborders J " &
                                    "INNER JOIN tbl_clients Cl ON J.ClientID = Cl.ClientID " &
                                    "WHERE J.ScheduledDate BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY) " &
                                    "AND J.Status IN ('Pending', 'Assigned')"

            Using cmdJ As New MySqlCommand(sqlJobs, conn)
                Using reader As MySqlDataReader = cmdJ.ExecuteReader()
                    While reader.Read()
                        Dim jobDate As Date = Convert.ToDateTime(reader("ScheduledDate"))
                        Dim client As String = reader("ClientName").ToString()
                        Dim type As String = reader("JobType").ToString()
                        Dim jid As Integer = Convert.ToInt32(reader("JobID"))

                        ' Calculate how many days away it is
                        Dim daysAway As Integer = (jobDate.Date - DateTime.Now.Date).Days

                        Dim timeMsg As String = ""
                        If daysAway = 0 Then
                            timeMsg = "is scheduled for TODAY."
                        ElseIf daysAway = 1 Then
                            timeMsg = "is scheduled for TOMORROW."
                        Else
                            timeMsg = $"is coming up on {jobDate:MMM dd}."
                        End If

                        Dim msg As String = $"Upcoming {type}: {client} {timeMsg}"

                        ' FIXED: Pass to jobID parameter
                        AddNotification("Upcoming Service", msg, "Job Update", jobID:=jid)
                    End While
                End Using
            End Using

        End Using
    End Sub

    ' 3. MARK SINGLE READ
    Public Shared Sub MarkAsRead(notifID As Integer)
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim cmd As New MySqlCommand("UPDATE tbl_notifications SET IsRead = 1 WHERE NotifID = @id", conn)
            cmd.Parameters.AddWithValue("@id", notifID)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' 4. GET UNREAD COUNT (For Red Badge)
    Public Shared Function GetUnreadCount() As Integer
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim cmd As New MySqlCommand("SELECT COUNT(*) FROM tbl_notifications WHERE IsRead = 0", conn)
            Dim result = cmd.ExecuteScalar()
            If result IsNot Nothing AndAlso IsNumeric(result) Then
                Return Convert.ToInt32(result)
            Else
                Return 0
            End If
        End Using
    End Function

End Class