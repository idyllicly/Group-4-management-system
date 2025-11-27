<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormDayDetails
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
        Me.lblDateHeader = New System.Windows.Forms.Label()
        Me.pnlList = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'lblDateHeader
        '
        Me.lblDateHeader.AutoSize = True
        Me.lblDateHeader.Location = New System.Drawing.Point(13, 13)
        Me.lblDateHeader.Name = "lblDateHeader"
        Me.lblDateHeader.Size = New System.Drawing.Size(57, 20)
        Me.lblDateHeader.TabIndex = 0
        Me.lblDateHeader.Text = "Label1"
        '
        'pnlList
        '
        Me.pnlList.AutoScroll = True
        Me.pnlList.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.pnlList.Location = New System.Drawing.Point(0, 57)
        Me.pnlList.Name = "pnlList"
        Me.pnlList.Size = New System.Drawing.Size(800, 393)
        Me.pnlList.TabIndex = 1
        '
        'FormDayDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlList)
        Me.Controls.Add(Me.lblDateHeader)
        Me.Name = "FormDayDetails"
        Me.Text = "FormDayDetails"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblDateHeader As Label
    Friend WithEvents pnlList As FlowLayoutPanel
End Class
