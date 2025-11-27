Imports MySql.Data.MySqlClient

Public Class EventManager
    Inherits DatabaseConnection

    ' Returns a list of days in a specific month/year that have jobs scheduled
    Public Function GetJobDays(month As Integer, year As Integer) As List(Of Integer)
        Dim eventDays As New List(Of Integer)()

        ' We look at tbl_job for ScheduleDate
        Dim sql As String = "SELECT DAY(ScheduleDate) as DayNum FROM tbl_job " &
                            "WHERE MONTH(ScheduleDate) = @m AND YEAR(ScheduleDate) = @y"

        Dim params As New Dictionary(Of String, Object)
        params.Add("@m", month)
        params.Add("@y", year)

        Dim dt As DataTable = ExecuteSelect(sql, params)

        For Each row As DataRow In dt.Rows
            eventDays.Add(Convert.ToInt32(row("DayNum")))
        Next

        Return eventDays
    End Function

    ' You can add a similar function for Inspections if you want different colored dots
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

    Public Function GetJobsForDate(targetDate As DateTime) As DataTable
        Dim sql As String = "SELECT j.ScheduleTime, s.ServiceInclusion " &
                            "FROM tbl_job j " &
                            "JOIN tbl_service s ON j.ServiceID = s.ServiceID " &
                            "WHERE j.ScheduleDate = @dt"

        Dim params As New Dictionary(Of String, Object)
        params.Add("@dt", targetDate.ToString("yyyy-MM-dd"))

        Return ExecuteSelect(sql, params)
    End Function

    ' NEW: Get detailed list of INSPECTIONS for a specific date
    Public Function GetInspectionsForDate(targetDate As DateTime) As DataTable
        Dim sql As String = "SELECT VisitTime, IRemarks FROM tbl_inspection " &
                            "WHERE VisitDate = @dt"

        Dim params As New Dictionary(Of String, Object)
        params.Add("@dt", targetDate.ToString("yyyy-MM-dd"))

        Return ExecuteSelect(sql, params)
    End Function
End Class