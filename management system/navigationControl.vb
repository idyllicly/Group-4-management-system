Imports System.Windows.Forms
Imports System.Drawing

Public Class NavigationControl1
    Inherits UserControl

    ' Private variable to hold the stack of menu buttons, declared at the class level.
    Private ReadOnly MenuButtonsContainer As New FlowLayoutPanel()

    Public Sub New()
        ' Initializes the control's visual content immediately upon creation.
        SetupMenuContent()
    End Sub

    ' IMPORTANT: Delete the Private Sub NavigationControl1_Load block if it still exists.

    Private Sub SetupMenuContent()
        ' --- 1. SET UP THE NAVIGATION CONTROL APPEARANCE (The outer box) ---
        Me.Width = 200
        Me.Height = 180
        Me.BorderStyle = BorderStyle.FixedSingle
        Me.BackColor = Color.WhiteSmoke

        ' --- 2. SET UP THE FLOW LAYOUT CONTAINER ---
        MenuButtonsContainer.FlowDirection = FlowDirection.TopDown
        MenuButtonsContainer.Dock = DockStyle.Fill
        MenuButtonsContainer.Padding = New Padding(10)
        Me.Controls.Add(MenuButtonsContainer)

        ' --- 3. ADD THE "Main Menu" LABEL ---
        Dim lblHeader As New Label() With {
            .Text = "Main Menu",
            .Font = New Font("Segoe UI", 12, FontStyle.Bold),
            .Margin = New Padding(10, 5, 10, 10),
            .AutoSize = True
        }
        MenuButtonsContainer.Controls.Add(lblHeader)

        ' --- 4. ADD THE MENU BUTTONS ---
        MenuButtonsContainer.Controls.Add(CreateMenuButton("Account Settings", Sub() MessageBox.Show("Account Settings clicked!")))
        MenuButtonsContainer.Controls.Add(CreateMenuButton("Dashboard", Sub() MessageBox.Show("Dashboard clicked!")))
        MenuButtonsContainer.Controls.Add(CreateMenuButton("Timeline", Sub() MessageBox.Show("Timeline clicked!")))
        MenuButtonsContainer.Controls.Add(CreateMenuButton("Inquiry", Sub() MessageBox.Show("Inquiry clicked!")))

        ' Set initial state to hidden
        Me.Visible = False
    End Sub

    Private Function CreateMenuButton(text As String, action As Action) As Button
        ' Helper function to create the styled buttons
        Dim btn As New Button() With {
            .Text = text,
            .Font = New Font("Segoe UI", 10),
            .Width = 180,
            .Height = 35,
            .FlatStyle = FlatStyle.Standard,
            .Margin = New Padding(0, 5, 0, 5)
        }
        AddHandler btn.Click, Sub() action()
        Return btn
    End Function

    ' Public method for main form to call to show/hide the menu
    Public Sub ToggleMenu()
        Me.Visible = Not Me.Visible
        Me.BringToFront()
    End Sub

End Class