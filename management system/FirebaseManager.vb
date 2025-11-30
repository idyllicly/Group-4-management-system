Imports FirebaseAdmin
Imports FirebaseAdmin.Auth
Imports Google.Cloud.Firestore
Imports Google.Apis.Auth.OAuth2

Public Class FirebaseManager

    Private Shared _db As FirestoreDb

    ' 1. INITIALIZE (Call this once at app start)
    Public Shared Sub Initialize()
        If FirebaseApp.DefaultInstance Is Nothing Then
            Dim path As String = AppDomain.CurrentDomain.BaseDirectory & "key.json"
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path)

            FirebaseApp.Create(New AppOptions() With {
                .Credential = GoogleCredential.GetApplicationDefault()
            })

            ' Connect to Firestore (Replace with your Project ID)
            _db = FirestoreDb.Create("rrc-tech-app")
        End If
    End Sub

    ' 2. CREATE USER (Auth + Firestore Data)
    Public Shared Async Function CreateTechnicianAccount(email As String, password As String, fullName As String, role As String) As Task(Of String)
        Try
            ' A. Create Authentication Login
            Dim args As New UserRecordArgs() With {
                .Email = email,
                .Password = password,
                .DisplayName = fullName,
                .Disabled = False
            }
            Dim userRecord As UserRecord = Await FirebaseAuth.DefaultInstance.CreateUserAsync(args)
            Dim uid As String = userRecord.Uid

            ' B. Save Profile Data to Firestore (So Mobile App can read it)
            Dim techData As New Dictionary(Of String, Object) From {
                {"name", fullName},
                {"email", email},
                {"role", role},
                {"fcmToken", ""}, ' Will be filled when they login on phone
                {"createdAt", DateTime.UtcNow}
            }

            ' Save to collection 'technicians' with the UID as the Document ID
            Dim docRef As DocumentReference = _db.Collection("technicians").Document(uid)
            Await docRef.SetAsync(techData)

            Return uid ' Return the ID to save in MySQL

        Catch ex As Exception
            Throw New Exception("Firebase Error: " & ex.Message)
        End Try
    End Function

    ' 3. UPDATE USER (Sync Edits)
    Public Shared Async Function UpdateTechnician(uid As String, newName As String, newEmail As String) As Task
        ' Update Auth Display Name
        Dim args As New UserRecordArgs() With {.Uid = uid, .DisplayName = newName, .Email = newEmail}
        Await FirebaseAuth.DefaultInstance.UpdateUserAsync(args)

        ' Update Firestore Data
        Dim docRef As DocumentReference = _db.Collection("technicians").Document(uid)
        Dim updates As New Dictionary(Of String, Object) From {
            {"name", newName},
            {"email", newEmail}
        }
        Await docRef.UpdateAsync(updates)
    End Function

    ' 4. DELETE USER (Remove Access)
    Public Shared Async Function DeleteTechnician(uid As String) As Task
        ' Delete from Auth
        Await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid)

        ' Delete from Firestore
        Await _db.Collection("technicians").Document(uid).DeleteAsync()
    End Function

    ' 5. DISPATCH JOB (Send to Mobile App)
    Public Shared Async Function DispatchJobToMobile(jobID As Integer, clientName As String, address As String, serviceName As String, dateScheduled As Date, techFirebaseUID As String) As Task(Of Boolean)
        Try
            ' A. Prepare Data for the Mobile App
            Dim jobData As New Dictionary(Of String, Object) From {
                {"sql_job_id", jobID},
                {"clientName", clientName},
                {"address", address},
                {"serviceType", serviceName},
                {"scheduleDate", dateScheduled.ToString("yyyy-MM-dd")},
                {"status", "Pending"},
                {"technician_uid", techFirebaseUID}, ' Important: Used for querying in the app
                {"timestamp", DateTime.UtcNow}
            }

            ' B. Save to Firestore collection "assigned_jobs"
            ' Note: We use the SQL ID as the Document ID so it's easy to find later
            Dim docRef As DocumentReference = _db.Collection("assigned_jobs").Document(jobID.ToString())
            Await docRef.SetAsync(jobData)

            Return True
        Catch ex As Exception
            ' Log error but don't crash the app
            Debug.WriteLine("Firebase Upload Error: " & ex.Message)
            Return False
        End Try
    End Function

End Class