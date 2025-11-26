Imports System.Data

' Child Class: Handles specific logic for User Accounts
Public Class AccountManager
    Inherits DatabaseConnection

    ' Function to check login credentials
    Public Function ValidateLogin(username As String, password As String) As Boolean
        ' Use parameters to prevent SQL Injection (security best practice)
        Dim sql As String = "SELECT AccountID FROM tbl_account WHERE AUsername = @user AND APassword = @pass"
        Dim params As New Dictionary(Of String, Object)
        params.Add("@user", username)
        params.Add("@pass", password)

        Dim dt As DataTable = ExecuteSelect(sql, params)

        If dt.Rows.Count > 0 Then
            Return True ' Login Success
        Else
            Return False ' Login Failed
        End If
    End Function

    ' Function to get user details after login
    Public Function GetUserDetails(username As String) As DataRow
        Dim sql As String = "SELECT * FROM tbl_account WHERE AUsername = @user"
        Dim params As New Dictionary(Of String, Object)
        params.Add("@user", username)

        Dim dt As DataTable = ExecuteSelect(sql, params)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        End If
        Return Nothing
    End Function

    ' Helper to quickly get just the Role (AccType)
    Public Function GetAccountRole(username As String) As String
        Dim row As DataRow = GetUserDetails(username)
        If row IsNot Nothing Then
            Return row("AccType").ToString()
        End If
        Return ""
    End Function

    ' Function to create a new account (Restricted to Super Admin)
    Public Function CreateNewAccount(creatorRole As String,
                                     newType As String,
                                     newUsername As String,
                                     newPassword As String,
                                     fName As String,
                                     lName As String,
                                     contact As String) As Boolean

        ' 1. Security Check: Only 'Super Admin' can create accounts
        If creatorRole <> "Super Admin" Then
            MessageBox.Show("Access Denied: Only a Super Admin can create new accounts.", "Permission Error")
            Return False
        End If

        ' 2. Check if username already exists
        Dim checkSql As String = "SELECT AccountID FROM tbl_account WHERE AUsername = @user"
        Dim checkParams As New Dictionary(Of String, Object)
        checkParams.Add("@user", newUsername)
        If ExecuteSelect(checkSql, checkParams).Rows.Count > 0 Then
            MessageBox.Show("That username is already taken.", "Input Error")
            Return False
        End If

        ' 3. Insert Logic (Filling required fields with defaults if empty)
        ' We insert empty strings for MiddleName, Email, FB, Viber as they are NOT NULL in your DB
        Dim sql As String = "INSERT INTO tbl_account " &
                            "(AccType, AUsername, APassword, AFirstName, ALastName, AMiddleName, AEmail, AContactno, AFacebook, AViber) " &
                            "VALUES (@type, @user, @pass, @fname, @lname, '', '', @contact, '', '')"

        Dim params As New Dictionary(Of String, Object)
        params.Add("@type", newType)
        params.Add("@user", newUsername)
        params.Add("@pass", newPassword)
        params.Add("@fname", fName)
        params.Add("@lname", lName)
        params.Add("@contact", contact)

        Dim result As Integer = ExecuteAction(sql, params)
        Return result > 0
    End Function

End Class