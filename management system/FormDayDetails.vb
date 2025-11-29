Public Class FormDayDetails
    Private _selectedDate As DateTime

    Public Sub New(dateClicked As DateTime)
        InitializeComponent()
        _selectedDate = dateClicked
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub FormDayDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblDateHeader.Text = _selectedDate.ToString("MMMM dd, yyyy")
        pnlList.Controls.Clear()
        LoadJobs()
        LoadInspections()

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

            Dim timeStr As String = DateTime.Parse(row("ScheduleTime").ToString()).ToString("hh:mm tt")
            Dim id As Integer = Convert.ToInt32(row("JobID"))

            ' --- NEW: Get Dynamic Color based on Status ---
            Dim status As String = row("JobStatus").ToString()
            Dim statusColor As Color = evtManager.GetStatusColor(status)
            ' ----------------------------------------------

            ' Pass the status color to the card
            card.SetData(id, row("ServiceInclusion").ToString(), "Status: " & status, statusColor)

            pnlList.Controls.Add(card)
        Next
    End Sub

    Private Sub LoadInspections()
        Dim evtManager As New EventManager()
        Dim dt As DataTable = evtManager.GetInspectionsForDate(_selectedDate)

        For Each row As DataRow In dt.Rows
            Dim card As New UC_JobCard()
            Dim timeStr As String = DateTime.Parse(row("VisitTime").ToString()).ToString("hh:mm tt")

            ' Inspections get a default color (e.g. LimeGreen)
            card.SetData(0, "Inspection: " & row("IRemarks").ToString(), "Time: " & timeStr, Color.LimeGreen)

            pnlList.Controls.Add(card)
        Next
    End Sub
End Class