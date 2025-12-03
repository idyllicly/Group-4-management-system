Imports MySql.Data.MySqlClient

Public Class newUcTechMonitor

    ' CONNECTION STRING
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    ' Variable to hold the ID of the currently selected technician (UserID)
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
                ' UPDATED SQL: Query tbl_users where Role is Technician
                Dim sql As String = "SELECT UserID, FullName FROM tbl_users WHERE Role = 'Technician'"
                Dim cmd As New MySqlCommand(sql, conn)
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                ' Bind to the ListBox
                lstTechnicians.DataSource = dt
                lstTechnicians.DisplayMember = "FullName"
                lstTechnicians.ValueMember = "UserID" ' This is now the UserID

                ' Clear selection initially
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
                ' Get UserID instead of TechnicianID
                _currentTechID = Convert.ToInt32(drv("UserID"))

                ' Load Profile
                LoadTechProfile(_currentTechID)

                ' Load Active Assignments
                LoadTechAssignments(_currentTechID)

                ' Load History (Default to ALL TIME when switching users)
                LoadTechHistory(_currentTechID)

                ' Reset the DatePickers visually
                dtpStart.Value = DateTime.Now.AddMonths(-1)
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
            ' 1. Get Basic Info from tbl_users
            Dim sqlInfo As String = "SELECT FullName, ContactNo, Status FROM tbl_users WHERE UserID = @id"
            Dim cmd As New MySqlCommand(sqlInfo, conn)
            cmd.Parameters.AddWithValue("@id", techID)

            Using reader As MySqlDataReader = cmd.ExecuteReader()
                If reader.Read() Then
                    ' UPDATED: Use FullName directly
                    lblTechName.Text = reader("FullName").ToString()

                    ' Handle Null ContactNo
                    If IsDBNull(reader("ContactNo")) Then
                        lblContact.Text = "Contact: N/A"
                    Else
                        lblContact.Text = "Contact: " & reader("ContactNo").ToString()
                    End If

                    lblStatus.Text = "Status: " & reader("Status").ToString()

                    ' Visual touch
                    If lblStatus.Text.Contains("Active") Then
                        lblStatus.ForeColor = Color.Green
                    Else
                        lblStatus.ForeColor = Color.Red
                    End If
                End If
            End Using

            ' 2. Get Performance Stats (Count of Pending vs Completed)
            ' Note: tbl_JobOrders still uses 'TechnicianID' column, but it holds UserIDs now.
            Dim sqlStats As String = "SELECT " &
                                     "(SELECT COUNT(*) FROM tbl_joborders WHERE TechnicianID = @id AND Status IN ('Pending', 'Assigned')) AS PendingCount, " &
                                     "(SELECT COUNT(*) FROM tbl_joborders WHERE TechnicianID = @id AND Status = 'Completed') AS CompletedCount"

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
            ' UPDATED SQL: Concatenate Address parts for display
            Dim sql As String = "SELECT " &
                                "   J.JobID, " &
                                "   J.ScheduledDate, " &
                                "   J.JobType, " &
                                "   C.ClientName, " &
                                "   CONCAT(C.StreetAddress, ', ', C.City) AS Address, " & ' Concatenated Address
                                "   J.Status " &
                                "FROM tbl_joborders J " &
                                "LEFT JOIN tbl_contracts Con ON J.ContractID = Con.ContractID " &
                                "LEFT JOIN tbl_clients C ON (Con.ClientID = C.ClientID OR J.ClientID_TempLink = C.ClientID) " &
                                "WHERE J.TechnicianID = @id AND J.Status != 'Completed' " &
                                "ORDER BY J.ScheduledDate ASC"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", techID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvAssignments.DataSource = dt

            ' Hide ID column
            If dgvAssignments.Columns("JobID") IsNot Nothing Then dgvAssignments.Columns("JobID").Visible = False
        End Using
    End Sub

    ' ==========================================
    ' PART E: LOAD JOB HISTORY (WITH FILTER)
    ' ==========================================
    Private Sub LoadTechHistory(techID As Integer, Optional startDate As Date? = Nothing, Optional endDate As Date? = Nothing)
        Using conn As New MySqlConnection(connString)
            conn.Open()

            ' UPDATED SQL: Concatenate Address parts
            Dim sql As String = "SELECT " &
                                "   J.ScheduledDate, " &
                                "   J.JobType, " &
                                "   C.ClientName, " &
                                "   J.StartTime, " &
                                "   J.EndTime " &
                                "FROM tbl_joborders J " &
                                "LEFT JOIN tbl_contracts Con ON J.ContractID = Con.ContractID " &
                                "LEFT JOIN tbl_clients C ON (Con.ClientID = C.ClientID OR J.ClientID_TempLink = C.ClientID) " &
                                "WHERE J.TechnicianID = @id AND J.Status = 'Completed' "

            ' DYNAMIC SQL: If dates are provided, add the filter
            If startDate.HasValue And endDate.HasValue Then
                sql &= " AND J.ScheduledDate BETWEEN @start AND @end "
            End If

            ' Order by newest first
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