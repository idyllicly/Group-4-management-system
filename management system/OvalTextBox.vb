Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms
Imports System.ComponentModel ' IMPORTANT for Properties Window visibility

Public Class OvalTextBox
    Inherits UserControl

    ' --- Custom Properties for Border and Shape ---
    Private _CurvedRadius As Integer = 15
    ''' <summary>The radius of the corner curvature.</summary>
    <Category("Appearance")>
    <Description("The radius of the corner curvature for the oval shape.")>
    <DefaultValue(15)>
    Public Property CurvedRadius() As Integer
        Get
            Return _CurvedRadius
        End Get
        Set(value As Integer)
            _CurvedRadius = value
            Me.Invalidate()
            Me.OnResize(EventArgs.Empty)
        End Set
    End Property

    Private _BorderColor As Color = Color.DimGray
    ''' <summary>The color of the oval border.</summary>
    <Category("Appearance")>
    <DefaultValue(GetType(Color), "DimGray")>
    Public Property BorderColor() As Color
        Get
            Return _BorderColor
        End Get
        Set(value As Color)
            _BorderColor = value
            Me.Invalidate()
        End Set
    End Property

    Private _BorderSize As Integer = 3
    ''' <summary>The thickness of the oval border in pixels.</summary>
    <Category("Appearance")>
    <DefaultValue(3)>
    Public Property BorderSize() As Integer
        Get
            Return _BorderSize
        End Get
        Set(value As Integer)
            _BorderSize = value
            Me.Invalidate()
        End Set
    End Property

    ' --- Exposed Inner TextBox Properties ---

    ''' <summary>Exposes the Text property of the inner TextBox.</summary>
    <Browsable(True)>
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Content)>
    Public Property Text() As String
        Get
            Return txtInput.Text
        End Get
        Set(value As String)
            txtInput.Text = value
        End Set
    End Property

    ''' <summary>Exposes the TextAlign property of the inner TextBox.</summary>
    <Category("Behavior")>
    <DefaultValue(HorizontalAlignment.Left)>
    Public Property TextAlign() As HorizontalAlignment
        Get
            Return txtInput.TextAlign
        End Get
        Set(value As HorizontalAlignment)
            txtInput.TextAlign = value
        End Set
    End Property

    ''' <summary>Exposes the PasswordChar property of the inner TextBox.</summary>
    <Category("Behavior")>
    <Description("The character to display for password input.")>
    <DefaultValue(CChar(" "))>
    Public Property PasswordChar() As Char
        Get
            Return txtInput.PasswordChar
        End Get
        Set(value As Char)
            txtInput.PasswordChar = value
        End Set
    End Property

    ' Forward essential events from the inner TextBox
    Public Event TextChanged As EventHandler
    Private Sub txtInput_TextChanged(sender As Object, e As EventArgs) Handles txtInput.TextChanged
        RaiseEvent TextChanged(Me, e)
    End Sub

    ' --- Shape Definition (GraphicsPath) ---
    Private Function GetCurvedPath(ByVal rect As Rectangle, ByVal radius As Integer) As GraphicsPath
        Dim path As New GraphicsPath()

        If radius > rect.Height / 2 Then radius = rect.Height / 2
        If radius > rect.Width / 2 Then radius = rect.Width / 2

        path.StartFigure()

        ' Top-Left Arc
        path.AddArc(rect.X, rect.Y, 2 * radius, 2 * radius, 180, 90)

        ' Top-Right Arc
        path.AddArc(rect.Right - 2 * radius, rect.Y, 2 * radius, 2 * radius, 270, 90)

        ' Bottom-Right Arc
        path.AddArc(rect.Right - 2 * radius, rect.Bottom - 2 * radius, 2 * radius, 2 * radius, 0, 90)

        ' Bottom-Left Arc
        path.AddArc(rect.X, rect.Bottom - 2 * radius, 2 * radius, 2 * radius, 90, 90)

        path.CloseFigure()
        Return path
    End Function

    ' --- Drawing the Border and Shape ---
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        Using fillBrush As New SolidBrush(Me.BackColor)
            e.Graphics.FillPath(fillBrush, GetCurvedPath(Me.ClientRectangle, Me.CurvedRadius))
        End Using

        Using borderPen As New Pen(Me.BorderColor, Me.BorderSize)
            borderPen.Alignment = PenAlignment.Inset
            e.Graphics.DrawPath(borderPen, GetCurvedPath(Me.ClientRectangle, Me.CurvedRadius))
        End Using
    End Sub

    ' --- Sizing and Clipping (Padding Fix) ---
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        Me.Region = New Region(GetCurvedPath(Me.ClientRectangle, Me.CurvedRadius))

        ' Vertical and Horizontal alignment fix:

        ' Generous horizontal padding to prevent text from hitting the curved edge (8px buffer)
        Dim totalPaddingX As Integer = Me.CurvedRadius + Me.BorderSize + 8

        ' Calculates vertical center (relies on inner textbox height being manually set lower)
        Dim verticalPadding As Integer = (Me.Height - txtInput.Height) / 2

        txtInput.Location = New Point(totalPaddingX, verticalPadding)
        txtInput.Width = Me.Width - (totalPaddingX * 2)
    End Sub

    Public Overrides Function GetPreferredSize(proposedSize As Size) As Size
        Return New Size(200, 30)
    End Function

End Class