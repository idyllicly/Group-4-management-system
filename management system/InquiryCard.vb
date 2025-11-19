Public Class InquiryCard
    Private Sub InquiryCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    ' 1. Create Properties so we can set data from the Dashboard
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

    ' Optional: Change color based on urgency or type
    Public Sub SetColor(c As Color)
        Me.BackColor = c
    End Sub
End Class
