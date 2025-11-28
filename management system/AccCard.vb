Imports System.Drawing
Imports System.IO

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
            ' Assuming LinkLabel1 displays the username and account type
            ' Example: "username (AccType)"
            LinkLabel1.Text = value
        End Set
    End Property

    ' Property to set the PictureBox image (Requires PictureBox1 to exist)
    Public Property UserImage As Image
        Get
            Return PictureBox1.Image
        End Get
        Set(value As Image)
            ' Dispose of old image before setting new one to free memory
            If PictureBox1.Image IsNot Nothing Then PictureBox1.Image.Dispose()

            PictureBox1.Image = value
            ' Ensure the image scaling is appropriate
            If value IsNot Nothing Then
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom ' Ensures image fits nicely
            End If
        End Set
    End Property

    ' Custom event to notify the parent form (ManageAccounts) when an action occurs
    Public Event ActionRequested(ByVal UserID As Integer, ByVal Action As String)

    ' ====================================================================
    ' 2. IMAGE LOADING FUNCTION (NEW)
    ' ====================================================================

    ''' <summary>
    ''' Converts a byte array (from the database) into an Image object 
    ''' and sets the PictureBox1 image property. This is necessary because 
    ''' the database stores images as raw byte data (BLOB).
    ''' </summary>
    ''' <param name="pictureData">The byte array containing the image data (from APicture column).</param>
    Public Sub SetPictureFromBytes(ByVal pictureData As Byte())
        If pictureData IsNot Nothing AndAlso pictureData.Length > 0 Then
            Try
                ' Use MemoryStream to read the byte array as an image stream
                Using ms As New MemoryStream(pictureData)
                    ' Assign the converted Image to the public property
                    Me.UserImage = Image.FromStream(ms)
                End Using
            Catch ex As Exception
                ' Handle potential corruption or conversion errors
                System.Diagnostics.Debug.WriteLine($"Error loading image for AccCard {Me.UserID}: {ex.Message}")
                Me.UserImage = Nothing
            End Try
        Else
            ' If there is no data, clear the image property
            Me.UserImage = Nothing
        End If
    End Sub

    ' ====================================================================
    ' 3. CONSTRUCTOR & INITIALIZATION
    ' ====================================================================

    Public Sub New()
        InitializeComponent()
    End Sub

    ' ====================================================================
    ' 4. USER INTERFACE TRIGGERS & ACTIONS
    ' ====================================================================

    ' 🔑 TRIGGERS THE MENU: Assumes ContextMenuStrip1 is the name of your menu control.
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        ' Show the menu right below the link label
        ContextMenuStrip1.Show(LinkLabel1, 0, LinkLabel1.Height)
    End Sub

    ' ----------------------------------------------------------------------
    ' ACTION HANDLERS: These fire the custom event, passing data to ManageAccounts
    ' ----------------------------------------------------------------------

    ' 🔎 VIEW ACCOUNT ACTION - Raises event with "View" action
    Private Sub ViewAccountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewAccountToolStripMenuItem.Click
        RaiseEvent ActionRequested(UserID, "View")
    End Sub

    ' 📝 EDIT ACCOUNT ACTION (Using the name you provided: EditAccountToolStripMenuItem1) - Raises event with "Edit" action
    Private Sub EditAccountToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditAccountToolStripMenuItem1.Click
        RaiseEvent ActionRequested(UserID, "Edit")
    End Sub

    ' 🗑️ DELETE ACCOUNT ACTION - Raises event with "Delete" action
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        RaiseEvent ActionRequested(UserID, "Delete")
    End Sub

    ' Placeholder for PictureBox click event
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        ' Add logic here if clicking the image should do something
    End Sub

    Private Sub AccCard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class