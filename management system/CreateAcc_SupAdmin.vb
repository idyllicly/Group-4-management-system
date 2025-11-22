
Public Class CreateAcc_SupAdmin
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Dispose of any previous image to prevent memory leaks
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
            End If
            ' Load the selected image into the PictureBox
            Try
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
            Catch ex As Exception
                ' Handle errors, such as invalid image format
                MessageBox.Show("Error loading image: " & ex.Message)
            End Try
        End If
    End Sub
End Class
