Public Class UC_DayBlock
    ' Property to set the day number (Changed to String to allow empty text)
    Public Sub SetDay(day As String)
        lblDay.Text = day
    End Sub

    ' Method to add a colored dot for an event
    Public Sub AddEventDot(color As Color)
        Dim dot As New Panel()
        dot.Size = New Size(10, 10) ' Size of the dot
        dot.BackColor = color
        dot.Margin = New Padding(2) ' Space between dots

        ' Make it circular
        Dim path As New System.Drawing.Drawing2D.GraphicsPath()
        path.AddEllipse(0, 0, 10, 10)
        dot.Region = New Region(path)

        pnlEvents.Controls.Add(dot)
    End Sub

    ' Helper to clear old data
    Public Sub ClearEvents()
        pnlEvents.Controls.Clear()
        lblDay.Text = ""
        Me.BackColor = Color.White
    End Sub

    Private Sub lblDay_Click(sender As Object, e As EventArgs) Handles lblDay.Click

    End Sub
End Class