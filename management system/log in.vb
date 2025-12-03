Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing.Drawing2D

Public Class log_in
    Dim bgImage As Image
    ' Connection string based on your local settings
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

        Dim userRole As String = String.Empty

        ' 2. Corrected Query to match your tbl_account columns
        Dim query As String = "SELECT Role FROM tbl_account WHERE Username = @user AND Password = @pass"

        Try
            Using con As New MySqlConnection(MyConnectionString)
                con.Open()
                Using cmd As New MySqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@user", username)
                    cmd.Parameters.AddWithValue("@pass", password)

                    Dim result = cmd.ExecuteScalar()

                    If result IsNot Nothing Then
                        userRole = result.ToString()
                    End If
                End Using
            End Using

        Catch ex As MySqlException
            MessageBox.Show("Database Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Catch ex As Exception
            MessageBox.Show("Unexpected Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End Try

        ' 3. Login Success Logic
        If Not String.IsNullOrEmpty(userRole) Then
            ' Hide Login Form
            Me.Hide()

            ' Open Main Form
            Dim mainForm As New frm_Main()
            mainForm.Show()
        Else
            MessageBox.Show("Invalid Username or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Keep your existing Load and Focus logic
    Private Sub log_in_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True

        ' SETTINGS: Your Blue Color
        Dim myBlue As Color = Color.FromArgb(255, 0, 0, 100) ' Adjust this blue to match your logo
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

    ' 2. Use the Paint Event to draw the custom background
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
        ' Create a transparent version of White (0 alpha, 255 red, 255 green, 255 blue)
        Dim colorEnd As Color = Color.FromArgb(0, 255, 255, 255)

        Using brush As New LinearGradientBrush(Me.ClientRectangle, colorStart, colorEnd, LinearGradientMode.Horizontal)
            Dim blend As New Blend()

            ' POSITIONS: 0.0 (Left), 0.4 (Middle), 1.0 (Right)
            blend.Positions = New Single() {0.0F, 0.4F, 1.0F}

            ' FACTORS: 0.0 = Solid Color, 1.0 = Fully Transparent
            ' This keeps the Left side Solid White, and fades to Clear on the Right
            blend.Factors = New Single() {0.0F, 0.0F, 1.0F}

            brush.Blend = blend
            g.FillRectangle(brush, Me.ClientRectangle)
        End Using
    End Sub

    ' Clean up memory when form closes
    Private Sub LoginForm_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If bgImage IsNot Nothing Then bgImage.Dispose()
    End Sub

End Class