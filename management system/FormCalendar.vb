Public Class FormCalendar
    ' Keep track of the current month/year we are viewing
    Private currentMonth As Integer = DateTime.Now.Month
    Private currentYear As Integer = DateTime.Now.Year

    Private Sub FormCalendar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayDays(currentMonth, currentYear)
    End Sub

    Private Sub DisplayDays(month As Integer, year As Integer)
        ' 1. Update the Header Label
        Dim monthName As String = New DateTime(year, month, 1).ToString("MMMM yyyy")
        lblMonthYear.Text = monthName.ToUpper()

        ' 2. Clear the container
        dayContainer.Controls.Clear()

        ' 3. Calculate start day and days in month
        Dim startOfTheMonth As New DateTime(year, month, 1)
        Dim daysInMonth As Integer = DateTime.DaysInMonth(year, month)

        ' Convert DayOfWeek to Integer (Sunday=0, Monday=1...)
        Dim startDayOfWeek As Integer = Convert.ToInt32(startOfTheMonth.DayOfWeek)

        ' 4. Connect to DB to get events
        Dim evtManager As New EventManager()
        Dim jobDays As List(Of Integer) = evtManager.GetJobDays(month, year)
        Dim inspectDays As List(Of Integer) = evtManager.GetInspectionDays(month, year)

        ' 5. Create "Blank" blocks for days before the 1st of the month
        For i As Integer = 1 To startDayOfWeek
            Dim blank As New UC_DayBlock()
            blank.SetDay("") ' This now works because SetDay accepts String!
            blank.Enabled = False ' Make it unclickable
            dayContainer.Controls.Add(blank)
        Next

        ' 6. Create actual Day blocks
        For i As Integer = 1 To daysInMonth
            Dim dayBlock As New UC_DayBlock()
            dayBlock.SetDay(i.ToString())

            ' Check if this day has a Job (Orange Dot)
            If jobDays.Contains(i) Then
                dayBlock.AddEventDot(Color.Orange)
            End If

            ' Check if this day has an Inspection (Green Dot)
            If inspectDays.Contains(i) Then
                dayBlock.AddEventDot(Color.LightGreen)
            End If

            ' Highlight "Today" (Optional)
            If i = DateTime.Now.Day AndAlso month = DateTime.Now.Month AndAlso year = DateTime.Now.Year Then
                dayBlock.BackColor = Color.LightYellow ' Highlight today
            End If

            dayContainer.Controls.Add(dayBlock)
        Next
    End Sub

    ' Button Next Month
    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        currentMonth += 1
        If currentMonth > 12 Then
            currentMonth = 1
            currentYear += 1
        End If
        DisplayDays(currentMonth, currentYear)
    End Sub

    ' Button Previous Month
    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        currentMonth -= 1
        If currentMonth < 1 Then
            currentMonth = 12
            currentYear -= 1
        End If
        DisplayDays(currentMonth, currentYear)
    End Sub

    Private Sub dayContainer_Paint(sender As Object, e As PaintEventArgs) Handles dayContainer.Paint

    End Sub
End Class