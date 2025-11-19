Partial Class InquiryPage
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
    Private Sub InitializeComponent()
        Me.UserControl11 = New management_system.NavigationControl()
        Me.SuspendLayout()
        '
        'UserControl11
        '
        Me.UserControl11.BackColor = System.Drawing.Color.White
        Me.UserControl11.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.UserControl11.Name = "UserControl11"
        Me.UserControl11.TabIndex = 0
        '
        'InquiryPage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(832, 582)
        Me.Controls.Add(Me.UserControl11)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "InquiryPage"
        Me.Text = "Form1"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UserControl11 As NavigationControl
End Class
