Imports MySql.Data.MySqlClient

Public Class JobRepository
    ' Central Connection String
    Private Const ConnString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' 1. GET CALENDAR COUNTS (Runs continuously for the calendar)
    Public Shared Function GetMonthlyJobCounts(m As Integer, y As Integer) As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(ConnString)
            conn.Open()
            ' [cite: 71-73] Optimized SQL for Calendar Counts
            Dim sql As String = "SELECT DAY(ScheduledDate) as DayNum, JobType, COUNT(*) as JobCount " &
                                "FROM tbl_joborders " &
                                "WHERE MONTH(ScheduledDate) = @m AND YEAR(ScheduledDate) = @y " &
                                "GROUP BY DAY(ScheduledDate), JobType"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@m", m)
                cmd.Parameters.AddWithValue("@y", y)
                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' 2. GET DAILY JOBS LIST (Runs when you click a day)
    Public Shared Function GetJobsByDate(viewDate As Date) As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(ConnString)
            conn.Open()
            ' [cite: 92-100] Full query for the DataGridView
            Dim sql As String = "SELECT " &
                                "   J.JobID, " &
                                "   CONCAT(C.ClientFirstName, ' ', C.ClientLastName) AS ClientName, " &
                                "   CONCAT_WS(', ', C.StreetAddress, C.Barangay, C.City) AS Address, " &
                                "   COALESCE(S.ServiceName, S_Job.ServiceName) AS ServiceName, " &
                                "   J.JobType, " &
                                "   J.VisitNumber, " &
                                "   J.Status, " &
                                "   DATE_FORMAT(J.StartTime, '%h:%i %p') AS 'Start Time', " &
                                "   DATE_FORMAT(J.EndTime, '%h:%i %p') AS 'End Time', " &
                                "   TIMEDIFF(J.EndTime, J.StartTime) AS Duration, " &
                                "   CONCAT(T.FirstName, ' ', T.LastName) AS AssignedTech " &
                                "FROM tbl_joborders J " &
                                "INNER JOIN tbl_clients C ON J.ClientID = C.ClientID " &
                                "LEFT JOIN tbl_contracts Con ON J.ContractID = Con.ContractID " &
                                "LEFT JOIN tbl_services S ON Con.ServiceID = S.ServiceID " &
                                "LEFT JOIN tbl_services S_Job ON J.ServiceID = S_Job.ServiceID " &
                                "LEFT JOIN tbl_users T ON J.TechnicianID = T.UserID " &
                                "WHERE J.ScheduledDate = @date"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@date", viewDate.Date)
                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' 3. GET TECHNICIANS LIST
    Public Shared Function GetTechnicians() As DataTable
        Dim dt As New DataTable()
        Using conn As New MySqlConnection(ConnString)
            conn.Open()
            ' [cite: 106]
            Dim sql As String = "SELECT UserID, CONCAT(FirstName, ' ', LastName) AS FullName, FirebaseUID " &
                                "FROM tbl_users WHERE Role='Technician' AND Status='Active'"
            Using cmd As New MySqlCommand(sql, conn)
                Using da As New MySqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    ' 4. ASSIGN TECHNICIAN
    Public Shared Sub AssignTechnician(jobID As Integer, techID As Integer)
        Using conn As New MySqlConnection(ConnString)
            conn.Open()
            ' [cite: 117-118]
            Dim sql As String = "UPDATE tbl_joborders SET TechnicianID = @tid, Status = 'Assigned' WHERE JobID = @jid"
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@tid", techID)
                cmd.Parameters.AddWithValue("@jid", jobID)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class