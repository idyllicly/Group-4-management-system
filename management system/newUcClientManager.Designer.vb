<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newUcClientManager
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.dgvClientList = New System.Windows.Forms.DataGridView()
        Me.btnCreateInquiry = New System.Windows.Forms.Button()
        Me.btnEditClient = New System.Windows.Forms.Button()
        Me.btnAddClient = New System.Windows.Forms.Button()
        Me.pnlAllInfo = New System.Windows.Forms.GroupBox()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblFullName = New System.Windows.Forms.Label()
        Me.lblFullAddress = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblContactInfo = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblStats = New System.Windows.Forms.Label()
        Me.lblRecentActivity = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnGoContracts = New System.Windows.Forms.Button()
        Me.btnGoBilling = New System.Windows.Forms.Button()
        Me.btnGoOculars = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        CType(Me.dgvClientList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAllInfo.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(57, 39)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(249, 25)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "CLIENT MANAGEMENT"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(62, 112)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(217, 26)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Text = "Search Name..."
        '
        'dgvClientList
        '
        Me.dgvClientList.AllowUserToAddRows = False
        Me.dgvClientList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvClientList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvClientList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvClientList.BackgroundColor = System.Drawing.Color.Teal
        Me.dgvClientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvClientList.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvClientList.Location = New System.Drawing.Point(62, 144)
        Me.dgvClientList.Name = "dgvClientList"
        Me.dgvClientList.ReadOnly = True
        Me.dgvClientList.RowHeadersVisible = False
        Me.dgvClientList.RowHeadersWidth = 62
        Me.dgvClientList.RowTemplate.Height = 28
        Me.dgvClientList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvClientList.Size = New System.Drawing.Size(1030, 445)
        Me.dgvClientList.TabIndex = 2
        '
        'btnCreateInquiry
        '
        Me.btnCreateInquiry.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCreateInquiry.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnCreateInquiry.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCreateInquiry.ForeColor = System.Drawing.Color.White
        Me.btnCreateInquiry.Location = New System.Drawing.Point(500, 631)
        Me.btnCreateInquiry.Name = "btnCreateInquiry"
        Me.btnCreateInquiry.Size = New System.Drawing.Size(167, 43)
        Me.btnCreateInquiry.TabIndex = 10
        Me.btnCreateInquiry.Text = "NEW INQUIRY"
        Me.btnCreateInquiry.UseVisualStyleBackColor = False
        '
        'btnEditClient
        '
        Me.btnEditClient.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEditClient.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnEditClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEditClient.ForeColor = System.Drawing.Color.White
        Me.btnEditClient.Location = New System.Drawing.Point(500, 691)
        Me.btnEditClient.Name = "btnEditClient"
        Me.btnEditClient.Size = New System.Drawing.Size(167, 43)
        Me.btnEditClient.TabIndex = 9
        Me.btnEditClient.Text = "EDIT"
        Me.btnEditClient.UseVisualStyleBackColor = False
        '
        'btnAddClient
        '
        Me.btnAddClient.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddClient.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnAddClient.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddClient.ForeColor = System.Drawing.Color.White
        Me.btnAddClient.Location = New System.Drawing.Point(898, 631)
        Me.btnAddClient.Name = "btnAddClient"
        Me.btnAddClient.Size = New System.Drawing.Size(132, 43)
        Me.btnAddClient.TabIndex = 8
        Me.btnAddClient.Text = "CREATE"
        Me.btnAddClient.UseVisualStyleBackColor = False
        '
        'pnlAllInfo
        '
        Me.pnlAllInfo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlAllInfo.Controls.Add(Me.FlowLayoutPanel1)
        Me.pnlAllInfo.Location = New System.Drawing.Point(62, 606)
        Me.pnlAllInfo.Name = "pnlAllInfo"
        Me.pnlAllInfo.Size = New System.Drawing.Size(432, 136)
        Me.pnlAllInfo.TabIndex = 7
        Me.pnlAllInfo.TabStop = False
        Me.pnlAllInfo.Text = "Client Profile"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.Label2)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblFullName)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblFullAddress)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label1)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblContactInfo)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblEmail)
        Me.FlowLayoutPanel1.Controls.Add(Me.Label3)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblStats)
        Me.FlowLayoutPanel1.Controls.Add(Me.lblRecentActivity)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(3, 22)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(426, 111)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "NAME:"
        '
        'lblFullName
        '
        Me.lblFullName.AutoSize = True
        Me.lblFullName.Location = New System.Drawing.Point(0, 23)
        Me.lblFullName.Margin = New System.Windows.Forms.Padding(0, 3, 0, 10)
        Me.lblFullName.Name = "lblFullName"
        Me.lblFullName.Size = New System.Drawing.Size(78, 20)
        Me.lblFullName.TabIndex = 0
        Me.lblFullName.Text = "Full name"
        '
        'lblFullAddress
        '
        Me.lblFullAddress.AutoSize = True
        Me.lblFullAddress.Location = New System.Drawing.Point(0, 56)
        Me.lblFullAddress.Margin = New System.Windows.Forms.Padding(0, 3, 0, 10)
        Me.lblFullAddress.Name = "lblFullAddress"
        Me.lblFullAddress.Size = New System.Drawing.Size(68, 20)
        Me.lblFullAddress.TabIndex = 1
        Me.lblFullAddress.Text = "Address"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 86)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 20)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "CONTACTS"
        '
        'lblContactInfo
        '
        Me.lblContactInfo.AutoSize = True
        Me.lblContactInfo.Location = New System.Drawing.Point(108, 3)
        Me.lblContactInfo.Margin = New System.Windows.Forms.Padding(0, 3, 0, 10)
        Me.lblContactInfo.Name = "lblContactInfo"
        Me.lblContactInfo.Size = New System.Drawing.Size(119, 20)
        Me.lblContactInfo.TabIndex = 2
        Me.lblContactInfo.Text = "Contact Person"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(108, 36)
        Me.lblEmail.Margin = New System.Windows.Forms.Padding(0, 3, 0, 10)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(137, 20)
        Me.lblEmail.TabIndex = 3
        Me.lblEmail.Text = "Email and number"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(111, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 20)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "----------------"
        '
        'lblStats
        '
        Me.lblStats.AutoSize = True
        Me.lblStats.Location = New System.Drawing.Point(245, 3)
        Me.lblStats.Margin = New System.Windows.Forms.Padding(0, 3, 0, 10)
        Me.lblStats.Name = "lblStats"
        Me.lblStats.Size = New System.Drawing.Size(47, 20)
        Me.lblStats.TabIndex = 4
        Me.lblStats.Text = "Stats"
        '
        'lblRecentActivity
        '
        Me.lblRecentActivity.AutoSize = True
        Me.lblRecentActivity.Location = New System.Drawing.Point(245, 36)
        Me.lblRecentActivity.Margin = New System.Windows.Forms.Padding(0, 3, 0, 10)
        Me.lblRecentActivity.Name = "lblRecentActivity"
        Me.lblRecentActivity.Size = New System.Drawing.Size(71, 20)
        Me.lblRecentActivity.TabIndex = 5
        Me.lblRecentActivity.Text = "Activities"
        '
        'btnRefresh
        '
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(285, 110)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(132, 30)
        Me.btnRefresh.TabIndex = 11
        Me.btnRefresh.Text = "REFRESH"
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'btnGoContracts
        '
        Me.btnGoContracts.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGoContracts.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnGoContracts.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoContracts.ForeColor = System.Drawing.Color.White
        Me.btnGoContracts.Location = New System.Drawing.Point(898, 691)
        Me.btnGoContracts.Name = "btnGoContracts"
        Me.btnGoContracts.Size = New System.Drawing.Size(167, 43)
        Me.btnGoContracts.TabIndex = 12
        Me.btnGoContracts.Text = "View Contract"
        Me.btnGoContracts.UseVisualStyleBackColor = False
        '
        'btnGoBilling
        '
        Me.btnGoBilling.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGoBilling.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnGoBilling.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoBilling.ForeColor = System.Drawing.Color.White
        Me.btnGoBilling.Location = New System.Drawing.Point(703, 631)
        Me.btnGoBilling.Name = "btnGoBilling"
        Me.btnGoBilling.Size = New System.Drawing.Size(167, 43)
        Me.btnGoBilling.TabIndex = 13
        Me.btnGoBilling.Text = "Go to Billing"
        Me.btnGoBilling.UseVisualStyleBackColor = False
        '
        'btnGoOculars
        '
        Me.btnGoOculars.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGoOculars.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnGoOculars.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGoOculars.ForeColor = System.Drawing.Color.White
        Me.btnGoOculars.Location = New System.Drawing.Point(703, 691)
        Me.btnGoOculars.Name = "btnGoOculars"
        Me.btnGoOculars.Size = New System.Drawing.Size(167, 45)
        Me.btnGoOculars.TabIndex = 14
        Me.btnGoOculars.Text = "View Oculars/Quotes"
        Me.btnGoOculars.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1026, 631)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 40)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "+"
        '
        'newUcClientManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnGoOculars)
        Me.Controls.Add(Me.btnGoBilling)
        Me.Controls.Add(Me.btnGoContracts)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.btnCreateInquiry)
        Me.Controls.Add(Me.btnEditClient)
        Me.Controls.Add(Me.btnAddClient)
        Me.Controls.Add(Me.pnlAllInfo)
        Me.Controls.Add(Me.dgvClientList)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "newUcClientManager"
        Me.Size = New System.Drawing.Size(1165, 766)
        CType(Me.dgvClientList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlAllInfo.ResumeLayout(False)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.FlowLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dgvClientList As DataGridView
    Friend WithEvents btnCreateInquiry As Button
    Friend WithEvents btnEditClient As Button
    Friend WithEvents btnAddClient As Button
    Friend WithEvents pnlAllInfo As GroupBox
    Friend WithEvents lblRecentActivity As Label
    Friend WithEvents lblStats As Label
    Friend WithEvents lblEmail As Label
    Friend WithEvents lblContactInfo As Label
    Friend WithEvents lblFullAddress As Label
    Friend WithEvents lblFullName As Label
    Friend WithEvents btnRefresh As Button
    Friend WithEvents FlowLayoutPanel1 As FlowLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents btnGoContracts As Button
    Friend WithEvents btnGoBilling As Button
    Friend WithEvents btnGoOculars As Button
    Friend WithEvents Label4 As Label
End Class
