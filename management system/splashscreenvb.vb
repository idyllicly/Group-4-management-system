Imports System.Windows.Forms
Imports System.Drawing
' The System.Threading import is no longer needed since we removed Application.ExitThread()
' Imports System.Threading 

Public Class splashscreenvb
    ' --- Private Fields ---
    Private WithEvents splashTimer As New Timer()
    ' Adjust this value to make the logo display longer or shorter (3000 ms = 3 seconds)
    Private Const SPLASH_DURATION_MS As Integer = 3000

    ' --- Constructor ---
    Public Sub New()
        ' CRITICAL FIX: This line MUST be called to initialize designer components
        ' and prevent 'Object reference not set' errors.
        InitializeComponent()

        ' Optional: Set basic form properties for a clean look
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.White ' Set a background color that suits your logo

        ' Initialize the Timer
        splashTimer.Interval = SPLASH_DURATION_MS
        ' The timer is started in the Form_Load event to ensure the form is fully rendered first.
    End Sub

    ' --- Event Handlers ---

    Private Sub splashTimer_Tick(sender As Object, e As EventArgs) Handles splashTimer.Tick
        ' Stop the timer immediately so it only executes once
        splashTimer.Stop()

        ' 1. Create and show the main login form
        Dim loginForm As New log_in()
        loginForm.Show()

        ' 2. CRITICAL FIX: Hide the startup form instead of closing the thread.
        ' Hiding the form allows the application process to continue running, 
        ' keeping the log_in form alive.
        Me.Hide()

        ' Note: DO NOT use Me.Close() or Application.ExitThread() here, as it will terminate the application.
    End Sub

    Private Sub Splash_Screen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Start the timer here, after the form has loaded, which is safer than the constructor.
        If Not splashTimer.Enabled Then
            splashTimer.Start()
        End If
    End Sub

End Class