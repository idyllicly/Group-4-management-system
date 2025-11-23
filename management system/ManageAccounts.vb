Public Class ManageAccounts
    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label5_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs)
        Me.Hide()
        CreateAcc_SupAdmin.Show()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        CreateAcc_SupAdmin.Show()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs)

    End Sub

    ' 🔑 CRITICAL FIX: Add the Handles clause to connect the event from the control instance.
    ' If your control is named something other than AccCard1, replace the name below.
    Private Sub AccCard_ActionRequested(UserID As Integer, Action As String) Handles AccCard1.ActionRequested

        Select Case Action
            Case "Edit"
                ' 📝 Action: Prepare data for EditAccountPage
                ' You must pass the UserID to the edit form here:
                ' EditAccountPage.LoadUser(UserID) 

                Me.Hide()
                EditAccountPage.Show()

            Case "Delete"
                ' 🗑️ Action: Confirm and attempt deletion
                If MessageBox.Show($"Are you sure you want to delete user {UserID}?", "Confirm Delete",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then

                    ' >>> PUT YOUR DATABASE DELETE COMMAND HERE <<<

                    MessageBox.Show($"User {UserID} Deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ' Refresh the list of accounts after deletion

                End If

            Case "View"
                ' 🔎 Action: View details 
                MessageBox.Show($"Showing Details for User ID: {UserID}", "View Details", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End Select

    End Sub

End Class