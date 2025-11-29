Imports MySql.Data.MySqlClient
Imports System.Collections.Generic
Imports System.Data
Imports System.Windows.Forms
Imports System.IO

Public Class DatabaseConnection

    ' ⚠️ CRITICAL: Verify this connection string is correct ⚠️
    Private Const MyConnectionString As String = "Server=localhost;Database=db_rrcms;Uid=root;Pwd=;"

    ''' <summary>
    ''' Helper function to correctly add parameters to the command, handling NOT NULL constraints 
    ''' for string types and allowing DBNull only for the image (BLOB) type.
    ''' </summary>
    Private Sub AddParametersToCommand(ByVal cmd As MySqlCommand, ByVal parameters As Dictionary(Of String, Object))
        If parameters Is Nothing Then Return

        For Each param In parameters
            Dim value As Object = param.Value

            If TypeOf value Is Byte() Then
                ' Handle BLOB (Picture) Data: Use DBNull.Value if no picture data is provided
                cmd.Parameters.Add(param.Key, MySqlDbType.Blob).Value = If(value Is Nothing, DBNull.Value, CType(value, Byte()))
            Else
                ' Handle String/Other Data: 
                ' Since many string columns in tbl_account are NOT NULL, we MUST pass 
                ' String.Empty for empty/null values to avoid MySQL Error 1048.
                Dim strValue As String = If(value Is Nothing OrElse value Is DBNull.Value, String.Empty, CStr(value))

                cmd.Parameters.AddWithValue(param.Key, strValue)
            End If
        Next
    End Sub

    ' ------------------------------------------------------------------
    ' --- 1. Execute Action (INSERT, UPDATE, DELETE) ---
    ' ------------------------------------------------------------------
    Public Function ExecuteAction(ByVal sql As String, ByVal parameters As Dictionary(Of String, Object)) As Integer
        Try
            Using con As New MySqlConnection(MyConnectionString)
                Using cmd As New MySqlCommand(sql, con)
                    AddParametersToCommand(cmd, parameters)

                    con.Open()
                    Return cmd.ExecuteNonQuery()
                End Using
            End Using

        Catch ex As MySqlException
            ' ⭐️ Enhanced Error Reporting: Shows the specific MySQL error ⭐️
            MessageBox.Show(
                $"Database action failed:{vbCrLf}" &
                $"MySQL Error Code: {ex.Number}{vbCrLf}" &
                $"Message: {ex.Message}",
                "Database Error: Constraint Violation or Server Issue",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Return 0

        Catch ex As Exception
            MessageBox.Show($"An unexpected application error occurred: {ex.Message}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0
        End Try
    End Function

    ' ------------------------------------------------------------------
    ' --- 2. Execute Select (The Missing Function) ---
    ' ------------------------------------------------------------------
    ''' <summary>
    ''' Executes a SELECT query and returns the results as a DataTable.
    ''' </summary>
    Public Function ExecuteSelect(ByVal sql As String, ByVal Optional parameters As Dictionary(Of String, Object) = Nothing) As DataTable
        Dim dataTable As New DataTable()

        Try
            Using con As New MySqlConnection(MyConnectionString)
                Using cmd As New MySqlCommand(sql, con)
                    ' Parameters are optional for simple SELECT * FROM table
                    If parameters IsNot Nothing Then
                        AddParametersToCommand(cmd, parameters)
                    End If

                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dataTable)
                    End Using
                End Using
            End Using

        Catch ex As MySqlException
            ' Handle and report SELECT query errors
            MessageBox.Show(
                $"Database SELECT failed:{vbCrLf}" &
                $"MySQL Error Code: {ex.Number}{vbCrLf}" &
                $"Message: {ex.Message}",
                "Database Error: SELECT Failure",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

        Catch ex As Exception
            MessageBox.Show($"An unexpected application error occurred during SELECT: {ex.Message}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dataTable
    End Function

End Class