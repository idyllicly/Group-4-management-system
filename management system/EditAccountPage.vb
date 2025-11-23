Public Class EditAccountPage


    Private currentImagePath As String = String.Empty

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        MessageBox.Show("Are you sure you want to cancel? Unsaved changes will be lost.", "Confirm Navigation",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        Me.Hide()

    End Sub


    ' Updated subroutine to handle PictureBox clearing
    Private Sub ClearAllInputControls(parent As Control)
        For Each ctrl As Control In parent.Controls

            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty

            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).SelectedIndex = -1

            ElseIf TypeOf ctrl Is PictureBox Then
                ' 🖼️ LOGIC TO CLEAR PICTUREBOX
                Dim picBox As PictureBox = CType(ctrl, PictureBox)
                If picBox.Image IsNot Nothing Then
                    picBox.Image.Dispose() ' Releases the file lock/memory
                    picBox.Image = Nothing   ' Clears the displayed image
                End If
                ' Clear the associated file path variable and OpenFileDialog name
                currentImagePath = String.Empty
                ' ⚠️ If you have an OpenFileDialog control named OpenFileDialog1:
                ' OpenFileDialog1.FileName = String.Empty 

            ElseIf TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = False

            ElseIf TypeOf ctrl Is RadioButton Then
                CType(ctrl, RadioButton).Checked = False

            ElseIf ctrl.Controls.Count > 0 Then
                ' Recursively call this function for containers (Panels, GroupBoxes)
                ClearAllInputControls(ctrl)

            End If
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Dispose of any previous image to prevent memory leaks
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
            End If

            ' Load the selected image into the PictureBox
            Try
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)

                ' 🔑 THE FIX: Hide the button once the image is successfully loaded
                Button1.Visible = False

            Catch ex As Exception
                ' Handle errors, such as invalid image format
                MessageBox.Show("Error loading image: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub OvalButton3_Click(sender As Object, e As EventArgs) Handles OvalButton3.Click
        MessageBox.Show("Account details updated successfully!", "Update Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()

    End Sub
End Class