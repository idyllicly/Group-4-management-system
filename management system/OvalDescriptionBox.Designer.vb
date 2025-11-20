<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OvalDescriptionBox
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
        Me.innerTextbox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'innerTextbox
        '
        Me.innerTextbox.Location = New System.Drawing.Point(27, 63)
        Me.innerTextbox.Name = "innerTextbox"
        Me.innerTextbox.Size = New System.Drawing.Size(100, 22)
        Me.innerTextbox.TabIndex = 0
        '
        'OvalDescriptionBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.innerTextbox)
        Me.Name = "OvalDescriptionBox"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents innerTextbox As TextBox
End Class
