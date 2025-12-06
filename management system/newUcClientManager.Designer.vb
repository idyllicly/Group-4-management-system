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
        Me.lblRecentActivity = New System.Windows.Forms.Label()
        Me.lblStats = New System.Windows.Forms.Label()
        Me.lblEmail = New System.Windows.Forms.Label()
        Me.lblContactInfo = New System.Windows.Forms.Label()
        Me.lblFullAddress = New System.Windows.Forms.Label()
        Me.lblFullName = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        CType(Me.dgvClientList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlAllInfo.SuspendLayout()
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
        Me.txtSearch.Location = New System.Drawing.Point(62, 91)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(217, 26)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Text = "Search Name..."
        '
        'dgvClientList
        '
        Me.dgvClientList.AllowUserToAddRows = False
        Me.dgvClientList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvClientList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvClientList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
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
        Me.dgvClientList.RowHeadersWidth = 62
        Me.dgvClientList.RowTemplate.Height = 28
        Me.dgvClientList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvClientList.Size = New System.Drawing.Size(1030, 251)
        Me.dgvClientList.TabIndex = 2
        '
        'btnCreateInquiry
        '
        Me.btnCreateInquiry.Location = New System.Drawing.Point(919, 646)
        Me.btnCreateInquiry.Name = "btnCreateInquiry"
        Me.btnCreateInquiry.Size = New System.Drawing.Size(132, 51)
        Me.btnCreateInquiry.TabIndex = 10
        Me.btnCreateInquiry.Text = "NEW INQUIRY"
        Me.btnCreateInquiry.UseVisualStyleBackColor = True
        '
        'btnEditClient
        '
        Me.btnEditClient.Location = New System.Drawing.Point(919, 570)
        Me.btnEditClient.Name = "btnEditClient"
        Me.btnEditClient.Size = New System.Drawing.Size(132, 72)
        Me.btnEditClient.TabIndex = 9
        Me.btnEditClient.Text = "EDIT"
        Me.btnEditClient.UseVisualStyleBackColor = True
        '
        'btnAddClient
        '
        Me.btnAddClient.Location = New System.Drawing.Point(919, 498)
        Me.btnAddClient.Name = "btnAddClient"
        Me.btnAddClient.Size = New System.Drawing.Size(132, 72)
        Me.btnAddClient.TabIndex = 8
        Me.btnAddClient.Text = "CREATE"
        Me.btnAddClient.UseVisualStyleBackColor = True
        '
        'pnlAllInfo
        '
        Me.pnlAllInfo.Controls.Add(Me.lblRecentActivity)
        Me.pnlAllInfo.Controls.Add(Me.lblStats)
        Me.pnlAllInfo.Controls.Add(Me.lblEmail)
        Me.pnlAllInfo.Controls.Add(Me.lblContactInfo)
        Me.pnlAllInfo.Controls.Add(Me.lblFullAddress)
        Me.pnlAllInfo.Controls.Add(Me.lblFullName)
        Me.pnlAllInfo.Location = New System.Drawing.Point(82, 480)
        Me.pnlAllInfo.Name = "pnlAllInfo"
        Me.pnlAllInfo.Size = New System.Drawing.Size(764, 229)
        Me.pnlAllInfo.TabIndex = 7
        Me.pnlAllInfo.TabStop = False
        Me.pnlAllInfo.Text = "All info"
        '
        'lblRecentActivity
        '
        Me.lblRecentActivity.AutoSize = True
        Me.lblRecentActivity.Location = New System.Drawing.Point(7, 130)
        Me.lblRecentActivity.Name = "lblRecentActivity"
        Me.lblRecentActivity.Size = New System.Drawing.Size(57, 20)
        Me.lblRecentActivity.TabIndex = 5
        Me.lblRecentActivity.Text = "Label7"
        '
        'lblStats
        '
        Me.lblStats.AutoSize = True
        Me.lblStats.Location = New System.Drawing.Point(7, 110)
        Me.lblStats.Name = "lblStats"
        Me.lblStats.Size = New System.Drawing.Size(57, 20)
        Me.lblStats.TabIndex = 4
        Me.lblStats.Text = "Label6"
        '
        'lblEmail
        '
        Me.lblEmail.AutoSize = True
        Me.lblEmail.Location = New System.Drawing.Point(7, 90)
        Me.lblEmail.Name = "lblEmail"
        Me.lblEmail.Size = New System.Drawing.Size(57, 20)
        Me.lblEmail.TabIndex = 3
        Me.lblEmail.Text = "Label5"
        '
        'lblContactInfo
        '
        Me.lblContactInfo.AutoSize = True
        Me.lblContactInfo.Location = New System.Drawing.Point(7, 70)
        Me.lblContactInfo.Name = "lblContactInfo"
        Me.lblContactInfo.Size = New System.Drawing.Size(57, 20)
        Me.lblContactInfo.TabIndex = 2
        Me.lblContactInfo.Text = "Label4"
        '
        'lblFullAddress
        '
        Me.lblFullAddress.AutoSize = True
        Me.lblFullAddress.Location = New System.Drawing.Point(7, 50)
        Me.lblFullAddress.Name = "lblFullAddress"
        Me.lblFullAddress.Size = New System.Drawing.Size(57, 20)
        Me.lblFullAddress.TabIndex = 1
        Me.lblFullAddress.Text = "Label3"
        '
        'lblFullName
        '
        Me.lblFullName.AutoSize = True
        Me.lblFullName.Location = New System.Drawing.Point(7, 26)
        Me.lblFullName.Name = "lblFullName"
        Me.lblFullName.Size = New System.Drawing.Size(57, 20)
        Me.lblFullName.TabIndex = 0
        Me.lblFullName.Text = "Label2"
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(960, 108)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(132, 30)
        Me.btnRefresh.TabIndex = 11
        Me.btnRefresh.Text = "REFRESH"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'newUcClientManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
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
        Me.pnlAllInfo.PerformLayout()
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
End Class
