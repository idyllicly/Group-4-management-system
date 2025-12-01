Imports MySql.Data.MySqlClient

Public Class newUcAccountManager

    ' ⚠️ Ensure your database has the 'ContactNumber' column in tbl_account
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    Private _selectedLocalID As Integer = 0
    Private _selectedFirebaseUID As String = ""
    Private _selectedRoleCategory As String = ""

    Private Sub newUcAccountManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAccounts("All")
        ' Initialize Firebase
        FirebaseManager.Initialize()
    End Sub

    ' ==========================================
    ' 1. MASTER LOADER (Updated to fetch Admin Phone)
    ' ==========================================
    Private Sub LoadAccounts(roleFilter As String)
        Dim dtMerged As New DataTable()
        dtMerged.Columns.Add("ID", GetType(Integer))
        dtMerged.Columns.Add("Name", GetType(String))
        dtMerged.Columns.Add("Role", GetType(String))
        dtMerged.Columns.Add("Username", GetType(String))
        dtMerged.Columns.Add("Phone", GetType(String))
        dtMerged.Columns.Add("FirebaseUID", GetType(String))
        dtMerged.Columns.Add("SourceTable", GetType(String))

        Using conn As New MySqlConnection(connString)
            conn.Open()

            ' A. FETCH TECHNICIANS (Field Staff - Uses 'ContactNo')
            If roleFilter = "All" Or roleFilter = "Technician" Then
                Dim sqlTech As String = "SELECT TechnicianID, CONCAT(FirstName, ' ', LastName) as Name, 'Technician' as Role, AppUsername, ContactNo, FirebaseUID FROM tbl_Technicians WHERE Status='Active'"
                Dim cmd As New MySqlCommand(sqlTech, conn)
                Dim rdr As MySqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    dtMerged.Rows.Add(rdr("TechnicianID"), rdr("Name"), "Technician", rdr("AppUsername"), rdr("ContactNo"), rdr("FirebaseUID"), "tbl_Technicians")
                End While
                rdr.Close()
            End If

            ' B. FETCH ADMINS (Office Staff - Uses 'ContactNumber')
            If roleFilter = "All" Or roleFilter = "Admin" Or roleFilter = "Super Admin" Then
                ' UPDATED: Now selects ContactNumber [cite: 7]
                Dim sqlAdmin As String = "SELECT AccountID, FullName, ContactNumber, Role, Username FROM tbl_Account"
                Dim cmd As New MySqlCommand(sqlAdmin, conn)
                Dim rdr As MySqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    ' Handle potential NULL values for ContactNumber
                    Dim phone As String = If(IsDBNull(rdr("ContactNumber")), "", rdr("ContactNumber").ToString())

                    dtMerged.Rows.Add(rdr("AccountID"), rdr("FullName"), rdr("Role"), rdr("Username"), phone, "", "tbl_Account")
                End While
                rdr.Close()
            End If
        End Using

        dgvAccounts.DataSource = dtMerged

        ' Clean up the grid view (Hide ID columns)
        If dgvAccounts.Columns("ID") IsNot Nothing Then dgvAccounts.Columns("ID").Visible = False
        If dgvAccounts.Columns("FirebaseUID") IsNot Nothing Then dgvAccounts.Columns("FirebaseUID").Visible = False
        If dgvAccounts.Columns("SourceTable") IsNot Nothing Then dgvAccounts.Columns("SourceTable").Visible = False
    End Sub

    Private Sub cmbFilterRole_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFilterRole.SelectedIndexChanged
        LoadAccounts(cmbFilterRole.Text)
    End Sub

    ' ==========================================
    ' 2. ADD ACCOUNT (Updated to Save Admin Phone)
    ' ==========================================
    Private Async Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        ' Validation
        If txtName.Text = "" Or txtEmail.Text = "" Or txtPassword.Text = "" Or cmbRole.Text = "" Then
            MessageBox.Show("Please fill in all fields.")
            Exit Sub
        End If

        Dim isTechnician As Boolean = (cmbRole.Text = "Technician")
        Dim firebaseID As String = ""

        btnSave.Enabled = False
        btnSave.Text = "Processing..."

        Try
            ' === LOGIC A: IF TECHNICIAN (Create Firebase Account) ===
            If isTechnician Then
                ' Mobile App needs a real email to login
                If Not txtEmail.Text.Contains("@") Then
                    MessageBox.Show("Technicians require a valid email address for the Mobile App (e.g. name@gmail.com).")
                    btnSave.Enabled = True
                    Exit Sub
                End If

                btnSave.Text = "Syncing to Cloud..."
                firebaseID = Await FirebaseManager.CreateTechnicianAccount(txtEmail.Text, txtPassword.Text, txtName.Text, "Technician")

                ' Check if Firebase failed
                If firebaseID.StartsWith("Error") Then
                    MessageBox.Show(firebaseID)
                    btnSave.Enabled = True
                    Exit Sub
                End If
            End If

            ' === LOGIC B: SAVE TO DATABASE (Both Admin & Tech) ===
            Using conn As New MySqlConnection(connString)
                conn.Open()
                Dim sql As String = ""
                Dim cmd As New MySqlCommand()
                cmd.Connection = conn

                If isTechnician Then
                    ' INSERT TECHNICIAN (With Firebase Link)
                    sql = "INSERT INTO tbl_Technicians (FirstName, LastName, ContactNo, AppUsername, AppPassword, FirebaseUID, Status) " &
                          "VALUES (@fname, @lname, @phone, @user, @pass, @uid, 'Active')"

                    ' Split Name for DB consistency
                    Dim names() As String = txtName.Text.Split(" "c)
                    Dim fName As String = names(0)
                    Dim lName As String = If(names.Length > 1, names(names.Length - 1), "")

                    cmd.Parameters.AddWithValue("@fname", fName)
                    cmd.Parameters.AddWithValue("@lname", lName)
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text)
                    cmd.Parameters.AddWithValue("@user", txtEmail.Text)
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
                    cmd.Parameters.AddWithValue("@uid", firebaseID)

                Else
                    ' INSERT ADMIN (Local Only)
                    ' UPDATED: Now inserts ContactNumber 
                    sql = "INSERT INTO tbl_Account (FullName, ContactNumber, Role, Username, Password) " &
                          "VALUES (@name, @phone, @role, @user, @pass)"

                    cmd.Parameters.AddWithValue("@name", txtName.Text)
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text) ' Added this parameter
                    cmd.Parameters.AddWithValue("@role", cmbRole.Text)
                    cmd.Parameters.AddWithValue("@user", txtEmail.Text) ' Admin uses this as Username
                    cmd.Parameters.AddWithValue("@pass", txtPassword.Text)
                End If

                cmd.CommandText = sql
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
    ' 3. DELETE LOGIC (Only delete Firebase if Tech)
    ' ==========================================
    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If _selectedLocalID = 0 Then Exit Sub

        If MessageBox.Show("Are you sure you want to delete this account?", "Confirm", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Try
                ' Only delete from cloud if it IS a technician and HAS a UID
                If _selectedRoleCategory = "Technician" AndAlso _selectedFirebaseUID <> "" Then
                    Await FirebaseManager.DeleteTechnician(_selectedFirebaseUID)
                End If

                ' Delete from Local DB
                Using conn As New MySqlConnection(connString)
                    conn.Open()
                    Dim sql As String = ""
                    If _selectedRoleCategory = "Technician" Then
                        sql = "DELETE FROM tbl_Technicians WHERE TechnicianID = @id"
                    Else
                        sql = "DELETE FROM tbl_Account WHERE AccountID = @id"
                    End If

                    Dim cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@id", _selectedLocalID)
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
    ' 4. SELECT FROM GRID
    ' ==========================================
    Private Sub dgvAccounts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAccounts.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvAccounts.Rows(e.RowIndex)

            _selectedLocalID = Convert.ToInt32(row.Cells("ID").Value)
            _selectedFirebaseUID = row.Cells("FirebaseUID").Value.ToString()
            _selectedRoleCategory = row.Cells("Role").Value.ToString()

            ' Slight hack: If role isn't Technician, treat as Admin
            If _selectedRoleCategory <> "Technician" Then _selectedRoleCategory = "Admin"

            txtName.Text = row.Cells("Name").Value.ToString()
            txtEmail.Text = row.Cells("Username").Value.ToString()
            txtPhone.Text = row.Cells("Phone").Value.ToString()
            cmbRole.Text = row.Cells("Role").Value.ToString()

            txtPassword.Text = "" ' Security

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
        _selectedLocalID = 0
        _selectedFirebaseUID = ""
        btnSave.Enabled = True
        btnUpdate.Enabled = False
        btnDelete.Enabled = False
    End Sub

End Class