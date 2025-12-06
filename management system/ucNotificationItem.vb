Public Class ucNotificationItem

    ' === 1. DEFINE CUSTOM EVENTS ===
    ' These act like signals to the main form
    Public Event NotificationSelected(sender As Object, e As EventArgs)
    Public Event DismissClicked(sender As Object, e As EventArgs)

    ' === 2. PROPERTIES ===
    Public Property NotifID As Integer
    Public Property RelatedID As Integer
    Public Property Category As String

    ' === 3. CONSTRUCTOR ===
    Public Sub New(id As Integer, title As String, msg As String, cat As String, related As Integer, time As Date, isRead As Boolean)
        InitializeComponent()

        ' Save Data
        Me.NotifID = id
        Me.Category = cat
        Me.RelatedID = related

        ' Set Text
        lblTitle.Text = title
        lblMessage.Text = msg
        lblTime.Text = time.ToString("MMM dd, hh:mm tt")

        ' Color Coding
        Select Case cat
            Case "Billing"
                pnlColorStrip.BackColor = Color.OrangeRed
                lblTitle.ForeColor = Color.OrangeRed
            Case "Job Update"
                pnlColorStrip.BackColor = Color.SeaGreen
                lblTitle.ForeColor = Color.SeaGreen
            Case "Contract"
                pnlColorStrip.BackColor = Color.Goldenrod
                lblTitle.ForeColor = Color.Goldenrod
            Case Else
                pnlColorStrip.BackColor = Color.Gray
        End Select

        ' Read vs Unread
        If isRead Then
            Me.BackColor = Color.WhiteSmoke
        Else
            Me.BackColor = Color.White
        End If

        ' === WIRE UP CLICK EVENTS ===
        ' If user clicks the text, the background, or the strip, we raise the Selected event
        AddHandler lblTitle.Click, AddressOf TriggerSelection
        AddHandler lblMessage.Click, AddressOf TriggerSelection
        AddHandler lblTime.Click, AddressOf TriggerSelection
        AddHandler pnlColorStrip.Click, AddressOf TriggerSelection
        AddHandler Me.Click, AddressOf TriggerSelection

        ' Wire up the Dismiss Button (Make sure your button is named btnDismiss in Designer)
        AddHandler btnDismiss.Click, AddressOf TriggerDismiss
    End Sub

    ' === 4. EVENT TRIGGERS ===

    ' When the card is clicked, tell the parent form
    Private Sub TriggerSelection(sender As Object, e As EventArgs)
        RaiseEvent NotificationSelected(Me, EventArgs.Empty)
    End Sub

    ' When the dismiss button is clicked, tell the parent form
    Private Sub TriggerDismiss(sender As Object, e As EventArgs)
        ' Stop the click from passing through to the card background
        Dim btn As Control = CType(sender, Control)
        btn.Capture = False

        RaiseEvent DismissClicked(Me, EventArgs.Empty)
    End Sub

End Class