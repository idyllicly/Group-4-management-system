Public Class InquiryPage

    Private Sub btnAssignTech_Click(sender As Object, e As EventArgs) Handles btnAssignTech.Click
        ' Use Show() and Hide() for SelectTechPage if it's a main screen. 
        ' If it's a small pop-up, you can still use ShowDialog().
        SelectTechPage.Show()
        Me.Hide()
    End Sub

    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
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
        End If

        ' --- END OF CONFIRMATION DIALOG LOGIC ---
    End Sub

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
            ElseIf ctrl.Controls.Count > 0 Then
                ' Recursive call to clear controls inside containers
                ClearAllInputControls(ctrl)
            End If
        Next
    End Sub
End Class