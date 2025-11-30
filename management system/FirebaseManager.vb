Imports FirebaseAdmin
Imports FirebaseAdmin.Auth
Imports Google.Cloud.Firestore
Imports Google.Apis.Auth.OAuth2
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms ' Required for MessageBox

Public Class FirebaseManager

    Private Shared _db As FirestoreDb
    ' ⚠️ CHECK YOUR CONNECTION STRING
    Private Shared connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' ==========================================
    ' 1. INITIALIZE (Call this once at app start)
    ' ==========================================
    Public Shared Sub Initialize()
        If FirebaseApp.DefaultInstance Is Nothing Then
            Try
                Dim path As String = AppDomain.CurrentDomain.BaseDirectory & "key.json"
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path)

                FirebaseApp.Create(New AppOptions() With {
                    .Credential = GoogleCredential.GetApplicationDefault()
                })

                ' Connect to Firestore (Replace with your Project ID)
                _db = FirestoreDb.Create("rrc-tech-app")
            Catch ex As Exception
                MessageBox.Show("Firebase Init Error: " & ex.Message)
            End Try
        End If
    End Sub

    ' ==========================================
    ' 2. DISPATCH JOB (Updated to include ServiceID)
    ' ==========================================
    ' Added 'Optional serviceID' so we can track the package ID through the system
    Public Shared Async Function DispatchJobToMobile(jobID As Integer, clientName As String, address As String, serviceName As String, dateScheduled As Date, techFirebaseUID As String, jobType As String, Optional serviceID As Integer = 0) As Task(Of Boolean)
        Try
            Dim jobData As New Dictionary(Of String, Object) From {
                {"sql_job_id", jobID},
                {"clientName", clientName},
                {"address", address},
                {"serviceType", serviceName},
                {"serviceID", serviceID}, ' Storing the ID hidden in Firebase
                {"jobType", jobType},
                {"scheduleDate", dateScheduled.ToString("yyyy-MM-dd")},
                {"status", "Pending"},
                {"technician_uid", techFirebaseUID},
                {"syncedToSQL", False},
                {"timestamp", DateTime.UtcNow}
            }

            ' We use the SQL ID as the Document ID so we can easily find it later
            Dim docRef As DocumentReference = _db.Collection("assigned_jobs").Document(jobID.ToString())
            Await docRef.SetAsync(jobData)
            Return True

        Catch ex As Exception
            MessageBox.Show("Firebase Upload Error: " & ex.Message)
            Return False
        End Try
    End Function


    ' ==========================================
    ' 3. SYNC LISTENER (Call on Form Load)
    ' ==========================================
    Public Shared Async Sub ListenForCompletedInspections()
        Try
            Dim query As Query = _db.Collection("assigned_jobs").WhereEqualTo("status", "completed").WhereEqualTo("syncedToSQL", False)

            Dim listener As FirestoreChangeListener = query.Listen(Async Sub(snapshot)
                                                                       For Each change In snapshot.Changes
                                                                           If change.ChangeType = DocumentChange.Type.Added Or change.ChangeType = DocumentChange.Type.Modified Then
                                                                               Dim doc = change.Document
                                                                               If doc.ContainsField("jobType") AndAlso doc.GetValue(Of String)("jobType") = "Inspection" Then
                                                                                   Await ProcessInspectionResult(doc)
                                                                               End If
                                                                           End If
                                                                       Next
                                                                   End Sub)
        Catch ex As Exception
            MessageBox.Show("Error starting listener: " & ex.Message)
        End Try
    End Sub

    ' ==========================================
    ' 4. PROCESS RESULT (Helper for Sync Listener)
    ' ==========================================
    Private Shared Async Function ProcessInspectionResult(doc As DocumentSnapshot) As Task
        Try
            ' --- A. GET DATA ---
            Dim jobID As Integer = Convert.ToInt32(doc.GetValue(Of Object)("sql_job_id"))

            Dim areaSize As Decimal = Convert.ToDecimal(doc.GetValue(Of Object)("inspectionData.areaSize"))
            Dim level As String = doc.GetValue(Of String)("inspectionData.infestationLevel")
            Dim price As Decimal = Convert.ToDecimal(doc.GetValue(Of Object)("inspectionData.quotedPrice"))

            ' Get Remarks
            Dim remarks As String = ""
            Try
                remarks = doc.GetValue(Of String)("inspectionData.remarks")
            Catch
                remarks = ""
            End Try

            ' Get Proposed Service ID (This comes from the original dispatch data)
            Dim proposedServiceID As Integer = 0
            Try
                proposedServiceID = Convert.ToInt32(doc.GetValue(Of Object)("serviceID"))
            Catch
                proposedServiceID = 0
            End Try

            ' --- B. SAVE TO MYSQL ---
            Using conn As New MySqlConnection(connString)
                conn.Open()

                ' 1. FIND THE CLIENT ID
                Dim finalClientID As Integer = 0
                Dim findClientSql As String = "SELECT COALESCE(Con.ClientID, J.ClientID_TempLink) AS RealClientID " &
                                              "FROM tbl_JobOrders J " &
                                              "LEFT JOIN tbl_Contracts Con ON J.ContractID = Con.ContractID " &
                                              "WHERE J.JobID = @jid"

                Using findCmd As New MySqlCommand(findClientSql, conn)
                    findCmd.Parameters.AddWithValue("@jid", jobID)
                    Dim result = findCmd.ExecuteScalar()
                    If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                        finalClientID = Convert.ToInt32(result)
                    End If
                End Using

                ' 2. INSERT INTO tbl_quotations (Includes ProposedService now)
                Dim sql As String = "INSERT INTO tbl_quotations (ClientID, InspectionJobID, ProposedService, AreaSize_Sqm, InfestationLevel, QuotedPrice, Remarks, Status, DateCreated) " &
                                    "VALUES (@cid, @jobID, @svcID, @area, @level, @price, @remarks, 'Pending', NOW())"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@cid", If(finalClientID > 0, finalClientID, DBNull.Value))
                cmd.Parameters.AddWithValue("@jobID", jobID)
                cmd.Parameters.AddWithValue("@svcID", If(proposedServiceID > 0, proposedServiceID, DBNull.Value)) ' Save the package ID
                cmd.Parameters.AddWithValue("@area", areaSize)
                cmd.Parameters.AddWithValue("@level", level)
                cmd.Parameters.AddWithValue("@price", price)
                cmd.Parameters.AddWithValue("@remarks", remarks)
                cmd.ExecuteNonQuery()

                ' 3. Update tbl_joborders
                Dim updateSql As String = "UPDATE tbl_joborders SET Status='Completed' WHERE JobID=@jobID"
                Dim updateCmd As New MySqlCommand(updateSql, conn)
                updateCmd.Parameters.AddWithValue("@jobID", jobID)
                updateCmd.ExecuteNonQuery()
            End Using

            ' --- C. UPDATE FIREBASE ---
            Dim updates As New Dictionary(Of String, Object) From {{"syncedToSQL", True}}
            Await doc.Reference.UpdateAsync(updates)

            MessageBox.Show($"SUCCESS! Quote saved for Job #{jobID}. Service ID: {proposedServiceID}")

        Catch ex As Exception
            MessageBox.Show("SYNC ERROR in ProcessInspectionResult: " & ex.Message)
        End Try
    End Function

    ' ==========================================
    ' 5. USER MANAGEMENT (Account Creation)
    ' ==========================================
    Public Shared Async Function CreateTechnicianAccount(email As String, password As String, fullName As String, role As String) As Task(Of String)
        Try
            ' Create Authentication Login
            Dim args As New UserRecordArgs() With {
                .Email = email,
                .Password = password,
                .DisplayName = fullName,
                .Disabled = False
            }
            Dim userRecord As UserRecord = Await FirebaseAuth.DefaultInstance.CreateUserAsync(args)
            Dim uid As String = userRecord.Uid

            ' Save Profile Data to Firestore
            Dim techData As New Dictionary(Of String, Object) From {
                {"name", fullName},
                {"email", email},
                {"role", role},
                {"fcmToken", ""},
                {"createdAt", DateTime.UtcNow}
            }

            Await _db.Collection("technicians").Document(uid).SetAsync(techData)
            Return uid

        Catch ex As Exception
            Throw New Exception("Firebase Account Error: " & ex.Message)
        End Try
    End Function

    Public Shared Async Function UpdateTechnician(uid As String, newName As String, newEmail As String) As Task
        ' Update Auth
        Dim args As New UserRecordArgs() With {.Uid = uid, .DisplayName = newName, .Email = newEmail}
        Await FirebaseAuth.DefaultInstance.UpdateUserAsync(args)

        ' Update Firestore
        Dim updates As New Dictionary(Of String, Object) From {
            {"name", newName},
            {"email", newEmail}
        }
        Await _db.Collection("technicians").Document(uid).UpdateAsync(updates)
    End Function

    Public Shared Async Function DeleteTechnician(uid As String) As Task
        Await FirebaseAuth.DefaultInstance.DeleteUserAsync(uid)
        Await _db.Collection("technicians").Document(uid).DeleteAsync()
    End Function

End Class