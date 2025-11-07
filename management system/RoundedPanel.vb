'--- Copy all of this code into your new RoundedPanel.vb file ---
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class RoundedPanel
    Inherits Panel ' This class IS a Panel, but with new features

    ' This new property will show up in the designer
    Private _CornerRadius As Integer = 10
    Public Property CornerRadius As Integer
        Get
            Return _CornerRadius
        End Get
        Set(value As Integer)
            _CornerRadius = value
            Me.Invalidate() ' Tell the panel to redraw
        End Set
    End Property

    ' This is the "hard code" you were asking for.
    ' We are taking over the drawing process.
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        ' Set up high-quality drawing
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        ' Create the "path" for our rounded rectangle
        Using path As New GraphicsPath()
            Dim rect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
            Dim radius As Integer = If(_CornerRadius > 0, _CornerRadius, 1)

            ' --- Create the rounded shape ---
            path.AddArc(rect.X, rect.Y, radius * 2, radius * 2, 180, 90) ' Top-left
            path.AddArc(rect.Right - (radius * 2), rect.Y, radius * 2, radius * 2, 270, 90) ' Top-right
            path.AddArc(rect.Right - (radius * 2), rect.Bottom - (radius * 2), radius * 2, radius * 2, 0, 90) ' Bottom-right
            path.AddArc(rect.X, rect.Bottom - (radius * 2), radius * 2, radius * 2, 90, 90) ' Bottom-left
            path.CloseFigure()
            ' ---------------------------------

            ' Apply the rounded shape as a "clip"
            Me.Region = New Region(path)

            ' (Optional) Draw a border along the rounded shape
            Using pen As New Pen(Me.ForeColor, 1)
                e.Graphics.DrawPath(pen, path)
            End Using
        End Using
    End Sub
End Class