Public Class CreateAcc_SupAdmin

    ' Variable to hold the file path, essential for a complete clear
    Private currentImagePath As String = String.Empty

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

    Private Sub CreateAcc_SupAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialization logic, if any
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ' --- START OF CONFIRMATION DIALOG LOGIC ---

        Dim result As MsgBoxResult

        ' Display the confirmation box.
        result = MsgBox("Are you sure you want to clear all the inputs? This action cannot be undone.",
                        MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation,
                        "Warning")

        ' Check if the user clicked the 'Yes' button
        If result = MsgBoxResult.Yes Then
            ' If the user confirms, then proceed to clear the inputs
            ClearAllInputControls(Me)
        Else
            ' If the user clicks 'No' or closes the box, do nothing.
        End If

        ' --- END OF CONFIRMATION DIALOG LOGIC ---
    End Sub

    ' Updated subroutine to handle PictureBox clearing
    Private Sub ClearAllInputControls(parent As Control)
        For Each ctrl As Control In parent.Controls

            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty

            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).SelectedIndex = -1

            ElseIf TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = False

            ElseIf TypeOf ctrl Is RadioButton Then
                CType(ctrl, RadioButton).Checked = False

            ElseIf TypeOf ctrl Is PictureBox Then
                ' 🖼️ NEW: Logic to clear the PictureBox and dispose of the image.
                Dim picBox As PictureBox = CType(ctrl, PictureBox)
                If picBox.Image IsNot Nothing Then
                    picBox.Image.Dispose() ' Important to release file lock/memory
                    picBox.Image = Nothing
                End If
                ' Also clear the associated file path variable
                currentImagePath = String.Empty
                OpenFileDialog1.FileName = String.Empty ' Optionally clear the OFD's remembered file name

            ElseIf ctrl.Controls.Count > 0 Then
                ' Recursively call this function for containers
                ClearAllInputControls(ctrl)

            End If
        Next
    End Sub
End Class