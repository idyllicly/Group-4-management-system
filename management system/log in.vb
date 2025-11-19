Public Class log_in
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim homepage As New Calenndar()

        ' Show the new form
        homepage.Show()

        ' Hide the current form (Form1)
        Me.Hide()
    End Sub
End Class