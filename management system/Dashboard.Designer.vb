<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dashboard
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
        Me.NavigationControl1 = New management_system.NavigationControl()
        Me.pnlMainContent = New System.Windows.Forms.Panel()
        Me.RoundedPanel1 = New management_system.RoundedPanel()
        Me.tlpColumns = New System.Windows.Forms.TableLayoutPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel3 = New System.Windows.Forms.FlowLayoutPanel()
        Me.FlowLayoutPanel4 = New System.Windows.Forms.FlowLayoutPanel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.pnlMainContent.SuspendLayout()
        Me.RoundedPanel1.SuspendLayout()
        Me.tlpColumns.SuspendLayout()
        Me.SuspendLayout()
        '
        'NavigationControl1
        '
        Me.NavigationControl1.BackColor = System.Drawing.Color.White
        Me.NavigationControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.NavigationControl1.Location = New System.Drawing.Point(0, 0)
        Me.NavigationControl1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.NavigationControl1.Name = "NavigationControl1"
        Me.NavigationControl1.Size = New System.Drawing.Size(783, 92)
        Me.NavigationControl1.TabIndex = 0
        '
        'pnlMainContent
        '
        Me.pnlMainContent.Controls.Add(Me.RoundedPanel1)
        Me.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMainContent.Location = New System.Drawing.Point(0, 92)
        Me.pnlMainContent.Name = "pnlMainContent"
        Me.pnlMainContent.Padding = New System.Windows.Forms.Padding(20)
        Me.pnlMainContent.Size = New System.Drawing.Size(783, 481)
        Me.pnlMainContent.TabIndex = 2
        '
        'RoundedPanel1
        '
        Me.RoundedPanel1.BackColor = System.Drawing.Color.White
        Me.RoundedPanel1.Controls.Add(Me.tlpColumns)
        Me.RoundedPanel1.Controls.Add(Me.lblTitle)
        Me.RoundedPanel1.CornerRadius = 20
        Me.RoundedPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RoundedPanel1.ForeColor = System.Drawing.Color.DarkCyan
        Me.RoundedPanel1.Location = New System.Drawing.Point(20, 20)
        Me.RoundedPanel1.Name = "RoundedPanel1"
        Me.RoundedPanel1.Padding = New System.Windows.Forms.Padding(10)
        Me.RoundedPanel1.Size = New System.Drawing.Size(743, 441)
        Me.RoundedPanel1.TabIndex = 0
        '
        'tlpColumns
        '
        Me.tlpColumns.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.[Single]
        Me.tlpColumns.ColumnCount = 4
        Me.tlpColumns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpColumns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpColumns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpColumns.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.tlpColumns.Controls.Add(Me.Label4, 3, 0)
        Me.tlpColumns.Controls.Add(Me.Label3, 2, 0)
        Me.tlpColumns.Controls.Add(Me.Label2, 1, 0)
        Me.tlpColumns.Controls.Add(Me.Label1, 0, 0)
        Me.tlpColumns.Controls.Add(Me.FlowLayoutPanel1, 0, 1)
        Me.tlpColumns.Controls.Add(Me.FlowLayoutPanel2, 1, 1)
        Me.tlpColumns.Controls.Add(Me.FlowLayoutPanel3, 2, 1)
        Me.tlpColumns.Controls.Add(Me.FlowLayoutPanel4, 3, 1)
        Me.tlpColumns.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tlpColumns.Location = New System.Drawing.Point(10, 53)
        Me.tlpColumns.Name = "tlpColumns"
        Me.tlpColumns.RowCount = 2
        Me.tlpColumns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40.0!))
        Me.tlpColumns.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlpColumns.Size = New System.Drawing.Size(723, 378)
        Me.tlpColumns.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(544, 1)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(175, 40)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Cancelled/Rejected"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Lime
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(364, 1)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(173, 40)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Completed Jobs"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Cyan
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(184, 1)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(173, 40)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Follow-up Jobs"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(4, 1)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(173, 40)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Jobs in Progress"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(4, 45)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(173, 329)
        Me.FlowLayoutPanel1.TabIndex = 4
        '
        'FlowLayoutPanel2
        '
        Me.FlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel2.Location = New System.Drawing.Point(184, 45)
        Me.FlowLayoutPanel2.Name = "FlowLayoutPanel2"
        Me.FlowLayoutPanel2.Size = New System.Drawing.Size(173, 329)
        Me.FlowLayoutPanel2.TabIndex = 5
        '
        'FlowLayoutPanel3
        '
        Me.FlowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel3.Location = New System.Drawing.Point(364, 45)
        Me.FlowLayoutPanel3.Name = "FlowLayoutPanel3"
        Me.FlowLayoutPanel3.Size = New System.Drawing.Size(173, 329)
        Me.FlowLayoutPanel3.TabIndex = 6
        '
        'FlowLayoutPanel4
        '
        Me.FlowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel4.Location = New System.Drawing.Point(544, 45)
        Me.FlowLayoutPanel4.Name = "FlowLayoutPanel4"
        Me.FlowLayoutPanel4.Size = New System.Drawing.Size(175, 329)
        Me.FlowLayoutPanel4.TabIndex = 7
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblTitle.Font = New System.Drawing.Font("Arial", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(10, 10)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(251, 43)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "DASHBOARD"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(783, 573)
        Me.Controls.Add(Me.pnlMainContent)
        Me.Controls.Add(Me.NavigationControl1)
        Me.Name = "Dashboard"
        Me.Text = "Dashboard"
        Me.pnlMainContent.ResumeLayout(False)
        Me.RoundedPanel1.ResumeLayout(False)
        Me.RoundedPanel1.PerformLayout()
        Me.tlpColumns.ResumeLayout(False)
        Me.tlpColumns.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents NavigationControl1 As NavigationControl
    Friend WithEvents pnlMainContent As Panel
    Friend WithEvents RoundedPanel1 As RoundedPanel
    Friend WithEvents lblTitle As Label
    Friend WithEvents tlpColumns As TableLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel2 As FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel3 As FlowLayoutPanel
    Friend WithEvents FlowLayoutPanel4 As FlowLayoutPanel
End Class
