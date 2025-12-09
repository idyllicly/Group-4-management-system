Imports MySql.Data.MySqlClient

Public Class newUcAccountManager

    ' Connection String
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' Variables for selection
    Private _selectedID As Integer = 0
    Private _selectedFirebaseUID As String = ""
    Private _selectedRole As String = ""

    Private Sub newUcAccountManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAccounts("All")
        ' Initialize Firebase (Keep this for Technicians)
        FirebaseManager.Initialize()
        ClearForm()
    End Sub

    ' ==========================================
    ' 1. MASTER LOADER (Updated for Split Names)
    ' ==========================================
    Private Sub LoadAccounts(roleFilter As String)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' UPDATED SQL: Select individual name parts
                Dim sql As String = "SELECT UserID, LastName, FirstName, MiddleName, Role, Username, ContactNo, FirebaseUID FROM tbl_users"

                ' Apply Filter
                If roleFilter <> "All" Then
                    sql &= " WHERE Role = @role"
                End If

                ' Order by LastName, then FirstName
                sql &= " ORDER BY LastName ASC, FirstName ASC"

                Dim cmd As New MySqlCommand(sql, conn)
                If roleFilter <> "All" Then
                    cmd.Parameters.AddWithValue("@role", roleFilter)
                End If

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvAccounts.DataSource = dt

                ' Hide Technical Columns
                If dgvAccounts.Columns("UserID") IsNot Nothing Then dgvAccounts.Columns("UserID").Visible = False
                If dgvAccounts.Columns("FirebaseUID") IsNot Nothing Then dgvAccounts.Columns("FirebaseUID").Visible = False

            Catch ex As Exception
                MessageBox.Show("Error loading accounts: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub cmbFilterRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterRole.SelectedIndexChanged
        LoadAccounts(cmbFilterRole.Text)
    End Sub

    ' ==========================================
    ' 2. SAVE ACCOUNT (Create New) - UPDATED
    ' ==========================================
    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        ' --- NEW VALIDATION CALL ---
        If ValidateInputs() = False Then Exit Sub
        ' ---------------------------

        Dim role As String = cmbRole.Text
        Dim firebaseID As String = ""
        Dim fullNameForFirebase As String = txtFirstName.Text & " " & txtLastName.Text

        btnSave.Enabled = False
        btnSave.Text = "Processing..."

        Try
            ' === STEP A: FIREBASE (Only if Technician) ===
            If role = "Technician" Then
                ' Email format is already checked in ValidateInputs, so we just proceed
                btnSave.Text = "Syncing to Cloud..."

                ' Create in Firebase using the combined name [cite: 12]
                firebaseID = Await FirebaseManager.CreateTechnicianAccount(txtEmail.Text.Trim(), txtPassword.Text, fullNameForFirebase, "Technician")

                If firebaseID.StartsWith("Error") Then
                    MessageBox.Show(firebaseID)
                    btnSave.Enabled = True
                    Exit Sub
                End If
            End If

            ' === STEP B: DATABASE INSERT ===
            Using conn As New MySqlConnection(connString)
                conn.Open()

                Dim sql As String = "INSERT INTO tbl_users (FirstName, MiddleName, LastName, Role, Username, Password, ContactNo, FirebaseUID, Status) " &
                                "VALUES (@fname, @mname, @lname, @role, @user, @pass, @phone, @uid, 'Active')"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@fname", txtFirstName.Text.Trim())
                cmd.Parameters.AddWithValue("@mname", txtMiddleName.Text.Trim())
                cmd.Parameters.AddWithValue("@lname", txtLastName.Text.Trim())
                cmd.Parameters.AddWithValue("@role", role)
                cmd.Parameters.AddWithValue("@user", txtEmail.Text.Trim())
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text) ' Password usually isn't trimmed to allow spaces if intended
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim())

                ' Handle Null UID for Admins
                If firebaseID = "" Then
                    cmd.Parameters.AddWithValue("@uid", DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue("@uid", firebaseID)
                End If

                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show("Account Created Successfully!")
            ClearForm()
            LoadAccounts("All")

        Catch ex As Exception
            MessageBox.Show("System Error: " & ex.Message)
        Finally
            btnSave.Enabled = True
            btnSave.Text = "CREATE ACCOUNT"
        End Try
    End Sub

    ' ==========================================
    ' 3. UPDATE ACCOUNT - UPDATED
    ' ==========================================
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If _selectedID = 0 Then Exit Sub

        ' --- NEW VALIDATION CALL ---
        If ValidateInputs() = False Then Exit Sub
        ' ---------------------------

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' SQL Update String [cite: 20]
                Dim sql As String = "UPDATE tbl_users SET FirstName=@fname, MiddleName=@mname, LastName=@lname, Username=@user, ContactNo=@phone, Role=@role"

                If txtPassword.Text <> "" Then
                    sql &= ", Password=@pass"
                End If

                sql &= " WHERE UserID=@id"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@fname", txtFirstName.Text.Trim())
                cmd.Parameters.AddWithValue("@mname", txtMiddleName.Text.Trim())
                cmd.Parameters.AddWithValue("@lname", txtLastName.Text.Trim())
                cmd.Parameters.AddWithValue("@user", txtEmail.Text.Trim())
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim())
                cmd.Parameters.AddWithValue("@role", cmbRole.Text)
                cmd.Parameters.AddWithValue("@id", _selectedID)

                If txtPassword.Text <> "" Then
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
                End If

                cmd.ExecuteNonQuery()
                MessageBox.Show("Account Updated!")
                ClearForm()
                LoadAccounts("All")

            Catch ex As Exception
                MessageBox.Show("Error updating: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 4. DELETE LOGIC (Unchanged)
    ' ==========================================
    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _selectedID = 0 Then Exit Sub

        If MessageBox.Show("Are you sure you want to delete this account?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                ' 1. Delete from Firebase if it's a Technician
                If _selectedRole = "Technician" AndAlso _selectedFirebaseUID <> "" Then
                    Await FirebaseManager.DeleteTechnician(_selectedFirebaseUID)
                End If

                ' 2. Delete from Local DB (tbl_users)
                Using conn As New MySqlConnection(connString)
                    conn.Open()
                    Dim sql As String = "DELETE FROM tbl_users WHERE UserID = @id"
                    Dim cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", _selectedID)
                    cmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Account Deleted.")
                ClearForm()
                LoadAccounts("All")

            Catch ex As Exception
                MessageBox.Show("Error deleting: " & ex.Message)
            End Try
        End If
    End Sub

    ' ==========================================
    ' 5. SELECT FROM GRID - UPDATED
    ' ==========================================
    Private Sub dgvAccounts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccounts.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvAccounts.Rows(e.RowIndex)

            _selectedID = Convert.ToInt32(row.Cells("UserID").Value)
            _selectedRole = row.Cells("Role").Value.ToString()

            ' Handle Null FirebaseUID safely
            If IsDBNull(row.Cells("FirebaseUID").Value) Then
                _selectedFirebaseUID = ""
            Else
                _selectedFirebaseUID = row.Cells("FirebaseUID").Value.ToString()
            End If

            ' UPDATED: Load split names into their respective textboxes
            txtLastName.Text = row.Cells("LastName").Value.ToString()
            txtFirstName.Text = row.Cells("FirstName").Value.ToString()

            If IsDBNull(row.Cells("MiddleName").Value) Then
                txtMiddleName.Text = ""
            Else
                txtMiddleName.Text = row.Cells("MiddleName").Value.ToString()
            End If

            txtEmail.Text = row.Cells("Username").Value.ToString()

            ' Handle Null ContactNo safely
            If IsDBNull(row.Cells("ContactNo").Value) Then
                txtPhone.Text = ""
            Else
                txtPhone.Text = row.Cells("ContactNo").Value.ToString()
            End If

            cmbRole.Text = _selectedRole
            txtPassword.Text = "" ' Do not display password for security

            btnSave.Enabled = False
            btnUpdate.Enabled = True
            btnDelete.Enabled = True
        End If
    End Sub

    ' UPDATED CLEAR FORM
    Private Sub ClearForm()
        txtLastName.Clear()
        txtFirstName.Clear()
        txtMiddleName.Clear()
        txtEmail.Clear()
        txtPassword.Clear()
        txtPhone.Clear()
        cmbRole.SelectedIndex = -1 ' Clear selection

        _selectedID = 0
        _selectedFirebaseUID = ""
        _selectedRole = ""

        btnSave.Enabled = True
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
    End Sub

    ' ==========================================
    ' HELPER: INPUT VALIDATION
    ' ==========================================
    Private Function ValidateInputs() As Boolean
        ' 1. Basic Empty Field Check
        If txtLastName.Text.Trim() = "" Or txtFirstName.Text.Trim() = "" Or txtEmail.Text.Trim() = "" Or cmbRole.Text.Trim() = "" Then
            MessageBox.Show("Please fill in all required fields (Last Name, First Name, Email, Role).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' 2. Email Validation (Regex)
        Dim emailPattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        If Not System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim(), emailPattern) Then
            MessageBox.Show("Invalid Email format. Please enter a valid email address (e.g., user@example.com).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtEmail.Focus()
            Return False
        End If

        ' 3. Phone Number Validation
        Dim phone As String = txtPhone.Text.Trim()

        ' Check if it contains only numbers (if not empty)
        If phone <> "" AndAlso Not IsNumeric(phone) Then
            MessageBox.Show("Contact number must contain numbers only.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtPhone.Focus()
            Return False
        End If

        ' Specific Rule: If it starts with "09", it MUST be 11 digits
        If phone.StartsWith("09") Then
            If phone.Length <> 11 Then
                MessageBox.Show("Mobile numbers starting with '09' must be exactly 11 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                txtPhone.Focus()
                Return False
            End If
        End If
        ' Note: If it does not start with 09 (e.g., Landline 028...), we ignore the length check as requested.

        ' 4. Password Check (Only required for NEW accounts, optional for updates if left blank)
        If _selectedID = 0 AndAlso txtPassword.Text.Trim() = "" Then
            MessageBox.Show("Password is required for new accounts.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

End Class