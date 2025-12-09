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
        Me.flpContent = New System.Windows.Forms.FlowLayoutPanel()
        Me.SuspendLayout()
        '
        'lblDayNumber
        '
        Me.lblDayNumber.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblDayNumber.Location = New System.Drawing.Point(0, 0)
        Me.lblDayNumber.Name = "lblDayNumber"
        Me.lblDayNumber.Size = New System.Drawing.Size(192, 23)
        Me.lblDayNumber.TabIndex = 0
        Me.lblDayNumber.Text = "day"
        '
        'flpContent
        '
        Me.flpContent.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpContent.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.flpContent.Location = New System.Drawing.Point(0, 23)
        Me.flpContent.Name = "flpContent"
        Me.flpContent.Padding = New System.Windows.Forms.Padding(10, 0, 10, 0)
        Me.flpContent.Size = New System.Drawing.Size(192, 100)
        Me.flpContent.TabIndex = 1
        Me.flpContent.WrapContents = False
        '
        'ucCalendarDay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.flpContent)
        Me.Controls.Add(Me.lblDayNumber)
        Me.Margin = New System.Windows.Forms.Padding(0)
        Me.Name = "ucCalendarDay"
        Me.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.Size = New System.Drawing.Size(202, 123)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblDayNumber As Label
    Friend WithEvents flpContent As FlowLayoutPanel
End Class
