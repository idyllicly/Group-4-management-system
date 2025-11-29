<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ManageAccounts
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.AccCard1 = New management_system.AccCard()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SideNavControl1 = New management_system.SideNavControl()
        Me.PageLabel1 = New management_system.PageLabel()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.MediumPurple
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(368, 114)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1164, 1201)
        Me.Panel1.TabIndex = 17
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.AutoScroll = True
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Button1)
        Me.Panel2.Controls.Add(Me.AccCard1)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(40, 96)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1107, 861)
        Me.Panel2.TabIndex = 18
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.White
        Me.Button1.Image = Global.management_system.My.Resources.Resources.buton123456
        Me.Button1.Location = New System.Drawing.Point(1017, 802)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(51, 45)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = False
        '
        'AccCard1
        '
        Me.AccCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.AccCard1.Location = New System.Drawing.Point(57, 4)
        Me.AccCard1.Margin = New System.Windows.Forms.Padding(4)
        Me.AccCard1.Name = "AccCard1"
        Me.AccCard1.Size = New System.Drawing.Size(656, 259)
        Me.AccCard1.TabIndex = 18
        Me.AccCard1.UserID = 0
        Me.AccCard1.UserImage = Nothing
        Me.AccCard1.UserName = "AccountNAME"
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Location = New System.Drawing.Point(1017, 802)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(51, 45)
        Me.Panel4.TabIndex = 17
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.Label1)
        Me.Panel3.Location = New System.Drawing.Point(408, 114)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1107, 100)
        Me.Panel3.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(50, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(180, 36)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Account List" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'SideNavControl1
        '
        Me.SideNavControl1.BackColor = System.Drawing.Color.White
        Me.SideNavControl1.Location = New System.Drawing.Point(0, 114)
        Me.SideNavControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SideNavControl1.Name = "SideNavControl1"
        Me.SideNavControl1.Size = New System.Drawing.Size(371, 987)
        Me.SideNavControl1.TabIndex = 34
        '
        'PageLabel1
        '
        Me.PageLabel1.BackColor = System.Drawing.Color.MediumPurple
        Me.PageLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PageLabel1.Location = New System.Drawing.Point(0, 0)
        Me.PageLabel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PageLabel1.Name = "PageLabel1"
        Me.PageLabel1.Size = New System.Drawing.Size(1515, 114)
        Me.PageLabel1.TabIndex = 33
        '
        'ManageAccounts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1515, 1100)
        Me.Controls.Add(Me.SideNavControl1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.PageLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ManageAccounts"
        Me.Text = "ManageAccounts"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Button1 As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents AccCard1 As AccCard
    Friend WithEvents PageLabel1 As PageLabel
    Friend WithEvents SideNavControl1 As SideNavControl
End Class
