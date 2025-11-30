<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newUcInquiryManager
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
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.dgvClients = New System.Windows.Forms.DataGridView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblClientName = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbInspector = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpInspectDate = New System.Windows.Forms.DateTimePicker()
        Me.btnDispatch = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.dgvClients, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(22, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(359, 25)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "INQUIRY & INSPECTION DISPATCH"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(27, 101)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(240, 26)
        Me.txtSearch.TabIndex = 1
        Me.txtSearch.Text = "Search Client..."
        '
        'dgvClients
        '
        Me.dgvClients.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dgvClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvClients.Location = New System.Drawing.Point(27, 133)
        Me.dgvClients.Name = "dgvClients"
        Me.dgvClients.RowHeadersWidth = 62
        Me.dgvClients.RowTemplate.Height = 28
        Me.dgvClients.Size = New System.Drawing.Size(339, 392)
        Me.dgvClients.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(120, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Selected Client:"
        '
        'lblClientName
        '
        Me.lblClientName.AutoSize = True
        Me.lblClientName.BackColor = System.Drawing.Color.Transparent
        Me.lblClientName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClientName.ForeColor = System.Drawing.Color.Green
        Me.lblClientName.Location = New System.Drawing.Point(139, 38)
        Me.lblClientName.Name = "lblClientName"
        Me.lblClientName.Size = New System.Drawing.Size(27, 20)
        Me.lblClientName.TabIndex = 4
        Me.lblClientName.Text = "---"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(189, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Select Inspector (Owner):"
        '
        'cmbInspector
        '
        Me.cmbInspector.FormattingEnabled = True
        Me.cmbInspector.Location = New System.Drawing.Point(213, 75)
        Me.cmbInspector.Name = "cmbInspector"
        Me.cmbInspector.Size = New System.Drawing.Size(121, 28)
        Me.cmbInspector.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(126, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Inspection Date:"
        '
        'dtpInspectDate
        '
        Me.dtpInspectDate.CustomFormat = "MM/dd/yyyy hh:mm tt"
        Me.dtpInspectDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInspectDate.Location = New System.Drawing.Point(160, 124)
        Me.dtpInspectDate.Name = "dtpInspectDate"
        Me.dtpInspectDate.Size = New System.Drawing.Size(182, 26)
        Me.dtpInspectDate.TabIndex = 8
        '
        'btnDispatch
        '
        Me.btnDispatch.Location = New System.Drawing.Point(185, 268)
        Me.btnDispatch.Name = "btnDispatch"
        Me.btnDispatch.Size = New System.Drawing.Size(174, 108)
        Me.btnDispatch.TabIndex = 9
        Me.btnDispatch.Text = "SCHEDULE OCULAR"
        Me.btnDispatch.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dtpInspectDate)
        Me.GroupBox1.Controls.Add(Me.btnDispatch)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblClientName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbInspector)
        Me.GroupBox1.Location = New System.Drawing.Point(401, 133)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(376, 392)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Schedule Inspection"
        '
        'newUcInquiryManager
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dgvClients)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.Label1)
        Me.Name = "newUcInquiryManager"
        Me.Size = New System.Drawing.Size(793, 567)
        CType(Me.dgvClients, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents dgvClients As DataGridView
    Friend WithEvents Label2 As Label
    Friend WithEvents lblClientName As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents cmbInspector As ComboBox
    Friend WithEvents Label4 As Label
    Friend WithEvents dtpInspectDate As DateTimePicker
    Friend WithEvents btnDispatch As Button
    Friend WithEvents GroupBox1 As GroupBox
End Class
