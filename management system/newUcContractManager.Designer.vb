<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newUcContractManager
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgvContracts = New System.Windows.Forms.DataGridView()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.chkDateFilter = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboSortOrder = New System.Windows.Forms.ComboBox()
        Me.cboServiceFilter = New System.Windows.Forms.ComboBox()
        Me.btnNewContract = New System.Windows.Forms.Button()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.tabDetails = New System.Windows.Forms.TabControl()
        Me.tpOverview = New System.Windows.Forms.TabPage()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.lblService = New System.Windows.Forms.Label()
        Me.lblTotalAmount = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.lblClientName = New System.Windows.Forms.Label()
        Me.tpSchedule = New System.Windows.Forms.TabPage()
        Me.dgvSchedule = New System.Windows.Forms.DataGridView()
        Me.tpJobs = New System.Windows.Forms.TabPage()
        Me.dgvJobHistory = New System.Windows.Forms.DataGridView()
        Me.tpPayments = New System.Windows.Forms.TabPage()
        Me.dgvPayments = New System.Windows.Forms.DataGridView()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgvContracts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.tabDetails.SuspendLayout()
        Me.tpOverview.SuspendLayout()
        Me.tpSchedule.SuspendLayout()
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpJobs.SuspendLayout()
        CType(Me.dgvJobHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpPayments.SuspendLayout()
        CType(Me.dgvPayments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.dgvContracts)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tabDetails)
        Me.SplitContainer1.Size = New System.Drawing.Size(984, 732)
        Me.SplitContainer1.SplitterDistance = 443
        Me.SplitContainer1.TabIndex = 0
        '
        'dgvContracts
        '
        Me.dgvContracts.AllowUserToAddRows = False
        Me.dgvContracts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvContracts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvContracts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvContracts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvContracts.Location = New System.Drawing.Point(0, 131)
        Me.dgvContracts.Name = "dgvContracts"
        Me.dgvContracts.ReadOnly = True
        Me.dgvContracts.RowHeadersVisible = False
        Me.dgvContracts.RowHeadersWidth = 62
        Me.dgvContracts.RowTemplate.Height = 28
        Me.dgvContracts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvContracts.Size = New System.Drawing.Size(984, 312)
        Me.dgvContracts.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.dtpTo)
        Me.Panel1.Controls.Add(Me.dtpFrom)
        Me.Panel1.Controls.Add(Me.chkDateFilter)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.cboSortOrder)
        Me.Panel1.Controls.Add(Me.cboServiceFilter)
        Me.Panel1.Controls.Add(Me.btnNewContract)
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.txtSearch)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(984, 131)
        Me.Panel1.TabIndex = 0
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(719, 97)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 20)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "To"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(719, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 20)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "From"
        '
        'dtpTo
        '
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpTo.Location = New System.Drawing.Point(771, 61)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(200, 26)
        Me.dtpTo.TabIndex = 9
        '
        'dtpFrom
        '
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFrom.Location = New System.Drawing.Point(771, 94)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(200, 26)
        Me.dtpFrom.TabIndex = 8
        '
        'chkDateFilter
        '
        Me.chkDateFilter.AutoSize = True
        Me.chkDateFilter.Location = New System.Drawing.Point(584, 99)
        Me.chkDateFilter.Name = "chkDateFilter"
        Me.chkDateFilter.Size = New System.Drawing.Size(129, 24)
        Me.chkDateFilter.TabIndex = 7
        Me.chkDateFilter.Text = "Filter by Date"
        Me.chkDateFilter.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(250, 101)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Filter by Service"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(0, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Sort Order"
        '
        'cboSortOrder
        '
        Me.cboSortOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortOrder.FormattingEnabled = True
        Me.cboSortOrder.Items.AddRange(New Object() {"Newest Contracts First", "Oldest Contracts First", "Client Name (A-Z)"})
        Me.cboSortOrder.Location = New System.Drawing.Point(89, 97)
        Me.cboSortOrder.Name = "cboSortOrder"
        Me.cboSortOrder.Size = New System.Drawing.Size(155, 28)
        Me.cboSortOrder.TabIndex = 4
        '
        'cboServiceFilter
        '
        Me.cboServiceFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServiceFilter.FormattingEnabled = True
        Me.cboServiceFilter.Location = New System.Drawing.Point(376, 95)
        Me.cboServiceFilter.Name = "cboServiceFilter"
        Me.cboServiceFilter.Size = New System.Drawing.Size(202, 28)
        Me.cboServiceFilter.TabIndex = 3
        '
        'btnNewContract
        '
        Me.btnNewContract.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNewContract.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnNewContract.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNewContract.ForeColor = System.Drawing.Color.White
        Me.btnNewContract.Location = New System.Drawing.Point(784, 8)
        Me.btnNewContract.Name = "btnNewContract"
        Me.btnNewContract.Size = New System.Drawing.Size(187, 47)
        Me.btnNewContract.TabIndex = 2
        Me.btnNewContract.Text = "Create New Contract"
        Me.btnNewContract.UseVisualStyleBackColor = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(241, 9)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 35)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(4, 13)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(221, 26)
        Me.txtSearch.TabIndex = 0
        '
        'tabDetails
        '
        Me.tabDetails.Controls.Add(Me.tpOverview)
        Me.tabDetails.Controls.Add(Me.tpSchedule)
        Me.tabDetails.Controls.Add(Me.tpJobs)
        Me.tabDetails.Controls.Add(Me.tpPayments)
        Me.tabDetails.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tabDetails.Location = New System.Drawing.Point(0, 0)
        Me.tabDetails.Name = "tabDetails"
        Me.tabDetails.SelectedIndex = 0
        Me.tabDetails.Size = New System.Drawing.Size(984, 285)
        Me.tabDetails.TabIndex = 0
        '
        'tpOverview
        '
        Me.tpOverview.Controls.Add(Me.lblStatus)
        Me.tpOverview.Controls.Add(Me.lblService)
        Me.tpOverview.Controls.Add(Me.lblTotalAmount)
        Me.tpOverview.Controls.Add(Me.lblBalance)
        Me.tpOverview.Controls.Add(Me.lblClientName)
        Me.tpOverview.Location = New System.Drawing.Point(4, 29)
        Me.tpOverview.Name = "tpOverview"
        Me.tpOverview.Padding = New System.Windows.Forms.Padding(3)
        Me.tpOverview.Size = New System.Drawing.Size(976, 252)
        Me.tpOverview.TabIndex = 0
        Me.tpOverview.Text = "Overview"
        Me.tpOverview.UseVisualStyleBackColor = True
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(20, 152)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(0, 25)
        Me.lblStatus.TabIndex = 5
        '
        'lblService
        '
        Me.lblService.AutoSize = True
        Me.lblService.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblService.Location = New System.Drawing.Point(20, 46)
        Me.lblService.Name = "lblService"
        Me.lblService.Size = New System.Drawing.Size(0, 25)
        Me.lblService.TabIndex = 4
        '
        'lblTotalAmount
        '
        Me.lblTotalAmount.AutoSize = True
        Me.lblTotalAmount.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAmount.Location = New System.Drawing.Point(20, 80)
        Me.lblTotalAmount.Name = "lblTotalAmount"
        Me.lblTotalAmount.Size = New System.Drawing.Size(0, 25)
        Me.lblTotalAmount.TabIndex = 2
        '
        'lblBalance
        '
        Me.lblBalance.AutoSize = True
        Me.lblBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalance.Location = New System.Drawing.Point(20, 116)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(0, 25)
        Me.lblBalance.TabIndex = 1
        '
        'lblClientName
        '
        Me.lblClientName.AutoSize = True
        Me.lblClientName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClientName.Location = New System.Drawing.Point(20, 17)
        Me.lblClientName.Name = "lblClientName"
        Me.lblClientName.Size = New System.Drawing.Size(0, 25)
        Me.lblClientName.TabIndex = 0
        '
        'tpSchedule
        '
        Me.tpSchedule.Controls.Add(Me.dgvSchedule)
        Me.tpSchedule.Location = New System.Drawing.Point(4, 29)
        Me.tpSchedule.Name = "tpSchedule"
        Me.tpSchedule.Padding = New System.Windows.Forms.Padding(3)
        Me.tpSchedule.Size = New System.Drawing.Size(976, 252)
        Me.tpSchedule.TabIndex = 1
        Me.tpSchedule.Text = "Payment Schedule"
        Me.tpSchedule.UseVisualStyleBackColor = True
        '
        'dgvSchedule
        '
        Me.dgvSchedule.AllowUserToAddRows = False
        Me.dgvSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSchedule.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSchedule.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvSchedule.Location = New System.Drawing.Point(3, 3)
        Me.dgvSchedule.Name = "dgvSchedule"
        Me.dgvSchedule.RowHeadersWidth = 62
        Me.dgvSchedule.RowTemplate.Height = 28
        Me.dgvSchedule.Size = New System.Drawing.Size(970, 246)
        Me.dgvSchedule.TabIndex = 0
        '
        'tpJobs
        '
        Me.tpJobs.Controls.Add(Me.dgvJobHistory)
        Me.tpJobs.Location = New System.Drawing.Point(4, 29)
        Me.tpJobs.Name = "tpJobs"
        Me.tpJobs.Padding = New System.Windows.Forms.Padding(3)
        Me.tpJobs.Size = New System.Drawing.Size(976, 252)
        Me.tpJobs.TabIndex = 2
        Me.tpJobs.Text = "Job Visits"
        Me.tpJobs.UseVisualStyleBackColor = True
        '
        'dgvJobHistory
        '
        Me.dgvJobHistory.AllowUserToAddRows = False
        Me.dgvJobHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvJobHistory.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvJobHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvJobHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvJobHistory.Location = New System.Drawing.Point(3, 3)
        Me.dgvJobHistory.Name = "dgvJobHistory"
        Me.dgvJobHistory.ReadOnly = True
        Me.dgvJobHistory.RowHeadersWidth = 62
        Me.dgvJobHistory.RowTemplate.Height = 28
        Me.dgvJobHistory.Size = New System.Drawing.Size(970, 246)
        Me.dgvJobHistory.TabIndex = 0
        '
        'tpPayments
        '
        Me.tpPayments.Controls.Add(Me.dgvPayments)
        Me.tpPayments.Location = New System.Drawing.Point(4, 29)
        Me.tpPayments.Name = "tpPayments"
        Me.tpPayments.Padding = New System.Windows.Forms.Padding(3)
        Me.tpPayments.Size = New System.Drawing.Size(976, 252)
        Me.tpPayments.TabIndex = 3
        Me.tpPayments.Text = "Payments Made"
        Me.tpPayments.UseVisualStyleBackColor = True
        '
        'dgvPayments
        '
        Me.dgvPayments.AllowUserToAddRows = False
        Me.dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPayments.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPayments.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPayments.Location = New System.Drawing.Point(3, 3)
        Me.dgvPayments.Name = "dgvPayments"
        Me.dgvPayments.RowHeadersWidth = 62
        Me.dgvPayments.RowTemplate.Height = 28
        Me.dgvPayments.Size = New System.Drawing.Size(970, 246)
        Me.dgvPayments.TabIndex = 0
        '
        'newUcContractManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "newUcContractManager"
        Me.Size = New System.Drawing.Size(984, 732)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgvContracts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.tabDetails.ResumeLayout(False)
        Me.tpOverview.ResumeLayout(False)
        Me.tpOverview.PerformLayout()
        Me.tpSchedule.ResumeLayout(False)
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpJobs.ResumeLayout(False)
        CType(Me.dgvJobHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpPayments.ResumeLayout(False)
        CType(Me.dgvPayments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents dgvContracts As DataGridView
    Friend WithEvents Panel1 As Panel
    Friend WithEvents btnSearch As Button
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents tabDetails As TabControl
    Friend WithEvents tpOverview As TabPage
    Friend WithEvents tpSchedule As TabPage
    Friend WithEvents tpJobs As TabPage
    Friend WithEvents tpPayments As TabPage
    Friend WithEvents lblTotalAmount As Label
    Friend WithEvents lblBalance As Label
    Friend WithEvents lblClientName As Label
    Friend WithEvents dgvSchedule As DataGridView
    Friend WithEvents dgvJobHistory As DataGridView
    Friend WithEvents dgvPayments As DataGridView
    Friend WithEvents btnNewContract As Button
    Friend WithEvents lblStatus As Label
    Friend WithEvents lblService As Label
    Friend WithEvents cboSortOrder As ComboBox
    Friend WithEvents cboServiceFilter As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents dtpTo As DateTimePicker
    Friend WithEvents dtpFrom As DateTimePicker
    Friend WithEvents chkDateFilter As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
End Class
