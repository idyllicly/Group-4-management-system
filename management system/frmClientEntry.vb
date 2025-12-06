Imports MySql.Data.MySqlClient

Public Class frmClientEntry

    ' ID property to determine if we are in Add (0) or Edit (>0) mode
    Public Property ClientID As Integer = 0

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    Private Sub frmClientEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ClientID > 0 Then
            LoadClientData()
            Me.Text = "Edit Client Details"
        Else
            Me.Text = "Add New Client"
        End If
    End Sub

    Private Sub LoadClientData()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' We select * so we get all the new name columns
                Dim cmd As New MySqlCommand("SELECT * FROM tbl_clients WHERE ClientID = @id", conn)
                cmd.Parameters.AddWithValue("@id", ClientID)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()

                If reader.Read() Then
                    ' --- NEW NAME FIELDS ---
                    txtClientFirst.Text = If(IsDBNull(reader("ClientFirstName")), "", reader("ClientFirstName").ToString())
                    txtClientMiddle.Text = If(IsDBNull(reader("ClientMiddleName")), "", reader("ClientMiddleName").ToString())
                    txtClientLast.Text = If(IsDBNull(reader("ClientLastName")), "", reader("ClientLastName").ToString())

                    txtContactFirst.Text = If(IsDBNull(reader("ContactFirstName")), "", reader("ContactFirstName").ToString())
                    txtContactMiddle.Text = If(IsDBNull(reader("ContactMiddleName")), "", reader("ContactMiddleName").ToString())
                    txtContactLast.Text = If(IsDBNull(reader("ContactLastName")), "", reader("ContactLastName").ToString())

                    ' --- EXISTING FIELDS ---
                    txtPhone.Text = If(IsDBNull(reader("ContactNumber")), "", reader("ContactNumber").ToString())
                    txtEmail.Text = If(IsDBNull(reader("Email")), "", reader("Email").ToString())
                    txtStreet.Text = If(IsDBNull(reader("StreetAddress")), "", reader("StreetAddress").ToString())
                    txtBarangay.Text = If(IsDBNull(reader("Barangay")), "", reader("Barangay").ToString())
                    txtCity.Text = If(IsDBNull(reader("City")), "", reader("City").ToString())
                End If

            Catch ex As Exception
                MessageBox.Show("Error loading client: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Validation: Ensure at least the Main Client Name and City are there
        If txtClientFirst.Text = "" Or txtClientLast.Text = "" Or txtCity.Text = "" Then
            MessageBox.Show("Client First Name, Last Name, and City are required.")
            Exit Sub
        End If

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim sql As String = ""
            Dim cmd As New MySqlCommand()
            cmd.Connection = conn

            If ClientID = 0 Then
                ' INSERT NEW RECORD (Using the 6 new name columns)
                sql = "INSERT INTO tbl_clients " &
                      "(ClientFirstName, ClientMiddleName, ClientLastName, " &
                      "ContactFirstName, ContactMiddleName, ContactLastName, " &
                      "ContactNumber, Email, StreetAddress, Barangay, City) " &
                      "VALUES " &
                      "(@cFirst, @cMid, @cLast, " &
                      "(@conFirst, @conMid, @conLast, " &
                      "@phone, @email, @street, @brgy, @city)"
            Else
                ' UPDATE EXISTING RECORD
                sql = "UPDATE tbl_clients SET " &
                      "ClientFirstName=@cFirst, ClientMiddleName=@cMid, ClientLastName=@cLast, " &
                      "ContactFirstName=@conFirst, ContactMiddleName=@conMid, ContactLastName=@conLast, " &
                      "ContactNumber=@phone, Email=@email, StreetAddress=@street, Barangay=@brgy, City=@city " &
                      "WHERE ClientID=@id"
                cmd.Parameters.AddWithValue("@id", ClientID)
            End If

            cmd.CommandText = sql

            ' --- CLIENT NAME PARAMS ---
            cmd.Parameters.AddWithValue("@cFirst", txtClientFirst.Text.Trim())
            cmd.Parameters.AddWithValue("@cMid", txtClientMiddle.Text.Trim())
            cmd.Parameters.AddWithValue("@cLast", txtClientLast.Text.Trim())

            ' --- CONTACT PERSON PARAMS ---
            cmd.Parameters.AddWithValue("@conFirst", txtContactFirst.Text.Trim())
            cmd.Parameters.AddWithValue("@conMid", txtContactMiddle.Text.Trim())
            cmd.Parameters.AddWithValue("@conLast", txtContactLast.Text.Trim())

            ' --- ADDRESS & INFO PARAMS ---
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim())
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim())
            cmd.Parameters.AddWithValue("@street", txtStreet.Text.Trim())
            cmd.Parameters.AddWithValue("@brgy", txtBarangay.Text.Trim())
            cmd.Parameters.AddWithValue("@city", txtCity.Text.Trim())

            Try
                cmd.ExecuteNonQuery()
                MessageBox.Show("Saved Successfully!")
                Me.DialogResult = DialogResult.OK
                Me.Close()

            Catch ex As Exception
                MessageBox.Show("Error saving: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class