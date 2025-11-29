Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Data
Imports System.Collections.Generic

Public Class ManageAccounts

    ' ⭐️ Uses the DatabaseConnection helper instance for consistent DB access ⭐️
    Private ReadOnly db As New DatabaseConnection()

    ' Define spacing constants for the Panel layout
    Private Const CARD_SPACING As Integer = 10
    Private Const CARD_X_START As Integer = 10

    ' ----------------------------------------------------
    ' | 1. FORM LOADING & FLOATING BUTTON
    ' ----------------------------------------------------

    Private Sub ManageAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Assumes Button1 is the plus button and Panel2 is the scrolling container.
        MakeButtonFloating(Button1, Panel2)
        LoadAccountCards()
    End Sub

    ''' <summary>
    ''' Moves a control (like a button) to the main form to achieve a fixed, floating effect.
    ''' </summary>
    Private Sub MakeButtonFloating(ByVal btn As Button, ByVal scrollContainer As Control)
        If btn Is Nothing OrElse scrollContainer Is Nothing Then Return

        Dim currentX As Integer = btn.Location.X + scrollContainer.Location.X
        Dim currentY As Integer = btn.Location.Y + scrollContainer.Location.Y

        btn.Parent = Me
        btn.Location = New Point(currentX, currentY)
        btn.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btn.BringToFront()
    End Sub

    ' ----------------------------------------------------
    ' | 2. ACCOUNT CARD LOADING (Uses db.ExecuteSelect)
    ' ----------------------------------------------------

    ''' <summary>
    ''' Clears the list and reloads all account cards from the database.
    ''' </summary>
    Public Sub LoadAccountCards()

        Panel2.Controls.Clear()
        Dim nextYPosition As Integer = CARD_SPACING

        ' Query selects all necessary user data for the card
        Dim query As String = "SELECT AccountID, AccType, AFirstName, ALastName, AMiddleName, APicture FROM tbl_account ORDER BY ALastName, AFirstName"

        Try
            ' Execute query using the DatabaseConnection helper
            Dim dt As DataTable = db.ExecuteSelect(query, New Dictionary(Of String, Object))

            For Each row As DataRow In dt.Rows
                Dim card As New AccCard()

                ' ⭐️ Set the position and size for the card ⭐️
                card.Location = New Point(CARD_X_START, nextYPosition)
                card.Width = Panel2.ClientSize.Width - (CARD_X_START * 2)

                ' A. Set Card Properties
                card.UserID = CType(row("AccountID"), Integer)
                card.UserName = $"{row("AFirstName")} {row("ALastName")} ({row("AccType")})"

                ' B. Load Picture Data
                If Not row.IsNull("APicture") Then
                    Dim pictureData As Byte() = DirectCast(row("APicture"), Byte())
                    card.SetPictureFromBytes(pictureData)
                End If

                ' C. ⭐️ CRITICAL FIX: Wire up the custom event handler ⭐️
                AddHandler card.ActionRequested, AddressOf AccCard_ActionRequested

                ' D. Add to the scrollable container
                Panel2.Controls.Add(card)

                ' Update the Y-position for the NEXT card
                nextYPosition += card.Height + CARD_SPACING
            Next

        Catch ex As Exception
            ' The helper usually handles the DB message, this catches application-level errors.
            MessageBox.Show($"Error loading account data: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        ' Set Scrollable Area
        Panel2.AutoScroll = True
        Panel2.AutoScrollMinSize = New Size(0, nextYPosition)
        Panel2.PerformLayout()
        Panel2.Refresh()
    End Sub

    ' ----------------------------------------------------
    ' | 3. ACC CARD EVENT HANDLER (View/Edit/Delete Logic)
    ' ----------------------------------------------------

    ''' <summary>
    ''' Handles the custom ActionRequested event raised by the AccCard control.
    ''' </summary>
    Private Sub AccCard_ActionRequested(ByVal cardUserID As Integer, ByVal Action As String)

        If Action = "Edit" Then
            Dim editForm As New EditAccountPage()
            editForm.ParentManageAccountsForm = Me ' Used to refresh this form after editing
            editForm.TargetUserID = cardUserID

            editForm.LoadAccountData(cardUserID) ' Assumes this method exists on EditAccountPage
            editForm.ShowDialog()

        ElseIf Action = "Delete" Then

            Dim result As DialogResult = MessageBox.Show($"Are you sure you want to delete Account ID: {cardUserID}?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

            If result = DialogResult.Yes Then

                Dim sqlDelete As String = "DELETE FROM tbl_account WHERE AccountID = @id"
                Dim params As New Dictionary(Of String, Object)
                params.Add("@id", cardUserID)

                Try
                    ' Execute delete action using the DatabaseConnection helper
                    Dim rowsAffected As Integer = db.ExecuteAction(sqlDelete, params)
                    If rowsAffected > 0 Then
                        MessageBox.Show($"Account ID: {cardUserID} deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        LoadAccountCards() ' Refresh list immediately
                    Else
                        MessageBox.Show("Deletion failed. Account not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                Catch ex As Exception
                    MessageBox.Show($"Error deleting account: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If

        ElseIf Action = "View" Then
            Dim viewForm As New ViewAccDetailsvb()
            viewForm.TargetUserID = cardUserID
            viewForm.ShowDialog()
        End If

    End Sub

    ' ----------------------------------------------------
    ' | 4. BUTTON CLICK EVENTS (Plus Button)
    ' ----------------------------------------------------

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        ' Assumes CreateAcc_SupAdmin is the next form to open
        Dim createForm As New CreateAcc_SupAdmin()
        createForm.Show()
    End Sub

End Class