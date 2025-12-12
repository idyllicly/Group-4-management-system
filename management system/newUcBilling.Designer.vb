<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newUcBilling
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
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.txtSearchBilling = New System.Windows.Forms.TextBox()
        Me.dgvBilling = New System.Windows.Forms.DataGridView()
        Me.grpPayment = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dgvSchedule = New System.Windows.Forms.DataGridView()
        Me.btnSavePayment = New System.Windows.Forms.Button()
        Me.txtPayAmount = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblContractInfo = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.dgvBilling, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPayment.SuspendLayout()
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.Location = New System.Drawing.Point(29, 18)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(189, 25)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Billing & Collections"
        '
        'txtSearchBilling
        '
        Me.txtSearchBilling.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.txtSearchBilling.Location = New System.Drawing.Point(34, 58)
        Me.txtSearchBilling.Name = "txtSearchBilling"
        Me.txtSearchBilling.Size = New System.Drawing.Size(368, 26)
        Me.txtSearchBilling.TabIndex = 1
        Me.txtSearchBilling.Text = "Search Client..."
        '
        'dgvBilling
        '
        Me.dgvBilling.AllowUserToAddRows = False
        Me.dgvBilling.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBilling.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvBilling.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvBilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvBilling.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvBilling.Location = New System.Drawing.Point(34, 102)
        Me.dgvBilling.Name = "dgvBilling"
        Me.dgvBilling.ReadOnly = True
        Me.dgvBilling.RowHeadersVisible = False
        Me.dgvBilling.RowHeadersWidth = 62
        Me.dgvBilling.RowTemplate.Height = 28
        Me.dgvBilling.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvBilling.Size = New System.Drawing.Size(1080, 228)
        Me.dgvBilling.TabIndex = 2
        '
        'grpPayment
        '
        Me.grpPayment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPayment.Controls.Add(Me.Label3)
        Me.grpPayment.Controls.Add(Me.dgvSchedule)
        Me.grpPayment.Controls.Add(Me.btnSavePayment)
        Me.grpPayment.Controls.Add(Me.txtPayAmount)
        Me.grpPayment.Controls.Add(Me.Label2)
        Me.grpPayment.Controls.Add(Me.lblBalance)
        Me.grpPayment.Controls.Add(Me.Label1)
        Me.grpPayment.Controls.Add(Me.lblContractInfo)
        Me.grpPayment.Location = New System.Drawing.Point(24, 368)
        Me.grpPayment.Name = "grpPayment"
        Me.grpPayment.Size = New System.Drawing.Size(1090, 307)
        Me.grpPayment.TabIndex = 3
        Me.grpPayment.TabStop = False
        Me.grpPayment.Text = "Receive Payment"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(294, 41)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(244, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Payment Schedule / Installments:"
        '
        'dgvSchedule
        '
        Me.dgvSchedule.AllowUserToAddRows = False
        Me.dgvSchedule.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvSchedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvSchedule.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvSchedule.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvSchedule.Location = New System.Drawing.Point(298, 79)
        Me.dgvSchedule.Name = "dgvSchedule"
        Me.dgvSchedule.ReadOnly = True
        Me.dgvSchedule.RowHeadersVisible = False
        Me.dgvSchedule.RowHeadersWidth = 62
        Me.dgvSchedule.RowTemplate.Height = 28
        Me.dgvSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSchedule.Size = New System.Drawing.Size(769, 196)
        Me.dgvSchedule.TabIndex = 6
        '
        'btnSavePayment
        '
        Me.btnSavePayment.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.btnSavePayment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSavePayment.ForeColor = System.Drawing.Color.White
        Me.btnSavePayment.Location = New System.Drawing.Point(27, 238)
        Me.btnSavePayment.Name = "btnSavePayment"
        Me.btnSavePayment.Size = New System.Drawing.Size(219, 46)
        Me.btnSavePayment.TabIndex = 5
        Me.btnSavePayment.Text = "CONFIRM PAYMENT"
        Me.btnSavePayment.UseVisualStyleBackColor = False
        '
        'txtPayAmount
        '
        Me.txtPayAmount.Location = New System.Drawing.Point(27, 206)
        Me.txtPayAmount.Name = "txtPayAmount"
        Me.txtPayAmount.Size = New System.Drawing.Size(219, 26)
        Me.txtPayAmount.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 174)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(135, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Payment Amount:"
        '
        'lblBalance
        '
        Me.lblBalance.AutoSize = True
        Me.lblBalance.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalance.ForeColor = System.Drawing.Color.IndianRed
        Me.lblBalance.Location = New System.Drawing.Point(27, 129)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(54, 25)
        Me.lblBalance.TabIndex = 2
        Me.lblBalance.Text = "0.00"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Balance Remaining:"
        '
        'lblContractInfo
        '
        Me.lblContractInfo.AutoSize = True
        Me.lblContractInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblContractInfo.Location = New System.Drawing.Point(23, 53)
        Me.lblContractInfo.Name = "lblContractInfo"
        Me.lblContractInfo.Size = New System.Drawing.Size(161, 20)
        Me.lblContractInfo.TabIndex = 0
        Me.lblContractInfo.Text = "Select a contract..."
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.Controls.Add(Me.lblHeader)
        Me.Panel1.Controls.Add(Me.grpPayment)
        Me.Panel1.Controls.Add(Me.txtSearchBilling)
        Me.Panel1.Controls.Add(Me.dgvBilling)
        Me.Panel1.Location = New System.Drawing.Point(23, 13)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1132, 701)
        Me.Panel1.TabIndex = 4
        '
        'newUcBilling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Name = "newUcBilling"
        Me.Size = New System.Drawing.Size(1178, 731)
        CType(Me.dgvBilling, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPayment.ResumeLayout(False)
        Me.grpPayment.PerformLayout()
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblHeader As Label
    Friend WithEvents txtSearchBilling As TextBox
    Friend WithEvents dgvBilling As DataGridView
    Friend WithEvents grpPayment As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblContractInfo As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblBalance As Label
    Friend WithEvents btnSavePayment As Button
    Friend WithEvents txtPayAmount As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dgvSchedule As DataGridView
    Friend WithEvents Panel1 As Panel
End Class
