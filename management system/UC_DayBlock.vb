Imports System.Drawing.Drawing2D

Public Class UC_DayBlock
    ' Store the full date for this block
    Public Property FullDate As DateTime

    Public Sub SetDay(day As String)
        lblDay.Text = day
    End Sub

    ' Method to add a colored dot for an event
    Public Sub AddEventDot(color As Color)
        Dim dot As New Panel()

        ' Size settings
        Dim dotSize As Integer = 16
        dot.Size = New Size(dotSize, dotSize)

        dot.BackColor = color
        dot.Margin = New Padding(2)

        Dim path As New GraphicsPath()
        path.AddEllipse(0, 0, dotSize, dotSize)
        dot.Region = New Region(path)

        ' *** FIX 1: Make the DOTS clickable ***
        ' We manually tell the dot: "When clicked, run the UC_DayBlock_Click function"
        AddHandler dot.Click, AddressOf UC_DayBlock_Click

        pnlEvents.Controls.Add(dot)
    End Sub

    Public Sub ClearEvents()
        pnlEvents.Controls.Clear()
        lblDay.Text = ""
        Me.BackColor = Color.White
    End Sub

    ' *** FIX 2: Ensure Handles clause covers Label and Panel ***
    Private Sub UC_DayBlock_Click(sender As Object, e As EventArgs) Handles MyBase.Click, lblDay.Click, pnlEvents.Click

        ' 1. Check if the date is valid (prevents empty boxes from opening)
        If FullDate <> DateTime.MinValue Then
            ' 3. Open the Popup
            Dim detailsForm As New FormDayDetails(FullDate)
            detailsForm.ShowDialog() ' ShowDialog makes it a popup that pauses the background
        Else
            ' DEBUGGING: If you see this, it means FormCalendar.vb is NOT passing the date correctly!
            MessageBox.Show("Debug Error: The 'FullDate' property is empty. Please check FormCalendar.vb logic.")
        End If
    End Sub
End Class