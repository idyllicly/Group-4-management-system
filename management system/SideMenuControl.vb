Public Class SideMenuControl
    ' --- ADD THIS CODE ---
    ' This forces double buffering on the User Control itself
    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000 ' Turn on WS_EX_COMPOSITED
            Return cp
        End Get
    End Property

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

    End Sub

    Private Sub SideMenuControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
