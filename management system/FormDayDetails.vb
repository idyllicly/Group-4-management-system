Public Class FormDayDetails
    Private _selectedDate As DateTime

    ' Constructor that accepts the date
    Public Sub New(dateClicked As DateTime)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _selectedDate = dateClicked

        ' --- FIX: CENTER THE POPUP ON SCREEN ---
        Me.StartPosition = FormStartPosition.CenterScreen
        ' ---------------------------------------
    End Sub

    Private Sub FormDayDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 1. Set Header
        lblDateHeader.Text = _selectedDate.ToString("MMMM dd, yyyy")

        ' 2. Clear List
        pnlList.Controls.Clear()

        ' 3. Load Data
        LoadJobs()
        LoadInspections()

        ' 4. Handle "Empty" State
        If pnlList.Controls.Count = 0 Then
            Dim lbl As New Label()
            lbl.Text = "No events scheduled for this day."
            lbl.AutoSize = True
            lbl.Padding = New Padding(20)
            pnlList.Controls.Add(lbl)
        End If
    End Sub

    Private Sub LoadJobs()
        Dim evtManager As New EventManager()
        Dim dt As DataTable = evtManager.GetJobsForDate(_selectedDate)

        For Each row As DataRow In dt.Rows
            Dim card As New UC_JobCard()
            ' Format time nicely (e.g., 13:00 -> 01:00 PM)
            Dim timeStr As String = DateTime.Parse(row("ScheduleTime").ToString()).ToString("hh:mm tt")

            card.SetData(row("ServiceInclusion").ToString(), "Time: " & timeStr, Color.SkyBlue)
            pnlList.Controls.Add(card)
        Next
    End Sub

    Private Sub LoadInspections()
        Dim evtManager As New EventManager()
        Dim dt As DataTable = evtManager.GetInspectionsForDate(_selectedDate)

        For Each row As DataRow In dt.Rows
            Dim card As New UC_JobCard()
            Dim timeStr As String = DateTime.Parse(row("VisitTime").ToString()).ToString("hh:mm tt")

            card.SetData("Inspection: " & row("IRemarks").ToString(), "Time: " & timeStr, Color.Orange)
            pnlList.Controls.Add(card)
        Next
    End Sub
End Class