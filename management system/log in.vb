Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D

Public Class log_in

    ' GLOBAL VARIABLE: To store who is currently logged in
    ' You can access this from other forms using: log_in.CurrentUserID or log_in.CurrentUserRole
    Public Shared CurrentUserID As Integer = 0
    Public Shared CurrentUserRole As String = ""

    Dim bgImage As Image
    ' Connection string (Ensure database name matches your PHPMyAdmin)
    Private Const MyConnectionString As String = "Server=localhost;Database=db_rrcms;Uid=root;Pwd=;"

    Private Sub OvalButton1_Click(sender As Object, e As EventArgs) Handles OvalButton1.Click

        ' 1. Validation
        If UserText Is Nothing OrElse PassText Is Nothing Then
            MessageBox.Show("Internal Error: Textboxes not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        Dim username As String = UserText.Text.Trim()
        Dim password As String = PassText.Text.Trim()

        If String.IsNullOrWhiteSpace(username) Or String.IsNullOrWhiteSpace(password) Then
            MessageBox.Show("Please enter both username and password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' 2. Database Login Logic
        ' UPDATED: Targeting 'tbl_users' instead of 'tbl_account'
        ' UPDATED: We also fetch UserID to track who is logged in
        Dim query As String = "SELECT UserID, Role, FullName FROM tbl_users WHERE Username = @user AND Password = @pass AND Status = 'Active'"

        Try
            Using con As New MySqlConnection(MyConnectionString)
                con.Open()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@user", username)
                    cmd.Parameters.AddWithValue("@pass", password)

                    ' Use ExecuteReader to get multiple columns (ID and Role)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            ' --- LOGIN SUCCESS ---

                            ' 1. Save Session Data (So we know who is using the app)
                            CurrentUserID = Convert.ToInt32(reader("UserID"))
                            CurrentUserRole = reader("Role").ToString()
                            Dim fullName As String = reader("FullName").ToString()

                            ' 2. Routing based on Role
                            If CurrentUserRole = "Admin" Then
                                MessageBox.Show("Welcome back, Admin " & fullName, "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                ' Hide Login and Show Main Dashboard
                                Me.Hide()
                                Dim mainForm As New frm_Main()
                                mainForm.Show()

                            ElseIf CurrentUserRole = "Technician" Then
                                ' OPTIONAL: If Technicians have a different form, put it here.
                                ' For now, we allow them into the Main form or show a restriction message.
                                MessageBox.Show("Welcome Technician " & fullName, "Login Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                                Me.Hide()
                                Dim mainForm As New frm_Main()
                                mainForm.Show()
                            Else
                                MessageBox.Show("Role not recognized.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            End If

                        Else
                            ' --- LOGIN FAILED ---
                            MessageBox.Show("Invalid Username, Password, or Account is Inactive.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
    ' UI AND DESIGN LOGIC (Kept exactly as you had it)
    ' =========================================================

    Private Sub log_in_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True

        ' SETTINGS: Your Blue Color
        Dim myBlue As Color = Color.FromArgb(255, 0, 0, 100)
        If OvalButton1 IsNot Nothing Then
            Me.AcceptButton = OvalButton1
        End If
        Me.BeginInvoke(New Action(AddressOf SetInitialFocus))
        Try
            bgImage = Image.FromFile("rrc_worker.jpg")
        Catch ex As Exception
            ' Fallback if image is missing, just to prevent crash
            bgImage = New Bitmap(1, 1)
        End Try
    End Sub

    Private Sub SetInitialFocus()
        If UserText IsNot Nothing Then UserText.Focus()
    End Sub

    Private Sub LoginForm_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics

        ' 1. SETTINGS: Change Color to White
        Dim myBaseColor As Color = Color.White

        ' 2. Clear the whole background with White first
        g.Clear(myBaseColor)

        ' 3. Draw the Image on the RIGHT side
        If My.Resources.rrc_worker IsNot Nothing Then
            Dim bgImage As Image = My.Resources.rrc_worker

            ' Draw image on the RIGHT side (60% width)
            Dim imgWidth As Integer = Me.Width * 0.6
            Dim imgRect As New Rectangle(Me.Width - imgWidth, 0, imgWidth, Me.Height)

            g.DrawImage(bgImage, imgRect)
        End If

        ' 4. THE GRADIENT OVERLAY (White -> Transparent White)
        Dim colorStart As Color = myBaseColor
        Dim colorEnd As Color = Color.FromArgb(0, 255, 255, 255)

        Using brush As New LinearGradientBrush(Me.ClientRectangle, colorStart, colorEnd, LinearGradientMode.Horizontal)
            Dim blend As New Blend()

            ' POSITIONS: 0.0 (Left), 0.4 (Middle), 1.0 (Right)
            blend.Positions = New Single() {0.0F, 0.4F, 1.0F}

            ' FACTORS: 0.0 = Solid Color, 1.0 = Fully Transparent
            blend.Factors = New Single() {0.0F, 0.0F, 1.0F}

            brush.Blend = blend
            g.FillRectangle(brush, Me.ClientRectangle)
        End Using
    End Sub

    Private Sub LoginForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If bgImage IsNot Nothing Then bgImage.Dispose()
    End Sub

End Class