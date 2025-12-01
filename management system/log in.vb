Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class log_in

    ' Connection string based on your local settings
    Private Const MyConnectionString As String = "Server=localhost;Database=db_rrcms;Uid=root;Pwd=;"

    Private Sub OvalButton1_Click(sender As Object, e As EventArgs) Handles OvalButton1.Click

        ' 1. Validation
        If UserText Is Nothing OrElse PassText Is Nothing Then
            MessageBox.Show("Internal Error: Textboxes not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim username As String = UserText.Text.Trim()
        Dim password As String = PassText.Text.Trim()

        If String.IsNullOrWhiteSpace(username) Or String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim userRole As String = String.Empty

        ' 2. Corrected Query to match your tbl_account columns
        Dim query As String = "SELECT Role FROM tbl_account WHERE Username = @user AND Password = @pass"

        Try
            Using con As New MySqlConnection(MyConnectionString)
                con.Open()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@user", username)
                    cmd.Parameters.AddWithValue("@pass", password)

                    Dim result = cmd.ExecuteScalar()

                    If result IsNot Nothing Then
                        userRole = result.ToString()
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Catch ex As Exception
            MessageBox.Show("Unexpected Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' 3. Login Success Logic
        If Not String.IsNullOrEmpty(userRole) Then
            ' Hide Login Form
            Me.Hide()

            ' Open Main Form
            Dim mainForm As New frm_Main()
            mainForm.Show()
        Else
            MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Keep your existing Load and Focus logic
    Private Sub log_in_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If OvalButton1 IsNot Nothing Then
            Me.AcceptButton = OvalButton1
        End If
        Me.BeginInvoke(New Action(AddressOf SetInitialFocus))
    End Sub

    Private Sub SetInitialFocus()
        If UserText IsNot Nothing Then UserText.Focus()
    End Sub

End Class