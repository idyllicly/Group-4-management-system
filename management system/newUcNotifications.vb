Imports MySql.Data.MySqlClient

Public Class newUcNotifications

    ' Database Connection
    Private connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

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
    Public Sub LoadNotifications()
        ' 1. Clear existing items to prevent duplicates
        If flowPanel IsNot Nothing Then flowPanel.Controls.Clear()

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' 2. Determine Logic: Inbox (Unread) vs History (Read)
                ' FIXED SQL: Explicitly selecting columns including the new ID columns (JobID, ContractID, PaymentID)
                Dim baseSql As String = "SELECT NotifID, Title, Message, Category, IsRead, DateCreated, JobID, ContractID, PaymentID FROM tbl_notifications "
                Dim whereClause As String = ""

                If chkShowHistory.Checked Then
                    ' HISTORY MODE: Show Read items (IsRead = 1)
                    whereClause = "WHERE IsRead = 1 "
                Else
                    ' INBOX MODE: Show Unread items (IsRead = 0)
                    whereClause = "WHERE IsRead = 0 "
                End If

                Dim sql As String = baseSql & whereClause & "ORDER BY DateCreated DESC LIMIT 50"

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
                    If flowPanel IsNot Nothing Then flowPanel.Controls.Add(lbl)
                End If

                ' 4. Loop through database rows and create cards
                While reader.Read()
                    Dim id As Integer = Convert.ToInt32(reader("NotifID"))
                    Dim title As String = reader("Title").ToString()
                    Dim msg As String = reader("Message").ToString()
                    Dim cat As String = reader("Category").ToString()
                    Dim d As Date = Convert.ToDateTime(reader("DateCreated"))
                    Dim read As Boolean = Convert.ToBoolean(reader("IsRead"))

                    ' FIXED: Intelligent ID Selection
                    ' Since 'RelatedID' column is gone, we check which specific ID is not null
                    Dim related As Integer = 0

                    If Not IsDBNull(reader("JobID")) Then
                        related = Convert.ToInt32(reader("JobID"))
                    ElseIf Not IsDBNull(reader("ContractID")) Then
                        related = Convert.ToInt32(reader("ContractID"))
                    End If
                    ' Note: If it's a Payment notification, it usually has a ContractID attached too.

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

            Catch ex As Exception
                MessageBox.Show("Error loading notifications: " & ex.Message)
            End Try
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
        MarkAsRead(card.NotifID)

        ' B. If in Inbox mode, remove it immediately (since it's now read)
        If chkShowHistory.Checked = False Then
            flowPanel.Controls.Remove(card)
            card.Dispose()
        End If

        ' C. Intelligent Navigation (Deep Linking)
        Try
            Select Case card.Category

                Case "Job Update"
                    ' Logic: Find the date of the job -> Open Dashboard -> Go to that date
                    Dim jobDate As Date = DateTime.Now
                    Using conn As New MySqlConnection(connString)
                        conn.Open()
                        Dim cmd As New MySqlCommand("SELECT ScheduledDate FROM tbl_joborders WHERE JobID = @jid", conn)
                        cmd.Parameters.AddWithValue("@jid", card.RelatedID)
                        Dim res = cmd.ExecuteScalar()
                        If res IsNot Nothing AndAlso Not IsDBNull(res) Then
                            jobDate = Convert.ToDateTime(res)
                        End If
                    End Using

                    Dim dash As New newUcDashboard()
                    dash.PresetDate = jobDate
                    mainForm.LoadPage(dash, "Daily Operations Dashboard")

                Case "Billing", "Payment Due"
                    ' Logic: Open Contracts -> Search for the Contract
                    ' (Billing notifications are now linked to ContractID in the database)
                    Dim conPage As New newUcContractManager()
                    conPage.PresetSearchID = card.RelatedID
                    mainForm.LoadPage(conPage, "Contract Manager")

                Case "Contract", "Contract Expiring"
                    ' Logic: Open Contracts -> Search for the Contract
                    Dim conPage As New newUcContractManager()
                    conPage.PresetSearchID = card.RelatedID
                    mainForm.LoadPage(conPage, "Contract Manager")

            End Select
        Catch ex As Exception
            MessageBox.Show("Could not open details: " & ex.Message)
        End Try
    End Sub

    ' ==========================================
    ' 4. EVENT HANDLER: DISMISS BUTTON
    ' ==========================================
    Private Sub OnDismissClicked(sender As Object, e As EventArgs)
        Dim card As ucNotificationItem = CType(sender, ucNotificationItem)

        ' A. Mark as Read in Database
        MarkAsRead(card.NotifID)

        ' B. Remove from screen visually
        flowPanel.Controls.Remove(card)
        card.Dispose()
    End Sub

    ' ==========================================
    ' 5. HELPER: Mark as Read (Local Version)
    ' ==========================================
    Private Sub MarkAsRead(id As Integer)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim cmd As New MySqlCommand("UPDATE tbl_notifications SET IsRead = 1 WHERE NotifID = @id", conn)
                cmd.Parameters.AddWithValue("@id", id)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                ' Silent fail is okay for reading marks
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 6. AUTO-RESIZE LOGIC (Responsive UI)
    ' ==========================================
    Private Sub newUcNotifications_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If flowPanel Is Nothing Then Return

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