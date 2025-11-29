Public Class InquiryPage

    ' --- (Other subroutines remain unchanged) ---

    Private Sub btnAssignTech_Click(sender As Object, e As EventArgs) Handles btnAssignTech.Click
        Dim result As MsgBoxResult = MsgBox("Inquiry has been created successfully! Would you like to assign a Technician to this job?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm Assignment")

        If result = MsgBoxResult.Yes Then
            SelectTechPage.Show()
            Me.Hide()
        End If
    End Sub

    Private Sub Clear_Click(sender As Object, e As EventArgs) Handles Clear.Click
        Dim result As MsgBoxResult
        result = MsgBox("Are you sure you want to clear all the inputs? This action cannot be undone.",
                         MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation,
                         "Warning")

        If result = MsgBoxResult.Yes Then
            ClearAllInputControls(Me)
        End If
    End Sub

    ' ⭐ FIX 1: UPDATE CLEARING LOGIC FOR OvalComboBox
    Private Sub ClearAllInputControls(parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty
            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).SelectedIndex = -1
                ' Check for the OvalComboBox type specifically
            ElseIf TypeOf ctrl Is OvalComboBox Then
                ' Set the synchronized .Text property to the default prompt
                CType(ctrl, OvalComboBox).Text = "Select an Item"
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

    ' ⭐ FIX 2: UPDATE LOAD LOGIC TO USE OvalComboBox.Text
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Assuming the instance of your custom control on the form is named OvalComboBox1

        ' 1. Clear any existing items (optional, and good practice before loading)
        OvalComboBox1.Items.Clear()

        ' 2. Add individual options
        OvalComboBox1.Items.Add("Residential")
        OvalComboBox1.Items.Add("Commercial")

        ' 3. Add an array of options
        Dim moreOptions() As String = {}
        OvalComboBox1.Items.AddRange(moreOptions)

        ' 4. Set the default selected text using the standard .Text property
        OvalComboBox1.Text = "Client Businees Area Type"

        ' 5. (Removed unnecessary selected text check, as setting .Text handles the internal state now)

    End Sub

End Class