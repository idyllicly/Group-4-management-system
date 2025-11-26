Public Class CreateAcc_SupAdmin

    ' Variable to hold the file path
    Private currentImagePath As String = String.Empty

    ' --- 1. IMAGE UPLOAD BUTTON ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ' Dispose of any previous image to prevent memory leaks
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
            End If

            ' Load the selected image into the PictureBox
            Try
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)

                ' Hide the button once the image is successfully loaded
                Button1.Visible = False

            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message)
            End Try
        End If
    End Sub

    Private Sub CreateAcc_SupAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    ' --- 2. CLEAR / BACK BUTTON (THE FIX IS HERE) ---
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim result As MsgBoxResult

        ' Display the confirmation box.
        result = MsgBox("Are you sure you want to clear all the inputs? This action cannot be undone.",
                        MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation,
                        "Warning")

        ' Check if the user clicked the 'Yes' button
        If result = MsgBoxResult.Yes Then
            ' Proceed to clear the inputs
            ClearAllInputControls(Me)

            ' Make the upload button visible again!
            Button1.Visible = True
        End If
    End Sub

    ' --- 3. CLEARING LOGIC ---
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
                ' Logic to clear the PictureBox and dispose of the image.
                Dim picBox As PictureBox = CType(ctrl, PictureBox)
                If picBox.Image IsNot Nothing Then
                    picBox.Image.Dispose()
                    picBox.Image = Nothing
                End If

                ' Reset variables
                currentImagePath = String.Empty
                OpenFileDialog1.FileName = String.Empty

            ElseIf ctrl.Controls.Count > 0 Then
                ' Recursively call this function for containers (Panels, GroupBoxes)
                ClearAllInputControls(ctrl)

            End If
        Next
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint
    End Sub

    ' --- 4. SUBMIT / NAVIGATION BUTTON (MODIFIED TO CLEAR INPUTS) ---
    Private Sub OvalButton3_Click(sender As Object, e As EventArgs) Handles OvalButton3.Click

        Dim confirmResult As DialogResult = MessageBox.Show("Are you sure that all of the details are correct?",
                                                     "Confirm Submission",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Warning)

        If confirmResult = DialogResult.Yes Then
            ' Account creation logic would typically go here (e.g., saving data)

            ' Show the success message.
            Dim successResult As DialogResult = MessageBox.Show("Account has been created succesfully!",
                                                                "Account Creation",
                                                                MessageBoxButtons.OK,
                                                                MessageBoxIcon.Information)

            ' If the user clicks OK on the success message, clear the form inputs.
            If successResult = DialogResult.OK Then
                ' Clear all inputs on the form
                ClearAllInputControls(Me)

                ' Make the image upload button visible again
                Button1.Visible = True
            End If

        End If

    End Sub

    Private Sub NavigationControl1_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub SideNavControl1_Load(sender As Object, e As EventArgs)

    End Sub
End Class