Public Class log_in
    Private Sub Button1_Click(sender As Object, e As EventArgs)


        ' Show the new form


        ' Hide the current form (Form1)
        Me.Hide()
    End Sub

    Private Sub OvalButton1_Click(sender As Object, e As EventArgs) Handles OvalButton1.Click
        If UserText.Text = "Admin" And PassText.Text = "12345" Then

            ' Show the new form

            ' Hide the current form (Form1)
            Me.Hide()
        Else
            MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

    End Sub
End Class