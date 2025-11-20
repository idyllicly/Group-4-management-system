Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class OvalDescriptionBox
    Inherits System.Windows.Forms.UserControl

#Region "Custom Properties"

    ' Private fields to store the custom drawing values
    Private _borderColor As Color = Color.Purple
    Private _borderSize As Integer = 3
    Private _curvedRadius As Integer = 10 ' Corner radius in pixels
    Private Const LINE_PADDING As Integer = 4 ' Space between innerTextBox and border

    ' Public Property for BorderColor (Fixes syntax errors near Line 64)
    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Me.Invalidate() ' Force redraw when color changes
        End Set
    End Property ' <-- BC30431, BC30632, BC30630 fix

    ' Public Property for BorderSize
    Public Property BorderSize As Integer
        Get
            Return _borderSize
        End Get
        Set(value As Integer)
            If value > 0 Then
                _borderSize = value
                Me.Invalidate() ' Force redraw when size changes
            End If
        End Set
    End Property

    ' Public Property for CurvedRadius
    Public Property CurvedRadius As Integer
        Get
            Return _curvedRadius
        End Get
        Set(value As Integer)
            If value >= 0 Then
                _curvedRadius = value
                Me.Invalidate() ' Force redraw when radius changes
            End If
        End Set
    End Property

#End Region

#Region "Exposed Properties and Constructor"

    ' Use SHADOWS because Multiline is not Overridable in UserControl
    Public Shadows Property Multiline As Boolean
        Get
            Return innerTextBox.Multiline
        End Get
        Set(value As Boolean)
            innerTextBox.Multiline = value
            Me.Invalidate()
        End Set
    End Property

    ' Use SHADOWS because Text is not Overridable in UserControl
    Public Shadows Property Text As String
        Get
            Return innerTextBox.Text
        End Get
        Set(value As String)
            innerTextBox.Text = value
            MyBase.Text = value
        End Set
    End Property

    ' Constructor (Fixes BC30689, BC30355, BC30203, BC30188 errors by wrapping code in a method)
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Set initial property values
        Me.BorderColor = Color.FromArgb(128, 0, 255) ' Purple border
        Me.BorderSize = 3
        Me.CurvedRadius = 10

        ' Initial setup for the internal TextBox (Fixes BC30451 if innerTextBox is declared in Designer)
        innerTextBox.Multiline = True
        innerTextBox.BorderStyle = BorderStyle.None
        innerTextBox.AcceptsReturn = True
        innerTextBox.Location = New Point(LINE_PADDING, LINE_PADDING)

        ' Set the initial bounds of the innerTextBox
        SetTextBoxBounds()
    End Sub

#End Region

#Region "Layout and Drawing"

    ' Helper function to set the position and size of the innerTextBox
    Private Sub SetTextBoxBounds()
        ' The inner TextBox is sized to fit within the custom border space
        innerTextBox.Size = New Size(
            Me.Width - 2 * LINE_PADDING,
            Me.Height - 2 * LINE_PADDING)
    End Sub

    ' Update the internal TextBox size whenever the UserControl size changes
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        SetTextBoxBounds()
        Me.Invalidate() ' Force redraw of border on resize
    End Sub

    ' Custom Border Drawing Logic
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        ' Define the main rectangle for the custom border, inset by half the border size
        Dim rect As New Rectangle(
            _borderSize \ 2,
            _borderSize \ 2,
            Me.Width - _borderSize,
            Me.Height - _borderSize)

        ' Use a GraphicsPath to create the rounded corners
        Using path As GraphicsPath = GetRoundedRect(rect, _curvedRadius)
            ' Draw the Border
            Using penBorder As New Pen(_borderColor, _borderSize)
                e.Graphics.DrawPath(penBorder, path)
            End Using
        End Using
    End Sub

    ' Helper function to generate a GraphicsPath for a rounded rectangle
    Private Function GetRoundedRect(ByVal rect As Rectangle, ByVal radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()

        Dim diameter As Integer = radius * 2
        Dim arcRect As New Rectangle(rect.Location, New Size(diameter, diameter))

        ' Top Left Arc
        path.AddArc(arcRect, 180, 90)

        ' Top Right Arc
        arcRect.X = rect.Right - diameter
        path.AddArc(arcRect, 270, 90)

        ' Bottom Right Arc
        arcRect.Y = rect.Bottom - diameter
        path.AddArc(arcRect, 0, 90)

        ' Bottom Left Arc
        arcRect.X = rect.Left
        arcRect.Y = rect.Bottom - diameter
        path.AddArc(arcRect, 90, 90)

        path.CloseFigure()
        Return path
    End Function

#End Region

End Class