Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class newUcDashboard

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedJobID As Integer = 0

    ' Calendar Variables
    Private _currentMonth As Integer = DateTime.Now.Month
    Private _currentYear As Integer = DateTime.Now.Year

    ' ---------------------------------------------------------
    ' 1. RESIZING LOGIC (The Math Fix)
    ' ---------------------------------------------------------
    Private Sub ResizeCalendarItems()
        ' Guard clause to prevent crash if calendar is empty or disposed
        If flpCalendar.Controls.Count = 0 Then Return

        flpCalendar.SuspendLayout()

        ' WIDTH MATH: Divide total width by 7 days
        ' We subtract 25px for scrollbar safety and margins
        Dim newWidth As Integer = CInt((flpCalendar.ClientSize.Width - 25) / 7)

        ' HEIGHT MATH: Divide total height by 6 rows (max weeks in a month)
        ' This ensures it fills the panel exactly, even if blocks become rectangles
        Dim newHeight As Integer = CInt((flpCalendar.ClientSize.Height - 20) / 6)

        ' Apply to all controls (Empty Panels & Day Tiles)
        For Each ctrl As Control In flpCalendar.Controls
            ctrl.Size = New Size(newWidth, newHeight)
            ctrl.Margin = New Padding(1) ' Keep margin tight
        Next

        flpCalendar.ResumeLayout()
    End Sub

    ' Event to trigger resize when window/panel changes size
    Private Sub flpCalendar_Resize(sender As Object, e As EventArgs) Handles flpCalendar.Resize
        ResizeCalendarItems()
    End Sub

    ' ---------------------------------------------------------
    ' 2. VISUAL UPDATE LOGIC (The Flicker Fix)
    ' ---------------------------------------------------------
    Private Sub UpdateSelectionVisuals()
        ' Loop through existing blocks instead of reloading the whole calendar
        For Each ctrl As Control In flpCalendar.Controls
            ' We only care about the actual Day Tiles, not the empty spacers
            If TypeOf ctrl Is ucCalendarDay Then
                Dim tile As ucCalendarDay = CType(ctrl, ucCalendarDay)

                ' Reset to default White
                tile.BackColor = Color.White

                ' Check if it is TODAY
                If tile.DayDate.Date = DateTime.Now.Date Then
                    tile.BackColor = Color.LightBlue
                End If

                ' Check if it is SELECTED (Matches the DateTimePicker)
                If tile.DayDate.Date = dtpViewDate.Value.Date Then
                    tile.BackColor = Color.LightGray
                End If
            End If
        Next
    End Sub

    ' ---------------------------------------------------------
    ' 3. CALENDAR GENERATION
    ' ---------------------------------------------------------
    Private Sub LoadCalendar()
        flpCalendar.Controls.Clear()
        flpCalendar.SuspendLayout() ' Stop flickering while drawing

        ' A. Update Header Label
        Dim strMonth As String = MonthName(_currentMonth)
        lblMonthYear.Text = $"{strMonth.ToUpper()} {_currentYear}"

        ' B. Get First Day of Month and Total Days
        Dim startOfMonth As New DateTime(_currentYear, _currentMonth, 1)
        Dim daysInMonth As Integer = DateTime.DaysInMonth(_currentYear, _currentMonth)
        Dim startDayOfWeek As Integer = CInt(startOfMonth.DayOfWeek) ' Sunday = 0

        ' C. GET JOB DATES FROM DATABASE (To show dots)
        Dim jobDays As New List(Of Integer)
        Using conn As New MySqlConnection(connString)
            conn.Open()
            ' We look at ScheduledDate to find matches in this month/year
            Dim sql As String = "SELECT DISTINCT DAY(ScheduledDate) FROM tbl_JobOrders " &
                                "WHERE MONTH(ScheduledDate) = @m AND YEAR(ScheduledDate) = @y"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@m", _currentMonth)
            cmd.Parameters.AddWithValue("@y", _currentYear)
            Dim dr As MySqlDataReader = cmd.ExecuteReader()
            While dr.Read()
                jobDays.Add(dr.GetInt32(0))
            End While
        End Using

        ' D. Generate Empty Slots (Padding for days before the 1st)
        For i As Integer = 0 To startDayOfWeek - 1
            Dim emptyPanel As New Panel()
            ' Initial size doesn't matter, ResizeCalendarItems will fix it immediately
            emptyPanel.Size = New Size(10, 10)
            flpCalendar.Controls.Add(emptyPanel)
        Next

        ' E. Generate Actual Days
        For day As Integer = 1 To daysInMonth
            Dim dayTile As New ucCalendarDay()
            Dim currentDate As New DateTime(_currentYear, _currentMonth, day)

            ' Check if this day has a job
            Dim hasJob As Boolean = jobDays.Contains(day)

            dayTile.SetDay(day, currentDate, hasJob)

            ' Highlight Today
            If currentDate.Date = DateTime.Now.Date Then
                dayTile.BackColor = Color.LightBlue
            End If

            ' Highlight Selected Date
            If currentDate.Date = dtpViewDate.Value.Date Then
                dayTile.BackColor = Color.LightGray
            End If

            ' Add Click Event Handler
            AddHandler dayTile.DayClicked, AddressOf OnDayTileClicked

            flpCalendar.Controls.Add(dayTile)
        Next

        ' F. Apply the calculated sizes immediately after loading
        ResizeCalendarItems()

        flpCalendar.ResumeLayout()
    End Sub

    Private Sub btnPrevMonth_Click(sender As Object, e As EventArgs) Handles btnPrevMonth.Click
        _currentMonth -= 1
        If _currentMonth < 1 Then
            _currentMonth = 12
            _currentYear -= 1
        End If
        LoadCalendar()
    End Sub

    Private Sub btnNextMonth_Click(sender As Object, e As EventArgs) Handles btnNextMonth.Click
        _currentMonth += 1
        If _currentMonth > 12 Then
            _currentMonth = 1
            _currentYear += 1
        End If
        LoadCalendar()
    End Sub

    ' This runs when a user clicks a calendar tile
    Private Sub OnDayTileClicked(selectedDate As Date)
        ' 1. Update the DateTimePicker (triggers grid refresh)
        dtpViewDate.Value = selectedDate

        ' 2. Update visuals ONLY (No Reloading!)
        UpdateSelectionVisuals()
    End Sub

    ' ---------------------------------------------------------
    ' 4. MAIN FORM LOGIC (Existing Code)
    ' ---------------------------------------------------------
    Private Sub newUcDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize Firebase Connection
        FirebaseManager.Initialize()

        ' Load Data
        LoadTechnicians()
        LoadJobs(DateTime.Now)

        ' Initialize Calendar
        _currentMonth = DateTime.Now.Month
        _currentYear = DateTime.Now.Year
        LoadCalendar()

        ' Start Real-time Listener
        FirebaseManager.ListenForJobUpdates()
        AddHandler FirebaseManager.JobStatusUpdated, AddressOf OnJobStatusUpdate
    End Sub

    Private Sub OnJobStatusUpdate(jobID As Integer, newStatus As String)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() OnJobStatusUpdate(jobID, newStatus))
        Else
            LoadJobs(dtpViewDate.Value)
        End If
    End Sub

    Private Sub LoadTechnicians()
        Using conn As New MySqlConnection(connString)
            Dim cmd As New MySqlCommand("SELECT TechnicianID, CONCAT(FirstName, ' ', LastName) AS FullName, FirebaseUID FROM tbl_Technicians WHERE Status='Active'", conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            cmbTechnician.DataSource = dt
            cmbTechnician.DisplayMember = "FullName"
            cmbTechnician.ValueMember = "TechnicianID"
        End Using
    End Sub

    Private Sub LoadJobs(viewDate As Date)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim sql As String = "SELECT " &
                                    "   J.JobID, " &
                                    "   C.ClientName, " &
                                    "   C.Address, " &
                                    "   S.ServiceName, " &
                                    "   S.ServiceID, " &
                                    "   J.JobType, " &
                                    "   J.VisitNumber, " &
                                    "   J.Status, " &
                                    "   CONCAT(T.FirstName, ' ', T.LastName) AS AssignedTech " &
                                    "FROM tbl_JobOrders J " &
                                    "LEFT JOIN tbl_Contracts Con ON J.ContractID = Con.ContractID " &
                                    "LEFT JOIN tbl_Clients C ON (Con.ClientID = C.ClientID OR J.ClientID_TempLink = C.ClientID) " &
                                    "LEFT JOIN tbl_Services S ON Con.ServiceID = S.ServiceID " &
                                    "LEFT JOIN tbl_Technicians T ON J.TechnicianID = T.TechnicianID " &
                                    "WHERE J.ScheduledDate = @date"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@date", viewDate.Date)

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvDailyJobs.DataSource = dt
                dgvDailyJobs.Columns("JobID").Visible = False
                If dgvDailyJobs.Columns("ServiceID") IsNot Nothing Then dgvDailyJobs.Columns("ServiceID").Visible = False

                ColorCodeRows()

            Catch ex As Exception
                MessageBox.Show("Error loading jobs: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ColorCodeRows()
        For Each row As DataGridViewRow In dgvDailyJobs.Rows
            Dim status As String = row.Cells("Status").Value.ToString()
            Dim type As String = row.Cells("JobType").Value.ToString()

            If status = "Completed" Then
                row.DefaultCellStyle.BackColor = Color.LightGreen
            ElseIf status = "Accepted" Then
                row.DefaultCellStyle.BackColor = Color.LightBlue
            ElseIf status = "In Progress" Then
                row.DefaultCellStyle.BackColor = Color.Orange
            ElseIf type = "Inspection" Then
                row.DefaultCellStyle.BackColor = Color.LightYellow
            End If
        Next
    End Sub

    Private Sub dtpViewDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpViewDate.ValueChanged
        LoadJobs(dtpViewDate.Value)
        ' Optional: Force calendar to update its grey selection if date changes via picker
        UpdateSelectionVisuals()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadJobs(dtpViewDate.Value)
    End Sub

    Private Sub dgvDailyJobs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDailyJobs.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvDailyJobs.Rows(e.RowIndex)
            _selectedJobID = Convert.ToInt32(row.Cells("JobID").Value)
            Dim client As String = row.Cells("ClientName").Value.ToString()
            Dim service As String = row.Cells("ServiceName").Value.ToString()
            lblSelectedJob.Text = "Dispatching: " & client & " (" & service & ")"
            lblSelectedJob.ForeColor = Color.Blue
        End If
    End Sub

    Private Async Sub btnAssignJob_Click(sender As Object, e As EventArgs) Handles btnAssignJob.Click
        If _selectedJobID = 0 Or cmbTechnician.SelectedIndex = -1 Then
            MessageBox.Show("Please select a job and a technician.")
            Exit Sub
        End If

        Dim techID As Integer = Convert.ToInt32(cmbTechnician.SelectedValue)
        Dim drv As DataRowView = CType(cmbTechnician.SelectedItem, DataRowView)
        Dim techFirebaseUID As String = drv("FirebaseUID").ToString()

        Dim selectedRow As DataGridViewRow = dgvDailyJobs.CurrentRow
        Dim clientName As String = selectedRow.Cells("ClientName").Value.ToString()
        Dim address As String = selectedRow.Cells("Address").Value.ToString()
        Dim serviceName As String = selectedRow.Cells("ServiceName").Value.ToString()
        Dim visitDate As Date = dtpViewDate.Value
        Dim jobType As String = selectedRow.Cells("JobType").Value.ToString()

        Dim serviceID As Integer = 0
        If Not IsDBNull(selectedRow.Cells("ServiceID").Value) Then
            serviceID = Convert.ToInt32(selectedRow.Cells("ServiceID").Value)
        End If

        btnAssignJob.Enabled = False
        btnAssignJob.Text = "Syncing..."

        Try
            Using conn As New MySqlConnection(connString)
                conn.Open()
                Dim sql As String = "UPDATE tbl_JobOrders SET TechnicianID = @tid, Status = 'Assigned' WHERE JobID = @jid"
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@tid", techID)
                cmd.Parameters.AddWithValue("@jid", _selectedJobID)
                cmd.ExecuteNonQuery()
            End Using

            If techFirebaseUID <> "" Then
                Await FirebaseManager.DispatchJobToMobile(_selectedJobID, clientName, address, serviceName, visitDate, techFirebaseUID, jobType, serviceID)
            Else
                MessageBox.Show("Warning: Technician has no Mobile Account.")
            End If

            MessageBox.Show("Job Dispatched Successfully!")
            LoadJobs(dtpViewDate.Value)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            btnAssignJob.Enabled = True
            btnAssignJob.Text = "ASSIGN TECH"
        End Try
    End Sub

End Class