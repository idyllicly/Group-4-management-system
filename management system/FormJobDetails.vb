Imports System.Drawing

Public Class FormJobDetails
    ' Property to receive the ID
    Public Property TargetJobID As Integer = 0

    Private Sub FormJobDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If TargetJobID > 0 Then
            LoadJobData()
        End If
    End Sub

    Private Sub LoadJobData()
        Dim evtManager As New EventManager()
        Dim row As DataRow = evtManager.GetFullJobDetails(TargetJobID)

        If row IsNot Nothing Then
            ' Map Database Columns to your Labels

            ' Label 1: Client Name
            clientName.Text = "Client: " & row("CFirstName").ToString() & " " & row("CLastName").ToString()

            ' Label 2: Job Type
            jobType.Text = "Service: " & row("ServiceInclusion").ToString()

            ' Label 3: Address (From tbl_client)
            address.Text = "Address: " & row("Address").ToString()

            ' Label 4: Contact No
            contact.Text = "Contact: " & row("ClientNo").ToString()

            ' Label 5: Scheduled Date
            Dim dateVal As DateTime = Convert.ToDateTime(row("ScheduleDate"))
            scheduledDate.Text = "Date: " & dateVal.ToString("MMMM dd, yyyy")

            ' Label 6: Scheduled Time
            Dim timeObj As DateTime = DateTime.Parse(row("ScheduleTime").ToString())
            scheduledTime.Text = "Time: " & timeObj.ToString("hh:mm tt")

            ' Label 8: Description Text
            descriptionText.Text = row("JobRemarks").ToString()

            ' Label 7 is just the header "Description", so we leave it alone or set it:
            description.Text = "Description:"

            ' 2. NEW: Status Label Logic
            Dim status As String = row("JobStatus").ToString()

            ' Update the Status Label Text
            lblStatus.Text = status.ToUpper()

            ' Get Color from EventManager Helper
            lblStatus.BackColor = evtManager.GetStatusColor(status)

            ' Adjust text color for readability
            If status.ToLower() = "pending" Or status.ToLower() = "assigned (pending)" Then
                lblStatus.ForeColor = Color.Black
            Else
                lblStatus.ForeColor = Color.White
            End If

            ' --- 3. NEW: Button Visibility Logic ---

            ' A. "Assign Technician" Button (Button5)
            ' Hide if job is already assigned, active, completed, or cancelled.
            ' Show ONLY if status is 'Pending' or 'Follow Up' (and unassigned)
            If status.ToLower() = "pending" Or status.ToLower() = "follow up" Then
                btnAssign.Visible = True
            Else
                btnAssign.Visible = False
            End If

            ' B. "Revisit" Button (Button1)
            ' Show ONLY if job is Completed.
            If status.ToLower() = "completed" Then
                btnRevisit.Visible = True
            Else
                btnRevisit.Visible = False
            End If

        Else
            MessageBox.Show("Job details not found!")
            Me.Close()
        End If
    End Sub

    ' Keep your Assign Technician click event
    Private Sub btnAssign_Click(sender As Object, e As EventArgs) Handles btnAssign.Click
        Dim selectPage As New SelectTechPage()
        selectPage.TargetJobID = Me.TargetJobID ' Pass the current Job ID
        selectPage.ShowDialog()

        ' Reload data after assignment to show new status and update button visibility
        LoadJobData()
    End Sub

    ' You can add logic for the Revisit button (Button1) here later
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnRevisit.Click
        MessageBox.Show("This will create a new Follow-up job based on this one.", "Revisit Feature")
        ' Logic to duplicate this job with "Follow Up" status can go here
    End Sub

End Class