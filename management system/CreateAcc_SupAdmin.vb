Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Collections.Generic

Public Class CreateAcc_SupAdmin

    ' Direct instance of DatabaseConnection
    Private ReadOnly db As New DatabaseConnection()

    Private currentImagePath As String = String.Empty

    ' --- Form Initialization ---
    Private Sub CreateAcc_SupAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeAccountTypeComboBox()
    End Sub

    ' --- HELPER: Load Account Types into the ComboBox ---
    Private Sub InitializeAccountTypeComboBox()
        ' Assuming OvalComboBox1 is your ComboBox control
        If OvalComboBox1 Is Nothing Then Return

        OvalComboBox1.Items.Clear()
        OvalComboBox1.Items.Add("Super Admin")
        OvalComboBox1.Items.Add("Admin")
        OvalComboBox1.Items.Add("User")

        If OvalComboBox1.Items.Contains("Admin") Then
            OvalComboBox1.Text = "Admin"
        ElseIf OvalComboBox1.Items.Count > 0 Then
            OvalComboBox1.Text = CStr(OvalComboBox1.Items(0))
        End If
    End Sub

    ' --- HELPER: Clears all input controls ---
    Private Sub ClearAllInputControls()
        ' Clear TextBoxes
        OvalTextBox9.Text = String.Empty  ' Full Name
        OvalTextBox10.Text = String.Empty ' Username
        OvalTextBox1.Text = String.Empty   ' Password 
        OvalTextBox2.Text = String.Empty   ' Address
        OvalTextBox3.Text = String.Empty   ' Email
        OvalTextBox4.Text = String.Empty   ' Contact No.
        OvalTextBox5.Text = String.Empty   ' Facebook
        OvalTextBox6.Text = String.Empty   ' Viber

        ' Clear ComboBox
        OvalComboBox1.Text = String.Empty
        InitializeAccountTypeComboBox()

        ' Clear PictureBox
        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
            PictureBox1.Image = Nothing
        End If
        currentImagePath = String.Empty
    End Sub

    ''' <summary>
    ''' Splits a full name into First, Middle, and Last name, returning a String array.
    ''' Index 0 = FirstName, Index 1 = MiddleName, Index 2 = LastName
    ''' </summary>
    Private Function SplitFullName(ByVal fullName As String) As String()
        Dim parts As String() = fullName.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
        Dim firstName As String = ""
        Dim middleName As String = ""
        Dim lastName As String = ""

        Select Case parts.Length
            Case 0
                ' All names are empty
            Case 1
                lastName = parts(0)
            Case 2
                firstName = parts(0)
                lastName = parts(1)
            Case Is >= 3
                firstName = parts(0)
                lastName = parts(parts.Length - 1)
                ' Join all parts between first and last name to form the middle name
                middleName = String.Join(" ", parts.Skip(1).Take(parts.Length - 2).ToArray())
        End Select

        Return New String() {firstName, middleName, lastName}
    End Function

    ' ------------------------------------------------------------------
    ' --- A. SAVE BUTTON (OvalButton3) ---
    ' ------------------------------------------------------------------
    Private Sub OvalButton3_Click(sender As Object, e As EventArgs) Handles OvalButton3.Click
        ' 1. COLLECT DATA
        ' Using the safe access method (If object IsNot Nothing) but simplified variable assignment
        Dim fullNameInput As String = OvalTextBox9.Text.Trim()
        Dim usernameInput As String = OvalTextBox10.Text.Trim()
        Dim passwordInput As String = OvalTextBox1.Text.Trim()
        Dim addressInput As String = OvalTextBox2.Text.Trim()
        Dim emailInput As String = OvalTextBox3.Text.Trim()
        Dim contactInput As String = OvalTextBox4.Text.Trim()
        Dim facebookInput As String = OvalTextBox5.Text.Trim()
        Dim viberInput As String = OvalTextBox6.Text.Trim()
        Dim accTypeInput As String = OvalComboBox1.Text.Trim()

        ' 2. PICTURE DATA
        Dim pictureData As Byte() = Nothing
        If Not String.IsNullOrEmpty(currentImagePath) Then
            Try
                pictureData = File.ReadAllBytes(currentImagePath)
            Catch ex As Exception
                MessageBox.Show($"Error reading image file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
        End If

        ' 3. SPLIT NAME (Using array to fix potential syntax error)
        Dim nameParts() As String = SplitFullName(fullNameInput)
        Dim fName As String = nameParts(0) ' First Name
        Dim mName As String = nameParts(1) ' Middle Name
        Dim lName As String = nameParts(2) ' Last Name

        ' 4. VALIDATION (Basic Check for Required Fields)
        If String.IsNullOrEmpty(fName) OrElse String.IsNullOrEmpty(lName) OrElse String.IsNullOrEmpty(usernameInput) OrElse String.IsNullOrEmpty(passwordInput) Then
            MessageBox.Show("First Name, Last Name, Username, and Password are required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 5. CONFIRMATION AND DATABASE CALL
        If MessageBox.Show("Are you sure you want to save this new account?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            ' Database Logic (SQL and Parameter Assembly)
            Dim sql As String = "INSERT INTO tbl_account (AccType, ALastName, AFirstName, AMiddleName, AUsername, APassword, AAddress, AEmail, AContactNo, AFacebook, AViber, APicture) " &
                                "VALUES (@AccType, @ALastName, @AFirstName, @AMiddleName, @AUsername, @APassword, @AAddress, @AEmail, @AContactNo, @AFacebook, @AViber, @APicture)"

            Dim parameters As New Dictionary(Of String, Object) From {
                {"@AccType", accTypeInput},
                {"@ALastName", lName},
                {"@AFirstName", fName},
                {"@AMiddleName", mName}, ' Fixed to use mName (Index 1)
                {"@AUsername", usernameInput},
                {"@APassword", passwordInput},
                {"@AAddress", addressInput},
                {"@AEmail", emailInput},
                {"@AContactNo", contactInput},
                {"@AFacebook", facebookInput},
                {"@AViber", viberInput},
                {"@APicture", pictureData} ' Handled as Nothing/DBNull in DatabaseConnection
            }

            Try
                Dim rowsAffected As Integer = db.ExecuteAction(sql, parameters)

                If rowsAffected > 0 Then
                    MessageBox.Show("Account successfully created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearAllInputControls()
                Else
                    ' Failure handled by DatabaseConnection's MessageBox, which shows the MySQL error code
                    ' We only need a generic failure message here as a fallback
                End If

            Catch ex As Exception
                MessageBox.Show($"Application error during account creation: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' --- B. CLEAR OUTPUT BUTTON (btnBack_Click) ---
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        ClearAllInputControls()
    End Sub

    ' --- C. INSERT PHOTO BUTTON (Button1_Click) ---
    Private Sub InsertPhotoButton_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Title = "Select Account Picture"
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                Try
                    currentImagePath = openFileDialog.FileName

                    If PictureBox1.Image IsNot Nothing Then
                        PictureBox1.Image.Dispose()
                    End If

                    ' Create a temporary copy to release the file lock immediately
                    Using tempImage As Image = Image.FromFile(currentImagePath)
                        PictureBox1.Image = CType(tempImage.Clone(), Image)
                    End Using

                    PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
                Catch ex As Exception
                    MessageBox.Show($"Error loading image: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    currentImagePath = String.Empty
                End Try
            End If
        End Using
    End Sub

End Class