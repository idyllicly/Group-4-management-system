Public Class AccCard
    Inherits UserControl

    ' ====================================================================
    ' 1. PROPERTIES & CUSTOM EVENT (Structure for Data Exchange)
    ' ====================================================================

    ' Property to hold the unique ID for database lookups
    Public Property UserID As Integer

    ' Property to set the LinkLabel text
    Public Property UserName As String
        Get
            Return LinkLabel1.Text
        End Get
        Set(value As String)
            LinkLabel1.Text = value
        End Set
    End Property

    ' Property to set the PictureBox image (Requires PictureBox1 to exist)
    Public Property UserImage As Image
        Get
            Return PictureBox1.Image
        End Get
        Set(value As Image)
            PictureBox1.Image = value
        End Set
    End Property

    ' Custom event to notify the parent form (ManageAccounts) when an action occurs
    Public Event ActionRequested(ByVal UserID As Integer, ByVal Action As String)

    ' ====================================================================
    ' 2. CONSTRUCTOR & INITIALIZATION
    ' ====================================================================

    Public Sub New()
        InitializeComponent()
    End Sub

    ' NOTE: Since you are using the Designer, the programmatic menu build 
    ' (ActionMenu and BuildActionMenu subs) is now completely removed!

    ' ====================================================================
    ' 3. USER INTERFACE TRIGGERS & ACTIONS
    ' ====================================================================

    ' 🔑 TRIGGERS THE MENU: Assumes ContextMenuStrip1 is the name of your menu control.
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ' Show the menu right below the link label
        ContextMenuStrip1.Show(LinkLabel1, 0, LinkLabel1.Height)
    End Sub

    ' ----------------------------------------------------------------------
    ' ACTION HANDLERS: These fire the custom event, passing data to ManageAccounts
    ' ----------------------------------------------------------------------

    ' 🔎 VIEW ACCOUNT ACTION
    Private Sub ViewAccountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewAccountToolStripMenuItem.Click
        RaiseEvent ActionRequested(UserID, "View")
    End Sub

    ' 📝 EDIT ACCOUNT ACTION (Using the name you provided: EditAccountToolStripMenuItem1)
    Private Sub EditAccountToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditAccountToolStripMenuItem1.Click
        RaiseEvent ActionRequested(UserID, "Edit")
    End Sub

    ' 🗑️ DELETE ACCOUNT ACTION
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        RaiseEvent ActionRequested(UserID, "Delete")
    End Sub

    ' Placeholder for PictureBox click event
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' Add logic here if clicking the image should do something
    End Sub

End Class