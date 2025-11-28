Imports System.Drawing
Imports System.Windows.Forms
Imports System.Linq

''' <summary>
''' Represents a single, clickable notification item with detailed inquiry status updates.
''' Assumes labels: lblTitle, lbl_date, lbl_time, lbl_job, lbl_status, CheckBox1, LinkLabel1, 
''' and a panel: pnlStatus are present in the designer.
''' </summary>
Public Class NotifCard

    ' Private fields for notification data
    Private _title As String ' Main notification summary (e.g., "Inquiry #123 created")
    Private _recentActionDate As DateTime
    Private _jobAssignmentStatus As String ' Accepted, Rejected, Pending, Assigned
    Private _overallStatus As String      ' Finished, Follow Up, Pending, In Progress
    Private _isRead As Boolean = False

    ' Public Properties to set the card content

    Public Property NotificationTitle() As String
        Get
            Return _title
        End Get
        Set(value As String)
            _title = value
            ' Assumed main title label is lblTitle (inherited from previous version)
            Dim lblTitleCtrl As Control = Me.Controls.Find("lblTitle", True).FirstOrDefault()
            If Not lblTitleCtrl Is Nothing AndAlso TypeOf lblTitleCtrl Is Label Then
                DirectCast(lblTitleCtrl, Label).Text = value
            End If
        End Set
    End Property

    Public Property RecentActionDate() As DateTime
        Get
            Return _recentActionDate
        End Get
        Set(value As DateTime)
            _recentActionDate = value
            ' Update Date label
            Dim lblDateCtrl As Control = Me.Controls.Find("lbl_date", True).FirstOrDefault()
            If Not lblDateCtrl Is Nothing AndAlso TypeOf lblDateCtrl Is Label Then
                DirectCast(lblDateCtrl, Label).Text = value.ToString("MM/dd/yyyy")
            End If
            ' Update Time label
            Dim lblTimeCtrl As Control = Me.Controls.Find("lbl_time", True).FirstOrDefault()
            If Not lblTimeCtrl Is Nothing AndAlso TypeOf lblTimeCtrl Is Label Then
                DirectCast(lblTimeCtrl, Label).Text = value.ToString("hh:mm tt")
            End If
        End Set
    End Property

    Public Property JobAssignmentStatus() As String
        Get
            Return _jobAssignmentStatus
        End Get
        Set(value As String)
            _jobAssignmentStatus = value
            ' Update Job Assignment label
            Dim lblJobCtrl As Control = Me.Controls.Find("lbl_job", True).FirstOrDefault()
            If Not lblJobCtrl Is Nothing AndAlso TypeOf lblJobCtrl Is Label Then
                DirectCast(lblJobCtrl, Label).Text = value
                SetStatusColor(DirectCast(lblJobCtrl, Label), value)
            End If
        End Set
    End Property

    Public Property OverallStatus() As String
        Get
            Return _overallStatus
        End Get
        Set(value As String)
            _overallStatus = value
            ' Update Status label
            Dim lblStatusCtrl As Control = Me.Controls.Find("lbl_status", True).FirstOrDefault()
            If Not lblStatusCtrl Is Nothing AndAlso TypeOf lblStatusCtrl Is Label Then
                DirectCast(lblStatusCtrl, Label).Text = value
                SetStatusColor(DirectCast(lblStatusCtrl, Label), value)
            End If
        End Set
    End Property

    Public Property IsRead() As Boolean
        Get
            Return _isRead
        End Get
        Set(value As Boolean)
            _isRead = value
            ' Assuming CheckBox1 is used to visually mark as read/unread
            Dim chkBox As Control = Me.Controls.Find("CheckBox1", True).FirstOrDefault()
            If Not chkBox Is Nothing AndAlso TypeOf chkBox Is CheckBox Then
                DirectCast(chkBox, CheckBox).Checked = value
            End If
            UpdateCardStyle()
        End Set
    End Property

    Private Sub NotifCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateCardStyle()
        ' Hook up click events for all controls so the whole card is clickable
        AddHandler Me.Click, AddressOf NotifCard_Click
        For Each ctrl As Control In Me.Controls
            AddHandler ctrl.Click, AddressOf NotifCard_Click
        Next
    End Sub

    ''' <summary>
    ''' Helper to set color based on status for better visibility.
    ''' </summary>
    Private Sub SetStatusColor(ByVal lbl As Label, ByVal status As String)
        Select Case status.ToUpper()
            Case "ACCEPTED", "FINISHED", "APPROVED"
                lbl.ForeColor = Color.ForestGreen
            Case "REJECTED", "CANCELLED"
                lbl.ForeColor = Color.Red
            Case "PENDING", "FOLLOW UP", "ASSIGNED"
                lbl.ForeColor = Color.DarkOrange
            Case Else
                lbl.ForeColor = Color.Black
        End Select
    End Sub

    ''' <summary>
    ''' Updates the visual appearance (e.g., font style, unread indicator) based on the IsRead status.
    ''' </summary>
    Private Sub UpdateCardStyle()
        Dim pnlStatus As Control = Me.Controls.Find("pnlStatus", True).FirstOrDefault()
        Dim lblTitleCtrl As Control = Me.Controls.Find("lblTitle", True).FirstOrDefault()

        If _isRead Then
            ' Read state
            Me.BackColor = Color.FromArgb(245, 245, 245) ' Light gray
            If Not lblTitleCtrl Is Nothing Then DirectCast(lblTitleCtrl, Label).Font = New Font(DirectCast(lblTitleCtrl, Label).Font, FontStyle.Regular)
            If Not pnlStatus Is Nothing Then pnlStatus.BackColor = Color.Transparent
        Else
            ' Unread state
            Me.BackColor = Color.White
            If Not lblTitleCtrl Is Nothing Then DirectCast(lblTitleCtrl, Label).Font = New Font(DirectCast(lblTitleCtrl, Label).Font, FontStyle.Bold)
            If Not pnlStatus Is Nothing Then pnlStatus.BackColor = Color.DodgerBlue
        End If
    End Sub

    ''' <summary>
    ''' Handles the event when the user clicks the card.
    ''' </summary>
    Private Sub NotifCard_Click(sender As Object, e As EventArgs)
        ' This is where you would open the detailed Inquiry or Notification view.
        MessageBox.Show($"Inquiry notification clicked: {Me.NotificationTitle}. Opening detail view.", "View Notification")

        ' Mark the card as read visually and internally
        Me.IsRead = True

        ' IMPORTANT: You should also update the status in your database here.
    End Sub

    ' --- User's specific event handlers (kept for completeness) ---

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        ' When the user manually checks the box, update the IsRead property
        Dim chkBox As CheckBox = DirectCast(sender, CheckBox)
        Me.IsRead = chkBox.Checked
        ' Since IsRead update handles UpdateCardStyle(), no need to call it here.
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ' E.g., Open a link directly to the Inquiry Form
        MessageBox.Show("Link clicked! Navigate to Inquiry ID.", "Action Link")
    End Sub

    ' The following click handlers are usually not needed, as the whole card is clickable via NotifCard_Click, 
    ' but they are left here as stubs based on your request.
    Private Sub lbl_date_Click(sender As Object, e As EventArgs) Handles lbl_date.Click
        ' Same action as clicking the card
        NotifCard_Click(Me, EventArgs.Empty)
    End Sub

    Private Sub lbl_time_Click(sender As Object, e As EventArgs) Handles lbl_time.Click
        ' Same action as clicking the card
        NotifCard_Click(Me, EventArgs.Empty)
    End Sub

    Private Sub lbl_job_Click(sender As Object, e As EventArgs) Handles lbl_job.Click
        ' Same action as clicking the card
        NotifCard_Click(Me, EventArgs.Empty)
    End Sub

    Private Sub lbl_status_Click(sender As Object, e As EventArgs) Handles lbl_status.Click
        ' Same action as clicking the card
        NotifCard_Click(Me, EventArgs.Empty)
    End Sub
End Class