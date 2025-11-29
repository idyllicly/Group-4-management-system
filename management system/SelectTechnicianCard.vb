Imports MySql.Data.MySqlClient

Public Class SelectTechnicianCard
    Dim db As New DatabaseConnection()

    Public Event GoBackToCards()
    Public Event AssignmentComplete(ByVal action As String)

    ' Properties to hold IDs
    Public Property SelectedTechID As Integer = 0
    Public Property TargetJobID As Integer = 0

    ' ----------------------------------------------------------------------
    ' 1. RECEIVE DATA
    ' ----------------------------------------------------------------------
    Public Sub LoadProfileData(ByVal name As String, ByVal profile As SelectTechPage.TechnicianProfile)
        ' Store the ID so we can use it later
        Me.SelectedTechID = profile.ID

        ' Populate UI
        If Me.TechName IsNot Nothing Then
            Me.TechName.Text = name
        End If

        Me.TechDesc.Text = $"Specialization: {profile.Background}" & vbCrLf & "Ready for assignment."
        Me.TechNumber.Text = profile.ContactNo
        Me.TechEmail.Text = profile.Email
        Me.TechFB.Text = profile.Facebook
        Me.TechViber.Text = profile.Viber
    End Sub

    ' ----------------------------------------------------------------------
    ' 2. ASSIGN AND SUBMIT BUTTON (Now Functional)
    ' ----------------------------------------------------------------------
    Private Sub OvalButton1_Click(sender As Object, e As EventArgs) Handles OvalButton1.Click

        ' 1. Validation
        If TargetJobID = 0 Then
            MessageBox.Show("Error: No Job Selected to Assign.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If SelectedTechID = 0 Then
            MessageBox.Show("Error: Invalid Technician Selected.", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' 2. Database Update
        ' We update the Job to set the TechnicianID and change status to 'Assigned (Pending)'
        Dim sql As String = "UPDATE tbl_job SET TechnicianID = @techId, JobStatus = 'Assigned (Pending)' WHERE JobID = @jobId"
        Dim params As New Dictionary(Of String, Object)
        params.Add("@techId", SelectedTechID)
        params.Add("@jobId", TargetJobID)

        Dim rows As Integer = db.ExecuteAction(sql, params)

        If rows > 0 Then
            ' 3. Success UI
            Dim assignedTechName As String = If(Me.TechName IsNot Nothing, Me.TechName.Text, "Technician")

            Dim result As DialogResult = MessageBox.Show(
                $"Job Assigned Successfully!{vbCrLf}{vbCrLf}Technician {assignedTechName} has been notified.{vbCrLf}Status changed to 'Assigned (Pending)'.",
                "Assignment Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)

            ' Close and return
            RaiseEvent AssignmentComplete("Homepage")
        Else
            MessageBox.Show("Failed to assign job. Please try again.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub

    ' ----------------------------------------------------------------------
    ' 3. NAVIGATION
    ' ----------------------------------------------------------------------
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RaiseEvent GoBackToCards()
        Me.Dispose()
    End Sub

    Private Sub TechPicture_Click(sender As Object, e As EventArgs) Handles TechPicture.Click
    End Sub
End Class