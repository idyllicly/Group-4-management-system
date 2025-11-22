Public Class SelectTechPage

    ' ----------------------------------------------------------------------
    ' 1. INLINE DATA STRUCTURE AND DEFINITIONS (Required for LoadProfileData)
    ' ----------------------------------------------------------------------

    ' Structure must be PUBLIC so SelectTechnicianCard can access it.
    Public Structure TechnicianProfile
        Public ReadOnly Background As String
        Public ReadOnly ContactNo As String
        Public ReadOnly Email As String
        Public ReadOnly Facebook As String
        Public ReadOnly Viber As String

        Public Sub New(bg As String, no As String, mail As String, fb As String, vb As String)
            Me.Background = bg
            Me.ContactNo = no
            Me.Email = mail
            Me.Facebook = fb
            Me.Viber = vb
        End Sub
    End Structure

    Private ReadOnly TechnicianData As New Dictionary(Of String, TechnicianProfile)

    ' Define positioning constants
    Private Const CARDS_PER_ROW As Integer = 4
    Private Const CARD_MARGIN As Integer = 40

    Private ReadOnly technicianNames() As String = {"John Smith", "Sarah Connor", "Mike Ross", "Lisa Green", "Tom Baker", "Amy Wong"}

    ' ----------------------------------------------------------------------
    ' 2. DATA INITIALIZATION (Required for data lookup)
    ' ----------------------------------------------------------------------

    Private Sub InitializeTechnicianData()
        If TechnicianData.Count > 0 Then Return

        TechnicianData.Add("John Smith", New TechnicianProfile(
            "Rat Infestation & Rodent Control. Focuses on complex rodent exclusion using Integrated Pest Management (IPM).",
            "(555) 101-2001", "john.s@rrcpestcontrol.com", "/JohnSmithPestControl", "+63 917 111 2222"))

        TechnicianData.Add("Sarah Connor", New TechnicianProfile(
            "Termite Exterminator & Wood Preservation. Specialized in subterranean and drywood termite identification and advanced baiting systems.",
            "(555) 303-4003", "sarah.c@rrcpestcontrol.com", "/SarahConnorRRC", "+63 917 888 2222"))

        TechnicianData.Add("Mike Ross", New TechnicianProfile(
            "Cockroach Annihilator & Sanitation Expert. Rapid response for severe infestations in commercial settings, focusing on harborage elimination.",
            "(555) 505-6005", "mike.r@rrcpestcontrol.com", "/MikeRossPestSolutions", "+63 917 333 4444"))

        TechnicianData.Add("Lisa Green", New TechnicianProfile(
            "Bedbugs Exterminator & Heat Treatment. Certified in discreet residential bed bug eradication using thermal remediation (heat) and chemicals.",
            "(555) 707-8007", "lisa.g@rrcpestcontrol.com", "/LisaGreenBedBugs", "+63 917 555 6666"))

        TechnicianData.Add("Tom Baker", New TechnicianProfile(
            "General Insect Pest Control (Ants, Spiders). Focuses on preventative, recurring maintenance and control of common household insects.",
            "(555) 909-0009", "tom.b@rrcpestcontrol.com", "/TomBakerIPM", "+63 917 777 8888"))

        TechnicianData.Add("Amy Wong", New TechnicianProfile(
            "Wildlife & Nuisance Control. Focuses on the humane removal and exclusion of larger pests (squirrels, raccoons) and sealing entry points.",
            "(555) 212-3132", "amy.w@rrcpestcontrol.com", "/AmyWongExclusion", "+63 917 999 0000"))
    End Sub

    ' ----------------------------------------------------------------------
    ' 3. FORM LOAD AND CARD GENERATION (Standard Logic)
    ' ----------------------------------------------------------------------

    Private Sub SelectTechPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeTechnicianData()
        LoadTechnicianCards()
    End Sub

    Private Sub LoadTechnicianCards()
        FormHostPanel.Controls.Clear()

        Dim cardCount As Integer = 0
        Dim cardWidth As Integer = 0
        Dim cardHeight As Integer = 0
        Dim CENTER_OFFSET_X As Integer = 0

        For Each techName As String In technicianNames
            Dim card As New TechnicianCard()
            ' ... (Card positioning and event linking) ...
            If cardCount = 0 Then
                cardWidth = card.Width
                cardHeight = card.Height

                Dim totalRowWidth As Integer = (cardWidth * CARDS_PER_ROW) + (CARD_MARGIN * (CARDS_PER_ROW + 1))
                CENTER_OFFSET_X = (FormHostPanel.Width - totalRowWidth) \ 2

                If CENTER_OFFSET_X < 0 Then
                    CENTER_OFFSET_X = 0
                End If
            End If

            AddHandler card.CardClicked, AddressOf TechnicianCard1_CardClicked
            card.SetTechnicianName(techName)

            Dim currentXPosition As Integer = CENTER_OFFSET_X + CARD_MARGIN + ((cardWidth + CARD_MARGIN) * (cardCount Mod CARDS_PER_ROW))
            Dim currentYPosition As Integer = CARD_MARGIN + ((cardHeight + CARD_MARGIN) * (cardCount \ CARDS_PER_ROW))

            card.Location = New Point(currentXPosition, currentYPosition)
            FormHostPanel.Controls.Add(card)
            cardCount += 1
        Next

        FormHostPanel.PerformLayout()
    End Sub

    ' ----------------------------------------------------------------------
    ' 4. VIEW SWITCHING AND DATA PASSING HANDLER (FIXED)
    ' ----------------------------------------------------------------------

    Private Sub TechnicianCard1_CardClicked(technicianName As String) Handles TechnicianCard1.CardClicked

        If Not TechnicianData.ContainsKey(technicianName) Then
            MessageBox.Show($"Error: Data not found for {technicianName}.", "Data Error")
            Return
        End If

        Dim profile As TechnicianProfile = TechnicianData(technicianName)

        Dim SelectForm As New SelectTechnicianCard()

        ' Link GoBack event
        AddHandler SelectForm.GoBackToCards, AddressOf LoadTechnicianCards

        ' Link AssignmentComplete event
        AddHandler SelectForm.AssignmentComplete, AddressOf HandleAssignmentNavigation

        FormHostPanel.Controls.Clear()

        SelectForm.TopLevel = False
        SelectForm.FormBorderStyle = FormBorderStyle.None
        SelectForm.Dock = DockStyle.Fill

        ' *** FIX: Re-insert the call to LoadProfileData to set the details ***
        SelectForm.LoadProfileData(technicianName, profile)

        FormHostPanel.Controls.Add(SelectForm)
        SelectForm.Show()

    End Sub

    ' ----------------------------------------------------------------------
    ' 5. NAVIGATION HANDLER
    ' ----------------------------------------------------------------------

    Private Sub HandleAssignmentNavigation(ByVal action As String)
        FormHostPanel.Controls.Clear()

        If action = "Homepage" Then
            LoadTechnicianCards()
        ElseIf action = "ViewDetails" Then
            MessageBox.Show("Placeholder: Loading the specific Job Details form now.", "Job Details")
            LoadTechnicianCards()
        End If
    End Sub

End Class