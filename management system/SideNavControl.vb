Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq
Imports System.Reflection ' Required for Activator.CreateInstance

Public Class SideNavControl

    ' ====================================================================
    ' 🔔 NOTIFICATION COUNTER LOGIC
    ' ====================================================================

    ' Private variable to track the notification count
    Private _currentNotificationCount As Integer = 0

    ' Find the badge label control (You MUST have a Label named 'lblNotificationBadge' 
    ' positioned over PictureBox6 in your designer for this to work.)
    Private ReadOnly Property BadgeLabel As Label
        Get
            ' Searches the control collection (including nested controls) for the label name.
            Dim ctrl As Control = Me.Controls.Find("lblNotificationBadge", True).FirstOrDefault()
            Return If(TypeOf ctrl Is Label, DirectCast(ctrl, Label), Nothing)
        End Get
    End Property

    ''' <summary>
    ''' Updates the visual appearance and count of the red badge.
    ''' </summary>
    Private Sub UpdateNotificationBadge()
        Dim badgeLabel = Me.BadgeLabel
        If badgeLabel Is Nothing Then Exit Sub ' Exit if the label doesn't exist

        If _currentNotificationCount > 0 Then
            ' Show the count, limiting to 99+
            badgeLabel.Text = If(_currentNotificationCount > 99, "99+", _currentNotificationCount.ToString())
            badgeLabel.Visible = True

            ' Apply styling to ensure it looks like a red badge
            badgeLabel.BackColor = Color.Red
            badgeLabel.ForeColor = Color.White
            badgeLabel.TextAlign = ContentAlignment.MiddleCenter
            ' You might need to adjust the font and size in the designer
        Else
            badgeLabel.Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Public method to increase the count when a new notification arrives (e.g., from an external listener).
    ''' </summary>
    Public Sub IncrementNotificationCount()
        _currentNotificationCount += 1
        UpdateNotificationBadge()
    End Sub

    ''' <summary>
    ''' Public method to reset the count when notifications are read (called upon navigation).
    ''' </summary>
    Public Sub ResetNotificationCount()
        _currentNotificationCount = 0
        UpdateNotificationBadge()
    End Sub

    ' --- NOTIFICATION STATE TRACKER (Replaced by _currentNotificationCount) ---
    ' Private HasNotification As Boolean = False 

    ' --- HELPER FUNCTION FOR NAVIGATION ---
    ' MODIFIED: To allow passing constructor arguments (e.g., the SideNavControl itself)
    Private Sub NavigateTo(ByVal formType As Type, Optional ByVal constructorArgs As Object() = Nothing)
        ' ... (existing code for NavigateTo) ...
        If Me.ParentForm IsNot Nothing Then
            Me.ParentForm.Hide()
        End If

        ' Use Activator.CreateInstance with arguments if provided
        Dim nextForm As Form
        If constructorArgs Is Nothing Then
            nextForm = CType(Activator.CreateInstance(formType), Form)
        Else
            ' Use the overload that accepts an array of arguments
            nextForm = CType(Activator.CreateInstance(formType, constructorArgs), Form)
        End If
        nextForm.Show()
    End Sub

    ' --- NOTIFICATION HELPER FUNCTION ---
    ' Call this function from your application's logic when a new notification arrives.
    Public Sub NewNotificationArrived()
        ' Now uses the new counting mechanism
        Me.IncrementNotificationCount()
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

        ' When the user clicks the envelope, we navigate to the NotificationForm, 
        ' passing 'Me' (the SideNavControl instance) so the form can call ResetNotificationCount() later.
        NavigateTo(GetType(NotificationForm), New Object() {Me})

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

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ' NOTE: This handler uses hard references (NotificationForm.Show()) which may conflict 
        ' with the NavigateTo helper. Keeping it as provided, but NavigateTo is preferred.
        NotificationForm.Show()

    End Sub

    Private Sub LinkLabel5_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel5.LinkClicked
        FormCalendar.Show()

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dashboard.Show()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        timeline_page.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class