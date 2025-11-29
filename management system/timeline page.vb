Imports System.Linq
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing

Public Class timeline_page
    Dim db As New DatabaseConnection()
    Dim evtManager As New EventManager()

    ' Updated Data Class to include ID
    Public Class TimelineData
        Public Property JobID As Integer ' Added JobID
        Public Property Title As String
        Public Property ClientName As String
        Public Property EventDate As DateTime
        Public Property Status As String
    End Class

    Private Sub timeline_page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTimelineData()
    End Sub

    Private Sub LoadTimelineData()
        Dim timelineList As New List(Of TimelineData)()

        ' Updated Query to fetch JobID
        Dim sql As String = "SELECT j.JobID, j.ScheduleDate, j.JobStatus, s.ServiceInclusion, c.CFirstName, c.CLastName " &
                            "FROM tbl_job j " &
                            "JOIN tbl_client c ON j.ClientID = c.ClientID " &
                            "JOIN tbl_service s ON j.ServiceID = s.ServiceID " &
                            "ORDER BY j.ScheduleDate ASC"

        Dim dt As DataTable = db.ExecuteSelect(sql)

        For Each row As DataRow In dt.Rows
            Dim dateVal As DateTime
            If DateTime.TryParse(row("ScheduleDate").ToString(), dateVal) Then
                Dim item As New TimelineData()
                item.JobID = Convert.ToInt32(row("JobID")) ' Get ID
                item.EventDate = dateVal
                item.Title = row("ServiceInclusion").ToString()
                item.ClientName = row("CFirstName").ToString() & " " & row("CLastName").ToString()
                item.Status = row("JobStatus").ToString()

                timelineList.Add(item)
            End If
        Next

        Dim groupedData = timelineList.GroupBy(Function(item) item.EventDate.ToString("MMMM yyyy"))

        MainTimelinePanel.Controls.Clear()

        For Each monthGroup In groupedData
            Dim monthLabel As New Label()
            monthLabel.Text = monthGroup.Key.ToUpper()
            monthLabel.Font = New Font("Arial", 16, FontStyle.Bold)
            monthLabel.ForeColor = Color.DarkSlateBlue
            monthLabel.AutoSize = True
            monthLabel.Margin = New Padding(20, 30, 10, 10)
            MainTimelinePanel.Controls.Add(monthLabel)

            Dim monthItemsPanel As New FlowLayoutPanel()
            monthItemsPanel.FlowDirection = FlowDirection.LeftToRight
            monthItemsPanel.WrapContents = True
            monthItemsPanel.AutoSize = True
            monthItemsPanel.MaximumSize = New Size(MainTimelinePanel.Width - 40, 0)
            monthItemsPanel.Margin = New Padding(20, 0, 0, 20)

            For Each job In monthGroup
                Dim newItem As New TimelineItemControl()

                ' Pass the JobID here!
                newItem.Populate(job.JobID, job.Title, "Client: " & job.ClientName, job.EventDate)

                newItem.SetColor(evtManager.GetStatusColor(job.Status))
                newItem.Margin = New Padding(0, 0, 15, 15)

                monthItemsPanel.Controls.Add(newItem)
            Next

            MainTimelinePanel.Controls.Add(monthItemsPanel)
        Next
    End Sub

End Class