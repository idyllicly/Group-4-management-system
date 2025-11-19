Public Class SelectTechnicianPage
    Private Sub BtnOpen_Click(sender As Object, e As EventArgs)
        Dim childForn As New SelectTechnicianCard()
        childForn.MdiParent = Me
        childForn.Show()
    End Sub

    Private Sub OpenChildInPanel(SelectTechnicianCard As Form)

        Panel2.Controls.Clear()
        SelectTechnicianCard.TopLevel = False
        SelectTechnicianCard.FormBorderStyle = FormBorderStyle.None
        SelectTechnicianCard.Dock = DockStyle.Fill

        Panel2.Controls.Add(SelectTechnicianCard)
        Panel2.Tag = SelectTechnicianCard
        SelectTechnicianCard.Show()



    End Sub

    Private Sub SelectTechnicianPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim TechCard As New TechnicianCard()
        TechCard.LblName.Text = "Pondavilla, Vince P."

        AddHandler TechCard.CardClicked, AddressOf OnTechnicianSelected

    End Sub

    Private Sub OnTechnicianSelected(TechName As String)

        Dim detailForm As New SelectTechnicianCard()

        OpenChildInPanel(detailForm)
    End Sub

    Private Sub TechnicianCard1_Click(sender As Object, e As EventArgs) Handles TechnicianCard1.Click

        Dim ChildForm As New SelectTechnicianCard()

        ChildForm.MdiParent = Me

        ChildForm.Location = New Point(212, 147)

        ChildForm.Show()

    End Sub



End Class