Imports MySql.Data.MySqlClient

Public Class newUcInquiryManager

    ' ⚠️ CHECK YOUR CONNECTION STRING
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' Variables to hold selected data
    Private _selectedClientID As Integer = 0
    Private _selectedClientName As String = ""
    Private _selectedClientAddress As String = ""

    Private Sub newUcInquiryManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadClients("")
        LoadInspectors()
        FirebaseManager.Initialize() ' Ensure cloud is ready
    End Sub

    ' ==========================================
    ' 1. LOAD DATA
    ' ==========================================
    Private Sub LoadClients(search As String)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim sql As String = "SELECT ClientID, ClientName, Address, ContactNumber FROM tbl_Clients"
                If search <> "" Then sql &= " WHERE ClientName LIKE @s OR Address LIKE @s"

                Dim cmd As New MySqlCommand(sql, conn)
                If search <> "" Then cmd.Parameters.AddWithValue("@s", "%" & search & "%")

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvClients.DataSource = dt
                ' Hide ID but keep it available
                If dgvClients.Columns("ClientID") IsNot Nothing Then dgvClients.Columns("ClientID").Visible = False
            Catch ex As Exception
                MessageBox.Show("Error loading clients: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadClients(txtSearch.Text)
    End Sub

    ' Need to load FirebaseUID to sync immediately
    Private Sub LoadInspectors()
        Using conn As New MySqlConnection(connString)
            ' Fetch FirebaseUID so we can send notification immediately
            Dim cmd As New MySqlCommand("SELECT TechnicianID, CONCAT(FirstName, ' ', LastName) AS FullName, FirebaseUID FROM tbl_Technicians WHERE Status='Active'", conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            cmbInspector.DataSource = dt
            cmbInspector.DisplayMember = "FullName"
            cmbInspector.ValueMember = "TechnicianID"
        End Using
    End Sub

    ' ==========================================
    ' 2. HANDLE SELECTION
    ' ==========================================
    Private Sub dgvClients_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClients.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvClients.Rows(e.RowIndex)

            _selectedClientID = Convert.ToInt32(row.Cells("ClientID").Value)
            _selectedClientName = row.Cells("ClientName").Value.ToString()
            _selectedClientAddress = row.Cells("Address").Value.ToString()

            lblClientName.Text = _selectedClientName
            lblClientName.ForeColor = Color.DarkGreen
        End If
    End Sub

    ' ==========================================
    ' 3. THE FIX: DISPATCH & SYNC
    ' ==========================================
    Private Async Sub btnDispatch_Click(sender As Object, e As EventArgs) Handles btnDispatch.Click

        ' A. Validation
        If _selectedClientID = 0 Then
            MessageBox.Show("Please select a client first.")
            Exit Sub
        End If
        If cmbInspector.SelectedIndex = -1 Then
            MessageBox.Show("Please select an inspector.")
            Exit Sub
        End If

        ' B. Gather Data
        Dim inspectorID As Integer = Convert.ToInt32(cmbInspector.SelectedValue)

        ' Get Firebase UID from the hidden column in dropdown (or DataRow)
        Dim drv As DataRowView = CType(cmbInspector.SelectedItem, DataRowView)
        Dim inspectorFirebaseUID As String = drv("FirebaseUID").ToString()

        Dim visitDate As Date = dtpInspectDate.Value.Date
        Dim visitTime As TimeSpan = dtpInspectDate.Value.TimeOfDay

        btnDispatch.Enabled = False
        btnDispatch.Text = "Scheduling..."

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim trans As MySqlTransaction = conn.BeginTransaction()

            Try
                ' 1. INSERT INTO SQL (Fixed NULLs)
                ' We specifically fill ClientID_TempLink so the dashboard knows who this is
                Dim sql As String = "INSERT INTO tbl_JobOrders " &
                                    "(ContractID, ClientID_TempLink, TechnicianID, VisitNumber, ScheduledDate, StartTime, Status, JobType) " &
                                    "VALUES (NULL, @clientID, @techID, 1, @date, @time, 'Assigned', 'Inspection'); " &
                                    "SELECT LAST_INSERT_ID();"

                Dim cmd As New MySqlCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@clientID", _selectedClientID)
                cmd.Parameters.AddWithValue("@techID", inspectorID)
                cmd.Parameters.AddWithValue("@date", visitDate)
                cmd.Parameters.AddWithValue("@time", visitTime)

                ' Get the new ID
                Dim newJobID As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' 2. SYNC TO FIREBASE (Immediate Dispatch)
                If inspectorFirebaseUID <> "" Then
                    Await FirebaseManager.DispatchJobToMobile(newJobID, _selectedClientName, _selectedClientAddress, "Ocular Inspection", visitDate, inspectorFirebaseUID)
                End If

                ' 3. COMMIT
                trans.Commit()

                MessageBox.Show("Ocular Inspection Scheduled & Sent to Inspector's App!")

                ' Reset UI
                lblClientName.Text = "---"
                _selectedClientID = 0
                btnDispatch.Text = "SCHEDULE OCULAR"
                btnDispatch.Enabled = True

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error: " & ex.Message)
                btnDispatch.Enabled = True
                btnDispatch.Text = "SCHEDULE OCULAR"
            End Try
        End Using
    End Sub

    Private Sub dgvClientList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClients.CellContentClick

    End Sub
End Class