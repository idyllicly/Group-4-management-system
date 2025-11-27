Imports Google.Cloud.Firestore
Imports System.Environment
Imports FirebaseAdmin.Messaging
Imports FirebaseAdmin
Imports Google.Apis.Auth.OAuth2

Public Class Form1

    ' Variable to hold our database connection
    Private db As FirestoreDb

    ' --- 1. ROBUST INITIALIZATION METHOD ---
    ' This ensures Firebase is ready before we try to use it
    Private Sub EnsureFirebaseInitialized()
        ' Check if already initialized to prevent crashing
        If FirebaseApp.DefaultInstance IsNot Nothing Then
            Return
        End If

        Try
            Dim path As String = AppDomain.CurrentDomain.BaseDirectory + "key.json"
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path)

            FirebaseApp.Create(New AppOptions() With {
                .Credential = GoogleCredential.GetApplicationDefault()
            })
        Catch ex As Exception
            MessageBox.Show("Firebase Init Failed: " & ex.Message)
        End Try
    End Sub

    ' --- 2. FORM LOAD ---
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' A. Initialize the Admin SDK (Crucial for Notifications)
            EnsureFirebaseInitialized()

            ' B. Connect to Firestore (Crucial for Database)
            ' Note: EnsureFirebaseInitialized sets the Env Variable, so we are good to go.
            ' REPLACE 'rrc-tech-app' with your Project ID if different
            db = FirestoreDb.Create("rrc-tech-app")

        Catch ex As Exception
            MessageBox.Show("Error connecting to Firebase: " & ex.Message)
        End Try
    End Sub

    ' --- 3. BUTTON CLICK ---
    Private Async Sub btnAssignJob_Click(sender As Object, e As EventArgs) Handles btnAssignJob.Click
        ' Safety Check: Ensure init ran successfully
        EnsureFirebaseInitialized()

        If FirebaseMessaging.DefaultInstance Is Nothing Then
            MessageBox.Show("Critical Error: Firebase Messaging failed to start. Check key.json.")
            Return
        End If

        ' Disable button
        btnAssignJob.Enabled = False
        btnAssignJob.Text = "Processing..."

        Try
            ' --- PART A: SAVE TO DATABASE ---
            Dim technicianId As String = "5BmdL4dcYWdOrFi60rAcK80IjeK2" ' Your Real ID

            Dim jobData As New Dictionary(Of String, Object) From {
                {"clientName", "Euro gaufo"},
                {"address", "dlanay area b Quezon City, Manila"},
                {"serviceType", "general pest control"},
                {"description", "may mga piste."},
                {"schedule", DateTime.Now.ToString("MMMM dd, yyyy h:mm tt")},
                {"status", "pending"},
                {"assignedBy", "vince pondavilla"},
                {"technicianId", technicianId}
            }

            Dim collection As CollectionReference = db.Collection("jobs")
            Dim docRef As DocumentReference = Await collection.AddAsync(jobData)

            ' --- PART B: SEND NOTIFICATION ---

            ' 1. Find the technician's phone token
            Dim techDoc As DocumentSnapshot = Await db.Collection("technicians").Document(technicianId).GetSnapshotAsync()

            If techDoc.Exists AndAlso techDoc.ContainsField("fcmToken") Then
                Dim token As String = techDoc.GetValue(Of String)("fcmToken")

                ' 2. Create the message
                ' FIX: Using full namespace to avoid collision with Windows Message
                Dim message = New FirebaseAdmin.Messaging.Message() With {
                    .Token = token,
                    .Notification = New FirebaseAdmin.Messaging.Notification() With {
                        .Title = "New Job Assigned!",
                        .Body = "Client: euro gaufo"
                    },
                    .Data = New Dictionary(Of String, String) From {
                        {"jobId", docRef.Id}
                    }
                }

                ' 3. Send it!
                Dim response As String = Await FirebaseMessaging.DefaultInstance.SendAsync(message)
                MessageBox.Show("Job Saved & Notification Sent! ID: " & docRef.Id)
            Else
                MessageBox.Show("Job Saved, but Technician has no token (Login to app first).")
            End If

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        Finally
            btnAssignJob.Enabled = True
            btnAssignJob.Text = "Assign Job"
        End Try
    End Sub

End Class