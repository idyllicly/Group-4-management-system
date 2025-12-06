<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class newUcNotifications
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
        Me.flowPanel = New System.Windows.Forms.FlowLayoutPanel()
        Me.chkShowHistory = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'flowPanel
        '
        Me.flowPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flowPanel.AutoScroll = True
        Me.flowPanel.BackColor = System.Drawing.Color.WhiteSmoke
        Me.flowPanel.Location = New System.Drawing.Point(0, 33)
        Me.flowPanel.Name = "flowPanel"
        Me.flowPanel.Padding = New System.Windows.Forms.Padding(10)
        Me.flowPanel.Size = New System.Drawing.Size(997, 532)
        Me.flowPanel.TabIndex = 0
        '
        'chkShowHistory
        '
        Me.chkShowHistory.AutoSize = True
        Me.chkShowHistory.Location = New System.Drawing.Point(3, 3)
        Me.chkShowHistory.Name = "chkShowHistory"
        Me.chkShowHistory.Size = New System.Drawing.Size(171, 24)
        Me.chkShowHistory.TabIndex = 0
        Me.chkShowHistory.Text = "Show Read History"
        Me.chkShowHistory.UseVisualStyleBackColor = True
        '
        'newUcNotifications
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.chkShowHistory)
        Me.Controls.Add(Me.flowPanel)
        Me.Name = "newUcNotifications"
        Me.Size = New System.Drawing.Size(997, 565)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents flowPanel As FlowLayoutPanel
    Friend WithEvents chkShowHistory As CheckBox
End Class
