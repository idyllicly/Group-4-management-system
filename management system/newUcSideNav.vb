Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class newUcSideNav

    ' =========================================================
    '                 1. SETUP & COLORS
    ' =========================================================
    Private _mainForm As frm_Main

    ' --- TEAMMATE REQUEST: LIGHTER BACKGROUND ---
    ' Old Dark Navy was: (17, 16, 29)
    ' New Lighter Blue: (30, 40, 75) -> A professional mid-tone blue
    Private Color_Background As Color = Color.FromArgb(30, 40, 75)

    ' --- HOVER COLOR ---
    ' Since background is lighter, we make hover even brighter (Cornflower/Royal Blue)
    Private Color_Hover As Color = Color.FromArgb(60, 80, 150)

    ' --- TEXT COLORS ---
    Private Color_TextIdle As Color = Color.White
    Private Color_TextHover As Color = Color.White

    Public Sub New(mainForm As frm_Main)
        InitializeComponent()
        _mainForm = mainForm
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    ' =========================================================
    '                 2. AUTO-STYLE & ROUNDING
    ' =========================================================

    Private Sub newUcSideNav_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color_Background

        ' Find the TableLayoutPanel
        Dim tlp As TableLayoutPanel = Me.Controls.OfType(Of TableLayoutPanel)().FirstOrDefault()

        If tlp IsNot Nothing Then
            tlp.BackColor = Color_Background

            ' IMPORTANT: Remove gaps so the two rounded halves touch perfectly
            tlp.Padding = New Padding(0)

            For Each ctrl As Control In tlp.Controls

                ' --- BUTTONS (Right Side of the Pill) ---
                If TypeOf ctrl Is Button Then
                    Dim btn As Button = CType(ctrl, Button)

                    ' Style
                    btn.FlatStyle = FlatStyle.Flat
                    btn.FlatAppearance.BorderSize = 0
                    btn.FlatAppearance.MouseDownBackColor = Color_Hover
                    btn.FlatAppearance.MouseOverBackColor = Color_Hover
                    btn.BackColor = Color_Background
                    btn.ForeColor = Color_TextIdle
                    btn.TextAlign = ContentAlignment.MiddleLeft

                    ' Events
                    AddHandler btn.MouseEnter, AddressOf OnButtonEnter
                    AddHandler btn.MouseLeave, AddressOf OnButtonLeave

                    ' ROUNDING: Round ONLY the Right side
                    ApplyRounding(btn, 25, False, True)
                    AddHandler btn.SizeChanged, Sub(s, ev) ApplyRounding(CType(s, Control), 25, False, True)
                End If

                ' --- ICONS (Left Side of the Pill) ---
                If TypeOf ctrl Is PictureBox Then
                    Dim pic As PictureBox = CType(ctrl, PictureBox)
                    pic.BackColor = Color_Background

                    ' ROUNDING: Round ONLY the Left side
                    ApplyRounding(pic, 25, True, False)
                    AddHandler pic.SizeChanged, Sub(s, ev) ApplyRounding(CType(s, Control), 25, True, False)
                End If
            Next
        End If
    End Sub

    ' --- HELPER: CUT THE CORNERS ---
    Private Sub ApplyRounding(c As Control, radius As Integer, roundLeft As Boolean, roundRight As Boolean)
        Dim path As New GraphicsPath()
        Dim l As Integer = 0
        Dim t As Integer = 0
        Dim w As Integer = c.Width
        Dim h As Integer = c.Height
        Dim r As Integer = radius

        ' Top-Left Corner
        If roundLeft Then path.AddArc(l, t, r, r, 180, 90) Else path.AddLine(l, t, l, t)

        ' Top-Right Corner
        If roundRight Then path.AddArc(w - r, t, r, r, 270, 90) Else path.AddLine(w, t, w, t)

        ' Bottom-Right Corner
        If roundRight Then path.AddArc(w - r, h - r, r, r, 0, 90) Else path.AddLine(w, h, w, h)

        ' Bottom-Left Corner
        If roundLeft Then path.AddArc(l, h - r, r, r, 90, 90) Else path.AddLine(l, h, l, h)

        path.CloseFigure()
        c.Region = New Region(path)
    End Sub

    ' =========================================================
    '                 3. HOVER LOGIC
    ' =========================================================

    Private Sub OnButtonEnter(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        btn.BackColor = Color_Hover
        btn.ForeColor = Color_TextHover

        Dim icon As PictureBox = FindSiblingIcon(btn)
        If icon IsNot Nothing Then icon.BackColor = Color_Hover
    End Sub

    Private Sub OnButtonLeave(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)
        btn.BackColor = Color_Background
        btn.ForeColor = Color_TextIdle

        Dim icon As PictureBox = FindSiblingIcon(btn)
        If icon IsNot Nothing Then icon.BackColor = Color_Background
    End Sub

    Private Function FindSiblingIcon(btn As Button) As PictureBox
        Dim tlp As TableLayoutPanel = TryCast(btn.Parent, TableLayoutPanel)
        If tlp IsNot Nothing Then
            Dim row As Integer = tlp.GetRow(btn)
            Dim ctrl As Control = tlp.GetControlFromPosition(0, row)
            If TypeOf ctrl Is PictureBox Then Return CType(ctrl, PictureBox)
        End If
        Return Nothing
    End Function

    ' =========================================================
    '                 4. NAVIGATION
    ' =========================================================

    Private Sub NavigateTo(ByVal page As UserControl, ByVal title As String)
        If _mainForm Is Nothing Then _mainForm = TryCast(Me.ParentForm, frm_Main)
        If _mainForm IsNot Nothing Then
            _mainForm.LoadPage(page, title)
        End If
    End Sub

    ' --- BUTTON CLICKS (Unchanged) ---

    Private Sub btnNavDashboard_Click(sender As Object, e As EventArgs) Handles btnNavDashboard.Click
        NavigateTo(New newUcDashboard(), "Daily Operations Dashboard")
    End Sub

    Private Sub btnNavContracts_Click(sender As Object, e As EventArgs) Handles btnNavContracts.Click
        NavigateTo(New uc_NewContractEntry(), "Create New Contract")
    End Sub

    Private Sub btnNavCalendar_Click(sender As Object, e As EventArgs) Handles btnNavCalendar.Click
        NavigateTo(New newUcQuoteManager(), "Oculars")
    End Sub

    Private Sub btnNavClients_Click(sender As Object, e As EventArgs) Handles btnNavClients.Click
        NavigateTo(New newUcClientManager(), "Client Management")
    End Sub

    Private Sub btnNavBilling_Click(sender As Object, e As EventArgs) Handles btnNavBilling.Click
        NavigateTo(New newUcBilling(), "Payments & Billing")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        NavigateTo(New newUcContractManager(), "Contracts Database")
    End Sub

    Private Sub btnNavTechs_Click(sender As Object, e As EventArgs) Handles btnNavTechs.Click
        NavigateTo(New newUcTechMonitor(), "Technician Monitor")
    End Sub

    Private Sub btnManageAccounts_Click(sender As Object, e As EventArgs) Handles btnManageAccounts.Click
        NavigateTo(New newUcAccountManager(), "Account Settings")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        NavigateTo(New newUcDataSync(), "Data Management")
    End Sub

End Class