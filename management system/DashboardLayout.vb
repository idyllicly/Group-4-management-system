Public Class DashboardLayout

    ' Data structure to hold your job data.
    Private Class JobData
        Public Property Name As String
        Public Property DateNote As String
        Public Property Status As String
    End Class

    ' Form load event
    Private Sub DashboardLayout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeDashboard()
    End Sub

    ' Creates sample job cards and places them into FlowLayoutPanels
    Private Sub InitializeDashboard()

        ' 1. Create the list of test jobs. 
        Dim jobs As New List(Of JobData) From {
            New JobData With {.Name = "Marla", .DateNote = "11/15/2025", .Status = "Progress"},
            New JobData With {.Name = "James", .DateNote = "11/14/2025", .Status = "Progress"},
            New JobData With {.Name = "Sofia", .DateNote = "11/12/2025", .Status = "Progress"},
            New JobData With {.Name = "Tom", .DateNote = "11/11/2025", .Status = "Progress"},
            New JobData With {.Name = "Amelia", .DateNote = "11/10/2025", .Status = "Progress"},
            New JobData With {.Name = "John", .DateNote = "11/09/2025", .Status = "Progress"},
            New JobData With {.Name = "Linda", .DateNote = "11/08/2025", .Status = "Progress"},
            New JobData With {.Name = "Henry", .DateNote = "11/07/2025", .Status = "Progress"},
            New JobData With {.Name = "Karen", .DateNote = "11/06/2025", .Status = "Progress"},
            New JobData With {.Name = "Leo", .DateNote = "11/05/2025", .Status = "Progress"},
            New JobData With {.Name = "Nina", .DateNote = "11/04/2025", .Status = "Progress"},
            New JobData With {.Name = "Oliver", .DateNote = "11/03/2025", .Status = "Progress"},
            New JobData With {.Name = "FollowUp - James", .DateNote = "Needs client signature.", .Status = "FollowUp"},
            New JobData With {.Name = "Completed - Sofia", .DateNote = "Invoicing completed.", .Status = "Completed"},
            New JobData With {.Name = "Rejected - Leo", .DateNote = "Client chose competitor.", .Status = "Rejected"}
        }

        ' 2. Clear panels before adding new cards
        flpProgress.Controls.Clear()
        flpFollowUp.Controls.Clear()
        flpCompleted.Controls.Clear()
        flpRejected.Controls.Clear()

        ' 3. Add cards to appropriate panels
        For Each job In jobs

            Dim card As New JobCardControl()
            card.JobName = job.Name
            card.JobDate = job.DateNote

            Dim parentPanel As FlowLayoutPanel

            Select Case job.Status
                Case "Progress"
                    parentPanel = flpProgress
                Case "FollowUp"
                    parentPanel = flpFollowUp
                Case "Completed"
                    parentPanel = flpCompleted
                Case "Rejected"
                    parentPanel = flpRejected
                Case Else
                    Continue For
            End Select

            parentPanel.Controls.Add(card)

            ' Make card stretch correctly inside the FlowLayoutPanel
            ' We use the helper function logic to ensure consistency
            card.Width = parentPanel.ClientSize.Width - parentPanel.Padding.Left - parentPanel.Padding.Right
        Next
    End Sub

    ' Resize behavior when user adjusts the window or layout
    ' Handles the resize event of your TableLayoutPanel (tblDashboard)
    Private Sub tblDashboard_Resize(sender As Object, e As EventArgs) Handles tblDashboard.Resize
        ResizeJobCards(flpProgress)
        ResizeJobCards(flpFollowUp)
        ResizeJobCards(flpCompleted)
        ResizeJobCards(flpRejected)
    End Sub

    ' Helper to resize all job cards inside a FlowLayoutPanel
    Private Sub ResizeJobCards(ByVal panel As FlowLayoutPanel)
        For Each ctrl As Control In panel.Controls
            If TypeOf ctrl Is JobCardControl Then
                ' Force card width to match the column width
                ctrl.Width = panel.ClientSize.Width - panel.Padding.Left - panel.Padding.Right
            End If
        Next
    End Sub

    Private Sub UserControl11_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub flpProgress_Paint(sender As Object, e As PaintEventArgs) Handles flpProgress.Paint

    End Sub

    Private Sub tblDashboard_Paint(sender As Object, e As PaintEventArgs) Handles tblDashboard.Paint

    End Sub
End Class