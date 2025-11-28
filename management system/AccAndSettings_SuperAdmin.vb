Public Class AccAndSettings_SuperAdmin

    Private Sub ManAcc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ManAcc.LinkClicked
        ' Show the default instance of ManageAccounts
        ManageAccounts.Show()

        ' Hide the current form instance (Me)
        Me.Hide()
    End Sub

    Private Sub PageLabel1_Load(sender As Object, e As EventArgs) Handles PageLabel1.Load

    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim result As MsgBoxResult = MsgBox("Are you sure you want to log out?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Logout")

        If result = MsgBoxResult.Yes Then
            ' Show the LoginForm
            log_in.Show()
            ' Hide the current form
            Me.Hide()
        End If
    End Sub
End Class