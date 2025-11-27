Public Class log_in
    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim nextFrom As New Dashboard()
        nextFrom.Show()


        ' Hide the current form (Form1)
        Me.Hide()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click

    End Sub

    Private Sub log_in_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    ' Inside log_in.vb

    Private Sub OvalButton1_Click(sender As Object, e As EventArgs) Handles OvalButton1.Click
        ' 1. Validate inputs
        If String.IsNullOrWhiteSpace(UserText.Text) Or String.IsNullOrWhiteSpace(PassText.Text) Then
            MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Create instance of our AccountManager
        Dim accManager As New AccountManager()

        ' 3. Attempt Login
        Dim isSuccess As Boolean = accManager.ValidateLogin(UserText.Text, PassText.Text)

        If isSuccess Then
            MessageBox.Show("Login Successful! Welcome.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' HIDE current login form
            Me.Hide()

            ' SHOW Main Menu (Assuming you have a Form named MainMenu)
            Dim mainForm As New FormCalendar()
            mainForm.Show()
        Else
            MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

End Class