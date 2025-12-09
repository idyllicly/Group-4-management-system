Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class newUcDashboard
    ' --- PROPERTIES & VARIABLES ---
    Public Property PresetDate As Date = DateTime.MinValue
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedJobID As Integer = 0

    ' Calendar Navigation Variables
    Private _currentMonth As Integer = DateTime.Now.Month
    Private _currentYear As Integer = DateTime.Now.Year

    ' ---------------------------------------------------------
    ' 1. MAIN FORM LOAD & SETUP
    ' ---------------------------------------------------------
    Private Sub newUcDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize Firebase & Techs
        FirebaseManager.Initialize()
        LoadTechnicians()

        ' Visual Tweaks for Grid
        dgvDailyJobs.DefaultCellStyle.SelectionBackColor = Color.White
        dgvDailyJobs.DefaultCellStyle.SelectionForeColor = Color.Black
        ' Dashboard Load Event
        dgvDailyJobs.BackgroundColor = Color.White
        dgvDailyJobs.BorderStyle = BorderStyle.None
        dgvDailyJobs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgvDailyJobs.EnableHeadersVisualStyles = False

        ' Header Style
        dgvDailyJobs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249) ' Soft Slate
        dgvDailyJobs.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(100, 116, 139) ' Cool Gray
        dgvDailyJobs.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Bold)
        dgvDailyJobs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgvDailyJobs.ColumnHeadersHeight = 40

        ' Row Style
        dgvDailyJobs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 246, 255) ' Very Light Blue highlight
        dgvDailyJobs.DefaultCellStyle.SelectionForeColor = Color.Black
        dgvDailyJobs.RowTemplate.Height = 35
        dgvDailyJobs.GridColor = Color.FromArgb(226, 232, 240) ' Very faint grid lines

        ' Set Initial Date
        If PresetDate <> DateTime.MinValue Then
            dtpViewDate.Value = PresetDate
        Else
            dtpViewDate.Value = DateTime.Now
        End If

        ' Initialize Calendar Variables
        _currentMonth = dtpViewDate.Value.Month
        _currentYear = dtpViewDate.Value.Year

        ' Initial Load
        LoadCalendars()
        LoadJobs(dtpViewDate.Value)

        ' Listen for real-time updates
        FirebaseManager.ListenForJobUpdates()
        AddHandler FirebaseManager.JobStatusUpdated, AddressOf OnJobStatusUpdate
    End Sub

    Private Sub OnJobStatusUpdate(jobID As Integer, newStatus As String)
        If Me.InvokeRequired Then
            Me.Invoke(Sub() OnJobStatusUpdate(jobID, newStatus))
        Else
            ' Refresh list and calendar counts when status changes
            LoadJobs(dtpViewDate.Value)
            LoadCalendars()
        End If
    End Sub

    ' ---------------------------------------------------------
    ' 2. CALENDAR GENERATION
    ' ---------------------------------------------------------
    Private Sub LoadCalendars()
        ' Generate current month (Panel 1)
        GenerateSingleCalendar(flpCalendar1, lblMonthYear1, _currentMonth, _currentYear)

        ' Calculate next month
        Dim nextMonth As Integer = _currentMonth + 1
        Dim nextYear As Integer = _currentYear
        If nextMonth > 12 Then
            nextMonth = 1
            nextYear += 1
        End If

        ' Generate next month (Panel 2)
        GenerateSingleCalendar(flpCalendar2, lblMonthYear2, nextMonth, nextYear)

        ' Ensure sizes are correct
        ResizeCalendarItems()
    End Sub

    Private Sub GenerateSingleCalendar(targetPanel As FlowLayoutPanel, targetLabel As Label, m As Integer, y As Integer)
        ' 1. Clear previous tiles
        targetPanel.Controls.Clear()
        targetPanel.SuspendLayout()

        ' 2. Set Header Text
        Dim strMonth As String = MonthName(m)
        targetLabel.Text = $"{strMonth.ToUpper()} {y}"

        ' 3. Create Day Headers (SUN, MON...)
        Dim dayNames As String() = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"}
        For Each dayName As String In dayNames
            Dim lbl As New Label()
            lbl.Text = dayName.ToUpper()
            lbl.TextAlign = ContentAlignment.MiddleCenter
            lbl.BackColor = Color.Transparent
            lbl.ForeColor = Color.FromArgb(100, 116, 139) ' Cool Gray
            lbl.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            lbl.AutoSize = False
            targetPanel.Controls.Add(lbl)
        Next

        ' 4. FETCH DATA: Get counts of jobs per day
        Dim dtCounts As New DataTable()
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim sql As String = "SELECT DAY(ScheduledDate) as DayNum, JobType, COUNT(*) as JobCount " &
                                "FROM tbl_joborders " &
                                "WHERE MONTH(ScheduledDate) = @m AND YEAR(ScheduledDate) = @y " &
                                "GROUP BY DAY(ScheduledDate), JobType"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@m", m)
            cmd.Parameters.AddWithValue("@y", y)
            Dim da As New MySqlDataAdapter(cmd)
            da.Fill(dtCounts)
        End Using

        ' 5. Calculate spacing
        Dim startOfMonth As New DateTime(y, m, 1)
        Dim daysInMonth As Integer = DateTime.DaysInMonth(y, m)
        Dim startDayOfWeek As Integer = CInt(startOfMonth.DayOfWeek)

        For i As Integer = 0 To startDayOfWeek - 1
            Dim emptyPanel As New Panel()
            emptyPanel.Size = New Size(10, 10)
            targetPanel.Controls.Add(emptyPanel)
        Next

        ' 6. Generate the actual Day Tiles
        For day As Integer = 1 To daysInMonth
            Dim dayTile As New ucCalendarDay()
            Dim currentDate As New DateTime(y, m, day)

            dayTile.ClearData()
            dayTile.SetDay(day, currentDate)

            ' Filter data for this specific day
            Dim rowsForDay As DataRow() = dtCounts.Select($"DayNum = {day}")

            ' Add pills
            For Each row As DataRow In rowsForDay
                Dim count As Integer = Convert.ToInt32(row("JobCount"))
                Dim type As String = row("JobType").ToString()
                dayTile.AddJobSummary(count, type)
            Next

            ' Visuals
            dayTile.BackColor = Color.White
            If currentDate.Date = DateTime.Now.Date Then dayTile.BackColor = Color.LightBlue
            If currentDate.Date = dtpViewDate.Value.Date Then dayTile.BackColor = Color.LightGray

            ' Hook up the click event
            AddHandler dayTile.DayClicked, AddressOf OnDayTileClicked
            targetPanel.Controls.Add(dayTile)
        Next

        targetPanel.ResumeLayout()
    End Sub

    ' ---------------------------------------------------------
    ' 3. RESIZING LOGIC
    ' ---------------------------------------------------------
    Private Sub ResizeCalendarItems()
        ResizeSinglePanel(flpCalendar1)
        ResizeSinglePanel(flpCalendar2)
    End Sub

    Private Sub ResizeSinglePanel(panel As FlowLayoutPanel)
        If panel.Controls.Count = 0 Then Return

        panel.SuspendLayout()
        Dim newWidth As Integer = CInt((panel.ClientSize.Width - 25) / 7)
        Dim headerHeight As Integer = 30

        Dim totalDaySlots As Integer = panel.Controls.Count - 7
        Dim numRows As Integer = Math.Ceiling(totalDaySlots / 7)
        If numRows < 1 Then numRows = 1

        Dim availableHeight As Integer = panel.ClientSize.Height - headerHeight - 10
        Dim newTileHeight As Integer = CInt(availableHeight / numRows)

        For i As Integer = 0 To panel.Controls.Count - 1
            Dim ctrl As Control = panel.Controls(i)
            If i < 7 Then
                ctrl.Size = New Size(newWidth, headerHeight)
                ctrl.Margin = New Padding(1, 0, 1, 0)
            Else
                ctrl.Size = New Size(newWidth, newTileHeight)
                ctrl.Margin = New Padding(1)
            End If
        Next
        panel.ResumeLayout()
    End Sub

    Private Sub flpCalendar_Resize(sender As Object, e As EventArgs) Handles flpCalendar1.Resize, flpCalendar2.Resize
        ResizeCalendarItems()
    End Sub

    ' ---------------------------------------------------------
    ' 4. CLICK & NAVIGATION EVENTS (Fixed)
    ' ---------------------------------------------------------

    Private Sub OnDayTileClicked(selectedDate As Date)
        ' Force the date change. This triggers ValueChanged below.
        dtpViewDate.Value = selectedDate
    End Sub

    Private Sub dtpViewDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpViewDate.ValueChanged
        Dim selDate As Date = dtpViewDate.Value

        ' SMART REFRESH: Only reload calendar if month changes
        Dim m1 As Integer = _currentMonth
        Dim y1 As Integer = _currentYear
        Dim m2 As Integer = _currentMonth + 1
        Dim y2 As Integer = _currentYear
        If m2 > 12 Then
            m2 = 1
            y2 += 1
        End If

        Dim isVisible As Boolean = (selDate.Month = m1 AndAlso selDate.Year = y1) OrElse (selDate.Month = m2 AndAlso selDate.Year = y2)

        If Not isVisible Then
            _currentMonth = selDate.Month
            _currentYear = selDate.Year
            LoadCalendars()
        Else
            ' Just update the grey selection box
            UpdateSelectionVisuals()
        End If

        ' Always load jobs for the selected day
        LoadJobs(selDate)
    End Sub

    Private Sub UpdateSelectionVisuals()
        UpdatePanelVisuals(flpCalendar1)
        UpdatePanelVisuals(flpCalendar2)
    End Sub

    Private Sub UpdatePanelVisuals(panel As FlowLayoutPanel)
        For Each ctrl As Control In panel.Controls
            If TypeOf ctrl Is ucCalendarDay Then
                Dim tile As ucCalendarDay = CType(ctrl, ucCalendarDay)

                ' Default State
                tile.BackColor = Color.White

                ' Today (Soft Accent)
                If tile.DayDate.Date = DateTime.Now.Date Then
                    tile.BackColor = Color.FromArgb(219, 234, 254) ' Very Light Blue
                End If

                ' Selected Date (Stronger Outline or Background)
                If tile.DayDate.Date = dtpViewDate.Value.Date Then
                    tile.BackColor = Color.FromArgb(59, 130, 246) ' Ocean Blue (Accent)
                    ' You might need to change text color to white if you do this:
                    ' tile.lblDayNumber.ForeColor = Color.White 
                End If
            End If
        Next
    End Sub

    Private Sub btnPrevMonth_Click(sender As Object, e As EventArgs) Handles btnPrevMonth.Click
        _currentMonth -= 1
        If _currentMonth < 1 Then
            _currentMonth = 12
            _currentYear -= 1
        End If
        LoadCalendars()
    End Sub

    Private Sub btnNextMonth_Click(sender As Object, e As EventArgs) Handles btnNextMonth.Click
        _currentMonth += 1
        If _currentMonth > 12 Then
            _currentMonth = 1
            _currentYear += 1
        End If
        LoadCalendars()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadJobs(dtpViewDate.Value)
        LoadCalendars()
    End Sub

    ' ---------------------------------------------------------
    ' 5. JOB DETAILS & SIDE PANEL
    ' ---------------------------------------------------------
    Private Sub LoadJobs(viewDate As Date)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
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

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@date", viewDate.Date)

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvDailyJobs.DataSource = dt
                dgvDailyJobs.RowHeadersVisible = False

                Dim colsToHide() As String = {"JobID", "Address", "ServiceName", "VisitNumber", "AssignedTech", "Start Time", "End Time", "Duration"}
                For Each colName As String In colsToHide
                    If dgvDailyJobs.Columns(colName) IsNot Nothing Then
                        dgvDailyJobs.Columns(colName).Visible = False
                    End If
                Next

                If dgvDailyJobs.Columns("Status") IsNot Nothing Then dgvDailyJobs.Columns("Status").Width = 80
                If dgvDailyJobs.Columns("JobType") IsNot Nothing Then dgvDailyJobs.Columns("JobType").Width = 80

                ClearDetails()
                ColorCodeRows()

                If dgvDailyJobs.Rows.Count > 0 Then
                    dgvDailyJobs.Rows(0).Selected = True
                    PopulateJobDetails(dgvDailyJobs.Rows(0))
                End If

            Catch ex As Exception
                MessageBox.Show("Error loading jobs: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub LoadTechnicians()
        Using conn As New MySqlConnection(connString)
            Try
                Dim sql As String = "SELECT UserID, CONCAT(FirstName, ' ', LastName) AS FullName, FirebaseUID FROM tbl_users WHERE Role='Technician' AND Status='Active'"
                Dim cmd As New MySqlCommand(sql, conn)
                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                cmbTechnician.DataSource = dt
                cmbTechnician.DisplayMember = "FullName"
                cmbTechnician.ValueMember = "UserID"
            Catch ex As Exception
                MessageBox.Show("Error loading technicians: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub PopulateJobDetails(row As DataGridViewRow)
        _selectedJobID = Convert.ToInt32(row.Cells("JobID").Value)

        lblDetailClient.Text = row.Cells("ClientName").Value.ToString()
        lblDetailAddress.Text = row.Cells("Address").Value.ToString()
        lblDetailService.Text = If(IsDBNull(row.Cells("ServiceName").Value), "Not Specified", row.Cells("ServiceName").Value.ToString())
        lblDetailTech.Text = If(IsDBNull(row.Cells("AssignedTech").Value), "Unassigned", row.Cells("AssignedTech").Value.ToString())
        lblDetailVisit.Text = row.Cells("VisitNumber").Value.ToString()

        Dim sTime As String = row.Cells("Start Time").Value.ToString()
        Dim eTime As String = row.Cells("End Time").Value.ToString()
        Dim dur As String = row.Cells("Duration").Value.ToString()

        lblDetailStart.Text = If(String.IsNullOrEmpty(sTime), "---", sTime)
        lblDetailEnd.Text = If(String.IsNullOrEmpty(eTime), "---", eTime)
        lblDetailDuration.Text = If(String.IsNullOrEmpty(dur), "---", dur)
    End Sub

    Private Sub ClearDetails()
        lblDetailClient.Text = "---"
        lblDetailAddress.Text = "---"
        lblDetailService.Text = "---"
        lblDetailTech.Text = "---"
        lblDetailVisit.Text = "---"
        lblDetailStart.Text = "---"
        lblDetailEnd.Text = "---"
        lblDetailDuration.Text = "---"
        _selectedJobID = 0
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

    Private Sub dgvDailyJobs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDailyJobs.CellClick
        If e.RowIndex >= 0 Then
            PopulateJobDetails(dgvDailyJobs.Rows(e.RowIndex))
        End If
    End Sub

    Private Sub dgvDailyJobs_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvDailyJobs.RowPostPaint
        If dgvDailyJobs.Rows(e.RowIndex).Selected Then
            Using borderPen As New Pen(Color.Red, 2)
                Dim rowBounds As Rectangle = e.RowBounds
                Dim rect As New Rectangle(rowBounds.Left, rowBounds.Top, rowBounds.Width - 1, rowBounds.Height - 1)
                e.Graphics.DrawRectangle(borderPen, rect)
            End Using
        End If
    End Sub

    ' ---------------------------------------------------------
    ' 6. JOB ASSIGNMENT
    ' ---------------------------------------------------------
    Private Async Sub btnAssignJob_Click(sender As Object, e As EventArgs) Handles btnAssignJob.Click
        If _selectedJobID = 0 Or cmbTechnician.SelectedIndex = -1 Then
            MessageBox.Show("Please select a job and a technician.")
            Exit Sub
        End If

        Dim techID As Integer = Convert.ToInt32(cmbTechnician.SelectedValue)
        Dim drv As DataRowView = CType(cmbTechnician.SelectedItem, DataRowView)
        Dim techFirebaseUID As String = If(Not IsDBNull(drv("FirebaseUID")), drv("FirebaseUID").ToString(), "")

        Dim selectedRow As DataGridViewRow = dgvDailyJobs.CurrentRow
        Dim clientName As String = selectedRow.Cells("ClientName").Value.ToString()
        Dim address As String = selectedRow.Cells("Address").Value.ToString()
        Dim serviceName As String = lblDetailService.Text
        Dim visitDate As Date = dtpViewDate.Value
        Dim jobType As String = selectedRow.Cells("JobType").Value.ToString()
        Dim serviceID As Integer = 0

        btnAssignJob.Enabled = False
        btnAssignJob.Text = "Syncing..."

        Try
            Using conn As New MySqlConnection(connString)
                conn.Open()
                Dim sql As String = "UPDATE tbl_joborders SET TechnicianID = @tid, Status = 'Assigned' WHERE JobID = @jid"
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@tid", techID)
                cmd.Parameters.AddWithValue("@jid", _selectedJobID)
                cmd.ExecuteNonQuery()
            End Using

            If techFirebaseUID <> "" Then
                Await FirebaseManager.DispatchJobToMobile(_selectedJobID, clientName, address, serviceName, visitDate, techFirebaseUID, jobType, serviceID)
            Else
                MessageBox.Show("Warning: Technician assigned but they have no Mobile Account.")
            End If

            MessageBox.Show("Job Dispatched Successfully!")

            LoadJobs(dtpViewDate.Value)
            LoadCalendars()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            btnAssignJob.Enabled = True
            btnAssignJob.Text = "ASSIGN TECH"
        End Try
    End Sub

    Private Sub cmbTechnician_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTechnician.SelectedIndexChanged

    End Sub
End Class