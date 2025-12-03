Imports MySql.Data.MySqlClient

Public Class newUcInquiryManager

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedClientID As Integer = 0
    Private _selectedClientName As String = ""
    Private _selectedClientAddress As String = ""

    Private Sub newUcInquiryManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FirebaseManager.Initialize()
        LoadClients("")
        LoadInspectors()
        LoadServices()
        FirebaseManager.ListenForJobUpdates()
    End Sub

    ' === LOAD CLIENTS ===
    Private Sub LoadClients(search As String)
        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim sql As String = "SELECT ClientID, ClientName, " &
                                "CONCAT_WS(', ', StreetAddress, Barangay, City) AS FullAddress, " &
                                "ContactNumber FROM tbl_clients"

            If search <> "" Then
                sql &= " WHERE ClientName LIKE @s OR City LIKE @s OR Barangay LIKE @s"
            End If

            Dim cmd As New MySqlCommand(sql, conn)
            If search <> "" Then cmd.Parameters.AddWithValue("@s", "%" & search & "%")

            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            dgvClients.DataSource = dt
            If dgvClients.Columns("ClientID") IsNot Nothing Then dgvClients.Columns("ClientID").Visible = False
        End Using
    End Sub

    ' === LOAD INSPECTORS ===
    Private Sub LoadInspectors()
        Using conn As New MySqlConnection(connString)
            Dim cmd As New MySqlCommand("SELECT UserID, FullName, FirebaseUID FROM tbl_users WHERE Role='Technician' AND Status='Active'", conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            cmbInspector.DataSource = dt
            cmbInspector.DisplayMember = "FullName"
            cmbInspector.ValueMember = "UserID"
        End Using
    End Sub

    ' === LOAD SERVICES ===
    Private Sub LoadServices()
        Using conn As New MySqlConnection(connString)
            Dim cmd As New MySqlCommand("SELECT ServiceID, ServiceName FROM tbl_services", conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            cmbService.DataSource = dt
            cmbService.DisplayMember = "ServiceName"
            cmbService.ValueMember = "ServiceID"
        End Using
    End Sub

    ' === GRID CLICK ===
    Private Sub dgvClients_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClients.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvClients.Rows(e.RowIndex)
            _selectedClientID = Convert.ToInt32(row.Cells("ClientID").Value)
            _selectedClientName = row.Cells("ClientName").Value.ToString()
            _selectedClientAddress = row.Cells("FullAddress").Value.ToString()

            lblClientName.Text = _selectedClientName
            lblClientName.ForeColor = Color.DarkGreen
        End If
    End Sub

    ' === DISPATCH BUTTON (Updated to Save ServiceID) ===
    Private Async Sub btnDispatch_Click(sender As Object, e As EventArgs) Handles btnDispatch.Click
        If _selectedClientID = 0 Or cmbInspector.SelectedIndex = -1 Then
            MessageBox.Show("Please select client and inspector.")
            Exit Sub
        End If
        If cmbService.SelectedIndex = -1 Then
            MessageBox.Show("Please select a Target Service Package.")
            Exit Sub
        End If

        Dim inspectorID As Integer = Convert.ToInt32(cmbInspector.SelectedValue)
        Dim drv As DataRowView = CType(cmbInspector.SelectedItem, DataRowView)
        Dim inspectorFirebaseUID As String = ""
        If Not IsDBNull(drv("FirebaseUID")) Then inspectorFirebaseUID = drv("FirebaseUID").ToString()

        Dim serviceID As Integer = Convert.ToInt32(cmbService.SelectedValue)
        Dim serviceName As String = cmbService.Text & " (Inspection)"

        Dim visitDate As Date = dtpInspectDate.Value.Date
        Dim visitTime As TimeSpan = dtpInspectDate.Value.TimeOfDay

        btnDispatch.Enabled = False
        btnDispatch.Text = "Scheduling..."

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim trans As MySqlTransaction = conn.BeginTransaction()

            Try
                ' UPDATED SQL: Now inserts @svcID into the new ServiceID column
                Dim sql As String = "INSERT INTO tbl_joborders " &
                                    "(ContractID, ClientID_TempLink, TechnicianID, ServiceID, VisitNumber, ScheduledDate, StartTime, Status, JobType) " &
                                    "VALUES (NULL, @clientID, @techID, @svcID, 1, @date, @time, 'Assigned', 'Inspection');" &
                                    "SELECT LAST_INSERT_ID();"

                Dim cmd As New MySqlCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@clientID", _selectedClientID)
                cmd.Parameters.AddWithValue("@techID", inspectorID)
                cmd.Parameters.AddWithValue("@svcID", serviceID)
                cmd.Parameters.AddWithValue("@date", visitDate)
                cmd.Parameters.AddWithValue("@time", visitTime)

                Dim newJobID As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                If inspectorFirebaseUID <> "" Then
                    Await FirebaseManager.DispatchJobToMobile(newJobID, _selectedClientName, _selectedClientAddress, serviceName, visitDate, inspectorFirebaseUID, "Inspection", serviceID)
                End If

                trans.Commit()
                MessageBox.Show("Ocular Inspection Scheduled!")

                lblClientName.Text = "---"
                _selectedClientID = 0
                btnDispatch.Enabled = True
                btnDispatch.Text = "SCHEDULE OCULAR"

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error: " & ex.Message)
                btnDispatch.Enabled = True
                btnDispatch.Text = "SCHEDULE OCULAR"
            End Try
        End Using
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadClients(txtSearch.Text)
    End Sub
End Class