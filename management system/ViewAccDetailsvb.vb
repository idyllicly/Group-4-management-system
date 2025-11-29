Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Collections.Generic ' Required for Dictionary used by DatabaseConnection

Public Class ViewAccDetailsvb

    ' ⭐️ FIX: Use Composition (instance) of the DatabaseConnection helper ⭐️
    Private ReadOnly db As New DatabaseConnection()

    ' Property to hold the unique ID passed from ManageAccounts
    Private _targetUserID As Integer
    Public Property TargetUserID As Integer
        Get
            Return _targetUserID
        End Get
        Set(value As Integer)
            _targetUserID = value
        End Set
    End Property

    ' ----------------------------------------------------
    ' |       RESOURCE MANAGEMENT (GDI+ FIX)             |
    ' ----------------------------------------------------
    ''' <summary>
    ''' Disposes the image in TechPicture and sets it to Nothing to free system resources.
    ''' </summary>
    Private Sub DisposePictureBoxImage()
        ' Assuming TechPicture is the name of your PictureBox control
        If TechPicture IsNot Nothing AndAlso TechPicture.Image IsNot Nothing Then
            Try
                TechPicture.Image.Dispose()
                TechPicture.Image = Nothing
            Catch
                ' Ignore disposal errors
            End Try
        End If
    End Sub

    ' ----------------------------------------------------
    ' |       FORM & DATABASE LOADING LOGIC              |
    ' ----------------------------------------------------

    ' Activated is appropriate if you want the data to refresh every time the form is focused.
    Private Sub ViewAccDetailsvb_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If TargetUserID > 0 Then
            ' Only load the data if the form is activated and a valid ID is set
            LoadUserDetails(TargetUserID)
        Else
            Me.Close()
        End If
    End Sub

    ' Form load is only executed once, good for initial setup, but Activated is better for reloading data.
    Private Sub ViewAccDetailsvb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Ensure the picture box handles scaling
        If TechPicture IsNot Nothing Then
            TechPicture.SizeMode = PictureBoxSizeMode.Zoom
        End If
    End Sub

    ''' <summary>
    ''' Fetches user data using the DatabaseConnection helper and populates controls.
    ''' </summary>
    Private Sub LoadUserDetails(ByVal accountId As Integer)

        DisposePictureBoxImage() ' Clear old image before loading new one

        Dim query As String = "SELECT AccType, ALastName, AFirstName, AMiddleName, AEmail, AContactno, AFacebook, AViber, APicture, AAddress " & ' Added AAddress
                              "FROM tbl_account WHERE AccountID = @AccountID"

        Dim parameters As New Dictionary(Of String, Object)
        parameters.Add("@AccountID", accountId)

        Try
            ' ⭐️ FIX: Use db.ExecuteSelect instead of direct MySqlConnection ⭐️
            Dim dt As DataTable = db.ExecuteSelect(query, parameters)

            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)

                ' --- LOAD TEXTUAL DATA ---
                Dim firstName As String = row("AFirstName").ToString()
                ' Handle DBNull for middle name gracefully
                Dim middleNameValue As String = row("AMiddleName").ToString()
                Dim middleName As String = If(String.IsNullOrWhiteSpace(middleNameValue), "", " " & middleNameValue)
                Dim lastName As String = row("ALastName").ToString()

                Dim fullName As String = $"{firstName}{middleName} {lastName}"
                Dim accTypeString As String = row("AccType").ToString()

                ' --- POPULATE STATIC VIEW CONTROLS (Assumes control names: TechName, accType, etc.) ---
                TechName.Text = fullName
                accType.Text = $"({accTypeString})"

                ' Use helper function to display "N/A" if DBNull or Empty
                TechNumber.Text = If(row("AContactno") Is DBNull.Value OrElse String.IsNullOrWhiteSpace(row("AContactno").ToString()), "N/A", row("AContactno").ToString())
                TechEmail.Text = If(row("AEmail") Is DBNull.Value OrElse String.IsNullOrWhiteSpace(row("AEmail").ToString()), "N/A", row("AEmail").ToString())
                TechFB.Text = If(row("AFacebook") Is DBNull.Value OrElse String.IsNullOrWhiteSpace(row("AFacebook").ToString()), "N/A", row("AFacebook").ToString())
                TechViber.Text = If(row("AViber") Is DBNull.Value OrElse String.IsNullOrWhiteSpace(row("AViber").ToString()), "N/A", row("AViber").ToString())
                ' Assuming there is a control for Address (e.g., TechAddress)
                ' TechAddress.Text = If(row("AAddress") Is DBNull.Value OrElse String.IsNullOrWhiteSpace(row("AAddress").ToString()), "N/A", row("AAddress").ToString())


                ' --- PICTURE DISPLAY ---
                If Not row("APicture") Is DBNull.Value Then
                    Dim pictureData As Byte() = DirectCast(row("APicture"), Byte())

                    Using ms As New MemoryStream(pictureData)
                        If TechPicture IsNot Nothing Then
                            ' Clone the image to release the stream immediately (GDI+ FIX)
                            TechPicture.Image = CType(Image.FromStream(ms).Clone(), Image)
                        End If
                    End Using
                End If

            Else
                MessageBox.Show($"Account ID {accountId} not found in the database.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.Close()
            End If

        Catch ex As Exception
            ' If db.ExecuteSelect fails, it already shows a DB error message.
            MessageBox.Show($"An unexpected error occurred during LoadUserDetails: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- FORM CLOSING/DISPOSAL HANDLER ---
    Private Sub ViewAccDetailsvb_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DisposePictureBoxImage() ' Final resource cleanup
    End Sub

    ' ----------------------------------------------------
    ' |       EXISTING CLICK HANDLERS (Static)           |
    ' ----------------------------------------------------

    Private Sub OvalButton3_Click(sender As Object, e As EventArgs) Handles OvalButton3.Click
        ' NOTE: Creating a new instance of ManageAccounts is inefficient but matches original intent.
        Me.Close()
        Dim mainManager As New ManageAccounts()
        mainManager.Show()
    End Sub
End Class