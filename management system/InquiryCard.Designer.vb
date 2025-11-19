<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InquiryCard
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
        Me.lblName = New System.Windows.Forms.Label()
        Me.lblDetails = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(26, 10)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(57, 20)
        Me.lblName.TabIndex = 0
        Me.lblName.Text = "Label1"
        '
        'lblDetails
        '
        Me.lblDetails.AutoSize = True
        Me.lblDetails.Location = New System.Drawing.Point(26, 30)
        Me.lblDetails.Name = "lblDetails"
        Me.lblDetails.Size = New System.Drawing.Size(57, 20)
        Me.lblDetails.TabIndex = 1
        Me.lblDetails.Text = "Label1"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(26, 50)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(57, 20)
        Me.lblDate.TabIndex = 2
        Me.lblDate.Text = "Label1"
        '
        'InquiryCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlDark
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lblDetails)
        Me.Controls.Add(Me.lblName)
        Me.Name = "InquiryCard"
        Me.Size = New System.Drawing.Size(198, 98)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblName As Label
    Friend WithEvents lblDetails As Label
    Friend WithEvents lblDate As Label
End Class
