Imports System.Windows.Forms
Imports System.Drawing

Public Class TimelineItemControl

    ' Property to hold the Job ID
    Public Property JobID As Integer = 0

    Public Sub New()
        InitializeComponent()
    End Sub

    ' Updated Populate to include Job ID
    Public Sub Populate(ByVal id As Integer, ByVal title As String, ByVal description As String, ByVal eventDate As DateTime)
        Me.JobID = id ' Store the ID
        lblTitle.Text = title
        lblDescription.Text = description
        lblDate.Text = eventDate.ToString("MMM dd, yyyy")
    End Sub

    ' --- Helper to set the background color ---
    Public Sub SetColor(c As Color)
        Me.BackColor = c
        If c.GetBrightness() < 0.5 Then
            lblTitle.ForeColor = Color.White
            lblDescription.ForeColor = Color.White
            lblDate.ForeColor = Color.White
        Else
            lblTitle.ForeColor = Color.Black
            lblDescription.ForeColor = Color.Black
            lblDate.ForeColor = Color.Black
        End If
    End Sub

    ' --- CLICK EVENT: Open the Details Form ---
    ' This handles clicks on the UserControl itself AND its labels so it feels responsive everywhere
    Private Sub TimelineItemControl_Click(sender As Object, e As EventArgs) Handles MyBase.Click, lblTitle.Click, lblDescription.Click, lblDate.Click
        If JobID > 0 Then
            ' Create the details form
            Dim detailsForm As New FormJobDetails()

            ' Pass the ID to the form (Assuming FormJobDetails has a Public Property TargetJobID)
            detailsForm.TargetJobID = Me.JobID

            ' Show it as a dialog
            detailsForm.ShowDialog()
        End If
    End Sub

End Class