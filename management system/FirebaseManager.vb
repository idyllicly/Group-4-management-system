Imports FirebaseAdmin
Imports FirebaseAdmin.Auth
Imports Google.Apis.Auth.OAuth2

Public Class FirebaseManager

    ' 1. CONNECT TO FIREBASE
    ' This function finds your key.json and opens the connection.
    Public Shared Sub Initialize()
        Try
            ' Check if we are already connected so we don't crash
            If FirebaseApp.DefaultInstance Is Nothing Then

                ' Find the key file
                Dim path As String = AppDomain.CurrentDomain.BaseDirectory & "key.json"

                ' Connect!
                FirebaseApp.Create(New AppOptions() With {
                    .Credential = GoogleCredential.FromFile(path)
                })
            End If
        Catch ex As Exception
            MessageBox.Show("Could not connect to Firebase: " & ex.Message)
        End Try
    End Sub

    ' 2. CREATE THE USER
    ' This sends the Email/Password to Firebase Authentication
    Public Shared Async Function CreateUser(email As String, password As String, displayName As String) As Task(Of String)
        Try
            Dim args As New UserRecordArgs() With {
                .Email = email,
                .Password = password,
                .DisplayName = displayName,
                .EmailVerified = True,
                .Disabled = False
            }

            ' The actual API call
            Dim userRecord As UserRecord = Await FirebaseAuth.DefaultInstance.CreateUserAsync(args)

            ' If successful, return the new User ID (UID)
            Return userRecord.Uid

        Catch ex As Exception
            ' If it fails (e.g., email already exists), return the error message
            Return "Error: " & ex.Message
        End Try
    End Function

End Class