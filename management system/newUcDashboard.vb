Imports MySql.Data.MySqlClient

Public Class newUcDashboard

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedJobID As Integer = 0

    Private Sub newUcDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTechnicians()
        LoadJobs(DateTime.Now) ' Load today by default
    End Sub

    ' === 1. LOAD TECHNICIANS FOR DROPDOWN ===
    Private Sub LoadTechnicians()
        Using conn As New MySqlConnection(connString)
            ' Added FirebaseUID to the select statement
            Dim cmd As New MySqlCommand("SELECT TechnicianID, CONCAT(FirstName, ' ', LastName) AS FullName, FirebaseUID FROM tbl_Technicians WHERE Status='Active'", conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            cmbTechnician.DataSource = dt
            cmbTechnician.DisplayMember = "FullName"
            cmbTechnician.ValueMember = "TechnicianID"
        End Using
    End Sub

    ' === 2. LOAD JOBS FOR THE SELECTED DATE ===
    Private Sub LoadJobs(viewDate As Date)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' JOIN QUERY: Connects JobOrders -> Contracts -> Clients -> Services
                ' This creates a readable list for the Admin
                Dim sql As String = "SELECT " &
                                    "   J.JobID, " &
                                    "   C.ClientName, " &
                                    "   C.Address, " &
                                    "   S.ServiceName, " &
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

                ' Note: The OR J.ClientID_TempLink logic handles the Inquiry jobs we made earlier

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@date", viewDate.Date)

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvDailyJobs.DataSource = dt
                dgvDailyJobs.Columns("JobID").Visible = False ' Hide ID

                ' Optional: Color code rows
                ColorCodeRows()

            Catch ex As Exception
                MessageBox.Show("Error loading jobs: " & ex.Message)
            End Try
        End Using
    End Sub

    ' === 3. COLOR CODING LOGIC ===
    Private Sub ColorCodeRows()
        For Each row As DataGridViewRow In dgvDailyJobs.Rows
            Dim status As String = row.Cells("Status").Value.ToString()
            Dim type As String = row.Cells("JobType").Value.ToString()

            If status = "Completed" Then
                row.DefaultCellStyle.BackColor = Color.LightGreen
            ElseIf type = "Inspection" Then
                row.DefaultCellStyle.BackColor = Color.LightYellow ' Highlight Inquiries
            End If
        Next
    End Sub

    ' === 4. DATE CHANGED ===
    Private Sub dtpViewDate_ValueChanged(sender As Object, e As EventArgs) Handles dtpViewDate.ValueChanged
        LoadJobs(dtpViewDate.Value)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadJobs(dtpViewDate.Value)
    End Sub

    ' === 5. SELECT A JOB ===
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

    ' === 6. ASSIGN TECHNICIAN ===
    Private Async Sub btnAssignJob_Click(sender As Object, e As EventArgs) Handles btnAssignJob.Click
        ' 1. Validation
        If _selectedJobID = 0 Then
            MessageBox.Show("Please select a job from the list.")
            Exit Sub
        End If
        If cmbTechnician.SelectedIndex = -1 Then
            MessageBox.Show("Please select a technician.")
            Exit Sub
        End If

        ' 2. Get Data for Firebase
        Dim techID As Integer = Convert.ToInt32(cmbTechnician.SelectedValue)

        ' We need the Technician's Firebase UID.
        ' (Make sure your LoadTechnicians() gets the FirebaseUID column too!)
        Dim drv As DataRowView = CType(cmbTechnician.SelectedItem, DataRowView)
        Dim techFirebaseUID As String = drv("FirebaseUID").ToString()

        ' Get Job Details from the selected row in the grid
        Dim selectedRow As DataGridViewRow = dgvDailyJobs.CurrentRow
        Dim clientName As String = selectedRow.Cells("ClientName").Value.ToString()
        Dim address As String = selectedRow.Cells("Address").Value.ToString()
        Dim serviceName As String = selectedRow.Cells("ServiceName").Value.ToString()
        Dim visitDate As Date = dtpViewDate.Value

        btnAssignJob.Enabled = False
        btnAssignJob.Text = "Syncing..."

        Try
            ' 3. UPDATE LOCAL MYSQL
            Using conn As New MySqlConnection(connString)
                conn.Open()
                Dim sql As String = "UPDATE tbl_JobOrders SET TechnicianID = @tid, Status = 'Assigned' WHERE JobID = @jid"
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@tid", techID)
                cmd.Parameters.AddWithValue("@jid", _selectedJobID)
                cmd.ExecuteNonQuery()
            End Using

            ' 4. PUSH TO FIREBASE (The Magic Step)
            If techFirebaseUID <> "" Then
                Await FirebaseManager.DispatchJobToMobile(_selectedJobID, clientName, address, serviceName, visitDate, techFirebaseUID)
            Else
                MessageBox.Show("Warning: This technician has no Mobile Account (FirebaseUID). Job saved locally only.")
            End If

            MessageBox.Show("Job Dispatched Successfully!")
            LoadJobs(dtpViewDate.Value) ' Refresh Grid

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            btnAssignJob.Enabled = True
            btnAssignJob.Text = "ASSIGN TECH"
        End Try
    End Sub

End Class