Public Class AccAndSettings_SuperAdmin

    Private Sub ManAcc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ManAcc.LinkClicked
        ' Show the default instance of ManageAccounts
        ManageAccounts.Show()

        ' Hide the current form instance (Me)
        Me.Hide()
    End Sub

    Private Sub PageLabel1_Load(sender As Object, e As EventArgs) Handles PageLabel1.Load

    End Sub
End Class