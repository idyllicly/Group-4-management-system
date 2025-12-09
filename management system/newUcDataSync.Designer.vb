<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newUcDataSync
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
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabImport = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pbImport = New System.Windows.Forms.ProgressBar()
        Me.btnImport = New System.Windows.Forms.Button()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.txtFilePath = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboImportProfile = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dgvPreview = New System.Windows.Forms.DataGridView()
        Me.tabExport = New System.Windows.Forms.TabPage()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.chkExportClients = New System.Windows.Forms.CheckBox()
        Me.chkExportContracts = New System.Windows.Forms.CheckBox()
        Me.chkExportJobs = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.tabImport.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvPreview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabExport.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabImport)
        Me.TabControl1.Controls.Add(Me.tabExport)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(821, 582)
        Me.TabControl1.TabIndex = 0
        '
        'tabImport
        '
        Me.tabImport.Controls.Add(Me.TableLayoutPanel1)
        Me.tabImport.Location = New System.Drawing.Point(4, 29)
        Me.tabImport.Name = "tabImport"
        Me.tabImport.Padding = New System.Windows.Forms.Padding(3)
        Me.tabImport.Size = New System.Drawing.Size(813, 549)
        Me.tabImport.TabIndex = 0
        Me.tabImport.Text = "Import Data"
        Me.tabImport.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.dgvPreview, 0, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(807, 543)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblStatus)
        Me.GroupBox1.Controls.Add(Me.pbImport)
        Me.GroupBox1.Controls.Add(Me.btnImport)
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.txtFilePath)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cboImportProfile)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(801, 144)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Step 1: Import Configuration"
        '
        'pbImport
        '
        Me.pbImport.Location = New System.Drawing.Point(308, 109)
        Me.pbImport.Name = "pbImport"
        Me.pbImport.Size = New System.Drawing.Size(487, 29)
        Me.pbImport.TabIndex = 6
        Me.pbImport.Visible = False
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(10, 105)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(155, 33)
        Me.btnImport.TabIndex = 5
        Me.btnImport.Text = "START IMPORT"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(606, 69)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(93, 26)
        Me.btnBrowse.TabIndex = 4
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.Location = New System.Drawing.Point(242, 69)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(368, 26)
        Me.txtFilePath.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(125, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Select Excel File"
        '
        'cboImportProfile
        '
        Me.cboImportProfile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboImportProfile.FormattingEnabled = True
        Me.cboImportProfile.Items.AddRange(New Object() {"Baiting System", "Termite Control / Soil Poisoning", "General Pest Control (GPC)", "Ocular / Inspection"})
        Me.cboImportProfile.Location = New System.Drawing.Point(242, 34)
        Me.cboImportProfile.Name = "cboImportProfile"
        Me.cboImportProfile.Size = New System.Drawing.Size(368, 28)
        Me.cboImportProfile.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(229, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Select Import Profile (File Type)"
        '
        'dgvPreview
        '
        Me.dgvPreview.BackgroundColor = System.Drawing.Color.White
        Me.dgvPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPreview.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPreview.Location = New System.Drawing.Point(3, 153)
        Me.dgvPreview.Name = "dgvPreview"
        Me.dgvPreview.ReadOnly = True
        Me.dgvPreview.RowHeadersWidth = 62
        Me.dgvPreview.RowTemplate.Height = 28
        Me.dgvPreview.Size = New System.Drawing.Size(801, 387)
        Me.dgvPreview.TabIndex = 1
        '
        'tabExport
        '
        Me.tabExport.Controls.Add(Me.TableLayoutPanel2)
        Me.tabExport.Location = New System.Drawing.Point(4, 29)
        Me.tabExport.Name = "tabExport"
        Me.tabExport.Padding = New System.Windows.Forms.Padding(3)
        Me.tabExport.Size = New System.Drawing.Size(813, 549)
        Me.tabExport.TabIndex = 1
        Me.tabExport.Text = "Export Data"
        Me.tabExport.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.GroupBox2, 1, 1)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.88889!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.555555!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(807, 543)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.FlowLayoutPanel1)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(206, 33)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(394, 476)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Export Configuration"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.chkExportClients)
        Me.FlowLayoutPanel1.Controls.Add(Me.chkExportContracts)
        Me.FlowLayoutPanel1.Controls.Add(Me.chkExportJobs)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label3)
        Me.FlowLayoutPanel1.Controls.Add(Me.dtpFrom)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label4)
        Me.FlowLayoutPanel1.Controls.Add(Me.dtpTo)
        Me.FlowLayoutPanel1.Controls.Add(Me.btnExport)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 22)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(388, 451)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'chkExportClients
        '
        Me.chkExportClients.AutoSize = True
        Me.chkExportClients.Checked = True
        Me.chkExportClients.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExportClients.Location = New System.Drawing.Point(3, 3)
        Me.chkExportClients.Name = "chkExportClients"
        Me.chkExportClients.Size = New System.Drawing.Size(157, 24)
        Me.chkExportClients.TabIndex = 0
        Me.chkExportClients.Text = "Client Master List"
        Me.chkExportClients.UseVisualStyleBackColor = True
        '
        'chkExportContracts
        '
        Me.chkExportContracts.AutoSize = True
        Me.chkExportContracts.Checked = True
        Me.chkExportContracts.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExportContracts.Location = New System.Drawing.Point(3, 33)
        Me.chkExportContracts.Name = "chkExportContracts"
        Me.chkExportContracts.Size = New System.Drawing.Size(151, 24)
        Me.chkExportContracts.TabIndex = 1
        Me.chkExportContracts.Text = "Active Contracts"
        Me.chkExportContracts.UseVisualStyleBackColor = True
        '
        'chkExportJobs
        '
        Me.chkExportJobs.AutoSize = True
        Me.chkExportJobs.Checked = True
        Me.chkExportJobs.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExportJobs.Location = New System.Drawing.Point(3, 63)
        Me.chkExportJobs.Name = "chkExportJobs"
        Me.chkExportJobs.Size = New System.Drawing.Size(197, 24)
        Me.chkExportJobs.TabIndex = 2
        Me.chkExportJobs.Text = "Job Schedules (Dates)"
        Me.chkExportJobs.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 20)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Date Range:"
        '
        'dtpFrom
        '
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(3, 113)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(200, 26)
        Me.dtpFrom.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(3, 142)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 20)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "To"
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(3, 165)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(200, 26)
        Me.dtpTo.TabIndex = 6
        '
        'btnExport
        '
        Me.btnExport.Location = New System.Drawing.Point(3, 197)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(157, 44)
        Me.btnExport.TabIndex = 7
        Me.btnExport.Text = "EXPORT DATA"
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(171, 111)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(121, 20)
        Me.lblStatus.TabIndex = 7
        Me.lblStatus.Text = "Waiting for file..."
        '
        'newUcDataSync
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "newUcDataSync"
        Me.Size = New System.Drawing.Size(821, 582)
        Me.TabControl1.ResumeLayout(False)
        Me.tabImport.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvPreview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabExport.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tabImport As TabPage
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents tabExport As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents pbImport As ProgressBar
    Friend WithEvents btnImport As Button
    Friend WithEvents btnBrowse As Button
    Friend WithEvents txtFilePath As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cboImportProfile As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvPreview As DataGridView
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents chkExportClients As CheckBox
    Friend WithEvents chkExportContracts As CheckBox
    Friend WithEvents chkExportJobs As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents btnExport As Button
    Friend WithEvents lblStatus As Label
End Class
