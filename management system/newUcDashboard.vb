Imports System.Globalization
Imports System.Threading.Tasks

Public Class newUcDashboard
    ' --- PROPERTIES & VARIABLES ---
    Public Property PresetDate As Date = DateTime.MinValue
    Private _selectedJobID As Integer = 0
    Private _currentMonth As Integer = DateTime.Now.Month
    Private _currentYear As Integer = DateTime.Now.Year

    ' ---------------------------------------------------------
    ' 1. MAIN LOAD
    ' ---------------------------------------------------------
    Private Async Sub newUcDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            FirebaseManager.Initialize()
        Catch ex As Exception
        End Try

        StyleGrid()

        If PresetDate <> DateTime.MinValue Then
            dtpViewDate.Value = PresetDate
        Else
            dtpViewDate.Value = DateTime.Now
        End If

        _currentMonth = dtpViewDate.Value.Month
        _currentYear = dtpViewDate.Value.Year

        Await LoadDataAsync()

        FirebaseManager.ListenForJobUpdates()
        AddHandler FirebaseManager.JobStatusUpdated, AddressOf OnJobStatusUpdate
    End Sub

    Private Sub StyleGrid()
        With dgvDailyJobs
            ' --- 1. General Frame & Background ---

            .BorderStyle = BorderStyle.FixedSingle ' Adds a subtle frame around the table
            .CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal ' Keep horizontal lines only for a clean look
            .GridColor = Color.FromArgb(226, 232, 240) ' Soft gray lines (Tailwind slate-200)
            .EnableHeadersVisualStyles = False ' Required to custom style headers

            ' --- 2. Header Style ---
            .ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249) ' Light gray header
            .ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(71, 85, 105)   ' Dark slate text
            .ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold) ' Slightly larger font
            .ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft ' Standard tables usually align left
            .ColumnHeadersDefaultCellStyle.Padding = New Padding(10, 0, 0, 0) ' Add padding to header text
            .ColumnHeadersHeight = 45 ' Taller header for a modern feel
            .ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single

            ' --- 3. Row Style & Padding (The "Table" Look) ---
            .RowTemplate.Height = 40 ' Give rows breathing room (Standard is usually too small)
            .DefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Regular)
            .DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 81)
            .DefaultCellStyle.Padding = New Padding(10, 0, 10, 0) ' CRITICAL: Keeps text away from the edges
            .DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254) ' Soft blue highlight (instead of harsh deep blue)
            .DefaultCellStyle.SelectionForeColor = Color.Black

            ' --- 4. Zebra Striping (Alternating Rows) ---
            ' This makes it much easier to read line-by-line
            .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252) ' Very light gray for alternate rows

            ' --- 5. Column Sizing Strategy ---
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
            ' We set specific column logic in BindJobsGrid
        End With
    End Sub

    ' ---------------------------------------------------------
    ' 2. ASYNC DATA LOADING
    ' ---------------------------------------------------------
    Private Async Function LoadDataAsync() As Task
        Dim tTechs As Task(Of DataTable) = Task.Run(Function() JobRepository.GetTechnicians())
        Dim tJobs As Task(Of DataTable) = Task.Run(Function() JobRepository.GetJobsByDate(dtpViewDate.Value))

        Await LoadCalendarsAsync()
        Await Task.WhenAll(tTechs, tJobs)

        BindTechnicians(tTechs.Result)
        BindJobsGrid(tJobs.Result)
    End Function

    Private Sub BindTechnicians(dt As DataTable)
        cmbTechnician.DataSource = dt
        cmbTechnician.DisplayMember = "FullName"
        cmbTechnician.ValueMember = "UserID"
    End Sub

    Private Sub BindJobsGrid(dt As DataTable)
        ' 1. Load the data (This creates the columns)
        dgvDailyJobs.DataSource = dt

        ' 2. Hide unwanted columns [cite: 51-52]
        Dim colsToHide() As String = {"JobID", "Address", "ServiceName", "VisitNumber", "AssignedTech", "Start Time", "End Time", "Duration"}
        For Each colName As String In colsToHide
            If dgvDailyJobs.Columns(colName) IsNot Nothing Then dgvDailyJobs.Columns(colName).Visible = False
        Next

        ' 3. APPLY SIZING HERE (Because columns actually exist now)
        With dgvDailyJobs
            ' Reset to ensure we can control individual columns
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

            ' A. Client Name: FILLS the empty space
            If .Columns("ClientName") IsNot Nothing Then
                .Columns("ClientName").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .Columns("ClientName").HeaderText = "CLIENT NAME" ' Optional: Make it caps
            End If

            ' B. Job Type
            If .Columns("JobType") IsNot Nothing Then
                .Columns("JobType").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                ' MiddleLeft often looks cleaner with the new padding, but Center is okay too
                .Columns("JobType").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
                .Columns("JobType").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
            End If

            ' C. Status
            If .Columns("Status") IsNot Nothing Then
                .Columns("Status").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                .Columns("Status").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .Columns("Status").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
        End With

        ClearDetails()
        ColorCodeRows()
    End Sub

    ' ---------------------------------------------------------
    ' 3. CALENDAR GENERATION (Async)
    ' ---------------------------------------------------------
    Private Async Function LoadCalendarsAsync() As Task
        Dim nextM As Integer = _currentMonth + 1
        Dim nextY As Integer = _currentYear
        If nextM > 12 Then
            nextM = 1
            nextY += 1
        End If

        Dim t1 As Task(Of DataTable) = Task.Run(Function() JobRepository.GetMonthlyJobCounts(_currentMonth, _currentYear))
        Dim t2 As Task(Of DataTable) = Task.Run(Function() JobRepository.GetMonthlyJobCounts(nextM, nextY))

        Await Task.WhenAll(t1, t2)

        RenderCalendar(flpCalendar1, lblMonthYear1, _currentMonth, _currentYear, t1.Result)
        RenderCalendar(flpCalendar2, lblMonthYear2, nextM, nextY, t2.Result)
        ResizeCalendarItems()
    End Function

    Private Sub RenderCalendar(panel As FlowLayoutPanel, lblHeader As Label, m As Integer, y As Integer, dtData As DataTable)
        panel.Controls.Clear()
        panel.SuspendLayout()

        lblHeader.Text = $"{MonthName(m).ToUpper()} {y}"

        Dim days() As String = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"}
        For Each d In days
            Dim l As New Label With {.Text = d.ToUpper(), .TextAlign = ContentAlignment.MiddleCenter, .ForeColor = Color.FromArgb(100, 116, 139), .Font = New Font("Segoe UI", 9, FontStyle.Bold), .AutoSize = False}
            panel.Controls.Add(l)
        Next

        Dim startDay As Integer = CInt(New DateTime(y, m, 1).DayOfWeek)
        For i As Integer = 0 To startDay - 1
            panel.Controls.Add(New Panel With {.Size = New Size(10, 10)})
        Next

        Dim daysInMonth As Integer = DateTime.DaysInMonth(y, m)
        For day As Integer = 1 To daysInMonth
            Dim tile As New ucCalendarDay()
            Dim curDate As New DateTime(y, m, day)

            tile.SetDay(day, curDate)

            ' Add Pills
            Dim rows As DataRow() = dtData.Select($"DayNum = {day}")
            For Each row As DataRow In rows
                tile.AddJobSummary(Convert.ToInt32(row("JobCount")), row("JobType").ToString())
            Next ' <--- FIXED: Correct Next Syntax

            ' Highlight Selection
            If curDate.Date = dtpViewDate.Value.Date Then
                tile.SetSelected(True) ' <--- CRITICAL: Use new method
            Else
                tile.SetSelected(False)
            End If

            AddHandler tile.DayClicked, AddressOf OnDayTileClicked
            panel.Controls.Add(tile)
        Next ' <--- Fixed
        panel.ResumeLayout()
    End Sub

    ' ---------------------------------------------------------
    ' 4. EVENTS & INTERACTION
    ' ---------------------------------------------------------
    Private Sub OnDayTileClicked(selectedDate As Date)
        ' This triggers ValueChanged
        dtpViewDate.Value = selectedDate
    End Sub

    Private Async Sub dtpViewDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpViewDate.ValueChanged
        Dim selDate As Date = dtpViewDate.Value

        Dim dtJobs As DataTable = Await Task.Run(Function() JobRepository.GetJobsByDate(selDate))
        BindJobsGrid(dtJobs)

        Dim mNext As Integer = _currentMonth + 1
        Dim yNext As Integer = _currentYear
        If mNext > 12 Then mNext = 1 : yNext += 1

        ' If date is outside current view, reload everything.
        If Not ((selDate.Month = _currentMonth And selDate.Year = _currentYear) Or (selDate.Month = mNext And selDate.Year = yNext)) Then
            _currentMonth = selDate.Month
            _currentYear = selDate.Year
            Await LoadCalendarsAsync()
        Else
            ' Else, just update the blue highlights
            UpdateSelectionVisuals()
        End If
    End Sub

    Private Sub UpdateSelectionVisuals()
        UpdatePanel(flpCalendar1)
        UpdatePanel(flpCalendar2)
    End Sub

    Private Sub UpdatePanel(p As FlowLayoutPanel)
        For Each c As Control In p.Controls
            If TypeOf c Is ucCalendarDay Then
                Dim tile As ucCalendarDay = CType(c, ucCalendarDay)

                ' Check if this tile matches the selected date
                Dim shouldBeSelected As Boolean = (tile.DayDate.Date = dtpViewDate.Value.Date)

                ' This handles Blue/White color and overrides the Hover forgetting state
                tile.SetSelected(shouldBeSelected)
            End If
        Next
    End Sub

    Private Async Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Await LoadDataAsync()
    End Sub

    Private Async Sub btnPrevMonth_Click(sender As Object, e As EventArgs) Handles btnPrevMonth.Click
        _currentMonth -= 1
        If _currentMonth < 1 Then _currentMonth = 12 : _currentYear -= 1
        Await LoadCalendarsAsync()
    End Sub

    Private Async Sub btnNextMonth_Click(sender As Object, e As EventArgs) Handles btnNextMonth.Click
        _currentMonth += 1
        If _currentMonth > 12 Then _currentMonth = 1 : _currentYear += 1
        Await LoadCalendarsAsync()
    End Sub

    ' ---------------------------------------------------------
    ' 5. JOB DETAILS & ASSIGNMENT
    ' ---------------------------------------------------------
    Private Sub dgvDailyJobs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDailyJobs.CellClick
        If e.RowIndex >= 0 Then PopulateJobDetails(dgvDailyJobs.Rows(e.RowIndex))
    End Sub

    Private Sub PopulateJobDetails(row As DataGridViewRow)
        _selectedJobID = Convert.ToInt32(row.Cells("JobID").Value)
        lblDetailClient.Text = row.Cells("ClientName").Value.ToString()
        lblDetailAddress.Text = row.Cells("Address").Value.ToString()
        lblDetailService.Text = If(IsDBNull(row.Cells("ServiceName").Value), "N/A", row.Cells("ServiceName").Value.ToString())
        lblDetailTech.Text = If(IsDBNull(row.Cells("AssignedTech").Value), "Unassigned", row.Cells("AssignedTech").Value.ToString())
        lblDetailVisit.Text = row.Cells("VisitNumber").Value.ToString()
        lblDetailStart.Text = row.Cells("Start Time").Value.ToString()
        lblDetailEnd.Text = row.Cells("End Time").Value.ToString()
        lblDetailDuration.Text = row.Cells("Duration").Value.ToString()
    End Sub

    Private Sub ClearDetails()
        lblDetailClient.Text = "---"
        lblDetailAddress.Text = "---"
        lblDetailService.Text = "---"
        _selectedJobID = 0
    End Sub

    Private Sub ColorCodeRows()
        For Each row As DataGridViewRow In dgvDailyJobs.Rows
            Dim status As String = row.Cells("Status").Value.ToString()
            Select Case status
                Case "Completed" : row.DefaultCellStyle.BackColor = Color.LightGreen
                Case "Accepted" : row.DefaultCellStyle.BackColor = Color.LightBlue
                Case "In Progress" : row.DefaultCellStyle.BackColor = Color.Orange
            End Select
        Next
    End Sub

    Private Async Sub btnAssignJob_Click(sender As Object, e As EventArgs) Handles btnAssignJob.Click
        If _selectedJobID = 0 Or cmbTechnician.SelectedIndex = -1 Then
            MessageBox.Show("Select a job and technician.")
            Exit Sub
        End If

        btnAssignJob.Enabled = False
        btnAssignJob.Text = "Syncing..."

        Dim techID As Integer = Convert.ToInt32(cmbTechnician.SelectedValue)
        Dim drv As DataRowView = CType(cmbTechnician.SelectedItem, DataRowView)
        Dim techUID As String = If(Not IsDBNull(drv("FirebaseUID")), drv("FirebaseUID").ToString(), "")

        Try
            JobRepository.AssignTechnician(_selectedJobID, techID)

            If techUID <> "" Then
                Dim row As DataGridViewRow = dgvDailyJobs.CurrentRow
                Await FirebaseManager.DispatchJobToMobile(_selectedJobID, row.Cells("ClientName").Value.ToString(),
                                                          row.Cells("Address").Value.ToString(), lblDetailService.Text,
                                                          dtpViewDate.Value, techUID, row.Cells("JobType").Value.ToString(), 0)
            End If

            MessageBox.Show("Dispatched!")

            Dim dtJobs As DataTable = Await Task.Run(Function() JobRepository.GetJobsByDate(dtpViewDate.Value))
            BindJobsGrid(dtJobs)
            Await LoadCalendarsAsync()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            btnAssignJob.Enabled = True
            btnAssignJob.Text = "ASSIGN TECH"
        End Try
    End Sub

    Private Sub ResizeCalendarItems()
        ResizeSinglePanel(flpCalendar1)
        ResizeSinglePanel(flpCalendar2)
    End Sub

    Private Sub ResizeSinglePanel(panel As FlowLayoutPanel)
        If panel.Controls.Count = 0 Then Return
        panel.SuspendLayout()
        Dim w As Integer = CInt((panel.ClientSize.Width - 25) / 7)
        Dim rows As Integer = Math.Max(1, Math.Ceiling((panel.Controls.Count - 7) / 7))
        Dim h As Integer = CInt((panel.ClientSize.Height - 30 - 10) / rows)

        For i As Integer = 0 To panel.Controls.Count - 1
            Dim c As Control = panel.Controls(i)
            If i < 7 Then c.Size = New Size(w, 30) Else c.Size = New Size(w, h)
            c.Margin = New Padding(1)
        Next
        panel.ResumeLayout()
    End Sub

    Private Sub flp_Resize(sender As Object, e As EventArgs) Handles flpCalendar1.Resize, flpCalendar2.Resize
        ResizeCalendarItems()
    End Sub

    Private Sub OnJobStatusUpdate(jobID As Integer, newStatus As String)
        If Me.InvokeRequired Then Me.Invoke(Sub() OnJobStatusUpdate(jobID, newStatus)) : Return
        btnRefresh.PerformClick()
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub dgvDailyJobs_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDailyJobs.CellContentClick

    End Sub

    Private Sub FlowLayoutPanel3_Paint(sender As Object, e As PaintEventArgs) Handles FlowLayoutPanel3.Paint

    End Sub
End Class