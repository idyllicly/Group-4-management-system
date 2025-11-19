Public Class TechnicianCard
    ' 1. Create a custom event that the Main Form can listen to
    Public Event CardClicked(technicianName As String)

    ' 2. When the user clicks the UserControl background
    Private Sub TechnicianCard_Click(sender As Object, e As EventArgs) Handles Me.Click
        ' Trigger the event and pass the name (or ID)
        RaiseEvent CardClicked(LblName.Text)
    End Sub

    ' Note: You might want to add this same Click event to the PictureBox and Label
    ' inside the UserControl so it works even if they click the text/image.
End Class