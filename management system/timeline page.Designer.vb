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
        Me.PageLabel1 = New management_system.PageLabel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.SideNavControl1 = New management_system.SideNavControl()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainTimelinePanel
        '
        Me.MainTimelinePanel.AutoScroll = True
        Me.MainTimelinePanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTimelinePanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.MainTimelinePanel.Location = New System.Drawing.Point(420, 3)
        Me.MainTimelinePanel.Name = "MainTimelinePanel"
        Me.MainTimelinePanel.Size = New System.Drawing.Size(1343, 902)
        Me.MainTimelinePanel.TabIndex = 0
        Me.MainTimelinePanel.WrapContents = False
        '
        'PageLabel1
        '
        Me.PageLabel1.BackColor = System.Drawing.Color.MediumPurple
        Me.PageLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PageLabel1.Location = New System.Drawing.Point(0, 0)
        Me.PageLabel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PageLabel1.Name = "PageLabel1"
        Me.PageLabel1.Size = New System.Drawing.Size(1766, 142)
        Me.PageLabel1.TabIndex = 1
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 417.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MainTimelinePanel, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SideNavControl1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 142)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1766, 908)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'SideNavControl1
        '
        Me.SideNavControl1.BackColor = System.Drawing.Color.White
        Me.SideNavControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SideNavControl1.Location = New System.Drawing.Point(3, 4)
        Me.SideNavControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SideNavControl1.Name = "SideNavControl1"
        Me.SideNavControl1.Size = New System.Drawing.Size(411, 900)
        Me.SideNavControl1.TabIndex = 1
        '
        'timeline_page
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1766, 1050)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.PageLabel1)
        Me.Name = "timeline_page"
        Me.Text = "timeline_page"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents MainTimelinePanel As FlowLayoutPanel
    Friend WithEvents PageLabel1 As PageLabel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents SideNavControl1 As SideNavControl
End Class
