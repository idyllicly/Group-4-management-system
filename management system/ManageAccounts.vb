Imports MySql.Data.MySqlClient
Imports System.Windows.Forms
Imports System.Data
Imports System.Drawing
Imports System.IO

Public Class ManageAccounts
    ' Create an instance of your DB Connection helper
    Dim db As New DatabaseConnection()

    Private Sub ManageAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Ensure the panel designed for displaying account cards is scrollable
        If Panel2 IsNot Nothing Then
            Panel2.AutoScroll = True
        End If

        LoadAccounts()
    End Sub

    ''' <summary>
    ''' Fetches user accounts from DB and generates cards, now including profile pictures.
    ''' </summary>
    Public Sub LoadAccounts()
        Dim targetPanel As Panel = Panel2 ' Assuming Panel2 is the container for the cards

        ' ⭐️ AGGRESSIVE SCROLL FIX 1: Reset scroll position before loading to eliminate previous gaps.
        If targetPanel IsNot Nothing Then
            targetPanel.AutoScrollPosition = New Point(0, 0)
            targetPanel.AutoScroll = False ' Temporarily disable scroll to prevent flicker/jumping
        End If
        ' -------------------------------------------------------------------

        ' 1. Clear existing cards to prevent duplication on refresh
        For i As Integer = targetPanel.Controls.Count - 1 To 0 Step -1
            Dim ctrl = targetPanel.Controls(i)
            If TypeOf ctrl Is AccCard Then
                targetPanel.Controls.Remove(ctrl)
                ctrl.Dispose()
            End If
        Next

        ' 2. Fetch Data
        Dim sqlQuery As String = "SELECT AccountID, AUsername, AccType, APicture FROM tbl_account"
        Dim dt As DataTable = db.ExecuteSelect(sqlQuery)

        ' 3. Layout Settings
        ' LAYOUT FIX: Reduced startY to minimize the gap between the header and the first card.
        Dim startY As Integer = 5
        Dim gap As Integer = 10

        ' 4. Loop through rows and create cards
        For Each row As DataRow In dt.Rows
            Dim newCard As New AccCard()

            ' Set Properties
            newCard.UserID = Convert.ToInt32(row("AccountID"))
            newCard.UserName = row("AUsername").ToString() & " (" & row("AccType").ToString() & ")"

            ' --- LOAD IMAGE DATA ---
            If row("APicture") IsNot DBNull.Value AndAlso TypeOf row("APicture") Is Byte() Then
                Dim pictureBytes As Byte() = CType(row("APicture"), Byte())
                newCard.SetPictureFromBytes(pictureBytes)
            Else
                newCard.SetPictureFromBytes(Nothing)
            End If
            ' -----------------------------

            ' Positioning
            newCard.Left = 57
            newCard.Top = startY

            ' Update next Y position
            startY += newCard.Height + gap

            ' Connect the event handler
            AddHandler newCard.ActionRequested, AddressOf GlobalActionHandler

            ' Add to Panel
            targetPanel.Controls.Add(newCard)
        Next

        ' ⭐️ AGGRESSIVE SCROLL FIX 2: Re-enable AutoScroll and reset position after drawing.
        If targetPanel IsNot Nothing Then
            targetPanel.AutoScroll = True
            ' Use BeginInvoke to ensure the reset happens after the form engine finishes layout calculation.
            targetPanel.BeginInvoke(
                New Action(
                    Sub()
                        targetPanel.AutoScrollPosition = New Point(0, 0)
                    End Sub
                )
            )
        End If
    End Sub

    ''' <summary>
    ''' The universal handler for all account card actions (View, Edit, Delete).
    ''' </summary>
    Private Sub GlobalActionHandler(UserID As Integer, Action As String)
        Select Case Action
            Case "Edit"
                EditAccountPage.TargetUserID = UserID
                ' Ensures form state is reset and data is loaded
                EditAccountPage.LoadAccountData(UserID)
                Me.Hide()
                EditAccountPage.Show()

            Case "View"
                Me.Hide()
                ViewAccDetailsvb.TargetUserID = UserID
                ViewAccDetailsvb.Show()

            Case "Delete"
                If MessageBox.Show("Are you sure you want to permanently delete this account? This cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.Yes Then

                    Dim sql As String = "DELETE FROM tbl_account WHERE AccountID = @id"
                    Dim params As New Dictionary(Of String, Object)
                    params.Add("@id", UserID)

                    Dim result As Integer = db.ExecuteAction(sql, params)

                    If result > 0 Then
                        MessageBox.Show("Account Deleted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' Refresh List immediately
                        LoadAccounts()
                    Else
                        MessageBox.Show("Deletion failed or account not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                End If
        End Select
    End Sub

    ' --- Navigation Button Handler (Leads to the Create Account form) ---
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        CreateAcc_SupAdmin.Show()
    End Sub

    ' --- Designer-Generated Event Handlers (Kept for compatibility) ---
    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint
    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint
    End Sub

    Private Sub PictureBox7_Click(sender As Object, e As EventArgs)
        Me.Hide()
        CreateAcc_SupAdmin.Show()
    End Sub


End Class