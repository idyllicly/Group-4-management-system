Public Class TechnicianCard

    ' 1. Custom Event definition
    Public Event CardClicked(technicianName As String)

    ' 2. Public Sub to allow the Main Form to set the name displayed on the card
    Public Sub SetTechnicianName(ByVal name As String)
        ' Assuming the Label inside your UserControl that displays the name is named 'LblName'
        If Me.LblName IsNot Nothing Then
            Me.LblName.Text = name
        End If
    End Sub

    ' 3. Trigger the event when the user clicks the UserControl background
    Private Sub TechnicianCard_Click(sender As Object, e As EventArgs) Handles Me.Click
        RaiseEvent CardClicked(LblName.Text)
    End Sub

    ' 4. Trigger the event if the user clicks the Label
    ' NOTE: LblName must exist on the designer and be named LblName
    Private Sub LblName_Click(sender As Object, e As EventArgs) Handles LblName.Click
        RaiseEvent CardClicked(LblName.Text)
        ' *** CODE DISABLED: This line caused the duplicate, floating form bug. ***
        ' OpenSelectTechnicianCard()
    End Sub

    ' 5. Trigger the event if the user clicks the PictureBox
    ' NOTE: PictureBoxTechnician must exist on the designer and be declared WithEvents
    Private Sub PictureBoxTechnician_Click(sender As Object, e As EventArgs) Handles PictureBoxTechnician.Click
        RaiseEvent CardClicked(LblName.Text)
        ' *** CODE DISABLED: This line caused the duplicate, floating form bug. ***
        ' OpenSelectTechnicianCard()
    End Sub

    ' *** CODE DISABLED: This handler is redundant and causes conflicts. ***
    Private Sub TechnicianCard1_Click(sender As Object, e As EventArgs) Handles Me.Click
        ' OpenSelectTechnicianCard()
    End Sub

    ' *** CODE DISABLED: This entire method implements direct navigation, which conflicts 
    ' *** with the Panel Embedding system managed by the parent form.
    Private Sub OpenSelectTechnicianCard()
        ' Create a new instance of the target form
        ' Dim selectForm As New SelectTechnicianCard()

        ' Show the new form
        ' selectForm.Show()
        ' Me.Hide()

        ' OPTIONAL: If the current form should close, you can use Me.Hide() 
        ' or Me.Close() depending on your application's structure. 
        ' However, this is usually done in the parent form.
    End Sub

End Class