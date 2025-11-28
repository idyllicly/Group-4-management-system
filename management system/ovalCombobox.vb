Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.Linq ' Needed for some internal logic, though not strictly used in the ListBox helper

''' <summary>
''' A custom WinForms UserControl designed to look like a pill-shaped combobox.
''' </summary>
Public Class OvalComboBox
    Inherits UserControl

    ' --- Private Fields ---
    Private _listBox As ListBox
    ' Renamed _selectedText to _text to reflect it's the official Text property value
    Private _text As String = "Select an Item"
    Private _isDropdownOpen As Boolean = False

    ' Defines the radius for the rounded corners (matching the visual style in the image)
    Private Const BorderRadius As Integer = 10

    ' --- Constructor ---
    Public Sub New()
        InitializeComponent()
        Me.SetStyle(ControlStyles.SupportsTransparentBackColor Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.AllPaintingInWmPaint Or ControlStyles.UserPaint, True)
        Me.BackColor = Color.Transparent ' Allows the custom drawing to control the background
        Me.Size = New Size(250, 32) ' Default size for the display

        ' Initialize the ListBox for the dropdown functionality
        _listBox = New ListBox()
        With _listBox
            .Visible = False
            .BorderStyle = BorderStyle.FixedSingle
            .Width = Me.Width
            .Height = 100 ' Default list height
        End With

        ' Handle item selection
        AddHandler _listBox.SelectedIndexChanged, AddressOf ListBox_SelectedIndexChanged

        ' Handle clicks outside the ListBox to close the dropdown
        AddHandler _listBox.LostFocus, AddressOf ListBox_LostFocus

        Me.Controls.Add(_listBox)
    End Sub

    ' --- Properties ---

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public ReadOnly Property Items As ListBox.ObjectCollection
        Get
            Return _listBox.Items
        End Get
    End Property

    ' ⭐ FIX 1: OVERRIDE THE STANDARD TEXT PROPERTY (This is what EditAccountPage reads)
    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)>
    Public Overrides Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            If _text <> value Then
                _text = value
                Me.Invalidate() ' Force redraw
                ' ⭐ Sync internal ListBox selection when text is set externally (e.g., LoadAccountData)
                FindAndSelectItem(value)
            End If
        End Set
    End Property

    <Browsable(False)>
    Public ReadOnly Property SelectedItem As Object
        Get
            Return _listBox.SelectedItem
        End Get
    End Property

    ' --- Custom Drawing Helpers (GetRoundedRectPath remains the same) ---

    Private Function GetRoundedRectPath(rect As Rectangle, radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()
        If radius > 0 Then
            Dim diameter As Integer = radius * 2
            Dim size As New Size(diameter, diameter)
            If diameter > rect.Width OrElse diameter > rect.Height Then
                radius = Math.Min(rect.Width, rect.Height) \ 2
                diameter = radius * 2
                size = New Size(diameter, diameter)
            End If

            Dim arc As New Rectangle(rect.Location, size)

            path.AddArc(arc, 180, 90)

            arc.X = rect.Right - diameter - 1
            path.AddArc(arc, 270, 90)

            arc.Y = rect.Bottom - diameter - 1
            path.AddArc(arc, 0, 90)

            arc.X = rect.Left
            path.AddArc(arc, 90, 90)

            path.CloseFigure()
        Else
            path.AddRectangle(rect)
        End If
        Return path
    End Function

    ' --- Custom Drawing (OnPaint) ---

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Dim rect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)

        Dim fillColor As Color = Color.FromArgb(240, 240, 240)
        Dim borderColor As Color = If(_isDropdownOpen, Color.FromArgb(64, 64, 64), Color.FromArgb(128, 128, 128))

        Using path As GraphicsPath = GetRoundedRectPath(rect, BorderRadius)

            Using brush As New SolidBrush(fillColor)
                e.Graphics.FillPath(brush, path)
            End Using

            Using pen As New Pen(borderColor, 1.5F)
                e.Graphics.DrawPath(pen, path)
            End Using
        End Using

        Dim textFormat As New StringFormat() With {
            .LineAlignment = StringAlignment.Center,
            .Alignment = StringAlignment.Near,
            .Trimming = StringTrimming.EllipsisCharacter
        }
        Dim textRect As New Rectangle(10, 0, Me.Width - 40, Me.Height)

        Using textBrush As New SolidBrush(Me.ForeColor)
            ' Uses the synchronized _text field for drawing
            e.Graphics.DrawString(_text, Me.Font, textBrush, textRect, textFormat)
        End Using

        Dim arrowSize As Integer = 8
        Dim arrowX As Integer = Me.Width - arrowSize - 10
        Dim arrowY As Integer = (Me.Height - arrowSize) \ 2

        Dim arrowPoints As Point()
        If _isDropdownOpen Then
            arrowPoints = {
                New Point(arrowX, arrowY + arrowSize),
                New Point(arrowX + arrowSize, arrowY + arrowSize),
                New Point(arrowX + arrowSize \ 2, arrowY)
            }
        Else
            arrowPoints = {
                New Point(arrowX, arrowY),
                New Point(arrowX + arrowSize, arrowY),
                New Point(arrowX + arrowSize \ 2, arrowY + arrowSize)
            }
        End If

        Using brush As New SolidBrush(Color.Black)
            e.Graphics.FillPolygon(brush, arrowPoints)
        End Using
    End Sub

    ' --- Event Handlers ---

    Protected Overrides Sub OnClick(e As EventArgs)
        ' Toggle the visibility of the dropdown list
        ToggleDropdown()
        MyBase.OnClick(e)
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        ' Ensure the listbox width matches the control width
        If _listBox IsNot Nothing Then
            _listBox.Width = Me.Width
        End If
        Me.Invalidate() ' Redraw the rounded rectangle when resized
    End Sub

    ' Handle selection change on the listbox
    Private Sub ListBox_SelectedIndexChanged(sender As Object, e As EventArgs)
        If _listBox.SelectedItem IsNot Nothing Then
            ' ⭐ FIX 2: Use Me.Text (the synchronized public property)
            Me.Text = _listBox.SelectedItem.ToString()
            ToggleDropdown(False)
        End If
    End Sub

    ' New handler to close the dropdown when the ListBox loses focus
    Private Sub ListBox_LostFocus(sender As Object, e As EventArgs)
        Me.BeginInvoke(New MethodInvoker(Sub()
                                             If Me.Parent IsNot Nothing AndAlso Not Me.ContainsFocus Then
                                                 ToggleDropdown(False)
                                             End If
                                         End Sub))
    End Sub

    ' --- Private Methods ---

    ' ⭐ NEW: Helper to find and set the ListBox selection when the Text property is set externally.
    Private Sub FindAndSelectItem(textToMatch As String)
        For i As Integer = 0 To _listBox.Items.Count - 1
            If _listBox.Items(i).ToString().Equals(textToMatch.Trim(), StringComparison.OrdinalIgnoreCase) Then
                _listBox.SelectedIndex = i
                Return
            End If
        Next
        _listBox.SelectedIndex = -1 ' Clear selection if no match found
    End Sub

    Private Sub ToggleDropdown(Optional show As Boolean? = Nothing)
        _isDropdownOpen = show.GetValueOrDefault(Not _isDropdownOpen)

        If _isDropdownOpen Then
            ' Check for items before attempting to open
            If _listBox.Items.Count > 0 AndAlso Me.Parent IsNot Nothing Then

                ' 1. Calculate the ListBox height
                Dim itemHeight As Integer = _listBox.ItemHeight
                Dim maxListHeight As Integer = 8 * itemHeight ' Max 8 items visible
                Dim neededHeight As Integer = Math.Min(maxListHeight, _listBox.Items.Count * itemHeight + 4) ' +4 for padding/border

                _listBox.Height = neededHeight

                ' 2. Change the ListBox's Parent to the main Form
                Me.Parent.Controls.Add(_listBox)

                ' 3. Calculate the new global position relative to the Form
                Dim globalLocation As Point = Me.PointToScreen(New Point(0, Me.Height + 1))
                Dim formLocation As Point = Me.Parent.PointToClient(globalLocation)

                _listBox.Location = formLocation

                ' 4. Show and focus
                _listBox.BringToFront()
                _listBox.Visible = True
                _listBox.Focus()
            Else
                _isDropdownOpen = False ' Cannot open without items or parent form
            End If
        Else
            ' 1. Hide the ListBox
            _listBox.Visible = False

            ' 2. Move ListBox back into the OvalComboBox's controls for proper cleanup/designer state
            If Not Me.Controls.Contains(_listBox) Then
                Me.Controls.Add(_listBox)
            End If
        End If

        Me.Invalidate() ' Redraw the control with updated border/arrow
    End Sub

    Private Sub OvalComboBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class