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
        CType(Me.dgvBilling, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpPayment.SuspendLayout()
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.Location = New System.Drawing.Point(41, 16)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(189, 25)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Billing & Collections"
        '
        'txtSearchBilling
        '
        Me.txtSearchBilling.ForeColor = System.Drawing.SystemColors.WindowFrame
        Me.txtSearchBilling.Location = New System.Drawing.Point(46, 56)
        Me.txtSearchBilling.Name = "txtSearchBilling"
        Me.txtSearchBilling.Size = New System.Drawing.Size(368, 26)
        Me.txtSearchBilling.TabIndex = 1
        Me.txtSearchBilling.Text = "Search Client..."
        '
        'dgvBilling
        '
        Me.dgvBilling.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvBilling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvBilling.Location = New System.Drawing.Point(46, 100)
        Me.dgvBilling.Name = "dgvBilling"
        Me.dgvBilling.RowHeadersWidth = 62
        Me.dgvBilling.RowTemplate.Height = 28
        Me.dgvBilling.Size = New System.Drawing.Size(411, 513)
        Me.dgvBilling.TabIndex = 2
        '
        'grpPayment
        '
        Me.grpPayment.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpPayment.Controls.Add(Me.Label3)
        Me.grpPayment.Controls.Add(Me.dgvSchedule)
        Me.grpPayment.Controls.Add(Me.btnSavePayment)
        Me.grpPayment.Controls.Add(Me.txtPayAmount)
        Me.grpPayment.Controls.Add(Me.Label2)
        Me.grpPayment.Controls.Add(Me.lblBalance)
        Me.grpPayment.Controls.Add(Me.Label1)
        Me.grpPayment.Controls.Add(Me.lblContractInfo)
        Me.grpPayment.Location = New System.Drawing.Point(487, 100)
        Me.grpPayment.Name = "grpPayment"
        Me.grpPayment.Size = New System.Drawing.Size(395, 513)
        Me.grpPayment.TabIndex = 3
        Me.grpPayment.TabStop = False
        Me.grpPayment.Text = "Receive Payment"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(244, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Payment Schedule / Installments:"
        '
        'dgvSchedule
        '
        Me.dgvSchedule.AllowUserToAddRows = False
        Me.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSchedule.Location = New System.Drawing.Point(27, 194)
        Me.dgvSchedule.Name = "dgvSchedule"
        Me.dgvSchedule.ReadOnly = True
        Me.dgvSchedule.RowHeadersVisible = False
        Me.dgvSchedule.RowHeadersWidth = 62
        Me.dgvSchedule.RowTemplate.Height = 28
        Me.dgvSchedule.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvSchedule.Size = New System.Drawing.Size(349, 219)
        Me.dgvSchedule.TabIndex = 6
        '
        'btnSavePayment
        '
        Me.btnSavePayment.Location = New System.Drawing.Point(169, 451)
        Me.btnSavePayment.Name = "btnSavePayment"
        Me.btnSavePayment.Size = New System.Drawing.Size(219, 46)
        Me.btnSavePayment.TabIndex = 5
        Me.btnSavePayment.Text = "CONFIRM PAYMENT"
        Me.btnSavePayment.UseVisualStyleBackColor = True
        '
        'txtPayAmount
        '
        Me.txtPayAmount.Location = New System.Drawing.Point(169, 419)
        Me.txtPayAmount.Name = "txtPayAmount"
        Me.txtPayAmount.Size = New System.Drawing.Size(219, 26)
        Me.txtPayAmount.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 419)
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
        'newUcBilling
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpPayment)
        Me.Controls.Add(Me.dgvBilling)
        Me.Controls.Add(Me.txtSearchBilling)
        Me.Controls.Add(Me.lblHeader)
        Me.Name = "newUcBilling"
        Me.Size = New System.Drawing.Size(901, 711)
        CType(Me.dgvBilling, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpPayment.ResumeLayout(False)
        Me.grpPayment.PerformLayout()
        CType(Me.dgvSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
End Class
