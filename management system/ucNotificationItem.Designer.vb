<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucNotificationItem
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
        Me.pnlColorStrip = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblTime = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnDismiss = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'pnlColorStrip
        '
        Me.pnlColorStrip.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlColorStrip.Location = New System.Drawing.Point(0, 0)
        Me.pnlColorStrip.Name = "pnlColorStrip"
        Me.pnlColorStrip.Size = New System.Drawing.Size(61, 204)
        Me.pnlColorStrip.TabIndex = 0
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(109, 20)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(125, 40)
        Me.lblTitle.TabIndex = 1
        Me.lblTitle.Text = "Label1"
        '
        'lblTime
        '
        Me.lblTime.AutoSize = True
        Me.lblTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTime.Location = New System.Drawing.Point(112, 78)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(63, 20)
        Me.lblTime.TabIndex = 2
        Me.lblTime.Text = "Label2"
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(115, 111)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(57, 20)
        Me.lblMessage.TabIndex = 3
        Me.lblMessage.Text = "Label3"
        '
        'btnDismiss
        '
        Me.btnDismiss.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDismiss.Location = New System.Drawing.Point(796, 137)
        Me.btnDismiss.Name = "btnDismiss"
        Me.btnDismiss.Size = New System.Drawing.Size(105, 46)
        Me.btnDismiss.TabIndex = 4
        Me.btnDismiss.Text = "Dismiss"
        Me.btnDismiss.UseVisualStyleBackColor = True
        '
        'ucNotificationItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Silver
        Me.Controls.Add(Me.btnDismiss)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.lblTime)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.pnlColorStrip)
        Me.Name = "ucNotificationItem"
        Me.Size = New System.Drawing.Size(939, 204)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pnlColorStrip As Panel
    Friend WithEvents lblTitle As Label
    Friend WithEvents lblTime As Label
    Friend WithEvents lblMessage As Label
    Friend WithEvents btnDismiss As Button
End Class
