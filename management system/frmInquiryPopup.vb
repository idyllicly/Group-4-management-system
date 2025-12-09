Imports MySql.Data.MySqlClient

Public Class frmInquiryPopup

    ' Properties to accept data from the main manager
    Public Property TargetClientID As Integer = 0
    Public Property TargetClientName As String = ""
    Public Property TargetClientAddress As String = ""

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    Private Sub frmInquiryPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize Firebase (ensure your project has the FirebaseManager module)
        FirebaseManager.Initialize()

        LoadInspectors()
        LoadServices()

        ' Display the client we are working on
        lblSelectedClient.Text = "Scheduling for: " & TargetClientName & vbCrLf & TargetClientAddress
        lblSelectedClient.ForeColor = Color.DarkBlue
    End Sub

    ' === LOAD INSPECTORS (UPDATED FOR DB SCHEMA) ===
    Private Sub LoadInspectors()
        Using conn As New MySqlConnection(connString)
            ' FIXED: tbl_users does not have a "FullName" column. 
            ' We must CONCAT FirstName and LastName to create it for the dropdown display.
            Dim sql As String = "SELECT UserID, CONCAT(FirstName, ' ', LastName) AS FullName, FirebaseUID " &
                                "FROM tbl_users WHERE Role='Technician' AND Status='Active'"

            Dim cmd As New MySqlCommand(sql, conn)
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
            ' This matches your SQL schema perfectly
            Dim cmd As New MySqlCommand("SELECT ServiceID, ServiceName FROM tbl_services", conn)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)

            cmbService.DataSource = dt
            cmbService.DisplayMember = "ServiceName"
            cmbService.ValueMember = "ServiceID"
        End Using
    End Sub

    ' === DISPATCH LOGIC ===
    Private Async Sub btnDispatch_Click(sender As Object, e As EventArgs) Handles btnDispatch.Click
        If TargetClientID = 0 Then
            MessageBox.Show("No client selected.")
            Exit Sub
        End If
        If cmbInspector.SelectedIndex = -1 Or cmbService.SelectedIndex = -1 Then
            MessageBox.Show("Please select an Inspector and a Service.")
            Exit Sub
        End If

        Dim inspectorID As Integer = Convert.ToInt32(cmbInspector.SelectedValue)

        ' Handle DataRowView safely for FirebaseUID
        Dim inspectorFirebaseUID As String = ""
        Dim drv As DataRowView = TryCast(cmbInspector.SelectedItem, DataRowView)
        If drv IsNot Nothing AndAlso Not IsDBNull(drv("FirebaseUID")) Then
            inspectorFirebaseUID = drv("FirebaseUID").ToString()
        End If

        Dim serviceID As Integer = Convert.ToInt32(cmbService.SelectedValue)
        Dim serviceName As String = cmbService.Text & " (Inspection)"

        Dim visitDate As Date = dtpInspectDate.Value.Date
        Dim visitTime As TimeSpan = dtpInspectDate.Value.TimeOfDay

        btnDispatch.Enabled = False
        btnDispatch.Text = "Sending..."

        Using conn As New MySqlConnection(connString)
            Await conn.OpenAsync() ' Changed to Async for smoother UI
            Dim trans As MySqlTransaction = conn.BeginTransaction()

            Try
                ' Create Job Order
                ' FIXED: Changed 'ClientID_TempLink' to 'ClientID' based on tbl_joborders schema
                Dim sql As String = "INSERT INTO tbl_joborders " &
                                    "(ContractID, ClientID, TechnicianID, ServiceID, VisitNumber, ScheduledDate, StartTime, Status, JobType) " &
                                    "VALUES (NULL, @clientID, @techID, @svcID, 1, @date, @time, 'Pending', 'Inspection');" &
                                    "SELECT LAST_INSERT_ID();"

                Dim cmd As New MySqlCommand(sql, conn, trans)
                cmd.Parameters.AddWithValue("@clientID", TargetClientID)
                cmd.Parameters.AddWithValue("@techID", inspectorID)
                cmd.Parameters.AddWithValue("@svcID", serviceID)
                cmd.Parameters.AddWithValue("@date", visitDate)
                cmd.Parameters.AddWithValue("@time", visitTime)

                Dim newJobID As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                ' Firebase Dispatch
                If inspectorFirebaseUID <> "" Then
                    ' Ensure FirebaseManager.DispatchJobToMobile is an Async Task, not Sub
                    Await FirebaseManager.DispatchJobToMobile(newJobID, TargetClientName, TargetClientAddress, serviceName, visitDate, inspectorFirebaseUID, "Inspection", serviceID)
                End If

                trans.Commit()
                MessageBox.Show("Success! Inspection Scheduled.")
                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Error: " & ex.Message)
                btnDispatch.Enabled = True
                btnDispatch.Text = "Confirm Dispatch"
            End Try
        End Using
    End Sub

    Private Sub cmbService_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbService.SelectedIndexChanged

    End Sub
End Class