Imports MySql.Data.MySqlClient

Public Class newUcTechMonitor

    ' CONNECTION STRING (Same as your Dashboard)
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' Variable to hold the ID of the currently selected technician
    Private _currentTechID As Integer = 0

    Private Sub newUcTechMonitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTechnicianList()
        ClearDetails()
    End Sub

    ' ==========================================
    ' PART A: LOAD THE LIST OF TECHNICIANS
    ' ==========================================
    Private Sub LoadTechnicianList()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' We get ID and Name. We concatenate First and Last name for display.
                Dim sql As String = "SELECT TechnicianID, CONCAT(FirstName, ' ', LastName) AS FullName FROM tbl_Technicians"
                Dim cmd As New MySqlCommand(sql, conn)
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                ' Bind to the ListBox
                lstTechnicians.DataSource = dt
                lstTechnicians.DisplayMember = "FullName"
                lstTechnicians.ValueMember = "TechnicianID"

                ' Clear selection initially so the user is forced to click one
                lstTechnicians.ClearSelected()
            Catch ex As Exception
                MessageBox.Show("Error loading techs: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' PART B: HANDLE SELECTION CHANGE
    ' ==========================================
    Private Sub lstTechnicians_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstTechnicians.SelectedIndexChanged
        If lstTechnicians.SelectedIndex <> -1 Then
            Try
                Dim drv As DataRowView = CType(lstTechnicians.SelectedItem, DataRowView)
                _currentTechID = Convert.ToInt32(drv("TechnicianID"))

                ' Load Profile
                LoadTechProfile(_currentTechID)

                ' Load Active Assignments
                LoadTechAssignments(_currentTechID)

                ' Load History (Default to ALL TIME when switching users)
                LoadTechHistory(_currentTechID)

                ' Optional: Reset the DatePickers to today visually
                dtpStart.Value = DateTime.Now.AddMonths(-1) ' Default to last month
                dtpEnd.Value = DateTime.Now

            Catch ex As Exception
                MessageBox.Show("Error selecting tech: " & ex.Message)
            End Try
        End If
    End Sub

    ' ==========================================
    ' PART C: LOAD PROFILE & STATS
    ' ==========================================
    Private Sub LoadTechProfile(techID As Integer)
        Using conn As New MySqlConnection(connString)
            conn.Open()
            ' 1. Get Basic Info
            Dim sqlInfo As String = "SELECT * FROM tbl_Technicians WHERE TechnicianID = @id"
            Dim cmd As New MySqlCommand(sqlInfo, conn)
            cmd.Parameters.AddWithValue("@id", techID)

            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    lblTechName.Text = reader("FirstName").ToString() & " " & reader("LastName").ToString()
                    lblContact.Text = "Contact: " & reader("ContactNo").ToString()
                    lblStatus.Text = "Status: " & reader("Status").ToString()

                    ' Visual touch: Green text if active
                    If lblStatus.Text.Contains("Active") Then
                        lblStatus.ForeColor = Color.Green
                    Else
                        lblStatus.ForeColor = Color.Red
                    End If
                End If
            End Using

            ' 2. Get Performance Stats (Count of Pending vs Completed)
            ' This is useful for monitoring workload balance
            Dim sqlStats As String = "SELECT " &
                                     "(SELECT COUNT(*) FROM tbl_JobOrders WHERE TechnicianID = @id AND Status IN ('Pending', 'Assigned')) AS PendingCount, " &
                                     "(SELECT COUNT(*) FROM tbl_JobOrders WHERE TechnicianID = @id AND Status = 'Completed') AS CompletedCount"

            Dim cmdStats As New MySqlCommand(sqlStats, conn)
            cmdStats.Parameters.AddWithValue("@id", techID)

            Using readerStats As MySqlDataReader = cmdStats.ExecuteReader()
                If readerStats.Read() Then
                    lblTotalAssigned.Text = "Active Jobs: " & readerStats("PendingCount").ToString()
                    lblCompletedJobs.Text = "Lifetime Completed: " & readerStats("CompletedCount").ToString()
                End If
            End Using
        End Using
    End Sub

    ' ==========================================
    ' PART D: LOAD CURRENT/UPCOMING ASSIGNMENTS
    ' ==========================================
    Private Sub LoadTechAssignments(techID As Integer)
        Using conn As New MySqlConnection(connString)
            ' We show jobs that are NOT completed yet (Assigned, Accepted, In Progress)
            Dim sql As String = "SELECT " &
                                "   J.JobID, " &
                                "   J.ScheduledDate, " &
                                "   J.JobType, " &
                                "   C.ClientName, " &
                                "   C.Address, " &
                                "   J.Status " &
                                "FROM tbl_JobOrders J " &
                                "LEFT JOIN tbl_Contracts Con ON J.ContractID = Con.ContractID " &
                                "LEFT JOIN tbl_Clients C ON (Con.ClientID = C.ClientID OR J.ClientID_TempLink = C.ClientID) " &
                                "WHERE J.TechnicianID = @id AND J.Status != 'Completed' " &
                                "ORDER BY J.ScheduledDate ASC"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", techID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvAssignments.DataSource = dt

            ' Hide ID column for cleaner look
            If dgvAssignments.Columns("JobID") IsNot Nothing Then dgvAssignments.Columns("JobID").Visible = False
        End Using
    End Sub

    ' ==========================================
    ' PART E: LOAD JOB HISTORY (COMPLETED)
    ' ==========================================
    ' ==========================================
    ' PART E: LOAD JOB HISTORY (UPDATED WITH FILTER)
    ' ==========================================
    ' We make startDate and endDate Optional. If not provided, we show everything.
    Private Sub LoadTechHistory(techID As Integer, Optional startDate As Date? = Nothing, Optional endDate As Date? = Nothing)
        Using conn As New MySqlConnection(connString)
            conn.Open()

            ' Base SQL: Get completed jobs for this tech
            ' We rely on J.ScheduledDate from tbl_joborders 
            Dim sql As String = "SELECT " &
                                "   J.ScheduledDate, " &
                                "   J.JobType, " &
                                "   C.ClientName, " &
                                "   J.StartTime, " &
                                "   J.EndTime " &
                                "FROM tbl_JobOrders J " &
                                "LEFT JOIN tbl_Contracts Con ON J.ContractID = Con.ContractID " &
                                "LEFT JOIN tbl_Clients C ON (Con.ClientID = C.ClientID OR J.ClientID_TempLink = C.ClientID) " &
                                "WHERE J.TechnicianID = @id AND J.Status = 'Completed' "

            ' DYNAMIC SQL: If dates are provided, add the filter
            If startDate.HasValue And endDate.HasValue Then
                sql &= " AND J.ScheduledDate BETWEEN @start AND @end "
            End If

            ' Always order by newest first
            sql &= " ORDER BY J.ScheduledDate DESC"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", techID)

            ' Add parameters only if we are filtering
            If startDate.HasValue And endDate.HasValue Then
                cmd.Parameters.AddWithValue("@start", startDate.Value.Date)
                cmd.Parameters.AddWithValue("@end", endDate.Value.Date)
            End If

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvHistory.DataSource = dt
        End Using
    End Sub

    ' ==========================================
    ' PART F: BUTTON EVENTS
    ' ==========================================

    Private Sub btnFilter_Click(sender As Object, e As EventArgs) Handles btnFilter.Click
        If _currentTechID = 0 Then
            MessageBox.Show("Please select a technician first.")
            Exit Sub
        End If

        ' Reload history with the specific date range
        LoadTechHistory(_currentTechID, dtpStart.Value, dtpEnd.Value)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        If _currentTechID = 0 Then Exit Sub

        ' Reload without dates (pass Nothing) to show everything
        LoadTechHistory(_currentTechID)
    End Sub

    ' Helper to reset UI if no one is selected
    Private Sub ClearDetails()
        lblTechName.Text = "Select a Technician"
        lblContact.Text = "..."
        lblStatus.Text = "..."
        lblTotalAssigned.Text = "0"
        lblCompletedJobs.Text = "0"
        dgvAssignments.DataSource = Nothing
        dgvHistory.DataSource = Nothing
    End Sub

    Private Sub SplitContainer1_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel1.Paint

    End Sub
End Class