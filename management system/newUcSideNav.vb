Public Class newUcSideNav

    ' This variable holds the reference to the main skeleton (Optional now, but good to keep)
    Private _mainForm As frm_Main

    ' 1. The Constructor
    Public Sub New(mainForm As frm_Main)
        InitializeComponent()
        _mainForm = mainForm
    End Sub

    ' Default constructor (Required by Visual Studio)
    Public Sub New()
        InitializeComponent()
    End Sub

    ' ==========================================
    '      NAVIGATION BUTTON EVENTS
    ' ==========================================

    ' 1. DASHBOARD (Daily Operations)
    Private Sub btnNavDashboard_Click(sender As Object, e As EventArgs) Handles btnNavDashboard.Click
        ' Replaces the center screen with the Dashboard
        NavigateTo(New newUcDashboard(), "Daily Operations Dashboard")
    End Sub

    ' 2. CONTRACTS (New Contract Entry)
    Private Sub btnNavContracts_Click(sender As Object, e As EventArgs) Handles btnNavContracts.Click
        ' Replaces center screen with Contract Form
        NavigateTo(New uc_NewContractEntry(), "Create New Contract")
    End Sub

    ' 3. CLIENTS / INQUIRY (Inquiry & Inspection Dispatch)
    Private Sub btnNavClients_Click(sender As Object, e As EventArgs) Handles btnNavClients.Click
        NavigateTo(New newUcClientManager(), "Client Management Database")
    End Sub

    ' 4. (Optional) Add other buttons here as we build more forms...
    ' Private Sub btnNavBilling_Click... 


    ' ==========================================
    '      HELPER FUNCTION (Cleaner Code)
    ' ==========================================
    ' This sub handles the finding of the parent form safely.
    ' You just call NavigateTo(NewPage, "Title")
    Private Sub NavigateTo(ByVal page As UserControl, ByVal title As String)
        ' 1. Try to find the Main Form dynamically
        Dim parentForm As frm_Main = TryCast(Me.ParentForm, frm_Main)

        ' 2. Only navigate if we actually found it
        If parentForm IsNot Nothing Then
            parentForm.LoadPage(page, title)
        Else
            ' This helps you debug if something is wrong
            MessageBox.Show("Error: The Side Navigation is not inside frm_Main, so it cannot switch pages.")
        End If
    End Sub

    Private Sub newUcSideNav_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Optional: Set default button styles here if needed
    End Sub

    Private Sub btnNavBilling_Click(sender As Object, e As EventArgs) Handles btnNavBilling.Click
        NavigateTo(New newUcBilling(), "Payments")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        NavigateTo(New newUcInquiryManager(), "inquiry")
    End Sub

    Private Sub btnManageAccounts_Click(sender As Object, e As EventArgs) Handles btnManageAccounts.Click
        NavigateTo(New newUcAccountManager(), "accounts")
    End Sub

    Private Sub btnNavCalendar_Click(sender As Object, e As EventArgs) Handles btnNavCalendar.Click
        NavigateTo(New newUcQuoteManager(), "Quotes")
    End Sub
End Class