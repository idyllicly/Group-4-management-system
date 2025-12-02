<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class newUcPageHeader
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.lblPageTitle = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblPageTitle
        '
        Me.lblPageTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblPageTitle.AutoSize = True
        Me.lblPageTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.2!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPageTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.lblPageTitle.Location = New System.Drawing.Point(63, 162)
        Me.lblPageTitle.Name = "lblPageTitle"
        Me.lblPageTitle.Size = New System.Drawing.Size(0, 38)
        Me.lblPageTitle.TabIndex = 3
        '
        'newUcPageHeader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkBlue
        Me.Controls.Add(Me.lblPageTitle)
        Me.Name = "newUcPageHeader"
        Me.Size = New System.Drawing.Size(1024, 376)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents lblPageTitle As Label
End Class
