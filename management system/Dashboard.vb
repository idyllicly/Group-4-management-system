Imports MySql.Data.MySqlClient

Public Class Dashboard
    ' Connection Instance
    Dim db As New DatabaseConnection()

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDashboardData()
    End Sub

    Public Sub LoadDashboardData()
        ' 1. Clear existing cards to prevent duplicates
        FlowLayoutPanel1.Controls.Clear()
        FlowLayoutPanel2.Controls.Clear()
        FlowLayoutPanel3.Controls.Clear()
        FlowLayoutPanel4.Controls.Clear()

        ' 2. Fetch ALL Jobs with Client and Service info
        Dim sql As String = "SELECT j.JobID, j.JobStatus, j.ScheduleDate, s.ServiceInclusion, c.CFirstName, c.CLastName " &
                            "FROM tbl_job j " &
                            "JOIN tbl_client c ON j.ClientID = c.ClientID " &
                            "JOIN tbl_service s ON j.ServiceID = s.ServiceID " &
                            "ORDER BY j.ScheduleDate ASC"

        Dim dt As DataTable = db.ExecuteSelect(sql)

        ' 3. Loop through rows and create cards
        For Each row As DataRow In dt.Rows
            Dim card As New InquiryCard()

            ' Set Data
            card.JobID = Convert.ToInt32(row("JobID"))
            card.CustomerName = row("CFirstName").ToString() & " " & row("CLastName").ToString()
            card.InquiryDetails = row("ServiceInclusion").ToString()

            ' Format Date
            Dim dateVal As DateTime
            If DateTime.TryParse(row("ScheduleDate").ToString(), dateVal) Then
                card.DateDate = dateVal.ToString("MMM dd, yyyy")
            End If

            ' 4. Sort into Columns based on Status
            Dim status As String = row("JobStatus").ToString().ToLower().Trim()

            Select Case status
                Case "active"
                    ' Column 1: Jobs In Progress
                    card.SetColor(Color.LightYellow)
                    AddCardToPanel(FlowLayoutPanel1, card)

                Case "follow up"
                    ' Column 2: Follow-up Jobs (ONLY Follow Up goes here now)
                    card.SetColor(Color.AliceBlue)
                    AddCardToPanel(FlowLayoutPanel2, card)

                Case "completed"
                    ' Column 3: Completed
                    card.SetColor(Color.Honeydew)
                    AddCardToPanel(FlowLayoutPanel3, card)

                Case "cancelled", "denied"
                    ' Column 4: Cancelled/Rejected
                    card.SetColor(Color.MistyRose)
                    AddCardToPanel(FlowLayoutPanel4, card)

                ' You might want to handle other statuses like 'pending' or 'assigned' elsewhere 
                ' or creating a new column for them if they shouldn't be in Follow Up.
                ' For now, I'll put them in Column 1 (In Progress) as a fallback, 
                ' or you can just ignore them by not adding the card.
                Case "pending", "assigned (pending)", "assigned (accepted)"
                    card.SetColor(Color.LightGray)
                    AddCardToPanel(FlowLayoutPanel1, card) ' Moving these to "In Progress" column for now.

            End Select
        Next
    End Sub

    ' Helper to add card and fix width
    Private Sub AddCardToPanel(pnl As FlowLayoutPanel, card As InquiryCard)
        ' Calculate width: Panel Width - Scrollbar - Margin
        card.Width = pnl.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10
        card.Margin = New Padding(3, 3, 3, 10)
        pnl.Controls.Add(card)
    End Sub

    ' Handle Resize to keep cards responsive
    Private Sub FlowLayoutPanels_Layout(sender As Object, e As LayoutEventArgs) Handles _
        FlowLayoutPanel1.Layout, FlowLayoutPanel2.Layout, FlowLayoutPanel3.Layout, FlowLayoutPanel4.Layout

        Dim pnl As FlowLayoutPanel = DirectCast(sender, FlowLayoutPanel)
        pnl.SuspendLayout()

        Dim newWidth As Integer = pnl.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10

        For Each ctrl As Control In pnl.Controls
            If ctrl.Width <> newWidth Then ctrl.Width = newWidth
        Next

        pnl.ResumeLayout()
    End Sub

    ' --- Keep your other UI event handlers below if needed ---
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblTitle.Click
    End Sub
End Class