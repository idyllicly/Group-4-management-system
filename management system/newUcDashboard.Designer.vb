<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newUcDashboard
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpViewDate = New System.Windows.Forms.DateTimePicker()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgvDailyJobs = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblSelectedJob = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbTechnician = New System.Windows.Forms.ComboBox()
        Me.btnAssignJob = New System.Windows.Forms.Button()
        CType(Me.dgvDailyJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(172, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Daily Operations"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(51, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Select Date:"
        '
        'dtpViewDate
        '
        Me.dtpViewDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpViewDate.Location = New System.Drawing.Point(155, 91)
        Me.dtpViewDate.Name = "dtpViewDate"
        Me.dtpViewDate.Size = New System.Drawing.Size(200, 26)
        Me.dtpViewDate.TabIndex = 2
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(388, 89)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(126, 35)
        Me.btnRefresh.TabIndex = 3
        Me.btnRefresh.Text = "Refresh List"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dgvDailyJobs
        '
        Me.dgvDailyJobs.AllowUserToAddRows = False
        Me.dgvDailyJobs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvDailyJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDailyJobs.Location = New System.Drawing.Point(72, 180)
        Me.dgvDailyJobs.Name = "dgvDailyJobs"
        Me.dgvDailyJobs.ReadOnly = True
        Me.dgvDailyJobs.RowHeadersWidth = 62
        Me.dgvDailyJobs.RowTemplate.Height = 28
        Me.dgvDailyJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDailyJobs.Size = New System.Drawing.Size(541, 176)
        Me.dgvDailyJobs.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnAssignJob)
        Me.GroupBox1.Controls.Add(Me.cmbTechnician)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblSelectedJob)
        Me.GroupBox1.Location = New System.Drawing.Point(308, 458)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(353, 157)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dispatch Job"
        '
        'lblSelectedJob
        '
        Me.lblSelectedJob.AutoSize = True
        Me.lblSelectedJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSelectedJob.Location = New System.Drawing.Point(26, 39)
        Me.lblSelectedJob.Name = "lblSelectedJob"
        Me.lblSelectedJob.Size = New System.Drawing.Size(177, 20)
        Me.lblSelectedJob.TabIndex = 0
        Me.lblSelectedJob.Text = "Select a Job above..."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 20)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Assign Technician:"
        '
        'cmbTechnician
        '
        Me.cmbTechnician.FormattingEnabled = True
        Me.cmbTechnician.Location = New System.Drawing.Point(177, 63)
        Me.cmbTechnician.Name = "cmbTechnician"
        Me.cmbTechnician.Size = New System.Drawing.Size(121, 28)
        Me.cmbTechnician.TabIndex = 2
        '
        'btnAssignJob
        '
        Me.btnAssignJob.Location = New System.Drawing.Point(177, 97)
        Me.btnAssignJob.Name = "btnAssignJob"
        Me.btnAssignJob.Size = New System.Drawing.Size(151, 39)
        Me.btnAssignJob.TabIndex = 3
        Me.btnAssignJob.Text = "ASSIGN TECH"
        Me.btnAssignJob.UseVisualStyleBackColor = True
        '
        'newUcDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvDailyJobs)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dtpViewDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "newUcDashboard"
        Me.Size = New System.Drawing.Size(693, 640)
        CType(Me.dgvDailyJobs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents dtpViewDate As DateTimePicker
    Friend WithEvents btnRefresh As Button
    Friend WithEvents dgvDailyJobs As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents btnAssignJob As Button
    Friend WithEvents cmbTechnician As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents lblSelectedJob As Label
End Class
