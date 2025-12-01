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
End Class