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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpViewDate = New System.Windows.Forms.DateTimePicker()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.dgvDailyJobs = New System.Windows.Forms.DataGridView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnAssignJob = New System.Windows.Forms.Button()
        Me.cmbTechnician = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnPrevMonth = New System.Windows.Forms.Button()
        Me.btnNextMonth = New System.Windows.Forms.Button()
        Me.lblMonthYear1 = New System.Windows.Forms.Label()
        Me.flpCalendar1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblMonthYear2 = New System.Windows.Forms.Label()
        Me.flpCalendar2 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grpJobDetails = New System.Windows.Forms.GroupBox()
        Me.lblDetailDuration = New System.Windows.Forms.Label()
        Me.lblDetailEnd = New System.Windows.Forms.Label()
        Me.lblDetailStart = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblDetailVisit = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblDetailTech = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblDetailService = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblDetailAddress = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblDetailClient = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.dgvDailyJobs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.grpJobDetails.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(14, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(162, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Day Operations"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(19, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(157, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Select Specific Date:"
        '
        'dtpViewDate
        '
        Me.dtpViewDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpViewDate.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.dtpViewDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.dtpViewDate.CalendarTitleBackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.dtpViewDate.CalendarTitleForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.dtpViewDate.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.dtpViewDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpViewDate.Location = New System.Drawing.Point(228, 46)
        Me.dtpViewDate.Name = "dtpViewDate"
        Me.dtpViewDate.Size = New System.Drawing.Size(196, 26)
        Me.dtpViewDate.TabIndex = 2
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.btnRefresh.Location = New System.Drawing.Point(333, 11)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(91, 29)
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
        Me.dgvDailyJobs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvDailyJobs.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvDailyJobs.BackgroundColor = System.Drawing.Color.Teal
        Me.dgvDailyJobs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvDailyJobs.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDailyJobs.EnableHeadersVisualStyles = False
        Me.dgvDailyJobs.Location = New System.Drawing.Point(19, 72)
        Me.dgvDailyJobs.Name = "dgvDailyJobs"
        Me.dgvDailyJobs.ReadOnly = True
        Me.dgvDailyJobs.RowHeadersVisible = False
        Me.dgvDailyJobs.RowHeadersWidth = 62
        Me.dgvDailyJobs.RowTemplate.Height = 28
        Me.dgvDailyJobs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvDailyJobs.Size = New System.Drawing.Size(405, 278)
        Me.dgvDailyJobs.TabIndex = 4
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnAssignJob)
        Me.GroupBox1.Controls.Add(Me.cmbTechnician)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(19, 613)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(405, 129)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Dispatch Job"
        '
        'btnAssignJob
        '
        Me.btnAssignJob.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAssignJob.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnAssignJob.FlatAppearance.BorderSize = 0
        Me.btnAssignJob.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAssignJob.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAssignJob.ForeColor = System.Drawing.Color.White
        Me.btnAssignJob.Location = New System.Drawing.Point(17, 79)
        Me.btnAssignJob.Name = "btnAssignJob"
        Me.btnAssignJob.Size = New System.Drawing.Size(371, 39)
        Me.btnAssignJob.TabIndex = 3
        Me.btnAssignJob.Text = "ASSIGN TECH"
        Me.btnAssignJob.UseVisualStyleBackColor = False
        '
        'cmbTechnician
        '
        Me.cmbTechnician.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbTechnician.FormattingEnabled = True
        Me.cmbTechnician.Location = New System.Drawing.Point(17, 45)
        Me.cmbTechnician.Name = "cmbTechnician"
        Me.cmbTechnician.Size = New System.Drawing.Size(371, 28)
        Me.cmbTechnician.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(13, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 20)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Assign Technician:"
        '
        'btnPrevMonth
        '
        Me.btnPrevMonth.Location = New System.Drawing.Point(25, 22)
        Me.btnPrevMonth.Name = "btnPrevMonth"
        Me.btnPrevMonth.Size = New System.Drawing.Size(89, 38)
        Me.btnPrevMonth.TabIndex = 6
        Me.btnPrevMonth.Text = "<"
        Me.btnPrevMonth.UseVisualStyleBackColor = True
        '
        'btnNextMonth
        '
        Me.btnNextMonth.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextMonth.Location = New System.Drawing.Point(516, 22)
        Me.btnNextMonth.Name = "btnNextMonth"
        Me.btnNextMonth.Size = New System.Drawing.Size(92, 38)
        Me.btnNextMonth.TabIndex = 7
        Me.btnNextMonth.Text = ">"
        Me.btnNextMonth.UseVisualStyleBackColor = True
        '
        'lblMonthYear1
        '
        Me.lblMonthYear1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.lblMonthYear1.AutoSize = True
        Me.lblMonthYear1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthYear1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lblMonthYear1.Location = New System.Drawing.Point(222, 27)
        Me.lblMonthYear1.Name = "lblMonthYear1"
        Me.lblMonthYear1.Size = New System.Drawing.Size(172, 25)
        Me.lblMonthYear1.TabIndex = 8
        Me.lblMonthYear1.Text = "OCTOBER 2025"
        '
        'flpCalendar1
        '
        Me.flpCalendar1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpCalendar1.BackColor = System.Drawing.Color.White
        Me.flpCalendar1.Location = New System.Drawing.Point(0, 1)
        Me.flpCalendar1.Name = "flpCalendar1"
        Me.flpCalendar1.Size = New System.Drawing.Size(625, 277)
        Me.flpCalendar1.TabIndex = 9
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grpJobDetails)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgvDailyJobs)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnRefresh)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.dtpViewDate)
        Me.SplitContainer1.Size = New System.Drawing.Size(1074, 780)
        Me.SplitContainer1.SplitterDistance = 625
        Me.SplitContainer1.TabIndex = 10
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblMonthYear2)
        Me.Panel2.Controls.Add(Me.flpCalendar2)
        Me.Panel2.Controls.Add(Me.flpCalendar1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 72)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(625, 708)
        Me.Panel2.TabIndex = 11
        '
        'lblMonthYear2
        '
        Me.lblMonthYear2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblMonthYear2.AutoSize = True
        Me.lblMonthYear2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthYear2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(116, Byte), Integer), CType(CType(139, Byte), Integer))
        Me.lblMonthYear2.Location = New System.Drawing.Point(222, 301)
        Me.lblMonthYear2.Name = "lblMonthYear2"
        Me.lblMonthYear2.Size = New System.Drawing.Size(172, 25)
        Me.lblMonthYear2.TabIndex = 9
        Me.lblMonthYear2.Text = "OCTOBER 2025"
        '
        'flpCalendar2
        '
        Me.flpCalendar2.BackColor = System.Drawing.Color.White
        Me.flpCalendar2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.flpCalendar2.Location = New System.Drawing.Point(0, 343)
        Me.flpCalendar2.Name = "flpCalendar2"
        Me.flpCalendar2.Size = New System.Drawing.Size(625, 365)
        Me.flpCalendar2.TabIndex = 10
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lblMonthYear1)
        Me.Panel1.Controls.Add(Me.btnNextMonth)
        Me.Panel1.Controls.Add(Me.btnPrevMonth)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(625, 72)
        Me.Panel1.TabIndex = 10
        '
        'grpJobDetails
        '
        Me.grpJobDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpJobDetails.Controls.Add(Me.lblDetailDuration)
        Me.grpJobDetails.Controls.Add(Me.lblDetailEnd)
        Me.grpJobDetails.Controls.Add(Me.lblDetailStart)
        Me.grpJobDetails.Controls.Add(Me.Label11)
        Me.grpJobDetails.Controls.Add(Me.Label10)
        Me.grpJobDetails.Controls.Add(Me.Label9)
        Me.grpJobDetails.Controls.Add(Me.lblDetailVisit)
        Me.grpJobDetails.Controls.Add(Me.Label8)
        Me.grpJobDetails.Controls.Add(Me.lblDetailTech)
        Me.grpJobDetails.Controls.Add(Me.Label7)
        Me.grpJobDetails.Controls.Add(Me.lblDetailService)
        Me.grpJobDetails.Controls.Add(Me.Label6)
        Me.grpJobDetails.Controls.Add(Me.lblDetailAddress)
        Me.grpJobDetails.Controls.Add(Me.Label5)
        Me.grpJobDetails.Controls.Add(Me.lblDetailClient)
        Me.grpJobDetails.Controls.Add(Me.Label4)
        Me.grpJobDetails.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.grpJobDetails.Location = New System.Drawing.Point(19, 356)
        Me.grpJobDetails.Name = "grpJobDetails"
        Me.grpJobDetails.Size = New System.Drawing.Size(405, 241)
        Me.grpJobDetails.TabIndex = 6
        Me.grpJobDetails.TabStop = False
        Me.grpJobDetails.Text = "Job Details"
        '
        'lblDetailDuration
        '
        Me.lblDetailDuration.AutoSize = True
        Me.lblDetailDuration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblDetailDuration.Location = New System.Drawing.Point(121, 214)
        Me.lblDetailDuration.Name = "lblDetailDuration"
        Me.lblDetailDuration.Size = New System.Drawing.Size(21, 20)
        Me.lblDetailDuration.TabIndex = 15
        Me.lblDetailDuration.Text = "..."
        '
        'lblDetailEnd
        '
        Me.lblDetailEnd.AutoSize = True
        Me.lblDetailEnd.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblDetailEnd.Location = New System.Drawing.Point(121, 189)
        Me.lblDetailEnd.Name = "lblDetailEnd"
        Me.lblDetailEnd.Size = New System.Drawing.Size(21, 20)
        Me.lblDetailEnd.TabIndex = 14
        Me.lblDetailEnd.Text = "..."
        '
        'lblDetailStart
        '
        Me.lblDetailStart.AutoSize = True
        Me.lblDetailStart.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblDetailStart.Location = New System.Drawing.Point(121, 169)
        Me.lblDetailStart.Name = "lblDetailStart"
        Me.lblDetailStart.Size = New System.Drawing.Size(21, 20)
        Me.lblDetailStart.TabIndex = 13
        Me.lblDetailStart.Text = "..."
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(25, 214)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 20)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "Duration:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(25, 189)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(42, 20)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "End:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(21, 169)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(48, 20)
        Me.Label9.TabIndex = 10
        Me.Label9.Text = "Start:"
        '
        'lblDetailVisit
        '
        Me.lblDetailVisit.AutoSize = True
        Me.lblDetailVisit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailVisit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblDetailVisit.Location = New System.Drawing.Point(121, 137)
        Me.lblDetailVisit.Name = "lblDetailVisit"
        Me.lblDetailVisit.Size = New System.Drawing.Size(24, 20)
        Me.lblDetailVisit.TabIndex = 9
        Me.lblDetailVisit.Text = "..."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(17, 137)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(56, 20)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Visit #:"
        '
        'lblDetailTech
        '
        Me.lblDetailTech.AutoSize = True
        Me.lblDetailTech.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailTech.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblDetailTech.Location = New System.Drawing.Point(121, 107)
        Me.lblDetailTech.Name = "lblDetailTech"
        Me.lblDetailTech.Size = New System.Drawing.Size(24, 20)
        Me.lblDetailTech.TabIndex = 7
        Me.lblDetailTech.Text = "..."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(17, 107)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(89, 20)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Technician:"
        '
        'lblDetailService
        '
        Me.lblDetailService.AutoSize = True
        Me.lblDetailService.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailService.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblDetailService.Location = New System.Drawing.Point(121, 79)
        Me.lblDetailService.Name = "lblDetailService"
        Me.lblDetailService.Size = New System.Drawing.Size(24, 20)
        Me.lblDetailService.TabIndex = 5
        Me.lblDetailService.Text = "..."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(17, 79)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 20)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Service:"
        '
        'lblDetailAddress
        '
        Me.lblDetailAddress.AutoSize = True
        Me.lblDetailAddress.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailAddress.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblDetailAddress.Location = New System.Drawing.Point(121, 50)
        Me.lblDetailAddress.Name = "lblDetailAddress"
        Me.lblDetailAddress.Size = New System.Drawing.Size(24, 20)
        Me.lblDetailAddress.TabIndex = 3
        Me.lblDetailAddress.Text = "..."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(17, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 20)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Address:"
        '
        'lblDetailClient
        '
        Me.lblDetailClient.AutoSize = True
        Me.lblDetailClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetailClient.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.lblDetailClient.Location = New System.Drawing.Point(121, 26)
        Me.lblDetailClient.Name = "lblDetailClient"
        Me.lblDetailClient.Size = New System.Drawing.Size(24, 20)
        Me.lblDetailClient.TabIndex = 1
        Me.lblDetailClient.Text = "..."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(65, Byte), Integer), CType(CType(85, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(17, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 20)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Client Name:"
        '
        'newUcDashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "newUcDashboard"
        Me.Size = New System.Drawing.Size(1074, 780)
        CType(Me.dgvDailyJobs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.grpJobDetails.ResumeLayout(False)
        Me.grpJobDetails.PerformLayout()
        Me.ResumeLayout(False)

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
    Friend WithEvents btnPrevMonth As Button
    Friend WithEvents btnNextMonth As Button
    Friend WithEvents lblMonthYear1 As Label
    Friend WithEvents flpCalendar1 As FlowLayoutPanel
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents grpJobDetails As GroupBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lblDetailAddress As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblDetailClient As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblDetailVisit As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents lblDetailTech As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents lblDetailService As Label
    Friend WithEvents flpCalendar2 As FlowLayoutPanel
    Friend WithEvents lblMonthYear2 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents lblDetailStart As Label
    Friend WithEvents lblDetailDuration As Label
    Friend WithEvents lblDetailEnd As Label
End Class
