<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class timeline_page
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MainTimelinePanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.UserControl11 = New management_system.NavigationControl()
        Me.SuspendLayout()
        '
        'MainTimelinePanel
        '
        Me.MainTimelinePanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MainTimelinePanel.AutoScroll = True
        Me.MainTimelinePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.MainTimelinePanel.Location = New System.Drawing.Point(92, 115)
        Me.MainTimelinePanel.Name = "MainTimelinePanel"
        Me.MainTimelinePanel.Size = New System.Drawing.Size(643, 568)
        Me.MainTimelinePanel.TabIndex = 0
        Me.MainTimelinePanel.WrapContents = False
        '
        'UserControl11
        '
        Me.UserControl11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserControl11.BackColor = System.Drawing.Color.White
        Me.UserControl11.Location = New System.Drawing.Point(-4, 1)
        Me.UserControl11.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.UserControl11.Name = "UserControl11"
        Me.UserControl11.Size = New System.Drawing.Size(815, 94)
        Me.UserControl11.TabIndex = 1
        '
        'timeline_page
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(815, 542)
        Me.Controls.Add(Me.UserControl11)
        Me.Controls.Add(Me.MainTimelinePanel)
        Me.Name = "timeline_page"
        Me.Text = "timeline_page"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainTimelinePanel As FlowLayoutPanel
    Friend WithEvents UserControl11 As NavigationControl
End Class
