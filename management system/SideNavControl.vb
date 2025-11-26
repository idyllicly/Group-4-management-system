Public Class SideNavControl

    ' --- NOTIFICATION STATE TRACKER ---
    Private HasNotification As Boolean = False
    ' Assuming PictureBox_Indicator is a Public/Friend control on this UserControl/Form 
    ' Or that you have another way to reference it from here.

    ' --- HELPER FUNCTION FOR NAVIGATION ---
    ' This method handles CREATING a new instance of the next form
    ' and HIDING the current parent form.
    Private Sub NavigateTo(ByVal formType As Type)
        ' ... (existing code for NavigateTo) ...
        If Me.ParentForm IsNot Nothing Then
            Me.ParentForm.Hide()
        End If

        Dim nextForm As Form = CType(Activator.CreateInstance(formType), Form)
        nextForm.Show()
    End Sub

    ' --- NOTIFICATION HELPER FUNCTION ---
    ' Call this function from your application's logic when a new notification arrives.
    Public Sub NewNotificationArrived()
        HasNotification = True
        ' ... (existing code for NewNotificationArrived) ...
    End Sub


    ' --- EVENT HANDLERS ---

    Private Sub AccSettings_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles AccSettings.LinkClicked
        ' Pass the TYPE of the form, and a new instance will be created.
        NavigateTo(GetType(AccAndSettings_SuperAdmin))
    End Sub

    ' ... other links ...

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        ' Links to InquiryPage (Pass the TYPE of the form)
        NavigateTo(GetType(InquiryPage))
    End Sub

    ' ====================================================================
    ' ✉️ NOTIFICATION ENVELOPE CLICK HANDLER (PictureBox6)
    ' ====================================================================
    Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
        If HasNotification Then

            ' 1. Hide the indicator and reset state (Mark as 'read')
            HasNotification = False
            ' Assuming PictureBox_Indicator is accessible:
            ' If Me.PictureBox_Indicator IsNot Nothing Then
            '     Me.PictureBox_Indicator.Visible = False
            ' End If

            ' 2. Navigate to the dedicated Notification Page/Form
            ' NOTE: Replace NotificationForm with the actual name of your notification form.
            NavigateTo(GetType(NotificationForm))

        Else
            ' No new notification, but still navigate (or just show a message)
            MessageBox.Show("No new notifications.", "Notification")

        End If
    End Sub

    ' ====================================================================
    ' 🖥️ SYSTEM TRAY ICON DOUBLE-CLICK HANDLER (NotifyIcon1)
    ' ====================================================================
    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseDoubleClick

        ' 1. Check if the parent form exists
        If Me.ParentForm IsNot Nothing Then

            ' 2. Show the main application window (restores it from a minimized or hidden state)
            Me.ParentForm.Show()

            ' 3. Bring the form to the front
            Me.ParentForm.WindowState = FormWindowState.Maximized
            Me.ParentForm.Activate()

            ' 4. Hide the NotifyIcon once the main form is back (optional, but standard practice)
            NotifyIcon1.Visible = False

        Else
            MessageBox.Show("Main application form could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Class