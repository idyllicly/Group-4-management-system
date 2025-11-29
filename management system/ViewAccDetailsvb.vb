Public Class ViewAccDetailsvb
    ' *** REQUIRED PROPERTY FOR REDIRECTION ***
    ' This Public Shared property allows the ManageAccounts form to pass the ID.
    Public Shared TargetUserID As Integer

    ' ... rest of your existing code and designer components ...

    Private Sub ViewAccDetailsvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' This code runs when the form loads.
        If TargetUserID > 0 Then
            ' Load the specific account details using the ID passed from ManageAccounts
            MessageBox.Show("Loading details for Account ID: " & TargetUserID.ToString())
            ' TODO: Implement DB query to fetch and display the user's details here
        Else
            MessageBox.Show("Error: No Account ID was provided.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

End Class