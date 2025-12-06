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
    End Sub

    ' ==========================================
    ' 1. MASTER LOADER (Now fetches from tbl_users)
    ' ==========================================
    Private Sub LoadAccounts(roleFilter As String)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                Dim sql As String = "SELECT UserID, FullName, Role, Username, ContactNo, FirebaseUID FROM tbl_users"

                ' Apply Filter
                If roleFilter <> "All" Then
                    sql &= " WHERE Role = @role"
                End If

                ' Order by ID
                sql &= " ORDER BY UserID DESC"

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
    ' 2. SAVE ACCOUNT (Create New)
    ' ==========================================
    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        ' Validation
        If txtName.Text = "" Or txtEmail.Text = "" Or txtPassword.Text = "" Or cmbRole.Text = "" Then
            MessageBox.Show("Please fill in all fields.")
            Exit Sub
        End If

        Dim role As String = cmbRole.Text
        Dim firebaseID As String = ""

        btnSave.Enabled = False
        btnSave.Text = "Processing..."

        Try
            ' === STEP A: FIREBASE (Only if Technician) ===
            If role = "Technician" Then
                If Not txtEmail.Text.Contains("@") Then
                    MessageBox.Show("Technicians require a valid email address.")
                    btnSave.Enabled = True
                    Exit Sub
                End If

                btnSave.Text = "Syncing to Cloud..."
                ' Create in Firebase
                firebaseID = Await FirebaseManager.CreateTechnicianAccount(txtEmail.Text, txtPassword.Text, txtName.Text, "Technician")

                If firebaseID.StartsWith("Error") Then
                    MessageBox.Show(firebaseID)
                    btnSave.Enabled = True
                    Exit Sub
                End If
            End If

            ' === STEP B: DATABASE INSERT (One Table Rule) ===
            Using conn As New MySqlConnection(connString)
                conn.Open()


                Dim sql As String = "INSERT INTO tbl_users (FullName, Role, Username, Password, ContactNo, FirebaseUID, Status) " &
                                    "VALUES (@name, @role, @user, @pass, @phone, @uid, 'Active')"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@name", txtName.Text)
                cmd.Parameters.AddWithValue("@role", role)
                cmd.Parameters.AddWithValue("@user", txtEmail.Text)
                cmd.Parameters.AddWithValue("@pass", txtPassword.Text) ' Ideally, hash this!
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text)

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
    ' 3. UPDATE ACCOUNT
    ' ==========================================
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If _selectedID = 0 Then Exit Sub

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' We only update basic info. Password updates usually require a separate flow for security, 
                ' but for this example, we update it if the box isn't empty.

                Dim sql As String = "UPDATE tbl_users SET FullName=@name, Username=@user, ContactNo=@phone, Role=@role"

                If txtPassword.Text <> "" Then
                    sql &= ", Password=@pass"
                End If

                sql &= " WHERE UserID=@id"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@name", txtName.Text)
                cmd.Parameters.AddWithValue("@user", txtEmail.Text)
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text)
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
    ' 4. DELETE LOGIC
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
    ' 5. SELECT FROM GRID
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

            txtName.Text = row.Cells("FullName").Value.ToString()
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

    Private Sub ClearForm()
        txtName.Clear()
        txtEmail.Clear()
        txtPassword.Clear()
        txtPhone.Clear()

        _selectedID = 0
        _selectedFirebaseUID = ""

        btnSave.Enabled = True
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
    End Sub

    Private Sub grpDetails_Enter(sender As Object, e As EventArgs) Handles grpDetails.Enter

    End Sub
End Class