Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq

Public Class EditAccountPage
    ' Create an instance of your DB Connection helper
    Dim db As New DatabaseConnection()

    ' Property to hold the ID of the user we are editing (Passed from ManageAccounts)
    Public Shared TargetUserID As Integer = 0

    ' Stores the path of the newly selected image (if any)
    Private currentImagePath As String = String.Empty

    ' Stores the byte array of the existing picture, loaded from the database (if any)
    Private existingPictureData As Byte() = Nothing

    ' --- HELPER: Name Splitting Function ---
    Private Function SplitFullName(ByVal fullName As String) As (FirstName As String, MiddleName As String, LastName As String)
        Dim parts As String() = fullName.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        Dim firstName As String = ""
        Dim middleName As String = ""
        Dim lastName As String = ""

        If parts.Length = 1 Then
            lastName = parts(0)

        ElseIf parts.Length >= 2 Then
            lastName = parts(parts.Length - 1)
            firstName = parts(0)

            If parts.Length > 2 Then
                middleName = String.Join(" ", parts.Skip(1).Take(parts.Length - 2).ToArray())
            End If
        End If

        Return (firstName, middleName, lastName)

    End Function

    ' --- 0. LOAD DATA WHEN FORM OPENS ---
    Private Sub EditAccountPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

        ' Populate the Account Type ComboBox
        If OvalComboBox1 IsNot Nothing Then
            OvalComboBox1.Items.Clear()
            OvalComboBox1.Items.Add("Super Admin")
            OvalComboBox1.Items.Add("Admin")
            OvalComboBox1.Items.Add("Technician")

            OvalComboBox1.Text = "Select an Item"
        End If
    End Sub

    ''' <summary>
    ''' Loads all existing account data into the text fields.
    ''' </summary>
    Public Sub LoadAccountData(id As Integer)

        ' --- PICTURE BOX RESET LOGIC ---
        ' Ensures the PictureBox is cleared and the upload button is visible 
        ' every time a new account is loaded for editing.
        currentImagePath = String.Empty
        existingPictureData = Nothing

        If PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
        End If
        PictureBox1.Image = Nothing

        If Button1 IsNot Nothing Then
            Button1.Visible = True ' Makes the "Insert Photo Here" button visible
        End If
        ' --- END PICTURE BOX RESET LOGIC ---

        ' 2. Fetch ALL account details from the database
        ' (AInfo is removed from this query as per your database schema)
        Dim sql As String = "SELECT AccType, AUsername, APassword, ALastName, AFirstName, AMiddleName, AEmail, AContactno, AFacebook, AViber, APicture, AAddress FROM tbl_account WHERE AccountID = @id"
        Dim params As New Dictionary(Of String, Object)
        params.Add("@id", id)

        Try
            Dim dt As DataTable = db.ExecuteSelect(sql, params)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                ' --- 3. POPULATE ALL TEXT FIELDS ---
                Dim firstName As String = If(Not row.IsNull("AFirstName"), row("AFirstName").ToString(), String.Empty).Trim()
                Dim middleName As String = If(Not row.IsNull("AMiddleName"), row("AMiddleName").ToString(), String.Empty).Trim()
                Dim lastName As String = If(Not row.IsNull("ALastName"), row("ALastName").ToString(), String.Empty).Trim()
                Dim fullName As String = $"{firstName} {middleName} {lastName}".Replace("  ", " ").Trim()

                ' Textboxes 
                OvalTextBox9.Text = fullName
                OvalTextBox1.Text = If(Not row.IsNull("AAddress"), row("AAddress").ToString(), String.Empty)
                OvalTextBox4.Text = If(Not row.IsNull("AContactno"), row("AContactno").ToString(), String.Empty)
                OvalTextBox5.Text = If(Not row.IsNull("AEmail"), row("AEmail").ToString(), String.Empty)
                OvalTextBox6.Text = If(Not row.IsNull("AFacebook"), row("AFacebook").ToString(), String.Empty)
                OvalTextBox13.Text = If(Not row.IsNull("AViber"), row("AViber").ToString(), String.Empty)

                OvalTextBox14.Text = If(Not row.IsNull("AUsername"), row("AUsername").ToString(), String.Empty)
                OvalTextBox8.Text = If(Not row.IsNull("APassword"), row("APassword").ToString(), String.Empty)
                OvalTextBox7.Text = If(Not row.IsNull("APassword"), row("APassword").ToString(), String.Empty)

                ' --- 4. POPULATE ACCOUNT TYPE ---
                Dim accTypeFromDb As String = String.Empty
                If Not row.IsNull("AccType") Then
                    accTypeFromDb = row("AccType").ToString().Trim()
                End If

                If OvalComboBox1 IsNot Nothing Then
                    If Not String.IsNullOrEmpty(accTypeFromDb) Then

                        Dim matchedItem = OvalComboBox1.Items.Cast(Of Object)().FirstOrDefault(Function(item) item.ToString().Equals(accTypeFromDb, StringComparison.OrdinalIgnoreCase))

                        If matchedItem IsNot Nothing Then
                            OvalComboBox1.Text = matchedItem.ToString()
                        Else
                            OvalComboBox1.Text = "Select an Item"
                        End If
                    Else
                        OvalComboBox1.Text = "Select an Item"
                    End If
                End If

                ' --- 5. STORE OLD PICTURE DATA (if it exists) ---
                If Not Convert.IsDBNull(row("APicture")) Then
                    existingPictureData = CType(row("APicture"), Byte())
                Else
                    existingPictureData = Nothing
                End If

            Else
                MessageBox.Show("Account not found.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show($"Error loading account data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- 1. SAVE / UPDATE BUTTON ---
    Private Sub OvalButton3_Click(sender As Object, e As EventArgs) Handles OvalButton3.Click

        ' 1. COLLECT AND VALIDATE DATA
        Dim fullNameInput As String = OvalTextBox9.Text.ToString().Trim()

        Dim rawAccTypeInput As String = OvalComboBox1.Text.ToString().Trim()

        Dim usernameInput As String = OvalTextBox14.Text.ToString().Trim()
        Dim passwordInput As String = OvalTextBox8.Text.ToString().Trim()
        Dim verifyPassword As String = OvalTextBox7.Text.ToString().Trim()

        Dim accType As String = String.Empty

        If TargetUserID <= 0 Then
            MessageBox.Show("Cannot save: Missing User ID. Update requires a target account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Mandatory checks
        If String.IsNullOrWhiteSpace(fullNameInput) Then
            MessageBox.Show("Please enter the full account name.", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(usernameInput) Then
            MessageBox.Show("Please enter a username.", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(passwordInput) Then
            MessageBox.Show("Please enter a password.", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If passwordInput <> verifyPassword Then
            MessageBox.Show("Password and Verify Password do not match.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' ROBUST VALIDATION BLOCK: Verify input against the item list
        Dim matchedItem = OvalComboBox1.Items.Cast(Of Object)().FirstOrDefault(
            Function(item) item.ToString().Trim().Equals(rawAccTypeInput, StringComparison.OrdinalIgnoreCase)
        )

        If matchedItem IsNot Nothing Then
            accType = matchedItem.ToString()
        Else
            MessageBox.Show("Please select a valid Account Type.", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' END ROBUST VALIDATION LOGIC

        Dim nameParts = SplitFullName(fullNameInput)

        ' --- PICTURE LOGIC ---
        Dim imageUpdated As Boolean = Not String.IsNullOrEmpty(currentImagePath)
        Dim pictureData As Byte() = Nothing

        If imageUpdated Then
            Try
                pictureData = File.ReadAllBytes(currentImagePath)
            Catch ex As Exception
                MessageBox.Show($"Error reading image file: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try
        End If
        ' --- END PICTURE LOGIC ---

        ' 2. CONFIRMATION AND UPDATE LOGIC
        Dim confirmResult As DialogResult = MessageBox.Show("Are you sure you want to update these details?",
                                                             "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmResult = DialogResult.Yes Then

            ' --- UPDATE SQL STATEMENT (AInfo removed) ---
            Dim sql As String = "UPDATE tbl_account SET " &
                                "AUsername=@user, APassword=@pass, AFirstName=@fname, ALastName=@lname, AMiddleName=@mname, " &
                                "AEmail=@email, AContactno=@contact, AFacebook=@fb, AViber=@viber, AccType=@type, APicture=@PictureData, " &
                                "AAddress=@addr " &
                                "WHERE AccountID=@id"
            ' --- END UPDATE SQL STATEMENT ---


            ' Prepare parameters for ALL fields
            Dim params As New Dictionary(Of String, Object)
            params.Add("@id", TargetUserID)
            params.Add("@user", usernameInput)
            params.Add("@pass", passwordInput)
            params.Add("@fname", nameParts.FirstName)
            params.Add("@lname", nameParts.LastName)
            params.Add("@mname", nameParts.MiddleName)

            ' Optional fields
            params.Add("@email", OvalTextBox5.Text.ToString().Trim())
            params.Add("@contact", OvalTextBox4.Text.ToString().Trim())
            params.Add("@fb", OvalTextBox6.Text.ToString().Trim())
            params.Add("@viber", OvalTextBox13.Text.ToString().Trim())

            params.Add("@addr", OvalTextBox1.Text.ToString().Trim())

            params.Add("@type", accType)

            ' --- PICTURE DATA PARAMETER HANDLING ---
            If imageUpdated Then
                params.Add("@PictureData", pictureData)
            Else
                If existingPictureData IsNot Nothing Then
                    params.Add("@PictureData", existingPictureData)
                Else
                    params.Add("@PictureData", DBNull.Value)
                End If
            End If


            Try
                Dim rowsAffected As Integer = db.ExecuteAction(sql, params)

                If rowsAffected > 0 Then
                    MessageBox.Show("Account details updated successfully!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Refresh data in the management form
                    If ManageAccounts IsNot Nothing Then
                        ManageAccounts.LoadAccounts()
                        ManageAccounts.Show()
                    End If
                    Me.Hide()

                Else
                    MessageBox.Show("Update failed. No changes were made.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Catch ex As Exception
                MessageBox.Show($"Error during database update: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' --- 2. BACK BUTTON ---
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to cancel and lose unsaved changes?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            Me.Hide()
            If ManageAccounts IsNot Nothing Then
                ManageAccounts.Show()
            End If
        End If
    End Sub

    ' ⭐ 3. UPLOAD PHOTO (FIXED: Resource Disposal/File Locking Issue) ⭐
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        OpenFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*"
        OpenFileDialog1.Title = "Select an Account Picture"

        currentImagePath = String.Empty

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            ' 1. Dispose of the current image resources in the PictureBox
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
                PictureBox1.Image = Nothing
            End If

            Try
                Dim filePath As String = OpenFileDialog1.FileName

                ' 2. Use a FileStream within a Using block to safely read the file content
                '    This prevents the file from being locked by the Image.FromStream operation.
                Using fs As New System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read)

                    ' 3. Create the Image object from the stream
                    Dim newImage As Image = Image.FromStream(fs)

                    ' 4. Assign the copy to the PictureBox
                    PictureBox1.Image = newImage

                End Using ' FileStream (fs) is guaranteed to close here, releasing the file lock

                currentImagePath = filePath ' Store the path for database saving
                Button1.Visible = False     ' Hide the "Insert Photo Here" button on success

            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message & vbCrLf & "Please try again or select a different file.", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                currentImagePath = String.Empty ' Clear the path on failure
                Button1.Visible = True          ' Ensure the button is visible on error
            End Try
        End If
    End Sub
End Class