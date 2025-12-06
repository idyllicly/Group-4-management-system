Public Class newUcPageHeader

    Private Sub newUcPageHeader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Run system checks (contracts/billing) once on startup
        NotificationService.RunSystemChecks()

        ' Start Timer
        timerNotifCheck.Interval = 5000 ' 5 seconds
        timerNotifCheck.Start()
        CheckNotifications()
    End Sub

    Private Sub timerNotifCheck_Tick(sender As Object, e As EventArgs) Handles timerNotifCheck.Tick
        CheckNotifications()
    End Sub

    Private Sub CheckNotifications()
        Try
            Dim count As Integer = NotificationService.GetUnreadCount()
            If count > 0 Then
                lblBadge.Visible = True
                lblBadge.Text = If(count > 9, "9+", count.ToString())
            Else
                lblBadge.Visible = False
            End If
        Catch ex As Exception
            ' Silent fail
        End Try
    End Sub

    Private Sub btnBell_Click(sender As Object, e As EventArgs) Handles btnBell.Click
        Dim mainForm As frm_Main = TryCast(Me.ParentForm, frm_Main)
        If mainForm IsNot Nothing Then
            mainForm.LoadPage(New newUcNotifications(), "Notifications Center")
            lblBadge.Visible = False
        End If
    End Sub
End Class