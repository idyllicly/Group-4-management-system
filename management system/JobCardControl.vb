Public Class JobCardControl

    ' Public properties to set the text from the main form
    Public Property JobName As String
        Get
            Return lblName.Text
        End Get
        Set(value As String)
            lblName.Text = value
        End Set
    End Property

    Public Property JobDate As String
        Get
            Return lblDate.Text
        End Get
        Set(value As String)
            lblDate.Text = value
        End Set
    End Property

    Private Sub JobCardControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    ' Use the designer to add controls:
    ' 1. A Label named 'lblName' for the person/job name.
    ' 2. A Label named 'lblDate' for the date/notes.

    ' Optional: Add color logic or borders here if needed.
End Class