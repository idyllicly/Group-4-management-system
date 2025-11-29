Imports MySql.Data.MySqlClient

Public Class CreateAcc_SupAdmin

    ' --- FIX: Declare the Database Connection ---
    Dim db As New DatabaseConnection()

    ' Variable to hold the file path
    Private currentImagePath As String = String.Empty

    ' --- 1. IMAGE UPLOAD BUTTON ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            If PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
            End If

            Try
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
                Button1.Visible = False
            Catch ex As Exception
                MessageBox.Show("Error loading image: " & ex.Message)
            End Try
        End If
    End Sub

    ' --- 2. INITIALIZE FIREBASE ON LOAD ---
    Private Sub CreateAcc_SupAdmin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FirebaseManager.Initialize()
    End Sub

    ' --- 3. CLEAR / BACK BUTTON ---
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If MsgBox("Are you sure you want to clear all the inputs?", MsgBoxStyle.YesNo + MsgBoxStyle.Exclamation, "Warning") = MsgBoxResult.Yes Then
            ClearAllInputControls(Me)
            Button1.Visible = True
        End If
    End Sub

    ' --- 4. CLEARING LOGIC ---
    Private Sub ClearAllInputControls(parent As Control)
        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty
            ElseIf TypeOf ctrl Is ComboBox Then
                CType(ctrl, ComboBox).SelectedIndex = -1
            ElseIf TypeOf ctrl Is CheckBox Then
                CType(ctrl, CheckBox).Checked = False
            ElseIf TypeOf ctrl Is RadioButton Then
                CType(ctrl, RadioButton).Checked = False
            ElseIf TypeOf ctrl Is PictureBox Then
                Dim picBox As PictureBox = CType(ctrl, PictureBox)
                If picBox.Image IsNot Nothing Then
                    picBox.Image.Dispose()
                    picBox.Image = Nothing
                End If
                currentImagePath = String.Empty
                OpenFileDialog1.FileName = String.Empty
            ElseIf ctrl.Controls.Count > 0 Then
                ClearAllInputControls(ctrl)
            End If
        Next
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint
    End Sub

    ' --- 5. SUBMIT BUTTON (ASYNC For Firebase) ---
    Private Async Sub OvalButton3_Click(sender As Object, e As EventArgs) Handles OvalButton3.Click

        ' --- A. SECURITY CHECK ---
        If AppState.CurrentUserRole <> "Super Admin" Then
            MessageBox.Show("Access Denied: Only Super Admins can create new accounts.", "Security Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' --- B. VALIDATION ---
        If OvalTextBox10.Text <> OvalTextBox11.Text Then
            MessageBox.Show("Passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        If String.IsNullOrWhiteSpace(OvalTextBox5.Text) Or String.IsNullOrWhiteSpace(OvalTextBox10.Text) Then
            MessageBox.Show("Email and Password are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' --- C. CONFIRMATION ---
        If MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then

            ' Prepare Variables
            Dim accType As String = If(RadioButton2.Checked, "Admin", "Technician")
            Dim fullName As String = OvalTextBox9.Text.Trim()

            Dim nameParts As String() = fullName.Split(" "c)
            Dim fName As String = If(nameParts.Length > 0, nameParts(0), "")
            Dim lName As String = If(nameParts.Length > 1, nameParts(nameParts.Length - 1), "")

            ' --- D. SAVE TO MYSQL ---
            Dim sql As String = "INSERT INTO tbl_account " &
                                "(AccType, AUsername, APassword, AFirstName, ALastName, AMiddleName, AEmail, AContactno, AFacebook, AViber) " &
                                "VALUES (@type, @user, @pass, @fname, @lname, '', @email, @contact, @fb, @viber)"

            Dim params As New Dictionary(Of String, Object)
            params.Add("@type", accType)
            params.Add("@user", OvalTextBox2.Text)
            params.Add("@pass", OvalTextBox10.Text)
            params.Add("@fname", fName)
            params.Add("@lname", lName)
            params.Add("@email", OvalTextBox5.Text)
            params.Add("@contact", OvalTextBox4.Text)
            params.Add("@fb", OvalTextBox6.Text)
            params.Add("@viber", OvalTextBox13.Text)

            ' This line now works because 'db' is declared at the top!
            Dim rows As Integer = db.ExecuteAction(sql, params)

            ' --- E. SAVE TO FIREBASE ---
            If rows > 0 Then
                If accType = "Technician" Then
                    Cursor = Cursors.WaitCursor

                    Dim fbResult As String = Await FirebaseManager.CreateUser(OvalTextBox5.Text, OvalTextBox10.Text, fullName)

                    Cursor = Cursors.Default

                    If fbResult.StartsWith("Error") Then
                        MessageBox.Show("MySQL Success, BUT Firebase Failed: " & fbResult, "Warning")
                    Else
                        MessageBox.Show("Success! Technician created in Database and Firebase.", "Complete")
                    End If
                Else
                    MessageBox.Show("Admin account created in Database.", "Success")
                End If

                ClearAllInputControls(Me)
                Button1.Visible = True
            End If
        End If
    End Sub

    Private Sub NavigationControl1_Load(sender As Object, e As EventArgs)
    End Sub

    Private Sub SideNavControl1_Load(sender As Object, e As EventArgs)
    End Sub
End Class