Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class EventManager
    Inherits DatabaseConnection

    ' --- 1. UPDATED COLOR LOGIC (Added "Follow Up") ---
    Public Function GetStatusColor(status As String) As Color
        Select Case status.ToLower().Trim()
            Case "pending"
                Return ColorTranslator.FromHtml("#B0B0B0") ' Light Gray
            Case "assigned (pending)"
                Return ColorTranslator.FromHtml("#FFA500") ' Orange
            Case "assigned (accepted)"
                Return ColorTranslator.FromHtml("#00BFFF") ' Sky Blue
            Case "active"
                Return ColorTranslator.FromHtml("#4169E1") ' Royal Blue
            Case "completed"
                Return ColorTranslator.FromHtml("#2ECC71") ' Green
            Case "follow up"
                Return ColorTranslator.FromHtml("#17A589") ' Teal (New Status)
            Case "denied"
                Return ColorTranslator.FromHtml("#FF4500") ' Red-Orange
            Case "cancelled"
                Return ColorTranslator.FromHtml("#C0392B") ' Dark Red
            Case Else
                Return Color.Gray
        End Select
    End Function

    ' --- 2. Get Colors for Calendar Dots ---
    Public Function GetJobColorsForMonth(month As Integer, year As Integer) As Dictionary(Of Integer, List(Of Color))
        Dim dayColors As New Dictionary(Of Integer, List(Of Color))()

        Dim sql As String = "SELECT DAY(ScheduleDate) as DayNum, JobStatus FROM tbl_job " &
                            "WHERE MONTH(ScheduleDate) = @m AND YEAR(ScheduleDate) = @y"

        Dim params As New Dictionary(Of String, Object)
        params.Add("@m", month)
        params.Add("@y", year)

        Dim dt As DataTable = ExecuteSelect(sql, params)

        For Each row As DataRow In dt.Rows
            Dim d As Integer = Convert.ToInt32(row("DayNum"))
            Dim status As String = row("JobStatus").ToString()
            Dim c As Color = GetStatusColor(status)

            If Not dayColors.ContainsKey(d) Then
                dayColors.Add(d, New List(Of Color)())
            End If
            dayColors(d).Add(c)
        Next
        Return dayColors
    End Function

    ' --- 3. EXISTING FETCH FUNCTIONS ---
    Public Function GetJobsForDate(targetDate As DateTime) As DataTable
        Dim sql As String = "SELECT j.JobID, j.ScheduleTime, j.JobStatus, s.ServiceInclusion " &
                            "FROM tbl_job j " &
                            "JOIN tbl_service s ON j.ServiceID = s.ServiceID " &
                            "WHERE j.ScheduleDate = @dt"

        Dim params As New Dictionary(Of String, Object)
        params.Add("@dt", targetDate.ToString("yyyy-MM-dd"))

        Return ExecuteSelect(sql, params)
    End Function

    Public Function GetFullJobDetails(jobID As Integer) As DataRow
        Dim sql As String = "SELECT j.ScheduleDate, j.ScheduleTime, j.JobRemarks, j.JobStatus, " &
                            "s.ServiceInclusion, " &
                            "c.CFirstName, c.CLastName, c.Address, c.ClientNo " &
                            "FROM tbl_job j " &
                            "JOIN tbl_client c ON j.ClientID = c.ClientID " &
                            "JOIN tbl_service s ON j.ServiceID = s.ServiceID " &
                            "WHERE j.JobID = @id"

        Dim params As New Dictionary(Of String, Object)
        params.Add("@id", jobID)

        Dim dt As DataTable = ExecuteSelect(sql, params)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        End If
        Return Nothing
    End Function

    Public Function GetInspectionDays(month As Integer, year As Integer) As List(Of Integer)
        Dim eventDays As New List(Of Integer)()
        Dim sql As String = "SELECT DAY(VisitDate) as DayNum FROM tbl_inspection " &
                            "WHERE MONTH(VisitDate) = @m AND YEAR(VisitDate) = @y"
        Dim params As New Dictionary(Of String, Object)
        params.Add("@m", month)
        params.Add("@y", year)
        Dim dt As DataTable = ExecuteSelect(sql, params)
        For Each row As DataRow In dt.Rows
            eventDays.Add(Convert.ToInt32(row("DayNum")))
        Next
        Return eventDays
    End Function

    Public Function GetInspectionsForDate(targetDate As DateTime) As DataTable
        Dim sql As String = "SELECT VisitTime, IRemarks FROM tbl_inspection " &
                            "WHERE VisitDate = @dt"
        Dim params As New Dictionary(Of String, Object)
        params.Add("@dt", targetDate.ToString("yyyy-MM-dd"))
        Return ExecuteSelect(sql, params)
    End Function
End Class