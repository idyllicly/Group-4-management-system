<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DashboardLayout
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.tblDashboard = New System.Windows.Forms.TableLayoutPanel()
        Me.flpCompleted = New System.Windows.Forms.FlowLayoutPanel()
        Me.flpFollowUp = New System.Windows.Forms.FlowLayoutPanel()
        Me.flpProgress = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.flpRejected = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.UserControl11 = New management_system.UserControl1()
        Me.Panel2.SuspendLayout()
        Me.tblDashboard.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.tblDashboard)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(140, 30)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(50)
        Me.Panel2.Size = New System.Drawing.Size(955, 495)
        Me.Panel2.TabIndex = 3
        '
        'tblDashboard
        '
        Me.tblDashboard.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tblDashboard.ColumnCount = 4
        Me.tblDashboard.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblDashboard.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblDashboard.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblDashboard.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tblDashboard.Controls.Add(Me.flpCompleted, 2, 1)
        Me.tblDashboard.Controls.Add(Me.flpFollowUp, 1, 1)
        Me.tblDashboard.Controls.Add(Me.flpProgress, 0, 1)
        Me.tblDashboard.Controls.Add(Me.Label6, 3, 0)
        Me.tblDashboard.Controls.Add(Me.Label3, 0, 0)
        Me.tblDashboard.Controls.Add(Me.Label5, 2, 0)
        Me.tblDashboard.Controls.Add(Me.Label4, 1, 0)
        Me.tblDashboard.Controls.Add(Me.flpRejected, 3, 1)
        Me.tblDashboard.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tblDashboard.Location = New System.Drawing.Point(50, 79)
        Me.tblDashboard.Name = "tblDashboard"
        Me.tblDashboard.RowCount = 2
        Me.tblDashboard.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39.0!))
        Me.tblDashboard.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.tblDashboard.Size = New System.Drawing.Size(855, 366)
        Me.tblDashboard.TabIndex = 3
        '
        'flpCompleted
        '
        Me.flpCompleted.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpCompleted.AutoScroll = True
        Me.flpCompleted.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpCompleted.Location = New System.Drawing.Point(430, 44)
        Me.flpCompleted.Name = "flpCompleted"
        Me.flpCompleted.Size = New System.Drawing.Size(206, 318)
        Me.flpCompleted.TabIndex = 23
        Me.flpCompleted.WrapContents = False
        '
        'flpFollowUp
        '
        Me.flpFollowUp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpFollowUp.AutoScroll = True
        Me.flpFollowUp.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpFollowUp.Location = New System.Drawing.Point(217, 44)
        Me.flpFollowUp.Name = "flpFollowUp"
        Me.flpFollowUp.Size = New System.Drawing.Size(206, 318)
        Me.flpFollowUp.TabIndex = 22
        Me.flpFollowUp.WrapContents = False
        '
        'flpProgress
        '
        Me.flpProgress.AutoScroll = True
        Me.flpProgress.AutoSize = True
        Me.flpProgress.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpProgress.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpProgress.Location = New System.Drawing.Point(4, 44)
        Me.flpProgress.Name = "flpProgress"
        Me.flpProgress.Size = New System.Drawing.Size(206, 318)
        Me.flpProgress.TabIndex = 21
        Me.flpProgress.WrapContents = False
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.LightCoral
        Me.Label6.Location = New System.Drawing.Point(643, 1)
        Me.Label6.MinimumSize = New System.Drawing.Size(0, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(208, 38)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Rejected Jobs"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(4, 1)
        Me.Label3.MinimumSize = New System.Drawing.Size(0, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(206, 39)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Jobs In Progress"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.GreenYellow
        Me.Label5.Location = New System.Drawing.Point(430, 1)
        Me.Label5.MinimumSize = New System.Drawing.Size(0, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(206, 39)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Completed jobs"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Aquamarine
        Me.Label4.Location = New System.Drawing.Point(217, 1)
        Me.Label4.MinimumSize = New System.Drawing.Size(0, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(206, 39)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Follow-Up Jobs"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'flpRejected
        '
        Me.flpRejected.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpRejected.AutoScroll = True
        Me.flpRejected.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpRejected.Location = New System.Drawing.Point(643, 44)
        Me.flpRejected.Name = "flpRejected"
        Me.flpRejected.Size = New System.Drawing.Size(208, 318)
        Me.flpRejected.TabIndex = 24
        Me.flpRejected.WrapContents = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(50, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 29)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Dashboard"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 95)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(140, 30, 140, 30)
        Me.Panel3.Size = New System.Drawing.Size(1235, 555)
        Me.Panel3.TabIndex = 11
        '
        'UserControl11
        '
        Me.UserControl11.BackColor = System.Drawing.Color.White
        Me.UserControl11.Dock = System.Windows.Forms.DockStyle.Top
        Me.UserControl11.Location = New System.Drawing.Point(0, 0)
        Me.UserControl11.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.UserControl11.Name = "UserControl11"
        Me.UserControl11.Size = New System.Drawing.Size(1235, 95)
        Me.UserControl11.TabIndex = 10
        '
        'DashboardLayout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1235, 650)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.UserControl11)
        Me.Name = "DashboardLayout"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DashboardLayout"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tblDashboard.ResumeLayout(False)
        Me.tblDashboard.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents UserControl11 As UserControl1
    Friend WithEvents tblDashboard As TableLayoutPanel
    Friend WithEvents flpCompleted As FlowLayoutPanel
    Friend WithEvents flpFollowUp As FlowLayoutPanel
    Friend WithEvents flpProgress As FlowLayoutPanel
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents flpRejected As FlowLayoutPanel
    Friend WithEvents Panel3 As Panel
End Class
