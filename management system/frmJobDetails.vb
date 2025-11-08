Public Class frmJobDetails

    ' This variable will hold the ID passed to the form
    Private _currentJobID As Integer

    ' This is the custom constructor.
    ' We will call this from our other forms.
    Public Sub New(ByVal jobID As Integer)
        ' This call is required by the designer
        InitializeComponent()

        ' Store the ID
        _currentJobID = jobID
    End Sub

    ' This runs after the constructor
    Private Sub frmJobDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Now, use the ID to load the data
        LoadJobData()
    End Sub

    Private Sub LoadJobData()
        ' 1. Write your database code here
        '    "SELECT * FROM Jobs WHERE JobID = " & _currentJobID

        ' 2. Get the data (e.g., from a DataTable or a Job object)
        '    Dim jobData = ...

        ' 3. Populate your controls
        '    txtJobTitle.Text = jobData("Title")
        '    txtClientName.Text = jobData("Client")
        '    '... and so on
    End Sub
    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

    End Sub
End Class