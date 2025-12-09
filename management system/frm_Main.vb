Public Class frm_Main

    ' 1. Declare your controls globally so we can access them anywhere in this form
    Private _sideNav As newUcSideNav
    Private _header As newUcPageHeader

    Private Sub frm_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' === A. SETUP SIDE MENU ===
        ' Pass "Me" to the side nav so it can control this form
        _sideNav = New newUcSideNav(Me)
        _sideNav.Dock = DockStyle.Fill
        pnlSideMenu.Controls.Add(_sideNav)

        ' === B. SETUP HEADER ===
        _header = New newUcPageHeader()
        _header.Dock = DockStyle.Fill
        pnlHeader.Controls.Add(_header)


        LoadPage(New newUcDashboard(), "Daily Operations Dashboard")
    End Sub

    ' === THE NAVIGATION ENGINE ===
    Public Sub LoadPage(ByVal nextPage As UserControl, ByVal pageTitle As String)
        ' 1. Clear the center panel
        pnlContent.Controls.Clear()

        ' 2. Style the new page to fill the space
        nextPage.Dock = DockStyle.Fill

        ' 3. Add the new user control to the panel
        pnlContent.Controls.Add(nextPage)

        ' 4. Update the Header Text automatically
        ' NOTE: Make sure your Label in newUcPageHeader is named 'lblPageTitle' and Modifiers = Public
        _header.lblPageTitle.Text = pageTitle.ToUpper()
    End Sub

    Private Sub frm_Main_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Application.Exit()
    End Sub

    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub pnlContent_Paint(sender As Object, e As PaintEventArgs) Handles pnlContent.Paint

    End Sub

    Private Sub pnlSideMenu_Load(sender As Object, e As EventArgs) Handles pnlSideMenu.Load

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub pnlHeader_Load(sender As Object, e As EventArgs) Handles pnlHeader.Load

    End Sub
End Class