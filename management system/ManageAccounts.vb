Imports MySql.Data.MySqlClient

Public Class ManageAccounts
    ' Create an instance of your DB Connection helper
    Dim db As New DatabaseConnection()

    Private Sub ManageAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAccounts()
    End Sub

    ' 🔄 Function to fetch users from DB and generate cards
    Public Sub LoadAccounts()
        ' 1. Clear existing cards (BUT keep the Header panel and Add Button)
        ' We loop backwards to safely remove controls
        For i As Integer = Panel2.Controls.Count - 1 To 0 Step -1
            Dim ctrl = Panel2.Controls(i)
            If TypeOf ctrl Is AccCard Then
                Panel2.Controls.Remove(ctrl)
            End If
        Next

        ' 2. Fetch Data
        Dim dt As DataTable = db.ExecuteSelect("SELECT AccountID, AUsername, AccType FROM tbl_account")

        ' 3. Layout Settings
        Dim startY As Integer = 150 ' Starting Y Position (below the header)
        Dim gap As Integer = 10     ' Gap between cards

        ' 4. Loop through rows and create cards
        For Each row As DataRow In dt.Rows
            Dim newCard As New AccCard()

            ' Set Properties (Ensure your AccCard has these properties!)
            newCard.UserID = Convert.ToInt32(row("AccountID"))
            newCard.UserName = row("AUsername").ToString() & " (" & row("AccType").ToString() & ")"

            ' Positioning
            newCard.Left = 57 ' Match the X position from your designer
            newCard.Top = startY

            ' Update next Y position
            startY += newCard.Height + gap

            ' 🔌 CONNECT THE EVENT
            ' This is the most important part: connecting the dynamic card to the handler
            AddHandler newCard.ActionRequested, AddressOf GlobalActionHandler

            ' Add to Panel
            Panel2.Controls.Add(newCard)
        Next
    End Sub

    ' 🎮 The Universal Handler for ALL cards
    Private Sub GlobalActionHandler(UserID As Integer, Action As String)
        Select Case Action
            Case "Edit"
                ' --- UPDATED: Connects to the Edit Form ---
                Me.Hide()
                EditAccountPage.TargetUserID = UserID ' Pass the ID to the Edit Form
                EditAccountPage.Show()

            Case "Delete"
                If MessageBox.Show("Are you sure you want to delete this account?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then

                    ' 1. Delete from DB
                    Dim sql As String = "DELETE FROM tbl_account WHERE AccountID = @id"
                    Dim params As New Dictionary(Of String, Object)
                    params.Add("@id", UserID)

                    db.ExecuteAction(sql, params)

                    ' 2. Refresh List
                    MessageBox.Show("Account Deleted!", "Success")
                    LoadAccounts()
                End If

            Case "View"
                MessageBox.Show("Viewing User ID: " & UserID)
        End Select
    End Sub

    ' Navigation Buttons (Add Account)
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        CreateAcc_SupAdmin.Show()
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs)
        Me.Hide()
        CreateAcc_SupAdmin.Show()
    End Sub

End Class