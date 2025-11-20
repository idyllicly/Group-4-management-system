Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class OvalButton
    Inherits Button

    ' --- Additional Motifs for Hover Effect ---
    Private _IsHovering As Boolean = False
    Private Const HOVER_FACTOR As Single = 0.85F ' Dim to 85% of original brightness

    ' A property to control the radius of the corners
    Private _CornerRadius As Integer = 15
    Public Property CornerRadius() As Integer
        Get
            Return _CornerRadius
        End Get
        Set(value As Integer)
            _CornerRadius = value
            Me.Invalidate()
        End Set
    End Property

    ' --- New Event Handlers for Hover State ---
    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)
        _IsHovering = True
        Me.Invalidate() ' Force redraw to apply hover color
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)
        _IsHovering = False
        Me.Invalidate() ' Force redraw to restore normal color
    End Sub

    ' --- Helper Function to Dim/Lighten Color ---
    Private Function ChangeBrightness(ByVal originalColor As Color, ByVal factor As Single) As Color
        Dim r As Integer = CInt(originalColor.R * factor)
        Dim g As Integer = CInt(originalColor.G * factor)
        Dim b As Integer = CInt(originalColor.B * factor)

        ' Ensure components are within valid range (0-255)
        Return Color.FromArgb(originalColor.A, Math.Min(255, r), Math.Min(255, g), Math.Min(255, b))
    End Function

    Protected Overrides Sub OnPaint(ByVal pevent As PaintEventArgs)
        ' DO NOT call MyBase.OnPaint(pevent) if you are fully custom drawing the button,
        ' as it can interfere or draw the default square button shape underneath.
        ' MyBase.OnPaint is now removed.

        pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias ' For smooth edges

        ' --- 1. Define the Colors Based on Hover State ---
        Dim currentBackColor As Color = Me.BackColor
        Dim currentBorderColor As Color = Color.Gray

        If _IsHovering Then
            ' Dim the background color when hovering
            currentBackColor = ChangeBrightness(Me.BackColor, HOVER_FACTOR)
            ' Optionally, change the border color too
            currentBorderColor = Color.DarkGray
        End If

        ' --- 2. Create the GraphicsPath for the rounded shape (same as before) ---
        Using path As New GraphicsPath()
            Dim rect As Rectangle = Me.ClientRectangle
            Dim radius As Integer = Me.CornerRadius
            Dim d As Integer = 2 * radius ' Diameter

            ' Safety check for radius
            If radius > rect.Height / 2 Then radius = rect.Height / 2
            If radius > rect.Width / 2 Then radius = rect.Width / 2

            ' Draw the rounded rectangle path
            path.AddArc(rect.X, rect.Y, d, d, 180, 90) ' Top-Left
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90) ' Top-Right
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90) ' Bottom-Right
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90) ' Bottom-Left
            path.CloseFigure()

            ' Set the button's Region to the path.
            Me.Region = New Region(path)

            ' --- 3. Custom Drawing (Using Hover Colors) ---

            ' Draw the background (Fill)
            Using buttonBrush As New SolidBrush(currentBackColor)
                pevent.Graphics.FillPath(buttonBrush, path)
            End Using

            ' Draw the border (Outline)
            Using pen As New Pen(currentBorderColor, 1)
                pevent.Graphics.DrawPath(pen, path)
            End Using

            ' Draw the text
            TextRenderer.DrawText(pevent.Graphics, Me.Text, Me.Font, Me.ClientRectangle, Me.ForeColor, TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter)

        End Using

    End Sub

    ' To prevent the default Windows drawing of the square border/focus rectangle
    Protected Overrides Sub OnHandleCreated(e As EventArgs)
        MyBase.OnHandleCreated(e)
        Me.FlatStyle = FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0 ' Remove any default square border
    End Sub

End Class