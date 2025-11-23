Imports MySql.Data.MySqlClient

Module db_connection
    ' 🔑 IMPORTANT: Replace these placeholders with your actual server, database, and user credentials.
    Public Const CONNECTION_STRING As String = "server=YOUR_SERVER_ADDRESS;user id=YOUR_USERNAME;password=YOUR_PASSWORD;database=YOUR_DATABASE_NAME"

    Public Function GetConnection() As MySqlConnection
        Try
            Dim conn As New MySqlConnection(CONNECTION_STRING)
            conn.Open()
            Return conn
        Catch ex As Exception
            MessageBox.Show("Database connection failed: " & ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing ' Return Nothing if connection fails
        End Try
    End Function
End Module