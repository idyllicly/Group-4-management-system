Public Class SelectTechnicianCard

    Public Event GoBackToCards()
    Public Event AssignmentComplete(ByVal action As String)

    ' ----------------------------------------------------------------------
    ' 1. PUBLIC SUB TO RECEIVE DATA (Restored)
    ' ----------------------------------------------------------------------

    ' This method needs to reference the Public Structure in SelectTechPage
    Public Sub LoadProfileData(ByVal name As String, ByVal profile As SelectTechPage.TechnicianProfile)

        Dim loremIpsum As String = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."

        ' *** IMPORTANT: These control names must match your designer ***

        If Me.TechName IsNot Nothing Then
            Me.TechName.Text = name
        End If

        ' Assuming TxtDescription is the large text area based on image_37ba7b
        Me.TechDesc.Text = $"Expertise: {profile.Background}{vbCrLf}{vbCrLf}{loremIpsum}"

        ' Assuming these labels display the actual contact values
        Me.TechNumber.Text = profile.ContactNo
        Me.TechEmail.Text = profile.Email
        Me.TechFB.Text = profile.Facebook
        Me.TechViber.Text = profile.Viber

    End Sub

    ' ----------------------------------------------------------------------
    ' 2. HANDLER FOR THE "ASSIGN AND SUBMIT" BUTTON (MessageBox with Choice)
    ' ----------------------------------------------------------------------


    ' ----------------------------------------------------------------------
    ' 3. BACK BUTTON HANDLER
    ' ----------------------------------------------------------------------

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        RaiseEvent GoBackToCards()
        Me.Dispose()
    End Sub

    ' ----------------------------------------------------------------------
    ' 4. OTHER HANDLERS
    ' ----------------------------------------------------------------------

    Private Sub TechPicture_Click(sender As Object, e As EventArgs) Handles TechPicture.Click

    End Sub

    Private Sub OvalButton1_Click(sender As Object, e As EventArgs) Handles OvalButton1.Click
        Dim assignedTechName As String = "Technician"
        If Me.TechName IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.TechName.Text) Then
            assignedTechName = Me.TechName.Text
        End If

        ' Display the pop-up message box with YES (View Details) and NO (Homepage) buttons
        Dim result As DialogResult = MessageBox.Show(
            $"Technician {assignedTechName} is Notified!{vbCrLf}{vbCrLf}You are being assigned to a job!!{vbCrLf}{vbCrLf}Click YES to View Job Details or NO to return to the Homepage.",
            "Technician Assignment Complete!",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information)

        Me.Dispose()

        ' Handle navigation based on user's choice
        If result = DialogResult.Yes Then
            RaiseEvent AssignmentComplete("ViewDetails")
        Else
            RaiseEvent AssignmentComplete("Homepage")
        End If

    End Sub
End Class