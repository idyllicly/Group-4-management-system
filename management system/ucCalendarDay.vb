Public Class ucCalendarDay
    Public Property DayDate As Date
    Public Event DayClicked(selectedDate As Date)

    Public Sub New()
        InitializeComponent()
        ' Hide the dot by default
        lblDot.Visible = False

        ' Correct way to make the label round (Circular Region)
        Dim path As New System.Drawing.Drawing2D.GraphicsPath()
        path.AddEllipse(0, 0, 10, 10) ' x, y, width, height (must match label size)

        lblDot.Region = New System.Drawing.Region(path)
    End Sub

    Public Sub SetDay(d As Integer, fullDate As Date, hasJob As Boolean)
        lblDayNumber.Text = d.ToString()
        DayDate = fullDate

        If hasJob Then
            lblDot.Visible = True
            lblDot.BackColor = Color.Orange ' Change color based on job type if you want
        Else
            lblDot.Visible = False
        End If
    End Sub

    ' Forward clicks to the main dashboard
    Private Sub ucCalendarDay_Click(sender As Object, e As EventArgs) Handles MyBase.Click, lblDayNumber.Click, lblDot.Click
        RaiseEvent DayClicked(DayDate)
    End Sub
End Class