Imports MySql.Data.MySqlClient

Public Class EditAccountPage
    Dim db As New DatabaseConnection()

    ' Property to hold the ID of the user we are editing
    Public Property TargetUserID As Integer = 0

    Private currentImagePath As String = String.Empty

    ' --- 0. LOAD DATA WHEN FORM OPENS ---
    Private Sub EditAccountPage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If TargetUserID > 0 Then
            LoadAccountData(TargetUserID)
        End If
    End Sub

    Public Sub LoadAccountData(id As Integer)
        Dim sql As String = "SELECT * FROM tbl_account WHERE AccountID = @id"
        Dim params As New Dictionary(Of String, Object)
        params.Add("@id", id)

        Dim dt As DataTable = db.ExecuteSelect(sql, params)

        If dt.Rows.Count > 0 Then
            Dim row As DataRow = dt.Rows(0)

            ' Populate fields (Assuming TextBoxes are named similarly to Create form)
            ' Adjust these OvalTextBox names if they are different in your EditForm Designer!
            OvalTextBox2.Text = row("AUsername").ToString()
            OvalTextBox10.Text = row("APassword").ToString()
            OvalTextBox9.Text = row("AFirstName").ToString() & " " & row("ALastName").ToString()
            OvalTextBox5.Text = row("AEmail").ToString()
            OvalTextBox4.Text = row("AContactno").ToString()
            OvalTextBox6.Text = row("AFacebook").ToString()
            OvalTextBox13.Text = row("AViber").ToString()

            ' Select Radio Button
            If row("AccType").ToString() = "Admin" Then
                RadioButton2.Checked = True
            Else
                RadioButton1.Checked = True
            End If
        End If
    End Sub

    ' --- 1. SAVE / UPDATE BUTTON ---
    Private Sub OvalButton3_Click(sender As Object, e As EventArgs) Handles OvalButton3.Click
        Dim confirmResult As DialogResult = MessageBox.Show("Are you sure that all of the details are correct?",
                                                            "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmResult = DialogResult.Yes Then
            ' Split Name Logic
            Dim fullName As String = OvalTextBox9.Text.Trim()
            Dim nameParts As String() = fullName.Split(" "c)
            Dim fName As String = If(nameParts.Length > 0, nameParts(0), "")
            Dim lName As String = If(nameParts.Length > 1, nameParts(nameParts.Length - 1), "")

            ' SQL Update
            Dim sql As String = "UPDATE tbl_account SET " &
                                "AUsername=@user, APassword=@pass, AFirstName=@fname, ALastName=@lname, " &
                                "AEmail=@email, AContactno=@contact, AFacebook=@fb, AViber=@viber, AccType=@type " &
                                "WHERE AccountID=@id"

            Dim params As New Dictionary(Of String, Object)
            params.Add("@id", TargetUserID)
            params.Add("@user", OvalTextBox2.Text)
            params.Add("@pass", OvalTextBox10.Text)
            params.Add("@fname", fName)
            params.Add("@lname", lName)
            params.Add("@email", OvalTextBox5.Text)
            params.Add("@contact", OvalTextBox4.Text)
            params.Add("@fb", OvalTextBox6.Text)
            params.Add("@viber", OvalTextBox13.Text)
            params.Add("@type", If(RadioButton2.Checked, "Admin", "Technician"))

            db.ExecuteAction(sql, params)

            MessageBox.Show("Account details updated successfully!", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh Manage Page and Close this one
            ManageAccounts.LoadAccounts() ' Ensure ManageAccounts has a Public LoadAccounts method
            ManageAccounts.Show()
            Me.Hide()
        End If
    End Sub

    ' --- 2. BACK BUTTON ---
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Dim result As DialogResult = MessageBox.Show("Cancel changes?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If result = DialogResult.Yes Then
            Me.Hide()
            ManageAccounts.Show()
        End If
    End Sub

    ' --- 3. UPLOAD PHOTO (Visual Only for now unless you add BLOB to DB) ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Try
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
                Button1.Visible = False
            Catch ex As Exception
                MessageBox.Show("Error loading image.")
            End Try
        End If
    End Sub

    ' --- 4. CLEARING SUBROUTINE ---
    Private Sub ClearAllInputControls(parent As Control)
        For Each ctrl As Control In parent.Controls
            ' Clear TextBoxes
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty
            End If

            ' Recursively clear containers
            If ctrl.Controls.Count > 0 Then
                ClearAllInputControls(ctrl)
            End If
        Next ' <--- FIXED: Replaced "End For" with "Next"
    End Sub
End Class