
Imports System.Linq
Imports System.Windows.Forms

Public Class timeline_page

    Private Sub timeline_page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTimelineData()
    End Sub

    Private Sub LoadTimelineData()
        ' --- 1. Get and sort your data ---
        ' Make sure the data is sorted by date for the grouping to be in order
        Dim timelineData = GetDataFromDatabase().OrderBy(Function(item) item.EventDate).ToList()

        ' --- 2. Group the data by month and year ---
        ' This is the key! It creates groups like "October 2025", "November 2025"
        Dim groupedData = timelineData.GroupBy(Function(item) item.EventDate.ToString("MMMM yyyy"))

        ' --- 3. Clear the main panel ---
        MainTimelinePanel.Controls.Clear() ' Assuming your panel is named MainTimelinePanel

        ' --- 4. Loop through each MONTH group ---
        For Each monthGroup In groupedData
            ' --- A. Create the Month Label (e.g., "October 2025") ---
            Dim monthLabel As New Label()
            monthLabel.Text = monthGroup.Key ' The Key is the month/year string
            monthLabel.Font = New Font("Arial", 16, FontStyle.Bold)
            monthLabel.AutoSize = True
            monthLabel.Margin = New Padding(10, 20, 10, 10) ' Give it space
            MainTimelinePanel.Controls.Add(monthLabel)

            ' --- B. Create a NEW horizontal panel for this month's jobs ---
            Dim monthItemsPanel As New FlowLayoutPanel()
            monthItemsPanel.FlowDirection = FlowDirection.LeftToRight ' Stack jobs side-by-side
            monthItemsPanel.WrapContents = True ' Let them wrap to the next line
            monthItemsPanel.AutoSize = True ' Let panel grow as tall as needed
            monthItemsPanel.MaximumSize = New Size(MainTimelinePanel.Width - 25, 0) ' Fit parent width
            monthItemsPanel.MinimumSize = New Size(MainTimelinePanel.Width - 25, 100) ' Set a min width

            ' --- C. Loop through all JOBS in this month ---
            For Each itemData In monthGroup
                ' Create your blue box template
                Dim newItem As New TimelineItemControl()
                newItem.Populate(itemData.Title, itemData.Details, itemData.EventDate)

                ' Add the job box to the HORIZONTAL panel
                monthItemsPanel.Controls.Add(newItem)
            Next

            ' --- D. Add the new horizontal panel to the MAIN vertical panel ---
            MainTimelinePanel.Controls.Add(monthItemsPanel)
        Next
    End Sub

    ' (Your existing GetDataFromDatabase and TimelineEvent class are here)
    ' ...
#Region "Database and Data Class Stub"
    Private Function GetDataFromDatabase() As List(Of TimelineEvent)
        Dim fakeList As New List(Of TimelineEvent)
        fakeList.Add(New TimelineEvent("Job in progress", "Fix the login bug", New Date(2025, 10, 1)))
        fakeList.Add(New TimelineEvent("Follow-up Job", "Call the client", New Date(2025, 10, 5)))
        fakeList.Add(New TimelineEvent("New Feature", "Add timeline", New Date(2025, 10, 15))) ' 3rd item for Oct
        fakeList.Add(New TimelineEvent("Completed Job", "Database migration", New Date(2025, 11, 5)))
        fakeList.Add(New TimelineEvent("Follow-up Job", "Send invoice", New Date(2025, 11, 6)))
        Return fakeList
    End Function

    Public Class TimelineEvent
        Public Property Title As String
        Public Property Details As String
        Public Property EventDate As Date
        Public Sub New(t As String, d As String, dt As Date)
            Title = t
            Details = d
            EventDate = dt
        End Sub
    End Class
#End Region

End Class