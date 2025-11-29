Public Class InquiryCard
    ' 1. New Property to store the Database ID
    Public Property JobID As Integer

    ' 2. Existing Properties
    Public Property CustomerName As String
        Get
            Return lblName.Text
        End Get
        Set(value As String)
            lblName.Text = value
        End Set
    End Property

    Public Property InquiryDetails As String
        Get
            Return lblDetails.Text
        End Get
        Set(value As String)
            lblDetails.Text = value
        End Set
    End Property

    Public Property DateDate As String
        Get
            Return lblDate.Text
        End Get
        Set(value As String)
            lblDate.Text = value
        End Set
    End Property

    Public Sub SetColor(c As Color)
        Me.BackColor = c
    End Sub

    ' 3. CLICK EVENT: This opens the details form
    ' We handle clicks on the Card itself AND the labels inside it
    Private Sub Card_Click(sender As Object, e As EventArgs) Handles MyBase.Click, lblName.Click, lblDetails.Click, lblDate.Click
        If JobID > 0 Then
            ' Create the details form
            Dim detailsForm As New FormJobDetails()
            ' Pass the ID to the form
            detailsForm.TargetJobID = Me.JobID
            ' Show it
            detailsForm.ShowDialog()
        End If
    End Sub
End Class