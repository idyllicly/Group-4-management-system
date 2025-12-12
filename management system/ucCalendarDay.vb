Public Class ucCalendarDay
    ' 1. Properties
    Public Property DayDate As Date
    Public Event DayClicked(selectedDate As Date)

    ' 2. State Variables
    Private _originalColor As Color = Color.White
    Private _isSelected As Boolean = False

    Public Sub New()
        InitializeComponent()
        ' Fix: Add handlers to the base containers immediately so clicks work everywhere
        BindEvents(Me)
        If lblDayNumber IsNot Nothing Then BindEvents(lblDayNumber)
        If flpContent IsNot Nothing Then BindEvents(flpContent)
    End Sub

    ' Helper to attach events to everything
    Private Sub BindEvents(c As Control)
        AddHandler c.Click, AddressOf Element_Click
        AddHandler c.MouseEnter, AddressOf OnMouseEnterAny
        AddHandler c.MouseLeave, AddressOf OnMouseLeaveAny
    End Sub

    Public Sub SetDay(d As Integer, fullDate As Date)
        lblDayNumber.Text = d.ToString()
        DayDate = fullDate
        UpdateBaseColor()
    End Sub

    ' --- CRITICAL: HANDLES SELECTION STATE ---
    Public Sub SetSelected(selected As Boolean)
        _isSelected = selected
        UpdateBaseColor()
    End Sub

    Private Sub UpdateBaseColor()
        If _isSelected Then
            ' If Selected: Ocean Blue
            _originalColor = Color.FromArgb(59, 130, 246)
            lblDayNumber.ForeColor = Color.White
        Else
            ' If Not Selected: Check if Today
            If DayDate.Date = DateTime.Now.Date Then
                _originalColor = Color.FromArgb(219, 234, 254) ' Today (Light Blue)
            Else
                _originalColor = Color.White
            End If
            lblDayNumber.ForeColor = Color.Black
        End If

        ' Apply the color immediately
        Me.BackColor = _originalColor
    End Sub

    Public Sub AddJobSummary(count As Integer, jobType As String)
        If flpContent Is Nothing Then Return

        Dim lbl As New Label()
        lbl.Text = $"{count} {jobType}"
        lbl.AutoSize = False
        lbl.Width = Me.Width - 10
        lbl.Height = 16
        lbl.Font = New Font("Segoe UI", 7, FontStyle.Regular)
        lbl.TextAlign = ContentAlignment.MiddleLeft
        lbl.Margin = New Padding(1)
        lbl.Cursor = Cursors.Hand

        ' Pill Colors
        Select Case jobType.ToLower()
            Case "service"
                lbl.BackColor = Color.FromArgb(219, 234, 254)
                lbl.ForeColor = Color.FromArgb(30, 64, 175)
            Case "inspection"
                lbl.BackColor = Color.FromArgb(254, 249, 195)
                lbl.ForeColor = Color.FromArgb(161, 98, 7)
            Case Else
                lbl.BackColor = Color.FromArgb(241, 245, 249)
                lbl.ForeColor = Color.FromArgb(71, 85, 105)
        End Select

        ' Ensure pills trigger events too
        BindEvents(lbl)

        flpContent.Controls.Add(lbl)
    End Sub

    Public Sub ClearData()
        lblDayNumber.Text = ""
        flpContent.Controls.Clear()
        Me.BackColor = Color.White
        _originalColor = Color.White
        _isSelected = False
    End Sub

    ' --- EVENTS ---
    Private Sub Element_Click(sender As Object, e As EventArgs)
        RaiseEvent DayClicked(DayDate)
    End Sub

    Private Sub OnMouseEnterAny(sender As Object, e As EventArgs)
        ' Fix: If Selected, darken the blue. If not, show gray hover.
        If _isSelected Then
            Me.BackColor = Color.FromArgb(37, 99, 235) ' Darker Blue
        Else
            Me.BackColor = Color.FromArgb(248, 250, 252) ' Light Gray Hover
        End If
    End Sub

    Private Sub OnMouseLeaveAny(sender As Object, e As EventArgs)
        ' CRITICAL: Only reset color if the mouse has ACTUALLY left the entire square.
        Dim mousePt As Point = Me.PointToClient(Cursor.Position)

        If Not Me.ClientRectangle.Contains(mousePt) Then
            ' Reset to Base Color (Blue if selected, White if not)
            Me.BackColor = _originalColor
        End If
    End Sub
End Class