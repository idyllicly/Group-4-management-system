Imports MySql.Data.MySqlClient
Imports System.Globalization

Public Class newUcDashboard

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedJobID As Integer = 0

    ' Calendar Variables
    Private _currentMonth As Integer = DateTime.Now.Month
    Private _currentYear As Integer = DateTime.Now.Year

    ' ---------------------------------------------------------
    ' 1. RESIZING LOGIC
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
    ' 2. VISUAL UPDATE LOGIC
    ' ---------------------------------------------------------
    Private Sub UpdateSelectionVisuals()
        UpdatePanelVisuals(flpCalendar1)
        UpdatePanelVisuals(flpCalendar2)
    End Sub

    Private Sub UpdatePanelVisuals(panel As FlowLayoutPanel)
        For Each ctrl As Control In panel.Controls
            If TypeOf ctrl Is ucCalendarDay Then
                Dim tile As ucCalendarDay = CType(ctrl, ucCalendarDay)
                tile.BackColor = Color.White
                If tile.DayDate.Date = DateTime.Now.Date Then tile.BackColor = Color.LightBlue
                If tile.DayDate.Date = dtpViewDate.Value.Date Then tile.BackColor = Color.LightGray
            End If
        Next
    End Sub

    ' ---------------------------------------------------------
    ' 3. CALENDAR GENERATION
    ' ---------------------------------------------------------
    Private Sub LoadCalendars()
        GenerateSingleCalendar(flpCalendar1, lblMonthYear1, _currentMonth, _currentYear)
        Dim nextMonth As Integer = _currentMonth + 1
        Dim nextYear As Integer = _currentYear
        If nextMonth > 12 Then
            nextMonth = 1
            nextYear += 1
        End If
        GenerateSingleCalendar(flpCalendar2, lblMonthYear2, nextMonth, nextYear)
        ResizeCalendarItems()
    End Sub

    Private Sub GenerateSingleCalendar(targetPanel As FlowLayoutPanel, targetLabel As Label, m As Integer, y As Integer)
        targetPanel.Controls.Clear()
        targetPanel.SuspendLayout()

        Dim strMonth As String = MonthName(m)
        targetLabel.Text = $"{strMonth.ToUpper()} {y}"

        Dim dayNames As String() = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"}
        For Each dayName As String In dayNames
            Dim lbl As New Label()
            lbl.Text = dayName.ToUpper()
            lbl.TextAlign = ContentAlignment.MiddleCenter
            lbl.Font = New Font("Segoe UI", 9, FontStyle.Bold)
            lbl.BackColor = Color.Transparent
            lbl.ForeColor = Color.DimGray
            lbl.AutoSize = False
            targetPanel.Controls.Add(lbl)
        Next

        Dim jobDays As New List(Of Integer)
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim sql As String = "SELECT DISTINCT DAY(ScheduledDate) FROM tbl_joborders " &
                                "WHERE MONTH(ScheduledDate) = @m AND YEAR(ScheduledDate) = @y"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@m", m)
            cmd.Parameters.AddWithValue("@y", y)
            Dim dr As MySqlDataReader = cmd.ExecuteReader()
            While dr.Read()
                jobDays.Add(dr.GetInt32(0))
            End While
        End Using

        Dim startOfMonth As New DateTime(y, m, 1)
        Dim daysInMonth As Integer = DateTime.DaysInMonth(y, m)
        Dim startDayOfWeek As Integer = CInt(startOfMonth.DayOfWeek)

        For i As Integer = 0 To startDayOfWeek - 1
            Dim emptyPanel As New Panel()
            emptyPanel.Size = New Size(10, 10)
            targetPanel.Controls.Add(emptyPanel)
        Next

        For day As Integer = 1 To daysInMonth
            Dim dayTile As New ucCalendarDay()
            Dim currentDate As New DateTime(y, m, day)
            Dim hasJob As Boolean = jobDays.Contains(day)
            dayTile.SetDay(day, currentDate, hasJob)

            If currentDate.Date = DateTime.Now.Date Then dayTile.BackColor = Color.LightBlue
            If currentDate.Date = dtpViewDate.Value.Date Then dayTile.BackColor = Color.LightGray

            AddHandler dayTile.DayClicked, AddressOf OnDayTileClicked
            targetPanel.Controls.Add(dayTile)
        Next

        targetPanel.ResumeLayout()
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

    Private Sub OnDayTileClicked(selectedDate As Date)
        dtpViewDate.Value = selectedDate
        UpdateSelectionVisuals()
    End Sub

    ' ---------------------------------------------------------
    ' 4. MAIN FORM LOGIC
    ' ---------------------------------------------------------
    Private Sub newUcDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FirebaseManager.Initialize()
        LoadTechnicians()
        LoadJobs(DateTime.Now)

        _currentMonth = DateTime.Now.Month
        _currentYear = DateTime.Now.Year
        LoadCalendars()

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
            Dim cmd As New MySqlCommand("SELECT UserID, FullName, FirebaseUID FROM tbl_users WHERE Role='Technician' AND Status='Active'", conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            cmbTechnician.DataSource = dt
            cmbTechnician.DisplayMember = "FullName"
            cmbTechnician.ValueMember = "UserID"
        End Using
    End Sub

    Private Sub LoadJobs(viewDate As Date)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' UPDATED SQL:
                ' 1. Use CONCAT_WS for Address
                ' 2. Use COALESCE(S.ServiceName, S_Job.ServiceName) to find the service name
                '    from either the Contract OR the Job (for inspections).
                Dim sql As String = "SELECT " &
                                    "   J.JobID, " &
                                    "   C.ClientName, " &
                                    "   CONCAT_WS(', ', C.StreetAddress, C.Barangay, C.City) AS Address, " &
                                    "   COALESCE(S.ServiceName, S_Job.ServiceName) AS ServiceName, " &
                                    "   J.JobType, " &
                                    "   J.VisitNumber, " &
                                    "   J.Status, " &
                                    "   T.FullName AS AssignedTech " &
                                    "FROM tbl_joborders J " &
                                    "LEFT JOIN tbl_contracts Con ON J.ContractID = Con.ContractID " &
                                    "LEFT JOIN tbl_clients C ON (Con.ClientID = C.ClientID OR J.ClientID_TempLink = C.ClientID) " &
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
                dgvDailyJobs.DefaultCellStyle.WrapMode = DataGridViewTriState.True

                Dim colsToHide() As String = {"JobID", "Address", "ServiceName", "VisitNumber", "AssignedTech"}
                For Each colName As String In colsToHide
                    If dgvDailyJobs.Columns(colName) IsNot Nothing Then
                        dgvDailyJobs.Columns(colName).Visible = False
                    End If
                Next

                If dgvDailyJobs.Columns("ClientName") IsNot Nothing Then dgvDailyJobs.Columns("ClientName").Width = 150
                If dgvDailyJobs.Columns("JobType") IsNot Nothing Then dgvDailyJobs.Columns("JobType").Width = 100
                If dgvDailyJobs.Columns("Status") IsNot Nothing Then dgvDailyJobs.Columns("Status").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                ClearDetails()
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
        UpdateSelectionVisuals()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadJobs(dtpViewDate.Value)
    End Sub

    Private Sub dgvDailyJobs_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDailyJobs.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvDailyJobs.Rows(e.RowIndex)

            _selectedJobID = Convert.ToInt32(row.Cells("JobID").Value)

            lblDetailClient.Text = row.Cells("ClientName").Value.ToString()
            lblDetailAddress.Text = row.Cells("Address").Value.ToString()

            ' Now shows "General Pest Control" even for Inspections
            If IsDBNull(row.Cells("ServiceName").Value) Then
                lblDetailService.Text = "Not Specified"
            Else
                lblDetailService.Text = row.Cells("ServiceName").Value.ToString()
            End If

            If IsDBNull(row.Cells("AssignedTech").Value) Then
                lblDetailTech.Text = "Unassigned"
            Else
                lblDetailTech.Text = row.Cells("AssignedTech").Value.ToString()
            End If

            lblDetailVisit.Text = row.Cells("VisitNumber").Value.ToString()
        End If
    End Sub

    Private Sub ClearDetails()
        lblDetailClient.Text = "---"
        lblDetailAddress.Text = "---"
        lblDetailService.Text = "---"
        lblDetailTech.Text = "---"
        lblDetailVisit.Text = "---"
        _selectedJobID = 0
    End Sub

    Private Async Sub btnAssignJob_Click(sender As Object, e As EventArgs) Handles btnAssignJob.Click
        If _selectedJobID = 0 Or cmbTechnician.SelectedIndex = -1 Then
            MessageBox.Show("Please select a job and a technician.")
            Exit Sub
        End If

        Dim techID As Integer = Convert.ToInt32(cmbTechnician.SelectedValue)
        Dim drv As DataRowView = CType(cmbTechnician.SelectedItem, DataRowView)

        Dim techFirebaseUID As String = ""
        If Not IsDBNull(drv("FirebaseUID")) Then
            techFirebaseUID = drv("FirebaseUID").ToString()
        End If

        Dim selectedRow As DataGridViewRow = dgvDailyJobs.CurrentRow
        Dim clientName As String = selectedRow.Cells("ClientName").Value.ToString()
        Dim address As String = selectedRow.Cells("Address").Value.ToString()

        ' Use the visible label instead of hidden column to ensure we get the data
        Dim serviceName As String = lblDetailService.Text

        Dim visitDate As Date = dtpViewDate.Value
        Dim jobType As String = selectedRow.Cells("JobType").Value.ToString()

        ' We don't have ServiceID directly in the grid for Inspections easily, 
        ' but that's okay, mobile only strictly needs the Name for display.
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

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            btnAssignJob.Enabled = True
            btnAssignJob.Text = "ASSIGN TECH"
        End Try
    End Sub

    ' --- EMPTY HANDLERS ---
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
    End Sub
    Private Sub SplitContainer1_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer1.Panel2.Paint
    End Sub
    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
    End Sub
    Private Sub flpCalendar_Paint(sender As Object, e As PaintEventArgs) Handles flpCalendar1.Paint
    End Sub
    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
    End Sub
    Private Sub lblDetailTech_Click(sender As Object, e As EventArgs) Handles lblDetailTech.Click
    End Sub
    Private Sub lblDetailVisit_Click(sender As Object, e As EventArgs) Handles lblDetailVisit.Click
    End Sub
    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
    End Sub
    Private Sub lblDetailClient_Click(sender As Object, e As EventArgs) Handles lblDetailClient.Click
    End Sub
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
    End Sub
End Class