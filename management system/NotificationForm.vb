Imports System.Windows.Forms
Imports System.Drawing
Imports System.Linq ' Required for FirstOrDefault and Count/Linq operations

Public Class NotificationForm

    ' Reference to the SideNavControl to update the notification badge
    Private ReadOnly _sideNav As SideNavControl

    ''' <summary>
    ''' Constructor that accepts the SideNavControl instance for badge synchronization.
    ''' NOTE: You must also call InitializeComponent() in the actual code for this form.
    ''' </summary>
    Public Sub New(ByVal sideNavInstance As SideNavControl)
        ' Ensure the designer-generated code runs first
        ' InitializeComponent() 
        _sideNav = sideNavInstance
    End Sub

    ' Parameterless constructor for designer compatibility (optional but recommended)
    Public Sub New()
        ' InitializeComponent()
    End Sub

    ' This method runs when the form opens
    Private Sub NotificationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Notifications"
        Me.BackColor = Color.WhiteSmoke ' clean background

        ' 1. Find the FlowLayoutPanel (container)
        Dim flowPanel As Control = Me.Controls.Find("flowPanelNotifications", True).FirstOrDefault()

        ' Error handling if the panel is missing
        If flowPanel Is Nothing OrElse Not TypeOf flowPanel Is FlowLayoutPanel Then
            MessageBox.Show("Error: FlowLayoutPanel named 'flowPanelNotifications' not found.", "System Error")
            Exit Sub
        End If

        Dim finalPanel As FlowLayoutPanel = DirectCast(flowPanel, FlowLayoutPanel)

        ' 2. Clear old notifications
        finalPanel.Controls.Clear()

        ' 3. Load the data
        LoadNotifications(finalPanel)

        ' 4. SYNCHRONIZE BADGE: Since the user is viewing the notifications, clear the badge counter on the SideNavControl.
        If _sideNav IsNot Nothing Then
            _sideNav.ResetNotificationCount()
        End If

    End Sub

    ''' <summary>
    ''' Populates the container with NotifCard controls.
    ''' </summary>
    Private Sub LoadNotifications(ByVal container As FlowLayoutPanel)

        ' ==========================================================================================
        ' PART A: SIMULATED DATA (Replace this block with your Database Code later)
        ' ==========================================================================================
        Dim dummyData As New List(Of (Title As String, ActionDate As DateTime, JobStatus As String, OverallStatus As String, IsRead As Boolean)) From {
            ("Inquiry #1005: Termite Control", DateTime.Now, "Pending", "Pending", False),
            ("Inquiry #1004: General Cleaning", DateTime.Now.AddHours(-2), "Accepted", "In Progress", False),
            ("Inquiry #1003: Aircon Maintenance", DateTime.Now.AddDays(-1), "Rejected", "Follow Up", True),
            ("Inquiry #1002: Plumbing Repair", DateTime.Now.AddDays(-2), "Finished", "Finished", True),
            ("Inquiry #1001: Electrical Wiring", DateTime.Now.AddDays(-5), "Assigned", "Pending", True)
        }

        ' Loop through the dummy data to create cards
        For Each row In dummyData
            AddCardToPanel(container, row.Title, row.ActionDate, row.JobStatus, row.OverallStatus, row.IsRead)
        Next
        ' ==========================================================================================


        ' ==========================================================================================
        ' PART B: DATABASE INTEGRATION EXAMPLE (Uncomment and adapt this when connecting to SQL)
        ' ==========================================================================================
        ' Dim dt As DataTable = YourDatabaseClass.GetNotifications(CurrentUserId) 
        ' 
        ' For Each row As DataRow In dt.Rows
        '     Dim title As String = "Inquiry #" & row("InquiryID").ToString() & ": " & row("ServiceType").ToString()
        '     Dim actDate As DateTime = Convert.ToDateTime(row("DateCreated"))
        '     Dim jStatus As String = row("JobAssignmentStatus").ToString() ' e.g., "Accepted"
        '     Dim oStatus As String = row("OverallStatus").ToString()       ' e.g., "In Progress"
        '     Dim read As Boolean = Convert.ToBoolean(row("IsRead"))
        '
        '     AddCardToPanel(container, title, actDate, jStatus, oStatus, read)
        ' Next
        ' ==========================================================================================
    End Sub

    ''' <summary>
    ''' Helper method to create a card, set its properties, and add it to the flow panel.
    ''' </summary>
    Private Sub AddCardToPanel(ByVal panel As FlowLayoutPanel, title As String, dateVal As DateTime, jobStat As String, overallStat As String, readStat As Boolean)

        Dim card As New NotifCard()

        ' 1. Map the data to the User Control Properties
        card.NotificationTitle = title
        card.RecentActionDate = dateVal
        card.JobAssignmentStatus = jobStat
        card.OverallStatus = overallStat
        card.IsRead = readStat

        ' 2. LAYOUT FIX: Calculate width to avoid horizontal scrollbars
        ' We subtract roughly 25px to account for the vertical scrollbar width and margins
        Dim scrollBarWidth As Integer = If(panel.VerticalScroll.Visible, SystemInformation.VerticalScrollBarWidth, 0)
        card.Width = panel.ClientSize.Width - scrollBarWidth - 10

        ' Optional: Add a small margin between cards
        card.Margin = New Padding(3, 3, 3, 5)

        ' 3. Add to container
        panel.Controls.Add(card)

    End Sub


End Class