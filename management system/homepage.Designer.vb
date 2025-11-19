<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class homepage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.menu = New management_system.SideMenuControl()
        Me.NavigationControl2 = New management_system.NavigationControl()
        Me.SuspendLayout()
        '
        'menu
        '
        Me.menu.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.menu.Location = New System.Drawing.Point(573, -205)
        Me.menu.Name = "menu"
        Me.menu.Size = New System.Drawing.Size(264, 297)
        Me.menu.TabIndex = 1
        '
        'NavigationControl2
        '
        Me.NavigationControl2.BackColor = System.Drawing.Color.White
        Me.NavigationControl2.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavigationControl2.Location = New System.Drawing.Point(0, 0)
        Me.NavigationControl2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NavigationControl2.Name = "NavigationControl2"
        Me.NavigationControl2.Size = New System.Drawing.Size(827, 92)
        Me.NavigationControl2.TabIndex = 0
        '
        'homepage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1829, 855)
        Me.Controls.Add(Me.UserControl11)
        Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.Name = "homepage"
        Me.Text = "homepage"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents NavigationControl1 As NavigationControl
    Friend WithEvents mySideMenu As SideMenuControl
    Friend WithEvents NavigationControl2 As NavigationControl
    Friend WithEvents menu As SideMenuControl
End Class
