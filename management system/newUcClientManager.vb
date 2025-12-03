Imports MySql.Data.MySqlClient

Public Class newUcClientManager

    ' Connection string
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedID As Integer = 0

    Private Sub newUcClientManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadClients("")
    End Sub

    ' === 1. LOAD LIST (Updated for New Address Columns) ===
    Private Sub LoadClients(searchText As String)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' We now select from the new structure
                Dim sql As String = "SELECT * FROM tbl_clients"

                ' Updated Search: Searches Name, City, or Barangay
                If searchText <> "" Then
                    sql &= " WHERE ClientName LIKE @s OR City LIKE @s OR Barangay LIKE @s"
                End If

                Dim da As New MySqlDataAdapter(sql, conn)
                If searchText <> "" Then
                    da.SelectCommand.Parameters.AddWithValue("@s", "%" & searchText & "%")
                End If

                Dim dt As New DataTable()
                da.Fill(dt)
                dgvClientList.DataSource = dt

                ' Hide ID for cleaner look
                If dgvClientList.Columns("ClientID") IsNot Nothing Then
                    dgvClientList.Columns("ClientID").Visible = False
                End If

                ' Optional: Hide coordinates if you don't want to see them in the grid
                If dgvClientList.Columns("Coordinates") IsNot Nothing Then
                    dgvClientList.Columns("Coordinates").Visible = False
                End If

            Catch ex As Exception
                MessageBox.Show("Error loading clients: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadClients(txtSearch.Text)
    End Sub

    ' === 2. ADD NEW CLIENT (Splitting Address) ===
    Private Sub btnAddClient_Click(sender As Object, e As EventArgs) Handles btnAddClient.Click

        ' Basic validation
        If txtName.Text = "" Or txtStreet.Text = "" Or txtCity.Text = "" Then
            MessageBox.Show("Name, Street Address, and City are required.")
            Exit Sub
        End If

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Try
                ' UPDATED SQL: Inserting into the 3 separate address columns
                Dim sql As String = "INSERT INTO tbl_clients (ClientName, ContactPerson, ContactNumber, StreetAddress, Barangay, City, Email) " &
                                    "VALUES (@name, @person, @phone, @street, @brgy, @city, @email)"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@name", txtName.Text)
                cmd.Parameters.AddWithValue("@person", txtContactPerson.Text)
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text)

                ' New Address Parameters
                cmd.Parameters.AddWithValue("@street", txtStreet.Text)
                cmd.Parameters.AddWithValue("@brgy", txtBarangay.Text)
                cmd.Parameters.AddWithValue("@city", txtCity.Text)

                cmd.Parameters.AddWithValue("@email", txtEmail.Text)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Client Added Successfully!")

                ClearForm()
                LoadClients("") ' Refresh list

            Catch ex As Exception
                MessageBox.Show("Error adding client: " & ex.Message)
            End Try
        End Using
    End Sub

    ' === 3. SELECT CLIENT TO EDIT (Reading Split Address) ===
    Private Sub dgvClientList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientList.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvClientList.Rows(e.RowIndex)

            _selectedID = Convert.ToInt32(row.Cells("ClientID").Value)
            txtName.Text = row.Cells("ClientName").Value.ToString()

            ' Handle potential DBNulls safely
            txtContactPerson.Text = If(IsDBNull(row.Cells("ContactPerson").Value), "", row.Cells("ContactPerson").Value.ToString())
            txtPhone.Text = If(IsDBNull(row.Cells("ContactNumber").Value), "", row.Cells("ContactNumber").Value.ToString())
            txtEmail.Text = If(IsDBNull(row.Cells("Email").Value), "", row.Cells("Email").Value.ToString())

            ' --- NEW ADDRESS LOGIC ---
            ' We must load the data into the 3 separate textboxes
            txtStreet.Text = If(IsDBNull(row.Cells("StreetAddress").Value), "", row.Cells("StreetAddress").Value.ToString())
            txtBarangay.Text = If(IsDBNull(row.Cells("Barangay").Value), "", row.Cells("Barangay").Value.ToString())
            txtCity.Text = If(IsDBNull(row.Cells("City").Value), "", row.Cells("City").Value.ToString())

            btnAddClient.Enabled = False ' Disable Add mode
            btnUpdateClient.Enabled = True ' Enable Update mode
        End If
    End Sub

    ' === 4. UPDATE CLIENT (Saving Split Address) ===
    Private Sub btnUpdateClient_Click(sender As Object, e As EventArgs) Handles btnUpdateClient.Click
        If _selectedID = 0 Then Exit Sub

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Try
                ' UPDATED SQL: Updating the 3 separate address columns
                Dim sql As String = "UPDATE tbl_clients SET " &
                                    "ClientName=@name, ContactPerson=@person, ContactNumber=@phone, " &
                                    "StreetAddress=@street, Barangay=@brgy, City=@city, Email=@email " &
                                    "WHERE ClientID=@id"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@name", txtName.Text)
                cmd.Parameters.AddWithValue("@person", txtContactPerson.Text)
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text)

                ' New Address Parameters
                cmd.Parameters.AddWithValue("@street", txtStreet.Text)
                cmd.Parameters.AddWithValue("@brgy", txtBarangay.Text)
                cmd.Parameters.AddWithValue("@city", txtCity.Text)

                cmd.Parameters.AddWithValue("@email", txtEmail.Text)
                cmd.Parameters.AddWithValue("@id", _selectedID)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Client Updated!")

                ClearForm()
                LoadClients("")
            Catch ex As Exception
                MessageBox.Show("Error updating client: " & ex.Message)
            End Try
        End Using
    End Sub

    ' === HELPER: CLEAR FORM ===
    Private Sub ClearForm()
        txtName.Clear()
        txtContactPerson.Clear()
        txtPhone.Clear()
        txtEmail.Clear()

        ' Clear the 3 new boxes
        txtStreet.Clear()
        txtBarangay.Clear()
        txtCity.Clear()

        _selectedID = 0
        btnAddClient.Enabled = True
        btnUpdateClient.Enabled = False
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        ClearForm()
    End Sub

    Private Sub dgvClientList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientList.CellContentClick
        ' Usually empty unless using buttons inside the grid
    End Sub
End Class