Public Class EditAccountPage

    Private currentImagePath As String = String.Empty

    ' --- 1. SAVE / CONFIRM EDIT BUTTON (UPDATED LOGIC) ---
    Private Sub OvalButton3_Click(sender As Object, e As EventArgs) Handles OvalButton3.Click

        ' Q1: Ask if details are correct
        Dim confirmResult As DialogResult = MessageBox.Show("Are you sure that all of the details are correct?",
                                                            "Confirm Update",
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question)

        If confirmResult = DialogResult.Yes Then
            ' --- OPTION A: USER SAID YES (Proceed) ---

            ' (Insert your SQL Update code here)

            MessageBox.Show("Account details updated successfully!", "Update Successful",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

            ManageAccounts.Show()
            Me.Hide()

        Else
            ' --- OPTION B: USER SAID NO (Ask to clear) ---

            Dim clearResult As DialogResult = MessageBox.Show("You selected 'No'. Do you want to clear all inputs to start over?",
                                                              "Clear Form?",
                                                              MessageBoxButtons.YesNo,
                                                              MessageBoxIcon.Warning)

            If clearResult = DialogResult.Yes Then
                ' User wants to wipe everything
                ClearAllInputControls(Me)

                ' 🔑 IMPORTANT: Bring back the upload button because the picture is gone now
                Button1.Visible = True
            Else
                ' User said No to clearing. 
                ' Do nothing. They stay on the page to fix small typos.
            End If

        End If
    End Sub

    ' --- 2. BACK BUTTON ---
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel? Unsaved changes will be lost.",
                                                     "Confirm Navigation",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Warning)

        If result = DialogResult.Yes Then
            ClearAllInputControls(Me)
            Button1.Visible = True ' Reset button visibility
            Me.Hide()
            ManageAccounts.Show()
        End If
    End Sub

    ' --- 3. UPLOAD PHOTO BUTTON ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
            End If

            Try
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
                ' Hide button when image is loaded
                Button1.Visible = False
            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message)
            End Try
        End If
    End Sub

    ' --- 4. CLEARING SUBROUTINE ---
    Private Sub ClearAllInputControls(parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty
            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).SelectedIndex = -1
            ElseIf TypeOf ctrl Is PictureBox Then
                Dim picBox As PictureBox = CType(ctrl, PictureBox)
                If picBox.Image IsNot Nothing Then
                    picBox.Image.Dispose()
                    picBox.Image = Nothing
                End If
                currentImagePath = String.Empty
                OpenFileDialog1.FileName = String.Empty
            ElseIf TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = False
            ElseIf TypeOf ctrl Is RadioButton Then
                CType(ctrl, RadioButton).Checked = False
            ElseIf ctrl.Controls.Count > 0 Then
                ClearAllInputControls(ctrl)
            End If
        Next
    End Sub

    Private Sub EditAccountPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

End Class