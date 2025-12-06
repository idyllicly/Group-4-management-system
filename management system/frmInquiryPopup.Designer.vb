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
        Me.SuspendLayout()
        '
        'lblSelectedClient
        '
        Me.lblSelectedClient.AutoSize = True
        Me.lblSelectedClient.Location = New System.Drawing.Point(72, 60)
        Me.lblSelectedClient.Name = "lblSelectedClient"
        Me.lblSelectedClient.Size = New System.Drawing.Size(57, 20)
        Me.lblSelectedClient.TabIndex = 0
        Me.lblSelectedClient.Text = "Label1"
        '
        'cmbInspector
        '
        Me.cmbInspector.FormattingEnabled = True
        Me.cmbInspector.Location = New System.Drawing.Point(218, 60)
        Me.cmbInspector.Name = "cmbInspector"
        Me.cmbInspector.Size = New System.Drawing.Size(121, 28)
        Me.cmbInspector.TabIndex = 1
        '
        'cmbService
        '
        Me.cmbService.FormattingEnabled = True
        Me.cmbService.Location = New System.Drawing.Point(442, 60)
        Me.cmbService.Name = "cmbService"
        Me.cmbService.Size = New System.Drawing.Size(121, 28)
        Me.cmbService.TabIndex = 2
        '
        'dtpInspectDate
        '
        Me.dtpInspectDate.Location = New System.Drawing.Point(105, 128)
        Me.dtpInspectDate.Name = "dtpInspectDate"
        Me.dtpInspectDate.Size = New System.Drawing.Size(200, 26)
        Me.dtpInspectDate.TabIndex = 3
        '
        'btnDispatch
        '
        Me.btnDispatch.Location = New System.Drawing.Point(426, 351)
        Me.btnDispatch.Name = "btnDispatch"
        Me.btnDispatch.Size = New System.Drawing.Size(85, 37)
        Me.btnDispatch.TabIndex = 4
        Me.btnDispatch.Text = "dispatch"
        Me.btnDispatch.UseVisualStyleBackColor = True
        '
        'frmInquiryPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
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
End Class
