Public Class UC_JobCard
    ' Property to hold the ID
    Public Property JobID As Integer = 0

    ' Updated SetData to accept JobID
    Public Sub SetData(id As Integer, title As String, time As String, bgColor As Color)
        Me.JobID = id ' Store the ID

        lblTitle.Text = title
        lblTime.Text = time
        Me.BackColor = bgColor

        ' (Keep your existing styling/centering code here...)
        Me.Size = New Size(350, 75)
        lblTitle.Location = New Point(15, 10)
        lblTime.Location = New Point(15, 35)
    End Sub

    ' CLICK EVENT: Open the Details Form
    Private Sub UC_JobCard_Click(sender As Object, e As EventArgs) Handles MyBase.Click, lblTitle.Click, lblTime.Click
        If JobID > 0 Then
            ' Create the details form and pass the ID
            Dim detailsForm As New FormJobDetails()
            detailsForm.TargetJobID = Me.JobID
            detailsForm.ShowDialog()
        End If
    End Sub
End Class