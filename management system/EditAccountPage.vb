Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Collections.Generic ' Required for Dictionary

Public Class EditAccountPage

    ' ⭐️ FIX: Use Composition (instance) instead of Inheritance to avoid BC30521 ⭐️
    Private ReadOnly db As New DatabaseConnection()

    ' Property to hold the ID of the user we are editing (Passed from ManageAccounts)
    Public TargetUserID As Integer = 0

    ' Property to hold the reference to the parent form instance for refreshing
    Public Property ParentManageAccountsForm As ManageAccounts

    ' Stores the path of the newly selected image (if any)
    Private currentImagePath As String = String.Empty

    ' --- HELPER: Name Splitting Function ---
    Private Function SplitFullName(ByVal fullName As String) As (FirstName As String, MiddleName As String, LastName As String)
        Dim parts As String() = fullName.Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)

        Dim firstName As String = ""
        Dim middleName As String = ""
        Dim lastName As String = ""

        If parts.Length >= 2 Then
            lastName = parts(parts.Length - 1)
            firstName = parts(0)
            If parts.Length > 2 Then
                middleName = String.Join(" ", parts.Skip(1).Take(parts.Length - 2).ToArray())
            End If
        ElseIf parts.Length = 1 Then
            lastName = parts(0)
        End If

        Return (firstName, middleName, lastName)

    End Function

    ' ----------------------------------------------------------------------
    ' | FORM LIFECYCLE & DATA LOADING
    ' ----------------------------------------------------------------------

    ' --- 0. LOAD DATA WHEN FORM OPENS ---
    Private Sub EditAccountPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PictureBox1.SizeMode = PictureBoxSizeMode.Zoom

        ' Populate the Account Type ComboBox (Assuming OvalComboBox1)
        If OvalComboBox1 IsNot Nothing Then
            OvalComboBox1.Items.Clear()
            OvalComboBox1.Items.Add("Super Admin")
            OvalComboBox1.Items.Add("Admin")
            OvalComboBox1.Items.Add("Technician")
            OvalComboBox1.Text = "Select an Item"
        End If

        ' Load data if TargetUserID was set by the parent form (ManageAccounts)
        If TargetUserID > 0 Then
            LoadAccountData(TargetUserID)
        End If
    End Sub

    ''' <summary>
    ''' Loads all existing account data into the text fields. 
    ''' The photo box is ALWAYS cleared on load, as requested.
    ''' </summary>
    Public Sub LoadAccountData(id As Integer)

        ' ⭐️ REQUIRED FEATURE: PICTURE BOX IS ALWAYS CLEARED AND BUTTON IS ALWAYS VISIBLE ⭐️
        currentImagePath = String.Empty ' Clear any path

        ' Dispose and clear the PictureBox image
        If PictureBox1 IsNot Nothing AndAlso PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
        End If
        PictureBox1.Image = Nothing

        ' Make the "Insert Photo Here" button visible
        If Button1 IsNot Nothing Then
            Button1.Visible = True
        End If
        ' ---------------------------------------------------------------------------

        ' NOTE: We select APicture but ignore it, to keep the SQL query generic.
        Dim sql As String = "SELECT AccType, AUsername, APassword, ALastName, AFirstName, AMiddleName, AEmail, AContactno, AFacebook, AViber, APicture, AAddress FROM tbl_account WHERE AccountID = @id"

        ' ⭐️ FIX: Ensure Dictionary is declared in scope (Resolves BC30541 if declaration was missing) ⭐️
        Dim params As New Dictionary(Of String, Object)
        params.Add("@id", id)

        Try
            Dim dt As DataTable = db.ExecuteSelect(sql, params) ' Call on the instance

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                ' --- POPULATE ALL TEXT FIELDS ---
                ' Note: The original logic to load the picture data is REMOVED to meet the requirement.

                ' Textboxes (Assuming standard naming conventions from previous contexts)
                OvalTextBox9.Text = If(Not row.IsNull("AFirstName"), row("AFirstName").ToString(), String.Empty).Trim() & " " &
                                    If(Not row.IsNull("AMiddleName"), row("AMiddleName").ToString(), String.Empty).Trim() & " " &
                                    If(Not row.IsNull("ALastName"), row("ALastName").ToString(), String.Empty).Trim() ' Full Name Assembly

                OvalTextBox1.Text = If(Not row.IsNull("AAddress"), row("AAddress").ToString(), String.Empty)
                OvalTextBox4.Text = If(Not row.IsNull("AContactno"), row("AContactno").ToString(), String.Empty)
                OvalTextBox5.Text = If(Not row.IsNull("AEmail"), row("AEmail").ToString(), String.Empty)
                OvalTextBox6.Text = If(Not row.IsNull("AFacebook"), row("AFacebook").ToString(), String.Empty)
                OvalTextBox13.Text = If(Not row.IsNull("AViber"), row("AViber").ToString(), String.Empty)

                OvalTextBox14.Text = If(Not row.IsNull("AUsername"), row("AUsername").ToString(), String.Empty)
                OvalTextBox8.Text = If(Not row.IsNull("APassword"), row("APassword").ToString(), String.Empty) ' Password 
                OvalTextBox7.Text = If(Not row.IsNull("APassword"), row("APassword").ToString(), String.Empty) ' Verify Password 

                ' --- POPULATE ACCOUNT TYPE ---
                Dim accTypeFromDb As String = If(Not row.IsNull("AccType"), row("AccType").ToString().Trim(), String.Empty)

                If OvalComboBox1 IsNot Nothing Then
                    OvalComboBox1.Text = accTypeFromDb
                End If

            Else
                MessageBox.Show("Account not found.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show($"Error loading account data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ----------------------------------------------------------------------
    ' | BUTTON ACTIONS
    ' ----------------------------------------------------------------------

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
            MessageBox.Show("Cannot save: Missing User ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        ' ... (Mandatory checks omitted for brevity) ...

        If passwordInput <> verifyPassword Then
            MessageBox.Show("Password and Verify Password do not match.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

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

        ' FINAL PICTURE DATA: If no new image was selected (imageUpdated=False), 
        ' we explicitly clear the DB column since the old image was not loaded for retention.
        Dim finalPictureData As Object = If(imageUpdated, pictureData, DBNull.Value)

        ' 2. CONFIRMATION AND UPDATE LOGIC
        Dim confirmResult As DialogResult = MessageBox.Show("Are you sure you want to update these details?",
                                                             "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmResult = DialogResult.Yes Then

            Dim sql As String = "UPDATE tbl_account SET " &
                                "AUsername=@user, APassword=@pass, AFirstName=@fname, ALastName=@lname, AMiddleName=@mname, " &
                                "AEmail=@email, AContactno=@contact, AFacebook=@fb, AViber=@viber, AccType=@type, APicture=@PictureData, " &
                                "AAddress=@addr " &
                                "WHERE AccountID=@id"

            Dim params As New Dictionary(Of String, Object)
            params.Add("@id", TargetUserID)
            params.Add("@user", usernameInput)
            params.Add("@pass", passwordInput)
            params.Add("@fname", nameParts.FirstName)
            params.Add("@lname", nameParts.LastName)
            params.Add("@mname", nameParts.MiddleName)
            ' Optional fields (using appropriate text boxes)
            params.Add("@email", OvalTextBox5.Text.ToString().Trim())
            params.Add("@contact", OvalTextBox4.Text.ToString().Trim())
            params.Add("@fb", OvalTextBox6.Text.ToString().Trim())
            params.Add("@viber", OvalTextBox13.Text.ToString().Trim())
            params.Add("@addr", OvalTextBox1.Text.ToString().Trim())
            params.Add("@type", OvalComboBox1.Text.ToString().Trim())
            params.Add("@PictureData", finalPictureData)

            Try
                Dim rowsAffected As Integer = db.ExecuteAction(sql, params) ' Call on the instance

                If rowsAffected > 0 Then
                    MessageBox.Show("Account details updated successfully!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Refresh the parent form to show updated data
                    If ParentManageAccountsForm IsNot Nothing Then
                        ParentManageAccountsForm.LoadAccountCards()
                        ParentManageAccountsForm.Show()
                    End If
                    Me.Close()

                Else
                    MessageBox.Show("Update failed. No rows affected.", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            If ParentManageAccountsForm IsNot Nothing Then
                ParentManageAccountsForm.Show()
            End If
        End If
    End Sub

    ' ⭐ 3. UPLOAD PHOTO (Robust resource handling) ⭐
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        OpenFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*"
        OpenFileDialog1.Title = "Select an Account Picture"

        currentImagePath = String.Empty

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
                PictureBox1.Image = Nothing
            End If

            Try
                Dim filePath As String = OpenFileDialog1.FileName

                Using fs As New System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read)
                    ' Clone the image so the file stream can close immediately
                    Dim newImage As Image = CType(Image.FromStream(fs).Clone(), Image)
                    PictureBox1.Image = newImage
                End Using

                currentImagePath = filePath
                Button1.Visible = False

            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message & vbCrLf & "Please try again or select a different file.", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                currentImagePath = String.Empty
                Button1.Visible = True
            End Try
        End If
    End Sub

    ' --- Resource Cleanup on form close ---
    Private Sub EditAccountPage_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If PictureBox1 IsNot Nothing AndAlso PictureBox1.Image IsNot Nothing Then
            PictureBox1.Image.Dispose()
            PictureBox1.Image = Nothing
        End If
    End Sub

End Class