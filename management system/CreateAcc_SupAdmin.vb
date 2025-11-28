Imports System.Drawing
Imports System.IO
Imports System.Linq
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports MySqlConnector

Public Class CreateAcc_SupAdmin

    ' IMPORTANT: You MUST update this with your actual MySQL connection string details!
    Private Const MyConnectionString As String = "Server=localhost;Database=db_rrcms;Uid=root;Pwd=;"

    ' ⭐️ GDI+ FIX: Store the file contents in memory (Byte Array) instead of the file path (String).
    Private PrivatePictureData As Byte() = Nothing

    ' -------------------------------------------
    ' |           GDI+ FIX HELPERS              |
    ' -------------------------------------------

    ''' <summary>
    ''' Safely disposes of the PictureBox's Image object to release any file locks (GDI+ Fix).
    ''' </summary>
    Private Sub DisposePictureBoxImage()
        If PictureBox1 IsNot Nothing AndAlso PictureBox1.Image IsNot Nothing Then
            Try
                PictureBox1.Image.Dispose()
                PictureBox1.Image = Nothing
            Catch
                ' Ignore disposal errors if the resource is already released
            End Try
        End If
    End Sub

    ' --- 1. IMAGE UPLOAD BUTTON (DEFINITIVE GDI+ FIX: Read to Memory Immediately) ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*"
        OpenFileDialog1.Title = "Select an Account Picture"

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            ' 1. Dispose of any previous image resources
            DisposePictureBoxImage()

            Try
                Dim filePath As String = OpenFileDialog1.FileName

                ' 2. CRITICAL FIX: Read all bytes from the file immediately outside a stream 
                '    and store them in the memory array. The file handle is closed instantly.
                PrivatePictureData = File.ReadAllBytes(filePath)

                ' 3. Use a MemoryStream (in-memory data) to safely load the PictureBox.
                '    This ensures the PictureBox never locks the original file on disk.
                Using ms As New System.IO.MemoryStream(PrivatePictureData)

                    ' 4. Create the image and assign the cloned copy for extra safety.
                    Dim sourceImage As Image = Image.FromStream(ms)
                    Dim clonedImage As Image = CType(sourceImage.Clone(), Image)
                    PictureBox1.Image = clonedImage

                End Using ' MemoryStream closes

                Button1.Visible = False

            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message & vbCrLf & "Please try again or select a different file.", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                PrivatePictureData = Nothing
                Button1.Visible = True
            End Try
        End If
    End Sub

    ' --- 2. FORM CLOSE/HIDE HANDLER (GDI+ FIX) ---
    Private Sub CreateAcc_SupAdmin_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Ensure image resource is disposed when the form is closed/hidden to prevent GDI+ errors
        DisposePictureBoxImage()
    End Sub

    ' -------------------------------------------
    ' |          CONTROL ACCESS HELPER          |
    ' -------------------------------------------

    ''' <summary>
    ''' Safely finds and returns the text of an OvalTextBox by its name string.
    ''' This bypasses compiler errors related to protection level or corrupted references.
    ''' </summary>
    Private Function GetOvalTextBoxText(controlName As String) As String
        Try
            ' Recursively search controls within the form's hierarchy
            Dim foundControls As Control() = Me.Controls.Find(controlName, True)

            If foundControls.Length > 0 AndAlso foundControls(0).GetType().Name.Contains("OvalTextBox") Then
                ' Return the Text property of the found control
                Return foundControls(0).Text.Trim()
            End If
        Catch
        End Try
        Return String.Empty
    End Function

    ' -------------------------------------------
    ' |          FORM & UTILITY LOGIC           |
    ' -------------------------------------------

    ' --- FORM LOAD EVENT ---
    Private Sub CreateAcc_SupAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If OvalComboBox1 IsNot Nothing Then
            OvalComboBox1.Items.Clear()
            OvalComboBox1.Items.Add("Super Admin")
            OvalComboBox1.Items.Add("Admin")
            OvalComboBox1.Items.Add("Technician")

            OvalComboBox1.Text = "Select an Item"
        End If
    End Sub

    ' --- CLEAR / BACK BUTTON ---
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim result As DialogResult

        result = MessageBox.Show("Are you sure you want to clear all the inputs? This action cannot be undone.",
                                 "Warning",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Exclamation)

        If result = DialogResult.Yes Then
            ClearAllInputControls(Me)
            Button1.Visible = True
        End If
    End Sub

    ' --- CLEARING LOGIC ---
    Private Sub ClearAllInputControls(parent As Control)
        For Each ctrl As Control In parent.Controls

            If TypeOf ctrl Is TextBox OrElse ctrl.GetType().Name.Contains("OvalTextBox") Then
                Try
                    ctrl.Text = String.Empty
                Catch
                End Try

            ElseIf TypeOf ctrl Is ComboBox OrElse ctrl.GetType().Name.Contains("OvalComboBox") Then
                Try
                    ctrl.Text = "Select an Item"
                Catch
                    If TypeOf ctrl Is ComboBox Then
                        CType(ctrl, ComboBox).SelectedIndex = -1
                    End If
                End Try

            ElseIf TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = False

            ElseIf TypeOf ctrl Is RadioButton Then
                CType(ctrl, RadioButton).Checked = False

            ElseIf TypeOf ctrl Is PictureBox Then
                ' Use the safe disposal method and clear memory array
                DisposePictureBoxImage()
                PrivatePictureData = Nothing

            ElseIf ctrl.Controls.Count > 0 Then
                ClearAllInputControls(ctrl)
            End If
        Next
    End Sub

    ' --- Name Splitting Function ---
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

    ' -------------------------------------------
    ' |         DATABASE INSERTION LOGIC        |
    ' -------------------------------------------

    ' --- SUBMIT / SAVE BUTTON ---
    Private Sub OvalButton3_Click(sender As Object, e As EventArgs) Handles OvalButton3.Click

        ' 1. COLLECT AND VALIDATE DATA
        Dim fullNameInput As String = GetOvalTextBoxText("OvalTextBox9")
        Dim rawAccTypeInput As String = OvalComboBox1.Text.Trim()

        ' ⭐️ CONFIRMED CONTROL NAMES (2=Username, 10=Password, 11=Verify) ⭐️
        Dim username As String = GetOvalTextBoxText("OvalTextBox2")
        Dim password As String = GetOvalTextBoxText("OvalTextBox10")
        Dim verifyPassword As String = GetOvalTextBoxText("OvalTextBox11")

        Dim accType As String = ""

        ' --- Validation Block ---
        If String.IsNullOrWhiteSpace(fullNameInput) OrElse String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter the full name, username, and password.", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If password <> verifyPassword Then
            MessageBox.Show("Password and Verify Password do not match.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Dim matchedItem = OvalComboBox1.Items.Cast(Of Object)().FirstOrDefault(
            Function(item) item.ToString().Trim().Equals(rawAccTypeInput, StringComparison.OrdinalIgnoreCase)
        )

        If matchedItem IsNot Nothing Then
            accType = matchedItem.ToString()
        Else
            MessageBox.Show("Please select a valid Account Type.", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ' --- End Validation Block ---

        Dim nameParts = SplitFullName(fullNameInput)
        Dim lastName As String = nameParts.LastName
        Dim firstName As String = nameParts.FirstName
        Dim middleName As String = nameParts.MiddleName

        ' Gather optional/non-mandatory fields 
        Dim address As String = GetOvalTextBoxText("OvalTextBox12")
        Dim email As String = GetOvalTextBoxText("OvalTextBox5")
        Dim contactNo As String = GetOvalTextBoxText("OvalTextBox4")
        Dim facebook As String = GetOvalTextBoxText("OvalTextBox6")
        Dim viber As String = GetOvalTextBoxText("OvalTextBox13")

        ' ⭐️ GDI+ FIX: Use the byte array already stored in PrivatePictureData.
        Dim pictureData As Byte() = PrivatePictureData

        ' 2. DATABASE INSERTION
        ' FIXED QUERY: Removed the problematic AAddress column and parameter.
        Dim query As String = "INSERT INTO tbl_account (AccType, ALastName, AFirstName, AMiddleName, AUsername, APassword, AEmail, AContactno, AFacebook, AViber, APicture) " &
                              "VALUES (@AccType, @LastName, @FirstName, @MiddleName, @Username, @Password, @Email, @Contactno, @Facebook, @Viber, @PictureData)"

        Dim success As Boolean = False

        Try
            Using con As New MySqlConnection(MyConnectionString)
                con.Open()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@AccType", accType)
                    cmd.Parameters.AddWithValue("@LastName", lastName)
                    cmd.Parameters.AddWithValue("@FirstName", firstName)
                    cmd.Parameters.AddWithValue("@MiddleName", middleName)
                    cmd.Parameters.AddWithValue("@Username", username)
                    cmd.Parameters.AddWithValue("@Password", password)

                    ' Address parameter removed
                    ' cmd.Parameters.AddWithValue("@Address", If(String.IsNullOrEmpty(address), DBNull.Value, address)) 
                    cmd.Parameters.AddWithValue("@Email", If(String.IsNullOrEmpty(email), DBNull.Value, email))
                    cmd.Parameters.AddWithValue("@Contactno", If(String.IsNullOrEmpty(contactNo), DBNull.Value, contactNo))
                    cmd.Parameters.AddWithValue("@Facebook", If(String.IsNullOrEmpty(facebook), DBNull.Value, facebook))
                    cmd.Parameters.AddWithValue("@Viber", If(String.IsNullOrEmpty(viber), DBNull.Value, viber))

                    If pictureData IsNot Nothing Then
                        cmd.Parameters.AddWithValue("@PictureData", pictureData)
                    Else
                        cmd.Parameters.AddWithValue("@PictureData", DBNull.Value)
                    End If

                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()

                    If rowsAffected > 0 Then
                        success = True
                    Else
                        MessageBox.Show("Account registration failed. No rows affected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show($"Database error during registration: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return

        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' 3. POST-SUBMISSION LOGIC
        If success Then
            Dim confirmResult As DialogResult = MessageBox.Show("Are you sure that all of the details are correct?",
                                                               "Confirm Submission",
                                                               MessageBoxButtons.YesNo,
                                                               MessageBoxIcon.Warning)

            If confirmResult = DialogResult.Yes Then
                Dim successResult As DialogResult = MessageBox.Show("Account has been created successfully!",
                                                                    "Account Creation",
                                                                    MessageBoxButtons.OK,
                                                                    MessageBoxIcon.Information)

                If successResult = DialogResult.OK Then
                    ClearAllInputControls(Me)
                    Button1.Visible = True

                    ' REFRESH THE ACCOUNT LIST AND SHOW MANAGE ACCOUNTS FORM
                    If ManageAccounts IsNot Nothing Then
                        ManageAccounts.LoadAccounts()
                        Me.Hide()
                        ManageAccounts.Show()
                    End If
                End If
            End If
        End If
    End Sub

End Class