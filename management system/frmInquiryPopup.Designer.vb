<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInquiryPopup
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
        Me.lblSelectedClient = New System.Windows.Forms.Label()
        Me.cmbInspector = New System.Windows.Forms.ComboBox()
        Me.cmbService = New System.Windows.Forms.ComboBox()
        Me.dtpInspectDate = New System.Windows.Forms.DateTimePicker()
        Me.btnDispatch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblSelectedClient
        '
        Me.lblSelectedClient.AutoSize = True
        Me.lblSelectedClient.Location = New System.Drawing.Point(23, 18)
        Me.lblSelectedClient.Name = "lblSelectedClient"
        Me.lblSelectedClient.Size = New System.Drawing.Size(57, 20)
        Me.lblSelectedClient.TabIndex = 0
        Me.lblSelectedClient.Text = "Label1"
        '
        'cmbInspector
        '
        Me.cmbInspector.FormattingEnabled = True
        Me.cmbInspector.Location = New System.Drawing.Point(251, 125)
        Me.cmbInspector.Name = "cmbInspector"
        Me.cmbInspector.Size = New System.Drawing.Size(261, 28)
        Me.cmbInspector.TabIndex = 1
        '
        'cmbService
        '
        Me.cmbService.FormattingEnabled = True
        Me.cmbService.Location = New System.Drawing.Point(27, 199)
        Me.cmbService.Name = "cmbService"
        Me.cmbService.Size = New System.Drawing.Size(485, 28)
        Me.cmbService.TabIndex = 2
        '
        'dtpInspectDate
        '
        Me.dtpInspectDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpInspectDate.Location = New System.Drawing.Point(27, 127)
        Me.dtpInspectDate.Name = "dtpInspectDate"
        Me.dtpInspectDate.Size = New System.Drawing.Size(200, 26)
        Me.dtpInspectDate.TabIndex = 3
        '
        'btnDispatch
        '
        Me.btnDispatch.Location = New System.Drawing.Point(27, 263)
        Me.btnDispatch.Name = "btnDispatch"
        Me.btnDispatch.Size = New System.Drawing.Size(485, 102)
        Me.btnDispatch.TabIndex = 4
        Me.btnDispatch.Text = "dispatch"
        Me.btnDispatch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(247, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select Technician"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 104)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(151, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Set Inspection Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(23, 176)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(110, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Select Service"
        '
        'frmInquiryPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(536, 386)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDispatch)
        Me.Controls.Add(Me.dtpInspectDate)
        Me.Controls.Add(Me.cmbService)
        Me.Controls.Add(Me.cmbInspector)
        Me.Controls.Add(Me.lblSelectedClient)
        Me.Name = "frmInquiryPopup"
        Me.Text = "frmInquiryPopup"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblSelectedClient As Label
    Friend WithEvents cmbInspector As ComboBox
    Friend WithEvents cmbService As ComboBox
    Friend WithEvents dtpInspectDate As DateTimePicker
    Friend WithEvents btnDispatch As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
