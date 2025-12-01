Imports MySql.Data.MySqlClient

Public Class newUcClientManager

    ' ⚠️ UPDATE CONNECTION STRING
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedID As Integer = 0

    Private Sub newUcClientManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadClients("")
    End Sub

    ' === 1. LOAD LIST ===
    Private Sub LoadClients(searchText As String)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                Dim sql As String = "SELECT * FROM tbl_Clients"
                If searchText <> "" Then sql &= " WHERE ClientName LIKE @s OR Address LIKE @s"

                Dim da As New MySqlDataAdapter(sql, conn)
                If searchText <> "" Then da.SelectCommand.Parameters.AddWithValue("@s", "%" & searchText & "%")

                Dim dt As New DataTable()
                da.Fill(dt)
                dgvClientList.DataSource = dt

                ' Hide ID for cleaner look
                If dgvClientList.Columns("ClientID") IsNot Nothing Then
                    dgvClientList.Columns("ClientID").Visible = False
                End If
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadClients(txtSearch.Text)
    End Sub

    ' === 2. ADD NEW CLIENT ===
    Private Sub btnAddClient_Click(sender As Object, e As EventArgs) Handles btnAddClient.Click

        If txtName.Text = "" Or txtAddress.Text = "" Then
            MessageBox.Show("Name and Address are required.")
            Exit Sub
        End If

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Try
                Dim sql As String = "INSERT INTO tbl_Clients (ClientName, ContactPerson, ContactNumber, Address, Email) " &
                                    "VALUES (@name, @person, @phone, @addr, @email)"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@name", txtName.Text)
                cmd.Parameters.AddWithValue("@person", txtContactPerson.Text)
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text)
                cmd.Parameters.AddWithValue("@addr", txtAddress.Text)
                cmd.Parameters.AddWithValue("@email", txtEmail.Text)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Client Added Successfully!")

                ClearForm()
                LoadClients("") ' Refresh list

            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    ' === 3. SELECT CLIENT TO EDIT ===
    Private Sub dgvClientList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientList.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvClientList.Rows(e.RowIndex)

            _selectedID = Convert.ToInt32(row.Cells("ClientID").Value)
            txtName.Text = row.Cells("ClientName").Value.ToString()

            ' Handle potential DBNulls safely
            txtContactPerson.Text = If(IsDBNull(row.Cells("ContactPerson").Value), "", row.Cells("ContactPerson").Value.ToString())
            txtPhone.Text = If(IsDBNull(row.Cells("ContactNumber").Value), "", row.Cells("ContactNumber").Value.ToString())
            txtAddress.Text = row.Cells("Address").Value.ToString()
            txtEmail.Text = If(IsDBNull(row.Cells("Email").Value), "", row.Cells("Email").Value.ToString())

            btnAddClient.Enabled = False ' Disable Add mode
            btnUpdateClient.Enabled = True ' Enable Update mode
        End If
    End Sub

    ' === 4. UPDATE CLIENT ===
    Private Sub btnUpdateClient_Click(sender As Object, e As EventArgs) Handles btnUpdateClient.Click
        If _selectedID = 0 Then Exit Sub

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Try
                Dim sql As String = "UPDATE tbl_Clients SET ClientName=@name, ContactPerson=@person, ContactNumber=@phone, Address=@addr, Email=@email WHERE ClientID=@id"
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@name", txtName.Text)
                cmd.Parameters.AddWithValue("@person", txtContactPerson.Text)
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text)
                cmd.Parameters.AddWithValue("@addr", txtAddress.Text)
                cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                cmd.Parameters.AddWithValue("@id", _selectedID)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Client Updated!")

                ClearForm()
                LoadClients("")
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message)
            End Try
        End Using
    End Sub

    ' === HELPER: CLEAR FORM ===
    Private Sub ClearForm()
        txtName.Clear()
        txtContactPerson.Clear()
        txtPhone.Clear()
        txtAddress.Clear()
        txtEmail.Clear()
        _selectedID = 0
        btnAddClient.Enabled = True
        btnUpdateClient.Enabled = False
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub dgvClientList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientList.CellContentClick

    End Sub
End Class