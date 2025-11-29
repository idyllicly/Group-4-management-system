Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class AccCard
    Inherits UserControl

    ' ====================================================================
    ' 1. PROPERTIES & CUSTOM EVENT
    ' ====================================================================

    Public Property UserID As Integer ' Strongly typed

    Public Property UserName As String
        Get
            Return LinkLabel1.Text
        End Get
        Set(value As String)
            LinkLabel1.Text = value
        End Set
    End Property

    ' Property to set the PictureBox image (Handles disposal internally for resource safety)
    Public Property UserImage As Image
        Get
            Return PictureBox1.Image
        End Get
        Set(value As Image)
            ' GDI+ FIX: Dispose of the old image before setting a new one to free memory
            If PictureBox1 IsNot Nothing AndAlso PictureBox1.Image IsNot Nothing Then
                PictureBox1.Image.Dispose()
            End If

            PictureBox1.Image = value

            If PictureBox1 IsNot Nothing AndAlso value IsNot Nothing Then
                PictureBox1.SizeMode = PictureBoxSizeMode.Zoom
            End If
        End Set
    End Property

    ' FIX: Custom event declared with correct ByVal signature and strong types
    Public Event ActionRequested(ByVal UserID As Integer, ByVal Action As String)

    ' ====================================================================
    ' 2. IMAGE LOADING FUNCTION (Resource Safe)
    ' ====================================================================

    Public Sub SetPictureFromBytes(ByVal pictureData As Byte())
        Me.UserImage = Nothing ' Clear existing image and trigger disposal of old resource

        If pictureData IsNot Nothing AndAlso pictureData.Length > 0 Then
            Try
                ' Use Using block to automatically dispose of the MemoryStream
                Using ms As New MemoryStream(pictureData)
                    ' Clone the image so the MemoryStream can be closed without locking the image
                    Me.UserImage = CType(Image.FromStream(ms).Clone(), Image)
                End Using
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine($"Error loading image for AccCard {Me.UserID}: {ex.Message}")
                Me.UserImage = Nothing
            End Try
        End If
    End Sub

    ' ====================================================================
    ' 3. CONSTRUCTOR & INITIALIZATION
    ' ====================================================================

    Public Sub New()
        InitializeComponent()
    End Sub

    ' ----------------------------------------------------------------------
    ' 4. USER INTERFACE TRIGGERS & ACTIONS
    ' ----------------------------------------------------------------------

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If ContextMenuStrip1 IsNot Nothing AndAlso LinkLabel1 IsNot Nothing Then
            ' Shows the menu right below the LinkLabel
            ContextMenuStrip1.Show(LinkLabel1, 0, LinkLabel1.Height)
        End If
    End Sub

    ' Triggers View Action
    Private Sub ViewAccountToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewAccountToolStripMenuItem.Click
        RaiseEvent ActionRequested(UserID, "View")
    End Sub

    ' Triggers Edit Action
    Private Sub EditAccountToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles EditAccountToolStripMenuItem1.Click
        RaiseEvent ActionRequested(UserID, "Edit")
    End Sub

    ' Triggers Delete Action
    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        RaiseEvent ActionRequested(UserID, "Delete")
    End Sub

    ' ----------------------------------------------------------------------
    ' 5. RESOURCE CLEANUP
    ' ----------------------------------------------------------------------

    ' Explicitly handle disposal of the image when the control is disposed.
    Private Sub AccCard_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        ' Setting the property to Nothing handles the resource cleanup via the property's setter.
        Me.UserImage = Nothing
    End Sub

End Class