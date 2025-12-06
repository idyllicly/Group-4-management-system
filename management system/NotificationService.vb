Imports MySql.Data.MySqlClient

Public Class NotificationService
    Private Shared connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' 1. ADD NOTIFICATION (Used by Firebase and System Checks)
    Public Shared Sub AddNotification(title As String, message As String, category As String, relatedID As Integer)
        Using conn As New MySqlConnection(connString)
            conn.Open()
            ' Prevent duplicates for system alerts (don't alert twice today for the same thing)
            Dim checkSql As String = "SELECT COUNT(*) FROM tbl_notifications WHERE RelatedID=@rid AND Category=@cat AND DATE(DateCreated) = CURDATE()"
            Using cmdCheck As New MySqlCommand(checkSql, conn)
                cmdCheck.Parameters.AddWithValue("@rid", relatedID)
                cmdCheck.Parameters.AddWithValue("@cat", category)
                Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
                If count > 0 Then Exit Sub ' Already notified today
            End Using

            ' Insert New Notification
            Dim sql As String = "INSERT INTO tbl_notifications (Title, Message, Category, RelatedID, IsRead, DateCreated) VALUES (@t, @m, @c, @rid, 0, NOW())"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@t", title)
                cmd.Parameters.AddWithValue("@m", message)
                cmd.Parameters.AddWithValue("@c", category)
                cmd.Parameters.AddWithValue("@rid", relatedID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' 2. RUN SYSTEM CHECKS (Contracts & Billing)
    Public Shared Sub RunSystemChecks()
        Using conn As New MySqlConnection(connString)
            conn.Open()

            ' A. Check Expiring Contracts (7 Days warning)
            ' UPDATED: Concatenate Name
            Dim sqlContract As String = "SELECT ContractID, ClientName, EndDate " &
                                        "FROM (SELECT c.ContractID, CONCAT(cl.ClientFirstName, ' ', cl.ClientLastName) as ClientName, DATE_ADD(c.StartDate, INTERVAL c.DurationMonths MONTH) as EndDate " &
                                        "      FROM tbl_contracts c JOIN tbl_clients cl ON c.ClientID = cl.ClientID) as T " &
                                        "WHERE EndDate BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 7 DAY)"

            Using cmd As New MySqlCommand(sqlContract, conn)
                Using reader As MySqlDataReader = cmd.ExecuteReader()
                    While reader.Read()
                        Dim msg As String = $"Contract for {reader("ClientName")} expires on {Convert.ToDateTime(reader("EndDate")).ToString("MMM dd")}."
                        AddNotification("Contract Expiring", msg, "Contract", Convert.ToInt32(reader("ContractID")))
                    End While
                End Using
            End Using

            ' B. Check Payments Due (3 Days warning)
            ' UPDATED: Concatenate Name
            Dim sqlPayment As String = "SELECT S.ScheduleID, S.ContractID, CONCAT(CL.ClientFirstName, ' ', CL.ClientLastName) AS ClientName, S.DueDate, S.AmountDue " &
                                       "FROM tbl_paymentschedule S " &
                                       "JOIN tbl_contracts C ON S.ContractID = C.ContractID " &
                                       "JOIN tbl_clients CL ON C.ClientID = CL.ClientID " &
                                       "WHERE S.DueDate BETWEEN CURDATE() AND DATE_ADD(CURDATE(), INTERVAL 3 DAY)"

            Using cmdP As New MySqlCommand(sqlPayment, conn)
                Using reader As MySqlDataReader = cmdP.ExecuteReader()
                    While reader.Read()
                        Dim msg As String = $"Payment of {reader("AmountDue")} for {reader("ClientName")} is due on {Convert.ToDateTime(reader("DueDate")).ToString("MMM dd")}."
                        ' We link to ContractID so clicking takes you to the contract
                        AddNotification("Payment Due", msg, "Billing", Convert.ToInt32(reader("ContractID")))
                    End While
                End Using
            End Using

            ' C. CHECK UPCOMING JOBS (Next 7 Days)
            ' UPDATED: Removed ClientID_TempLink, used direct join to tbl_clients via J.ClientID
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

                        ' We use 'Job Update' category so clicking it takes you to the Calendar
                        AddNotification("Upcoming Service", msg, "Job Update", jid)
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
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function
End Class