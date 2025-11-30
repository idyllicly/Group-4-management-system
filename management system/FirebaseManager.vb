Imports FirebaseAdmin
Imports FirebaseAdmin.Auth
Imports Google.Cloud.Firestore
Imports Google.Apis.Auth.OAuth2
Imports MySql.Data.MySqlClient
Imports System.Windows.Forms ' Required for MessageBox
Imports System.IO

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
                ' This looks for key.json in the same folder as the .exe (Debug folder)
                Dim path As String = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json")

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
    Public Shared Async Function DispatchJobToMobile(jobID As Integer, clientName As String, address As String, serviceName As String, dateScheduled As Date, techFirebaseUID As String, jobType As String, Optional serviceID As Integer = 0) As Task(Of Boolean)
        Try
            If _db Is Nothing Then Throw New Exception("Database not initialized. Call Initialize() first.")

            Dim jobData As New Dictionary(Of String, Object) From {
                {"sql_job_id", jobID},
                {"clientName", clientName},
                {"address", address},
                {"serviceType", serviceName},
                {"serviceID", serviceID},
                {"jobType", jobType},
                {"scheduleDate", dateScheduled.ToString("yyyy-MM-dd")},
                {"status", "Pending"},
                {"technician_uid", techFirebaseUID},
                {"syncedToSQL", True}, ' Set to TRUE initially (Office created it, no need to sync back yet)
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
    ' 3. UNIVERSAL SYNC LISTENER (Handles Accept, In Progress, & Complete)
    ' ==========================================
    ' Define an event to notify the Dashboard to refresh UI
    Public Shared Event JobStatusUpdated(jobID As Integer, newStatus As String)

    Public Shared Async Sub ListenForJobUpdates()
        Try
            If _db Is Nothing Then Return ' Safety check

            ' LISTEN FOR ANY JOB where syncedToSQL is FALSE
            ' This means the Mobile App changed something and wants us to know
            Dim query As Query = _db.Collection("assigned_jobs").WhereEqualTo("syncedToSQL", False)

            Dim listener As FirestoreChangeListener = query.Listen(Async Sub(snapshot)
                                                                       For Each change In snapshot.Changes
                                                                           If change.ChangeType = DocumentChange.Type.Added Or change.ChangeType = DocumentChange.Type.Modified Then
                                                                               Dim doc = change.Document
                                                                               ' Process the update based on its status
                                                                               Await ProcessJobUpdate(doc)
                                                                           End If
                                                                       Next
                                                                   End Sub)
        Catch ex As Exception
            MessageBox.Show("Error starting listener: " & ex.Message)
        End Try
    End Sub

    ' ==========================================
    ' 4. PROCESS JOB UPDATE (Route based on Status)
    ' ==========================================
    Private Shared Async Function ProcessJobUpdate(doc As DocumentSnapshot) As Task
        Try
            Dim jobID As Integer = 0
            If doc.ContainsField("sql_job_id") Then
                jobID = Convert.ToInt32(doc.GetValue(Of Object)("sql_job_id"))
            Else
                Integer.TryParse(doc.Id, jobID)
            End If

            Dim status As String = doc.GetValue(Of String)("status")
            Dim jobType As String = ""
            If doc.ContainsField("jobType") Then jobType = doc.GetValue(Of String)("jobType")

            ' --- UPDATE MYSQL BASED ON STATUS ---
            Using conn As New MySqlConnection(connString)
                conn.Open()

                If status.ToLower() = "completed" AndAlso jobType = "Inspection" Then
                    ' *** SPECIAL LOGIC FOR INSPECTIONS ***
                    Await ProcessInspectionData(doc, conn, jobID)
                Else
                    ' *** STANDARD LOGIC FOR SERVICES (Accept, In Progress, or Normal Complete) ***
                    Dim updateSql As String = "UPDATE tbl_joborders SET Status=@stat WHERE JobID=@jid"
                    Using cmd As New MySqlCommand(updateSql, conn)
                        Dim sqlStatus As String = "Pending"
                        Select Case status.ToLower()
                            Case "accepted"
                                sqlStatus = "Accepted"
                            Case "in_progress"
                                sqlStatus = "In Progress"
                            Case "completed"
                                sqlStatus = "Completed"
                            Case Else
                                sqlStatus = status
                        End Select

                        cmd.Parameters.AddWithValue("@stat", sqlStatus)
                        cmd.Parameters.AddWithValue("@jid", jobID)
                        cmd.ExecuteNonQuery()
                    End Using
                End If
            End Using

            ' --- MARK FIREBASE AS SYNCED ---
            ' We set this to true so the listener doesn't fire again until the App changes it back to False
            Dim updates As New Dictionary(Of String, Object) From {{"syncedToSQL", True}}
            Await doc.Reference.UpdateAsync(updates)

            ' --- NOTIFY UI ---
            ' Raises event so the Dashboard can refresh the grid
            RaiseEvent JobStatusUpdated(jobID, status)

        Catch ex As Exception
            Debug.WriteLine("Sync Error: " & ex.Message)
        End Try
    End Function

    ' Extracted Inspection logic to helper
    Private Shared Async Function ProcessInspectionData(doc As DocumentSnapshot, conn As MySqlConnection, jobID As Integer) As Task
        ' Check if quote already exists to avoid duplicates
        Dim checkSql As String = "SELECT COUNT(*) FROM tbl_quotations WHERE InspectionJobID = @jid"
        Using cmdCheck As New MySqlCommand(checkSql, conn)
            cmdCheck.Parameters.AddWithValue("@jid", jobID)
            Dim count As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
            If count > 0 Then Return ' Already saved
        End Using

        ' Extract Inspection Data
        Dim areaSize As Decimal = 0
        Dim price As Decimal = 0
        Dim level As String = "Low"
        Dim remarks As String = ""

        Try
            areaSize = Convert.ToDecimal(doc.GetValue(Of Object)("inspectionData.areaSize"))
            level = doc.GetValue(Of String)("inspectionData.infestationLevel")
            price = Convert.ToDecimal(doc.GetValue(Of Object)("inspectionData.quotedPrice"))
            remarks = doc.GetValue(Of String)("inspectionData.remarks")
        Catch : End Try

        Dim proposedServiceID As Integer = 0
        Try : proposedServiceID = Convert.ToInt32(doc.GetValue(Of Object)("serviceID")) : Catch : End Try

        ' Find Client ID
        Dim finalClientID As Integer = 0
        Dim findClientSql As String = "SELECT COALESCE(Con.ClientID, J.ClientID_TempLink) AS RealClientID FROM tbl_JobOrders J LEFT JOIN tbl_Contracts Con ON J.ContractID = Con.ContractID WHERE J.JobID = @jid"
        Using findCmd As New MySqlCommand(findClientSql, conn)
            findCmd.Parameters.AddWithValue("@jid", jobID)
            Dim result = findCmd.ExecuteScalar()
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then finalClientID = Convert.ToInt32(result)
        End Using

        ' Insert Quote
        Dim sql As String = "INSERT IGNORE INTO tbl_quotations (ClientID, InspectionJobID, ProposedService, AreaSize_Sqm, InfestationLevel, QuotedPrice, Remarks, Status, DateCreated) VALUES (@cid, @jobID, @svcID, @area, @level, @price, @remarks, 'Pending', NOW())"
        Using cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@cid", If(finalClientID > 0, finalClientID, DBNull.Value))
            cmd.Parameters.AddWithValue("@jobID", jobID)
            cmd.Parameters.AddWithValue("@svcID", If(proposedServiceID > 0, proposedServiceID, DBNull.Value))
            cmd.Parameters.AddWithValue("@area", areaSize)
            cmd.Parameters.AddWithValue("@level", level)
            cmd.Parameters.AddWithValue("@price", price)
            cmd.Parameters.AddWithValue("@remarks", remarks)
            cmd.ExecuteNonQuery()
        End Using

        ' Update Job Order Status to Completed
        Dim updateSql As String = "UPDATE tbl_joborders SET Status='Completed' WHERE JobID=@jobID"
        Using updateCmd As New MySqlCommand(updateSql, conn)
            updateCmd.Parameters.AddWithValue("@jobID", jobID)
            updateCmd.ExecuteNonQuery()
        End Using
    End Function

    ' ==========================================
    ' 5. USER MANAGEMENT
    ' ==========================================
    Public Shared Async Function CreateTechnicianAccount(email As String, password As String, fullName As String, role As String) As Task(Of String)
        Try
            Dim args As New UserRecordArgs() With {
                .Email = email,
                .Password = password,
                .DisplayName = fullName,
                .Disabled = False
            }
            Dim userRecord As UserRecord = Await FirebaseAuth.DefaultInstance.CreateUserAsync(args)
            Dim uid As String = userRecord.Uid

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
        Dim args As New UserRecordArgs() With {.Uid = uid, .DisplayName = newName, .Email = newEmail}
        Await FirebaseAuth.DefaultInstance.UpdateUserAsync(args)

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