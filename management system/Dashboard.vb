Public Class Dashboard
    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblTitle.Click

    End Sub

    Private Sub tlpColumns_Paint(sender As Object, e As PaintEventArgs) Handles tlpColumns.Paint

    End Sub

    Private Sub RoundedPanel1_Paint(sender As Object, e As PaintEventArgs) Handles RoundedPanel1.Paint

    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub

    Private Sub Dashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load the test data when the form starts
        LoadSampleData()
    End Sub

    Private Sub LoadSampleData()
        ' --- COLUMN 1: Jobs In Progress (FlowLayoutPanel1) - Generate 8 cards ---
        For i As Integer = 1 To 8
            AddCardToColumn(FlowLayoutPanel1, "Client #" & i, "Termite Inspection Phase 1", "Nov " & i & ", 2025", Color.LightYellow)
        Next

        ' --- COLUMN 2: Follow-up Jobs (FlowLayoutPanel2) - Generate 5 cards ---
        For i As Integer = 1 To 5
            AddCardToColumn(FlowLayoutPanel2, "Restaurant Branch " & i, "Monthly Rodent Check", "Nov " & (10 + i) & ", 2025", Color.AliceBlue)
        Next

        ' --- COLUMN 3: Completed Jobs (FlowLayoutPanel3) - Generate 20 cards (This will definitely SCROLL) ---
        For i As Integer = 1 To 20
            ' Alternating services just to make it look realistic
            Dim service As String = If(i Mod 2 = 0, "General Disinfection", "Ant Control Treatment")
            AddCardToColumn(FlowLayoutPanel3, "Completed Client " & i, service, "Oct " & i & ", 2025", Color.Honeydew)
        Next

        ' --- COLUMN 4: Cancelled (FlowLayoutPanel4) - Generate 10 cards ---
        For i As Integer = 1 To 10
            AddCardToColumn(FlowLayoutPanel4, "Lead " & i, "Quote Rejected", "Nov " & i & ", 2025", Color.MistyRose)
        Next
    End Sub

    ' This helper sub creates the card and puts it in the right panel
    Private Sub AddCardToColumn(targetPanel As FlowLayoutPanel, name As String, details As String, dateStr As String, bgColor As Color)

        ' 1. Create a new instance of your UserControl
        Dim card As New InquiryCard()

        ' 2. Set the data
        card.CustomerName = name
        card.InquiryDetails = details
        card.DateDate = dateStr
        card.BackColor = bgColor ' Optional coloring

        ' 3. Set margins so cards don't stick together
        card.Margin = New Padding(5)

        ' 4. Add it to the specific FlowLayoutPanel
        targetPanel.Controls.Add(card)

        ' --- THIS IS THE NEW PART FOR WIDTH ---
        ' Set the width to the panel's width minus a little space for the scrollbar
        ' SystemInformation.VerticalScrollBarWidth is usually around 17-20 pixels
        card.Width = targetPanel.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10

        ' Set the height (optional, if you want them taller/shorter)
        card.Height = 100

        ' Add margin for spacing between cards
        card.Margin = New Padding(3, 3, 3, 10) ' Left, Top, Right, Bottom

        ' Add to panel
        targetPanel.Controls.Add(card)

    End Sub

    Private Sub FlowLayoutPanels_Layout(sender As Object, e As LayoutEventArgs) Handles _
        FlowLayoutPanel1.Layout,
        FlowLayoutPanel2.Layout,
        FlowLayoutPanel3.Layout,
        FlowLayoutPanel4.Layout

        Dim pnl As FlowLayoutPanel = DirectCast(sender, FlowLayoutPanel)

        ' Temporarily stop drawing to prevent flickering
        pnl.SuspendLayout()

        ' Calculate the new width: Panel Width - ScrollBar - Safety Buffer
        Dim newWidth As Integer = pnl.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10

        ' Loop through every card and update the width
        For Each ctrl As Control In pnl.Controls
            If ctrl.Width <> newWidth Then
                ctrl.Width = newWidth
            End If
        Next

        pnl.ResumeLayout()
    End Sub

End Class