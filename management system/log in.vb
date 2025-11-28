Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports MySqlConnector

Public Class log_in
    ' IMPORTANT: You MUST update this with your actual MySQL connection string details!
    ' This string is now defined within the log_in class, as requested.
    Private Const MyConnectionString As String = "Server=localhost;Database=db_rrcms;Uid=root;Pwd=;"

    ' NOTE: Assuming OvalButton1 is your Login Button, UserText is Username TextBox, 
    ' and PassText is Password TextBox.

    ' *** 1. EVENT HANDLER FOR THE LOGIN BUTTON (CONTAINS ALL LOGIC) ***
    Private Sub OvalButton1_Click(sender As Object, e As EventArgs) Handles OvalButton1.Click

        ' CRITICAL SAFETY CHECK: Ensure controls exist before accessing them
        If UserText Is Nothing OrElse PassText Is Nothing Then
            MessageBox.Show("Internal Error: Username or Password fields failed to load.", "Component Missing", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' 1. Validate inputs
        Dim username As String = UserText.Text.Trim()
        Dim password As String = PassText.Text.Trim()

        If String.IsNullOrWhiteSpace(username) Or String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim accountType As String = String.Empty
        Dim query As String = "SELECT AccType FROM tbl_account WHERE AUsername = @user AND APassword = @pass"

        ' 2. Attempt Login using real database validation
        Try
            Using con As New MySqlConnection(MyConnectionString)
                con.Open()
                Using cmd As New MySqlCommand(query, con)
                    ' Always use parameters to prevent SQL injection attacks.
                    cmd.Parameters.AddWithValue("@user", username)
                    cmd.Parameters.AddWithValue("@pass", password)

                    Dim result = cmd.ExecuteScalar()

                    If result IsNot Nothing Then
                        ' Login successful: store the account type
                        accountType = result.ToString()
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            ' Handle database connection or query errors
            MessageBox.Show($"Database connection or query error: Could not connect to the database or execute query. Check your MyConnectionString. Details: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return

        Catch ex As Exception
            ' Handle all other exceptions
            MessageBox.Show($"An unexpected error occurred during login: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' 3. Handle result
        If Not String.IsNullOrEmpty(accountType) Then
            ' Login successful
            MessageBox.Show($"Login Successful! Welcome, {accountType}.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' HIDE current login form
            Me.Hide()

            ' SHOW Main Menu (Assuming you have a Form named FormCalendar)
            Dim mainForm As New FormCalendar()
            mainForm.Show()
        Else
            ' Login failed
            MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' *** 2. FORM LOAD EVENT: SETS ENTER KEY ACTION AND INITIAL FOCUS ***
    Private Sub log_in_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' This ensures the Enter key triggers the login button.
        If OvalButton1 IsNot Nothing Then
            Me.AcceptButton = OvalButton1
        Else
            System.Diagnostics.Debug.WriteLine("ERROR: OvalButton1 control is Nothing and cannot be set as AcceptButton.")
        End If

        ' FIX: Use BeginInvoke to ensure focus is set AFTER the form is fully rendered.
        If UserText IsNot Nothing Then
            Me.BeginInvoke(New Action(AddressOf SetInitialFocus))
        End If
    End Sub

    ' Helper method to be called via BeginInvoke
    Private Sub SetInitialFocus()
        If UserText IsNot Nothing Then
            UserText.Focus()
        End If
    End Sub

    ' *** 3. CLEANUP: Other Handlers ***

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ' Assuming Button1 is a navigation button
        Dim nextFrom As New Dashboard()
        nextFrom.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        ' Placeholder for PictureBox click event
    End Sub

End Class