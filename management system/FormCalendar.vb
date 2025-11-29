Public Class FormCalendar
    Private currentMonth As Integer = DateTime.Now.Month
    Private currentYear As Integer = DateTime.Now.Year

    Private Sub FormCalendar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DisplayDays(currentMonth, currentYear)
    End Sub

    Private Sub FormCalendar_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        UpdateBlockSizes()
    End Sub

    Private Sub UpdateBlockSizes()
        If dayContainer.Controls.Count > 0 Then
            Dim newWidth As Integer = (dayContainer.ClientSize.Width / 7) - 12
            Dim newHeight As Integer = (dayContainer.ClientSize.Height / 6) - 12
            For Each ctrl As Control In dayContainer.Controls
                ctrl.Size = New Size(newWidth, newHeight)
            Next
        End If
    End Sub

    Private Sub DisplayDays(month As Integer, year As Integer)
        lblMonthYear.Text = New DateTime(year, month, 1).ToString("MMMM yyyy").ToUpper()
        dayContainer.Controls.Clear()

        Dim startOfTheMonth As New DateTime(year, month, 1)
        Dim daysInMonth As Integer = DateTime.DaysInMonth(year, month)
        Dim startDayOfWeek As Integer = Convert.ToInt32(startOfTheMonth.DayOfWeek)
        Dim blockWidth As Integer = (dayContainer.ClientSize.Width / 7) - 12
        Dim blockHeight As Integer = (dayContainer.ClientSize.Height / 6) - 12

        ' --- CONNECT TO DB ---
        Dim evtManager As New EventManager()

        ' 1. Fetch Job Colors (Dictionary of Day -> List of Colors)
        Dim jobColors As Dictionary(Of Integer, List(Of Color)) = evtManager.GetJobColorsForMonth(month, year)

        ' 2. Fetch Inspections (List of Days) - Keeping Inspections simple (Green) for now
        Dim inspectDays As List(Of Integer) = evtManager.GetInspectionDays(month, year)

        ' Create Blanks
        For i As Integer = 1 To startDayOfWeek
            Dim blank As New UC_DayBlock()
            blank.SetDay("")
            blank.Enabled = False
            blank.Size = New Size(blockWidth, blockHeight)
            dayContainer.Controls.Add(blank)
        Next

        ' Create Days
        For i As Integer = 1 To daysInMonth
            Dim dayBlock As New UC_DayBlock()
            dayBlock.SetDay(i.ToString())
            dayBlock.Size = New Size(blockWidth, blockHeight)
            dayBlock.FullDate = New DateTime(year, month, i)

            ' --- A. ADD JOB DOTS (Based on Status Color) ---
            If jobColors.ContainsKey(i) Then
                ' Loop through every job on this day and add its specific color dot
                For Each c As Color In jobColors(i)
                    dayBlock.AddEventDot(c)
                Next
            End If

            ' --- B. ADD INSPECTION DOTS (Standard Green) ---
            ' We check count to add multiple dots if multiple inspections exist (optional logic)
            Dim inspectionCount As Integer = inspectDays.Where(Function(x) x = i).Count()
            For k As Integer = 1 To inspectionCount
                ' Use a distinct color for inspections so they don't look like "Completed" jobs
                dayBlock.AddEventDot(Color.LimeGreen)
            Next

            ' Highlight Today
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
End Class