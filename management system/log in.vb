Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D

Public Class log_in

    ' GLOBAL VARIABLES
    Public Shared CurrentUserID As Integer = 0
    Public Shared CurrentUserRole As String = ""

    Dim bgImage As Image
    ' Connection string
    Private Const MyConnectionString As String = "Server=localhost;Database=db_rrcms;Uid=root;Pwd=;"

    ' =========================================================
    ' LOGIN BUTTON LOGIC
    ' =========================================================
    Private Sub OvalButton1_Click(sender As Object, e As EventArgs) Handles OvalButton1.Click

        ' 1. Input Validation
        If UserText Is Nothing OrElse PassText Is Nothing Then Return
        Dim username As String = UserText.Text.Trim()
        Dim password As String = PassText.Text.Trim()

        If String.IsNullOrWhiteSpace(username) Or String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. CHECK LOCK STATUS
        If My.Settings.LockoutEnd > DateTime.Now Then
            Dim timeLeft As TimeSpan = My.Settings.LockoutEnd - DateTime.Now

            Dim msg As String
            If timeLeft.TotalMinutes >= 1 Then
                msg = "System is temporarily locked due to too many failed attempts." & vbCrLf &
                      "Please wait " & Math.Ceiling(timeLeft.TotalMinutes) & " minutes."
            Else
                msg = "System is temporarily locked." & vbCrLf &
                      "Please wait " & Math.Ceiling(timeLeft.TotalSeconds) & " seconds."
            End If

            MessageBox.Show(msg, "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 3. DATABASE LOGIN LOGIC
        ' --- FIX IS HERE ---
        ' We use CONCAT_WS to join FirstName and LastName with a space, and call it 'FullName'
        ' This allows the rest of your code (reader("FullName")) to work without changes.
        Dim query As String = "SELECT UserID, Role, CONCAT_WS(' ', FirstName, LastName) AS FullName FROM tbl_users WHERE Username = @user AND Password = @pass AND Status = 'Active'"

        Try
            Using con As New MySqlConnection(MyConnectionString)
                con.Open()

                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@user", username)
                    cmd.Parameters.AddWithValue("@pass", password)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()

                        If reader.Read() Then
                            ' --- LOGIN SUCCESS ---

                            ' A. RESET THE LOCKS
                            My.Settings.FailCount = 0
                            My.Settings.LockoutEnd = DateTime.Now.AddYears(-1)
                            My.Settings.Save()

                            ' B. Store Session Data
                            CurrentUserID = Convert.ToInt32(reader("UserID"))
                            CurrentUserRole = reader("Role").ToString()
                            Dim fullName As String = reader("FullName").ToString()

                            ' C. Routing
                            ' Note: Based on your database data, Roles are "Super Admin", "Admin", and "Technician".
                            ' Your current code blocks Technicians. If this is intentional, keep it.
                            If CurrentUserRole = "Super Admin" Then
                                MessageBox.Show("Welcome back, Super Admin " & fullName, "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            ElseIf CurrentUserRole = "Admin" Then
                                MessageBox.Show("Welcome Admin " & fullName, "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                            Else
                                ' Technicians will fall here
                                MessageBox.Show("Role not recognized or authorized for desktop access.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Return
                            End If

                            ' D. Show Main Dashboard
                            Me.Hide()
                            Dim mainForm As New frm_Main()
                            mainForm.Show()

                        Else
                            ' --- LOGIN FAILED ---
                            HandleLoginFailure()
                        End If
                    End Using
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Unexpected Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    ' =========================================================
    ' HELPER SUB: HANDLES THE PENALTY MATH
    ' =========================================================
    Private Sub HandleLoginFailure()
        ' 1. Increase Fail Count locally
        My.Settings.FailCount += 1

        Dim attempts As Integer = My.Settings.FailCount
        Dim msg As String = "Invalid Username or Password."

        ' 2. Apply Penalty based on rules
        Select Case attempts
            Case 1, 2
                msg &= vbCrLf & "You have " & (6 - attempts) & " attempts remaining."
            Case 3
                ' 30 Seconds Lock
                My.Settings.LockoutEnd = DateTime.Now.AddSeconds(30)
                msg &= vbCrLf & "System locked for 30 seconds."
            Case 4
                ' 1 Minute Lock
                My.Settings.LockoutEnd = DateTime.Now.AddMinutes(1)
                msg &= vbCrLf & "System locked for 1 minute."
            Case 5
                ' 30 Minutes Lock
                My.Settings.LockoutEnd = DateTime.Now.AddMinutes(30)
                msg &= vbCrLf & "System locked for 30 minutes."
            Case Is >= 6
                ' 1 HOUR Lock
                My.Settings.LockoutEnd = DateTime.Now.AddHours(1)
                msg &= vbCrLf & "System locked for 1 HOUR."
        End Select

        ' 3. Save Settings permanently so closing the app doesn't reset it
        My.Settings.Save()

        MessageBox.Show(msg, "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    ' =========================================================
    ' UI AND DESIGN LOGIC (Existing code)
    ' =========================================================

    Private Sub log_in_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Dim myBlue As Color = Color.FromArgb(255, 0, 0, 100)
        If OvalButton1 IsNot Nothing Then
            Me.AcceptButton = OvalButton1
        End If
        Me.BeginInvoke(New Action(AddressOf SetInitialFocus))
        Try
            bgImage = Image.FromFile("rrc_worker.jpg")
        Catch ex As Exception
            bgImage = New Bitmap(1, 1)
        End Try
    End Sub

    Private Sub SetInitialFocus()
        If UserText IsNot Nothing Then UserText.Focus()
    End Sub

    Private Sub LoginForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics
        Dim myBaseColor As Color = Color.White
        g.Clear(myBaseColor)

        If My.Resources.rrc_worker IsNot Nothing Then
            Dim bgImage As Image = My.Resources.rrc_worker
            Dim imgWidth As Integer = Me.Width * 0.6
            Dim imgRect As New Rectangle(Me.Width - imgWidth, 0, imgWidth, Me.Height)
            g.DrawImage(bgImage, imgRect)
        End If

        Dim colorStart As Color = myBaseColor
        Dim colorEnd As Color = Color.FromArgb(0, 255, 255, 255)

        Using brush As New LinearGradientBrush(Me.ClientRectangle, colorStart, colorEnd, LinearGradientMode.Horizontal)
            Dim blend As New Blend()
            blend.Positions = New Single() {0.0F, 0.4F, 1.0F}
            blend.Factors = New Single() {0.0F, 0.0F, 1.0F}
            brush.Blend = blend
            g.FillRectangle(brush, Me.ClientRectangle)
        End Using
    End Sub

    Private Sub LoginForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If bgImage IsNot Nothing Then bgImage.Dispose()
    End Sub

End Class