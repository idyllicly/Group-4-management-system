Imports System.IO
Imports System.Diagnostics
Imports MySql.Data.MySqlClient

Public Class DatabaseManager

    ' =========================================================
    ' CONFIGURATION
    ' =========================================================
    ' We use a custom port (3307) to avoid conflicts if the user 
    ' already has XAMPP (which uses 3306) installed.
    Private Shared dbPort As String = "3307"

    ' The process object to keep track of the running database server
    Private Shared dbProcess As Process

    ' PUBLIC CONNECTION STRING
    ' Use this variable in all your other Forms instead of hardcoding it!
    ' It points to 127.0.0.1 (Localhost) on our custom port 3307.
    Public Shared ConnectionString As String = $"server=127.0.0.1;port={dbPort};user id=root;password=;database=db_rrcms;Convert Zero Datetime=True;"

    ' =========================================================
    ' 1. START SERVER
    ' Call this in your Login Form (Form_Load)
    ' =========================================================
    ' =========================================================
    ' 1. START SERVER (UPDATED TO FIX LOGIN ERROR)
    ' =========================================================
    Public Shared Sub StartServer()
        Dim appPath As String = Application.StartupPath
        ' Define paths
        Dim mysqldPath As String = Path.Combine(appPath, "db_server\bin\mysqld.exe")
        Dim dataPath As String = Path.Combine(appPath, "db_server\data")

        ' Safety Check
        If Not File.Exists(mysqldPath) Then
            MessageBox.Show("Database engine not found at: " & mysqldPath, "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim startInfo As New ProcessStartInfo()
        startInfo.FileName = mysqldPath

        ' --- THE FIX IS HERE ---
        ' Added: --skip-grant-tables (Bypasses the plugin error)
        ' Added: --bind-address=127.0.0.1 (Security: only allow local connections)
        startInfo.Arguments = $"--port={dbPort} --datadir=""{dataPath}"" --console --skip-grant-tables --bind-address=127.0.0.1"

        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.CreateNoWindow = True
        startInfo.UseShellExecute = False
        startInfo.RedirectStandardOutput = True

        Try
            dbProcess = Process.Start(startInfo)

            ' Wait 3 seconds for the engine to warm up
            System.Threading.Thread.Sleep(3000)

            CheckAndInstallDatabase()

        Catch ex As Exception
            MessageBox.Show("Failed to start local database: " & ex.Message)
        End Try
    End Sub

    ' =========================================================
    ' 2. STOP SERVER
    ' Call this in your Login Form (Form_Closed)
    ' =========================================================
    Public Shared Sub StopServer()
        Try
            If dbProcess IsNot Nothing AndAlso Not dbProcess.HasExited Then
                dbProcess.Kill()
                dbProcess.Dispose()
            End If
        Catch ex As Exception
            ' Ignore errors on close
        End Try
    End Sub

    ' =========================================================
    ' 3. AUTO-INSTALL LOGIC
    ' Checks if DB exists; if not, creates it and adds Admin.
    ' =========================================================
    Private Shared Sub CheckAndInstallDatabase()
        ' Connect to the SERVER (no database selected yet) to check existence
        Dim masterConnString As String = $"server=127.0.0.1;port={dbPort};user id=root;password=;"

        Using conn As New MySqlConnection(masterConnString)
            Try
                conn.Open()

                ' Check if our specific database 'db_rrcms' exists
                Dim cmdCheck As New MySqlCommand("SHOW DATABASES LIKE 'db_rrcms'", conn)
                Dim result = cmdCheck.ExecuteScalar()

                If result Is Nothing Then
                    ' DATABASE MISSING -> INSTALL FRESH
                    CreateDatabase(conn)
                End If

            Catch ex As Exception
                MessageBox.Show("Error checking database status: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Shared Sub CreateDatabase(conn As MySqlConnection)
        Try
            ' A. Create the Database Container
            Dim cmdCreate As New MySqlCommand("CREATE DATABASE db_rrcms;", conn)
            cmdCreate.ExecuteNonQuery()

            ' B. Switch to using that new database
            conn.ChangeDatabase("db_rrcms")

            ' C. The Master SQL Script
            ' This creates all your tables and inserts the first Admin account.
            ' I have combined the tables from your upload into this script.
            Dim sqlScript As String = "
                -- 1. USERS TABLE
                CREATE TABLE IF NOT EXISTS tbl_users (
                    UserID INT AUTO_INCREMENT PRIMARY KEY,
                    FirstName VARCHAR(100), MiddleName VARCHAR(100), LastName VARCHAR(100),
                    Role VARCHAR(50), Username VARCHAR(100), Password VARCHAR(100),
                    ContactNo VARCHAR(50), FirebaseUID VARCHAR(100), Status VARCHAR(20)
                );

                -- 2. CLIENTS TABLE
                CREATE TABLE IF NOT EXISTS tbl_clients (
                    ClientID INT AUTO_INCREMENT PRIMARY KEY,
                    ClientFirstName VARCHAR(100), ClientLastName VARCHAR(100), ClientMiddleName VARCHAR(100),
                    StreetAddress VARCHAR(255), Barangay VARCHAR(100), City VARCHAR(100),
                    ContactNumber VARCHAR(50), Email VARCHAR(100),
                    ContactFirstName VARCHAR(100), ContactLastName VARCHAR(100)
                );

                -- 3. SERVICES TABLE
                CREATE TABLE IF NOT EXISTS tbl_services (
                    ServiceID INT AUTO_INCREMENT PRIMARY KEY,
                    ServiceName VARCHAR(100), 
                    Description TEXT,
                    DefaultPrice DECIMAL(10,2)
                );
                -- Insert Default Services
                INSERT INTO tbl_services (ServiceName) VALUES ('General Pest Control'), ('Termite Control'), ('Disinfection');

                -- 4. CONTRACTS TABLE
                CREATE TABLE IF NOT EXISTS tbl_contracts (
                    ContractID INT AUTO_INCREMENT PRIMARY KEY,
                    ClientID INT, ServiceID INT, QuoteID INT,
                    StartDate DATE, DurationMonths INT, TotalAmount DECIMAL(10,2),
                    ServiceFrequency VARCHAR(50), PaymentTerms VARCHAR(50), ContractStatus VARCHAR(20)
                );

                -- 5. JOB ORDERS TABLE
                CREATE TABLE IF NOT EXISTS tbl_joborders (
                    JobID INT AUTO_INCREMENT PRIMARY KEY,
                    ClientID INT, ContractID INT, TechnicianID INT, ServiceID INT,
                    VisitNumber INT, ScheduledDate DATETIME, 
                    JobType VARCHAR(50), Status VARCHAR(50),
                    StartTime DATETIME, EndTime DATETIME
                );

                -- 6. PAYMENTS TABLE
                CREATE TABLE IF NOT EXISTS tbl_payments (
                    PaymentID INT AUTO_INCREMENT PRIMARY KEY,
                    ContractID INT, ScheduleID INT,
                    AmountPaid DECIMAL(10,2), PaymentDate DATETIME
                );

                -- 7. PAYMENT SCHEDULE TABLE
                CREATE TABLE IF NOT EXISTS tbl_paymentschedule (
                    ScheduleID INT AUTO_INCREMENT PRIMARY KEY,
                    ContractID INT, InstallmentNumber INT,
                    DueDate DATE, AmountDue DECIMAL(10,2)
                );

                -- 8. QUOTATIONS TABLE
                CREATE TABLE IF NOT EXISTS tbl_quotations (
                    QuoteID INT AUTO_INCREMENT PRIMARY KEY,
                    ClientID INT, ProposedServiceID INT,
                    DateCreated DATETIME, AreaSize_Sqm DECIMAL(10,2),
                    InfestationLevel VARCHAR(50), QuotedPrice DECIMAL(10,2),
                    Remarks TEXT, Status VARCHAR(50)
                );
                
                -- 9. NOTIFICATIONS TABLE
                CREATE TABLE IF NOT EXISTS tbl_notifications (
                    NotifID INT AUTO_INCREMENT PRIMARY KEY,
                    Title VARCHAR(100), Message TEXT, Category VARCHAR(50),
                    IsRead BOOLEAN DEFAULT 0, DateCreated DATETIME,
                    JobID INT, ContractID INT, PaymentID INT
                );

                -- 10. CREATE SUPER ADMIN ACCOUNT
                INSERT INTO tbl_users (FirstName, LastName, Role, Username, Password, Status)
                VALUES ('Super', 'Admin', 'Admin', 'admin', 'admin123', 'Active');
            "

            ' Execute the massive script
            Dim script As New MySqlScript(conn, sqlScript)
            script.Execute()

            MessageBox.Show("System Setup Complete!" & vbCrLf &
                            "Default Login:" & vbCrLf &
                            "User: admin" & vbCrLf &
                            "Pass: admin123",
                            "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error creating database tables: " & ex.Message)
        End Try
    End Sub

End Class