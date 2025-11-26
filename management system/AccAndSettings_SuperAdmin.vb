Public Class AccAndSettings_SuperAdmin

    Private Sub ManAcc_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ManAcc.LinkClicked
        ' Show the default instance of ManageAccounts
        ManageAccounts.Show()

        ' Hide the current form instance (Me)
        Me.Hide()
    End Sub

End Class