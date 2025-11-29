Imports MySql.Data.MySqlClient

Public Class SelectTechPage
    ' Database Connection
    Dim db As New DatabaseConnection()

    ' Property to know which Job we are assigning
    Public Property TargetJobID As Integer = 0

    ' ----------------------------------------------------------------------
    ' 1. INLINE DATA STRUCTURE (Updated with ID)
    ' ----------------------------------------------------------------------
    Public Structure TechnicianProfile
        Public ID As Integer ' Added ID
        Public ReadOnly Name As String
        Public ReadOnly Background As String
        Public ReadOnly ContactNo As String
        Public ReadOnly Email As String
        Public ReadOnly Facebook As String
        Public ReadOnly Viber As String

        Public Sub New(id As Integer, name As String, bg As String, no As String, mail As String, fb As String, vb As String)
            Me.ID = id
            Me.Name = name
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

    ' ----------------------------------------------------------------------
    ' 2. FORM LOAD (Fetch from DB)
    ' ----------------------------------------------------------------------

    Private Sub SelectTechPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTechnicianCards()
    End Sub

    Private Sub LoadTechnicianCards()
        FormHostPanel.Controls.Clear()
        TechnicianData.Clear()

        ' 1. Fetch Technicians from Database
        Dim sql As String = "SELECT * FROM tbl_technician"
        Dim dt As DataTable = db.ExecuteSelect(sql)

        Dim cardCount As Integer = 0
        Dim cardWidth As Integer = 0
        Dim cardHeight As Integer = 0
        Dim CENTER_OFFSET_X As Integer = 0

        ' 2. Loop through DB rows
        For Each row As DataRow In dt.Rows
            Dim id As Integer = Convert.ToInt32(row("TechnicianID"))
            Dim fullName As String = row("TFirstName").ToString() & " " & row("TLastName").ToString()
            Dim specialization As String = row("Specialization").ToString()
            Dim contact As String = row("TechnicianNo").ToString()

            ' Note: Email/FB/Viber are not in tbl_technician, using placeholders or you can join tbl_account if linked
            Dim profile As New TechnicianProfile(id, fullName, specialization, contact, "N/A", "N/A", "N/A")

            ' Store in dictionary (using Name as key for the event system)
            If Not TechnicianData.ContainsKey(fullName) Then
                TechnicianData.Add(fullName, profile)
            End If

            ' 3. Create Card
            Dim card As New TechnicianCard()

            ' Calculate Layout (Same as before)
            If cardCount = 0 Then
                cardWidth = card.Width
                cardHeight = card.Height
                Dim totalRowWidth As Integer = (cardWidth * CARDS_PER_ROW) + (CARD_MARGIN * (CARDS_PER_ROW + 1))
                CENTER_OFFSET_X = (FormHostPanel.Width - totalRowWidth) \ 2
                If CENTER_OFFSET_X < 0 Then CENTER_OFFSET_X = 0
            End If

            AddHandler card.CardClicked, AddressOf TechnicianCard1_CardClicked
            card.SetTechnicianName(fullName)
            ' Assuming SetTechnicianName sets the label text. 
            ' Ideally, TechnicianCard should have a Public Property ID to avoid string lookup, 
            ' but we'll stick to your existing Name-based pattern for now.

            Dim currentXPosition As Integer = CENTER_OFFSET_X + CARD_MARGIN + ((cardWidth + CARD_MARGIN) * (cardCount Mod CARDS_PER_ROW))
            Dim currentYPosition As Integer = CARD_MARGIN + ((cardHeight + CARD_MARGIN) * (cardCount \ CARDS_PER_ROW))

            card.Location = New Point(currentXPosition, currentYPosition)
            FormHostPanel.Controls.Add(card)
            cardCount += 1
        Next

        FormHostPanel.PerformLayout()
    End Sub

    ' ----------------------------------------------------------------------
    ' 3. VIEW SWITCHING (Pass Data)
    ' ----------------------------------------------------------------------

    Private Sub TechnicianCard1_CardClicked(technicianName As String)
        If Not TechnicianData.ContainsKey(technicianName) Then
            MessageBox.Show($"Error: Data not found for {technicianName}.", "Data Error")
            Return
        End If

        Dim profile As TechnicianProfile = TechnicianData(technicianName)

        Dim SelectForm As New SelectTechnicianCard()

        ' Pass the Job ID we are working on
        SelectForm.TargetJobID = Me.TargetJobID

        AddHandler SelectForm.GoBackToCards, AddressOf LoadTechnicianCards
        AddHandler SelectForm.AssignmentComplete, AddressOf HandleAssignmentNavigation

        FormHostPanel.Controls.Clear()
        SelectForm.TopLevel = False
        SelectForm.FormBorderStyle = FormBorderStyle.None
        SelectForm.Dock = DockStyle.Fill

        SelectForm.LoadProfileData(technicianName, profile)

        FormHostPanel.Controls.Add(SelectForm)
        SelectForm.Show()
    End Sub

    ' ----------------------------------------------------------------------
    ' 4. NAVIGATION HANDLER
    ' ----------------------------------------------------------------------

    Private Sub HandleAssignmentNavigation(ByVal action As String)
        If action = "Homepage" Then
            Me.Close() ' Close the selection page entirely
        ElseIf action = "ViewDetails" Then
            ' Logic to go back to job details could go here
            Me.Close()
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint
    End Sub
End Class