<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SelectTechPage
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.FormHostPanel = New System.Windows.Forms.Panel()
        Me.TechnicianCard1 = New management_system.TechnicianCard()
        Me.PanelHeader = New System.Windows.Forms.Label()
        Me.ShadowPanel = New System.Windows.Forms.Panel()
        Me.PageLabel1 = New management_system.PageLabel()
        Me.SideNavControl1 = New management_system.SideNavControl()
        Me.Panel1.SuspendLayout()
        Me.MainPanel.SuspendLayout()
        Me.FormHostPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PageLabel1)
        Me.Panel1.Controls.Add(Me.MainPanel)
        Me.Panel1.Controls.Add(Me.ShadowPanel)
        Me.Panel1.Controls.Add(Me.SideNavControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1429, 1100)
        Me.Panel1.TabIndex = 1
        '
        'MainPanel
        '
        Me.MainPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.MainPanel.AutoScroll = True
        Me.MainPanel.BackColor = System.Drawing.Color.White
        Me.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.MainPanel.Controls.Add(Me.FormHostPanel)
        Me.MainPanel.Controls.Add(Me.PanelHeader)
        Me.MainPanel.ForeColor = System.Drawing.Color.MediumBlue
        Me.MainPanel.Location = New System.Drawing.Point(397, 116)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(1069, 952)
        Me.MainPanel.TabIndex = 4
        '
        'FormHostPanel
        '
        Me.FormHostPanel.AutoScroll = True
        Me.FormHostPanel.Controls.Add(Me.TechnicianCard1)
        Me.FormHostPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FormHostPanel.Location = New System.Drawing.Point(0, 70)
        Me.FormHostPanel.Name = "FormHostPanel"
        Me.FormHostPanel.Size = New System.Drawing.Size(1067, 880)
        Me.FormHostPanel.TabIndex = 1
        '
        'TechnicianCard1
        '
        Me.TechnicianCard1.Location = New System.Drawing.Point(80, 44)
        Me.TechnicianCard1.Name = "TechnicianCard1"
        Me.TechnicianCard1.Size = New System.Drawing.Size(216, 215)
        Me.TechnicianCard1.TabIndex = 1
        '
        'PanelHeader
        '
        Me.PanelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PanelHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PanelHeader.Location = New System.Drawing.Point(0, 0)
        Me.PanelHeader.MinimumSize = New System.Drawing.Size(2, 70)
        Me.PanelHeader.Name = "PanelHeader"
        Me.PanelHeader.Size = New System.Drawing.Size(1067, 70)
        Me.PanelHeader.TabIndex = 0
        Me.PanelHeader.Text = "            SELECT TECHNICIAN"
        Me.PanelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ShadowPanel
        '
        Me.ShadowPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShadowPanel.BackColor = System.Drawing.Color.MediumPurple
        Me.ShadowPanel.Location = New System.Drawing.Point(370, 107)
        Me.ShadowPanel.Name = "ShadowPanel"
        Me.ShadowPanel.Size = New System.Drawing.Size(1059, 993)
        Me.ShadowPanel.TabIndex = 3
        '
        'PageLabel1
        '
        Me.PageLabel1.BackColor = System.Drawing.Color.MediumPurple
        Me.PageLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PageLabel1.Location = New System.Drawing.Point(0, 0)
        Me.PageLabel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PageLabel1.Name = "PageLabel1"
        Me.PageLabel1.Size = New System.Drawing.Size(1429, 119)
        Me.PageLabel1.TabIndex = 32
        '
        'SideNavControl1
        '
        Me.SideNavControl1.BackColor = System.Drawing.Color.White
        Me.SideNavControl1.Location = New System.Drawing.Point(0, 107)
        Me.SideNavControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SideNavControl1.Name = "SideNavControl1"
        Me.SideNavControl1.Size = New System.Drawing.Size(371, 994)
        Me.SideNavControl1.TabIndex = 33
        '
        'SelectTechPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1429, 1100)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "SelectTechPage"
        Me.Text = "SelectTechPage"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.MainPanel.ResumeLayout(False)
        Me.FormHostPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents MainPanel As Panel
    Friend WithEvents TechnicianCard1 As TechnicianCard
    Friend WithEvents PanelHeader As Label
    Friend WithEvents ShadowPanel As Panel
    Friend WithEvents FormHostPanel As Panel
    Friend WithEvents PageLabel1 As PageLabel
    Friend WithEvents SideNavControl1 As SideNavControl
End Class
