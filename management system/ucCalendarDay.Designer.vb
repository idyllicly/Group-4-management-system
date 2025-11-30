<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ucCalendarDay
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
        Me.lblDayNumber = New System.Windows.Forms.Label()
        Me.lblDot = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblDayNumber
        '
        Me.lblDayNumber.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblDayNumber.Location = New System.Drawing.Point(0, 0)
        Me.lblDayNumber.Name = "lblDayNumber"
        Me.lblDayNumber.Size = New System.Drawing.Size(78, 23)
        Me.lblDayNumber.TabIndex = 0
        Me.lblDayNumber.Text = "day"
        '
        'lblDot
        '
        Me.lblDot.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDot.Location = New System.Drawing.Point(18, 32)
        Me.lblDot.Name = "lblDot"
        Me.lblDot.Size = New System.Drawing.Size(10, 10)
        Me.lblDot.TabIndex = 1
        '
        'ucCalendarDay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblDot)
        Me.Controls.Add(Me.lblDayNumber)
        Me.Name = "ucCalendarDay"
        Me.Size = New System.Drawing.Size(78, 58)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblDayNumber As Label
    Friend WithEvents lblDot As Label
End Class
