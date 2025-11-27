Imports MySql.Data.MySqlClient

Public Class FormCalendar
    ' Keep track of the current month/year we are viewing
    Private currentMonth As Integer = DateTime.Now.Month
    Private currentYear As Integer = DateTime.Now.Year

    Private Sub FormCalendar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayDays(currentMonth, currentYear)
    End Sub

    ' New Event: When the window is resized, resize the blocks too
    Private Sub FormCalendar_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        UpdateBlockSizes()
    End Sub

    ' Helper to resize existing blocks without reloading the database
    Private Sub UpdateBlockSizes()
        If dayContainer.Controls.Count > 0 Then
            ' Calculate new size based on current container size
            ' We divide by 7 columns and 6 rows
            ' We subtract 12 pixels to account for margins and borders
            Dim newWidth As Integer = (dayContainer.ClientSize.Width / 7) - 12
            Dim newHeight As Integer = (dayContainer.ClientSize.Height / 6) - 12

            ' Apply to all blocks
            For Each ctrl As Control In dayContainer.Controls
                ctrl.Size = New Size(newWidth, newHeight)
            Next
        End If
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
        Dim startDayOfWeek As Integer = Convert.ToInt32(startOfTheMonth.DayOfWeek)

        ' Calculate Size Dynamically
        Dim blockWidth As Integer = (dayContainer.ClientSize.Width / 7) - 12
        Dim blockHeight As Integer = (dayContainer.ClientSize.Height / 6) - 12

        ' 4. Connect to DB to get events
        Dim evtManager As New EventManager()
        Dim jobDays As List(Of Integer) = evtManager.GetJobDays(month, year)
        Dim inspectDays As List(Of Integer) = evtManager.GetInspectionDays(month, year)

        ' 5. Create "Blank" blocks for days before the 1st of the month
        For i As Integer = 1 To startDayOfWeek
            Dim blank As New UC_DayBlock()
            blank.SetDay("")
            blank.Enabled = False
            blank.Size = New Size(blockWidth, blockHeight)
            dayContainer.Controls.Add(blank)
        Next

        ' 6. Create actual Day blocks
        For i As Integer = 1 To daysInMonth
            Dim dayBlock As New UC_DayBlock()
            dayBlock.SetDay(i.ToString())
            dayBlock.Size = New Size(blockWidth, blockHeight)

            ' =========================================================
            ' THIS IS THE FIX! Make sure this line is exactly here:
            ' We construct a Date object using the current Year, Month, and Loop Index (Day)
            ' =========================================================
            dayBlock.FullDate = New DateTime(year, month, i)
            ' =========================================================

            ' Check if this day has a Job (Orange Dot)
            If jobDays.Contains(i) Then
                dayBlock.AddEventDot(Color.Orange)
            End If

            ' Check if this day has an Inspection (Green Dot)
            If inspectDays.Contains(i) Then
                dayBlock.AddEventDot(Color.LightGreen)
            End If

            ' Highlight "Today"
            If i = DateTime.Now.Day AndAlso month = DateTime.Now.Month AndAlso year = DateTime.Now.Year Then
                dayBlock.BackColor = Color.LightYellow
            End If

            dayContainer.Controls.Add(dayBlock)
        Next
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        currentMonth += 1
        If currentMonth > 12 Then
            currentMonth = 1
            currentYear += 1
        End If
        DisplayDays(currentMonth, currentYear)
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        currentMonth -= 1
        If currentMonth < 1 Then
            currentMonth = 12
            currentYear -= 1
        End If
        DisplayDays(currentMonth, currentYear)
    End Sub

    Private Sub SideNavControl1_Load(sender As Object, e As EventArgs)

    End Sub
End Class