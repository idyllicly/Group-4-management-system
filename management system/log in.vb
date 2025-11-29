Imports System.Windows.Forms
Imports MySql.Data.MySqlClient ' Correct, non-ambiguous import

Public Class log_in

    Private Const MyConnectionString As String = "Server=localhost;Database=db_rrcms;Uid=root;Pwd=;"

    ' ----------------------------------------------------------------------
    ' | 1. EVENT HANDLER FOR THE LOGIN BUTTON
    ' ----------------------------------------------------------------------
    Private Sub OvalButton1_Click(sender As Object, e As EventArgs) Handles OvalButton1.Click

        If UserText Is Nothing OrElse PassText Is Nothing Then
            MessageBox.Show("Internal Error: Username or Password fields failed to load.", "Component Missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim username As String = UserText.Text.Trim()
        Dim password As String = PassText.Text.Trim()

        If String.IsNullOrWhiteSpace(username) Or String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim accountType As String = String.Empty
        Dim query As String = "SELECT AccType FROM tbl_account WHERE AUsername = @user AND APassword = @pass"

        Try
            Using con As New MySqlConnection(MyConnectionString)
                con.Open()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@user", username)
                    cmd.Parameters.AddWithValue("@pass", password)

                    Dim result = cmd.ExecuteScalar()

                    If result IsNot Nothing Then
                        accountType = result.ToString()
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show($"Database connection or query error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return

        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred during login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        If Not String.IsNullOrEmpty(accountType) Then

            Me.Hide()
            Dim mainForm As New FormCalendar()
            mainForm.Show()
        Else
            MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' ----------------------------------------------------------------------
    ' | 2. FORM LOAD EVENT
    ' ----------------------------------------------------------------------
    Private Sub log_in_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If OvalButton1 IsNot Nothing Then
            Me.AcceptButton = OvalButton1
        End If

        If UserText IsNot Nothing Then
            Me.BeginInvoke(New Action(AddressOf SetInitialFocus))
        End If
    End Sub

    Private Sub SetInitialFocus()
        If UserText IsNot Nothing Then
            UserText.Focus()
        End If
    End Sub

    ' ----------------------------------------------------------------------
    ' | 3. CLEANUP: Other Handlers
    ' ----------------------------------------------------------------------

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim nextFrom As New Dashboard()
        nextFrom.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
    End Sub

End Class