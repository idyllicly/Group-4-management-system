Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Linq

Public Class newUcSideNav

    Private _mainForm As frm_Main

    ' --- COLORS ---
    ' Background: Dark Navy
    Private Color_Background As Color = Color.FromArgb(30, 40, 75)
    ' Active/Hover: Brighter Blue
    Private Color_Hover As Color = Color.FromArgb(60, 80, 150)

    Private Color_TextIdle As Color = Color.White
    Private Color_TextHover As Color = Color.White

    ' --- TRACKING ---
    ' This variable stores which button is currently "Lit Up"
    Private _activeButton As Button = Nothing

    Public Sub New(mainForm As frm_Main)
        InitializeComponent()
        _mainForm = mainForm
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    ' =========================================================
    '                 1. SETUP & LOADING
    ' =========================================================
    Private Sub newUcSideNav_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BackColor = Color_Background
        Dim tlp As TableLayoutPanel = Me.Controls.OfType(Of TableLayoutPanel)().FirstOrDefault()

        If tlp IsNot Nothing Then
            tlp.BackColor = Color_Background
            tlp.Padding = New Padding(0)

            ' 1. CLEANING PHASE: Reset EVERYTHING to default dark mode first
            For Each ctrl As Control In tlp.Controls
                ' --- BUTTONS ---
                If TypeOf ctrl Is Button Then
                    Dim btn As Button = CType(ctrl, Button)
                    btn.FlatStyle = FlatStyle.Flat
                    btn.FlatAppearance.BorderSize = 0
                    btn.FlatAppearance.MouseDownBackColor = Color_Hover
                    btn.FlatAppearance.MouseOverBackColor = Color_Hover
                    btn.BackColor = Color_Background
                    btn.ForeColor = Color_TextIdle
                    btn.TextAlign = ContentAlignment.MiddleLeft

                    ' Remove old handlers to prevent duplicates if reloaded
                    RemoveHandler btn.MouseEnter, AddressOf OnButtonEnter
                    RemoveHandler btn.MouseLeave, AddressOf OnButtonLeave

                    AddHandler btn.MouseEnter, AddressOf OnButtonEnter
                    AddHandler btn.MouseLeave, AddressOf OnButtonLeave

                    ApplyRounding(btn, 25, False, True)
                    AddHandler btn.SizeChanged, Sub(s, ev) ApplyRounding(CType(s, Control), 25, False, True)
                End If

                ' --- ICONS ---
                If TypeOf ctrl Is PictureBox Then
                    Dim pic As PictureBox = CType(ctrl, PictureBox)
                    pic.BackColor = Color_Background
                    ApplyRounding(pic, 25, True, False)
                    AddHandler pic.SizeChanged, Sub(s, ev) ApplyRounding(CType(s, Control), 25, True, False)
                End If
            Next

            ' 2. ACTIVATION PHASE: Now that everything is clean, light up the Dashboard
            ' Check if the button exists before trying to click it
            If btnNavDashboard IsNot Nothing Then
                SetActive(btnNavDashboard)
            End If
        End If
    End Sub

    ' =========================================================
    '                 2. HIGHLIGHT LOGIC
    ' =========================================================

    Private Sub SetActive(btn As Button)
        ' 1. Turn OFF the previous button (if exists)
        If _activeButton IsNot Nothing AndAlso _activeButton IsNot btn Then
            _activeButton.BackColor = Color_Background
            _activeButton.ForeColor = Color_TextIdle

            Dim prevIcon As PictureBox = FindSiblingIcon(_activeButton)
            If prevIcon IsNot Nothing Then prevIcon.BackColor = Color_Background
        End If

        ' 2. Turn ON the new button
        _activeButton = btn
        _activeButton.BackColor = Color_Hover ' Keep it lit
        _activeButton.ForeColor = Color_TextHover

        Dim newIcon As PictureBox = FindSiblingIcon(btn)
        If newIcon IsNot Nothing Then newIcon.BackColor = Color_Hover
    End Sub

    Private Sub OnButtonEnter(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        ' Always highlight on hover
        btn.BackColor = Color_Hover
        Dim icon As PictureBox = FindSiblingIcon(btn)
        If icon IsNot Nothing Then icon.BackColor = Color_Hover
    End Sub

    Private Sub OnButtonLeave(sender As Object, e As EventArgs)
        Dim btn As Button = CType(sender, Button)

        ' CRITICAL: If this button is the "Active" one, DO NOT turn it off.
        If btn Is _activeButton Then Return

        ' Otherwise, revert to dark background
        btn.BackColor = Color_Background
        Dim icon As PictureBox = FindSiblingIcon(btn)
        If icon IsNot Nothing Then icon.BackColor = Color_Background
    End Sub

    ' =========================================================
    '                 3. HELPERS
    ' =========================================================
    Private Sub ApplyRounding(c As Control, radius As Integer, roundLeft As Boolean, roundRight As Boolean)
        Dim path As New GraphicsPath()
        Dim w As Integer = c.Width
        Dim h As Integer = c.Height
        Dim r As Integer = radius

        If roundLeft Then path.AddArc(0, 0, r, r, 180, 90) Else path.AddLine(0, 0, 0, 0)
        If roundRight Then path.AddArc(w - r, 0, r, r, 270, 90) Else path.AddLine(w, 0, w, 0)
        If roundRight Then path.AddArc(w - r, h - r, r, r, 0, 90) Else path.AddLine(w, h, w, h)
        If roundLeft Then path.AddArc(0, h - r, r, r, 90, 90) Else path.AddLine(0, h, 0, h)

        path.CloseFigure()
        c.Region = New Region(path)
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

    Private Sub NavigateTo(ByVal page As UserControl, ByVal title As String)
        If _mainForm Is Nothing Then _mainForm = TryCast(Me.ParentForm, frm_Main)
        If _mainForm IsNot Nothing Then _mainForm.LoadPage(page, title)
    End Sub

    ' =========================================================
    '                 4. NAVIGATION EVENTS
    ' =========================================================
    ' Note: We now call SetActive(sender) in every click

    Private Sub btnNavDashboard_Click(sender As Object, e As EventArgs) Handles btnNavDashboard.Click
        SetActive(CType(sender, Button))
        NavigateTo(New newUcDashboard(), "Daily Operations Dashboard")
    End Sub

    Private Sub btnNavContracts_Click(sender As Object, e As EventArgs) Handles btnNavContracts.Click
        SetActive(CType(sender, Button))
        NavigateTo(New uc_NewContractEntry(), "Create New Contract")
    End Sub

    Private Sub btnNavCalendar_Click(sender As Object, e As EventArgs) Handles btnNavCalendar.Click
        SetActive(CType(sender, Button))
        NavigateTo(New newUcQuoteManager(), "Oculars")
    End Sub

    Private Sub btnNavClients_Click(sender As Object, e As EventArgs) Handles btnNavClients.Click
        SetActive(CType(sender, Button))
        NavigateTo(New newUcClientManager(), "Client Management")
    End Sub

    Private Sub btnNavBilling_Click(sender As Object, e As EventArgs) Handles btnNavBilling.Click
        SetActive(CType(sender, Button))
        NavigateTo(New newUcBilling(), "Payments & Billing")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        SetActive(CType(sender, Button))
        NavigateTo(New newUcContractManager(), "Contracts Database")
    End Sub

    Private Sub btnNavTechs_Click(sender As Object, e As EventArgs) Handles btnNavTechs.Click
        SetActive(CType(sender, Button))
        NavigateTo(New newUcTechMonitor(), "Technician Monitor")
    End Sub

    Private Sub btnManageAccounts_Click(sender As Object, e As EventArgs) Handles btnManageAccounts.Click
        SetActive(CType(sender, Button))
        NavigateTo(New newUcAccountManager(), "Account Settings")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SetActive(CType(sender, Button))
        NavigateTo(New newUcDataSync(), "Data Management")
    End Sub
End Class