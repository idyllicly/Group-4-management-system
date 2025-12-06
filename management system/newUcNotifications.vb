Imports MySql.Data.MySqlClient

Public Class newUcNotifications

    ' Database Connection
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' ==========================================
    ' 1. INITIALIZATION & LOAD
    ' ==========================================
    Private Sub newUcNotifications_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Default: Unchecked (Show only Unseen/Inbox)
        chkShowHistory.Checked = False

        ' Load data
        LoadNotifications()
    End Sub

    ' Reload when the "Show History" checkbox is toggled
    Private Sub chkShowHistory_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowHistory.CheckedChanged
        LoadNotifications()
    End Sub

    ' ==========================================
    ' 2. MAIN LOADER FUNCTION
    ' ==========================================
    Private Sub LoadNotifications()
        ' 1. Clear existing items to prevent duplicates
        flowPanel.Controls.Clear()

        Using conn As New MySqlConnection(connString)
            conn.Open()

            ' 2. Determine Logic: Inbox (Unread) vs History (Read)
            Dim sql As String = ""

            If chkShowHistory.Checked Then
                ' HISTORY MODE: Show Read items (IsRead = 1)
                sql = "SELECT * FROM tbl_notifications WHERE IsRead = 1 ORDER BY DateCreated DESC LIMIT 50"
            Else
                ' INBOX MODE: Show Unread items (IsRead = 0)
                sql = "SELECT * FROM tbl_notifications WHERE IsRead = 0 ORDER BY DateCreated DESC LIMIT 50"
            End If

            Dim cmd As New MySqlCommand(sql, conn)
            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            ' 3. Handle Empty State
            If Not reader.HasRows Then
                Dim lbl As New Label()
                lbl.Text = If(chkShowHistory.Checked, "No history found.", "You're all caught up!" & vbCrLf & "No new notifications.")
                lbl.Font = New Font("Segoe UI", 12)
                lbl.ForeColor = Color.Gray
                lbl.AutoSize = True
                lbl.Margin = New Padding(20)
                flowPanel.Controls.Add(lbl)
            End If

            ' 4. Loop through database rows and create cards
            While reader.Read()
                Dim id As Integer = Convert.ToInt32(reader("NotifID"))
                Dim title As String = reader("Title").ToString()
                Dim msg As String = reader("Message").ToString()
                Dim cat As String = reader("Category").ToString()
                Dim related As Integer = Convert.ToInt32(reader("RelatedID"))
                Dim d As Date = Convert.ToDateTime(reader("DateCreated"))
                Dim read As Boolean = Convert.ToBoolean(reader("IsRead"))

                ' Create the User Control (The Card)
                Dim card As New ucNotificationItem(id, title, msg, cat, related, d, read)

                ' Set Width dynamically (Panel Width - Scrollbar - Margin)
                Dim scrollWidth As Integer = SystemInformation.VerticalScrollBarWidth
                card.Width = flowPanel.ClientSize.Width - scrollWidth - 10
                card.Margin = New Padding(0, 0, 0, 5)

                ' WIRE UP EVENTS (This fixes the Freeze/Crash issue)
                AddHandler card.NotificationSelected, AddressOf OnNotificationClicked
                AddHandler card.DismissClicked, AddressOf OnDismissClicked

                flowPanel.Controls.Add(card)
            End While
        End Using
    End Sub

    ' ==========================================
    ' 3. EVENT HANDLER: CLICKING THE CARD
    ' ==========================================
    Private Sub OnNotificationClicked(sender As Object, e As EventArgs)
        Dim card As ucNotificationItem = CType(sender, ucNotificationItem)
        Dim mainForm As frm_Main = CType(Application.OpenForms("frm_Main"), frm_Main)

        If mainForm Is Nothing Then Return

        ' A. Mark as Read in Database
        NotificationService.MarkAsRead(card.NotifID)

        ' B. If in Inbox mode, remove it immediately (since it's now read)
        If chkShowHistory.Checked = False Then
            flowPanel.Controls.Remove(card)
            card.Dispose()
        End If

        ' C. Intelligent Navigation (Deep Linking)
        Select Case card.Category

            Case "Job Update"
                ' Logic: Find the date of the job -> Open Dashboard -> Go to that date
                Dim jobDate As Date = DateTime.Now
                Using conn As New MySqlConnection(connString)
                    conn.Open()
                    Dim cmd As New MySqlCommand("SELECT ScheduledDate FROM tbl_joborders WHERE JobID = @jid", conn)
                    cmd.Parameters.AddWithValue("@jid", card.RelatedID)
                    Dim res = cmd.ExecuteScalar()
                    If res IsNot Nothing Then jobDate = Convert.ToDateTime(res)
                End Using

                Dim dash As New newUcDashboard()
                dash.PresetDate = jobDate
                mainForm.LoadPage(dash, "Daily Operations Dashboard")

            Case "Billing"
                ' Logic: Open Billing -> Select the Contract
                Dim billPage As New newUcBilling()
                billPage.PresetContractID = card.RelatedID
                mainForm.LoadPage(billPage, "Billing & Collections")

            Case "Contract"
                ' Logic: Open Contracts -> Search for the Contract
                Dim conPage As New newUcContractManager()
                conPage.PresetSearchID = card.RelatedID
                mainForm.LoadPage(conPage, "Contract Manager")

        End Select
    End Sub

    ' ==========================================
    ' 4. EVENT HANDLER: DISMISS BUTTON
    ' ==========================================
    Private Sub OnDismissClicked(sender As Object, e As EventArgs)
        Dim card As ucNotificationItem = CType(sender, ucNotificationItem)

        ' A. Mark as Read in Database
        NotificationService.MarkAsRead(card.NotifID)

        ' B. Remove from screen visually
        flowPanel.Controls.Remove(card)
        card.Dispose()
    End Sub

    ' ==========================================
    ' 5. AUTO-RESIZE LOGIC (Responsive UI)
    ' ==========================================
    Private Sub newUcNotifications_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        ' Calculate new width (Container width minus scrollbar)
        Dim newWidth As Integer = flowPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10

        ' Safety check for very small windows
        If newWidth < 100 Then newWidth = 100

        flowPanel.SuspendLayout() ' Pause drawing

        ' Resize every card in the list
        For Each ctrl As Control In flowPanel.Controls
            If TypeOf ctrl Is ucNotificationItem Then
                ctrl.Width = newWidth
            End If
        Next

        flowPanel.ResumeLayout() ' Resume drawing
    End Sub

End Class