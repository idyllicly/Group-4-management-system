Imports System.Data
Imports MySql.Data.MySqlClient
Imports MySqlConnector

' Parent Class: Handles the raw connection and generic query execution
Public Class DatabaseConnection
    ' connection string - adjust generic credentials if needed
    Private ReadOnly connString As String = "Server=localhost;Database=db_rrcms;Uid=root;Pwd=;"
    Protected conn As MySqlConnection

    Public Sub New()
        conn = New MySqlConnection(connString)
    End Sub

    ' Open Connection safely
    Protected Function Connect() As Boolean
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Connection Error: " & ex.Message)
            Return False
        End Try
    End Function

    ' Close Connection safely
    Protected Sub Disconnect()
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
            End If
        Catch ex As Exception
            MessageBox.Show("Disconnection Error: " & ex.Message)
        End Try
    End Sub

    ' Generic function to retrieve data (SELECT)
    Protected Function ExecuteSelect(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As DataTable
        Dim dt As New DataTable()
        Try
            If Connect() Then
                Using cmd As New MySqlCommand(query, conn)
                    ' Add parameters to prevent SQL Injection
                    If parameters IsNot Nothing Then
                        For Each param In parameters
                            cmd.Parameters.AddWithValue(param.Key, param.Value)
                        Next
                    End If

                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(dt)
                    End Using
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show("Query Error: " & ex.Message)
        Finally
            Disconnect()
        End Try
        Return dt
    End Function

    ' Generic function to modify data (INSERT, UPDATE, DELETE)
    Protected Function ExecuteAction(query As String, Optional parameters As Dictionary(Of String, Object) = Nothing) As Integer
        Dim rowsAffected As Integer = 0
        Try
            If Connect() Then
                Using cmd As New MySqlCommand(query, conn)
                    If parameters IsNot Nothing Then
                        For Each param In parameters
                            cmd.Parameters.AddWithValue(param.Key, param.Value)
                        Next
                    End If
                    rowsAffected = cmd.ExecuteNonQuery()
                End Using
            End If
        Catch ex As Exception
            MessageBox.Show("Action Error: " & ex.Message)
        Finally
            Disconnect()
        End Try
        Return rowsAffected
    End Function
End Class