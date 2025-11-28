<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NotificationForm
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
        Me.PageLabel1 = New management_system.PageLabel()
        Me.SideNavControl1 = New management_system.SideNavControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.OvalButton1 = New management_system.OvalButton()
        Me.OvalButton2 = New management_system.OvalButton()
        Me.NotifCard1 = New management_system.NotifCard()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'PageLabel1
        '
        Me.PageLabel1.BackColor = System.Drawing.Color.MediumPurple
        Me.PageLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PageLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PageLabel1.ForeColor = System.Drawing.Color.White
        Me.PageLabel1.Location = New System.Drawing.Point(0, 0)
        Me.PageLabel1.Name = "PageLabel1"
        Me.PageLabel1.Size = New System.Drawing.Size(1429, 119)
        Me.PageLabel1.TabIndex = 3
        '
        'SideNavControl1
        '
        Me.SideNavControl1.BackColor = System.Drawing.Color.White
        Me.SideNavControl1.Location = New System.Drawing.Point(0, 117)
        Me.SideNavControl1.Name = "SideNavControl1"
        Me.SideNavControl1.Size = New System.Drawing.Size(362, 985)
        Me.SideNavControl1.TabIndex = 30
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.BlueViolet
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(359, 79)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1107, 1023)
        Me.Panel1.TabIndex = 31
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.NotifCard1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.OvalButton1)
        Me.Panel2.Controls.Add(Me.OvalButton2)
        Me.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(37, 35)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1037, 933)
        Me.Panel2.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(1037, 70)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "          Notification"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OvalButton1
        '
        Me.OvalButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalButton1.BackColor = System.Drawing.Color.Cyan
        Me.OvalButton1.CornerRadius = 15
        Me.OvalButton1.FlatAppearance.BorderSize = 0
        Me.OvalButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OvalButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OvalButton1.Location = New System.Drawing.Point(1191, 867)
        Me.OvalButton1.Name = "OvalButton1"
        Me.OvalButton1.Size = New System.Drawing.Size(145, 48)
        Me.OvalButton1.TabIndex = 82
        Me.OvalButton1.Text = "Create Account"
        Me.OvalButton1.UseVisualStyleBackColor = False
        '
        'OvalButton2
        '
        Me.OvalButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalButton2.BackColor = System.Drawing.Color.White
        Me.OvalButton2.CornerRadius = 15
        Me.OvalButton2.FlatAppearance.BorderSize = 0
        Me.OvalButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OvalButton2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OvalButton2.Location = New System.Drawing.Point(1366, 867)
        Me.OvalButton2.Name = "OvalButton2"
        Me.OvalButton2.Size = New System.Drawing.Size(127, 48)
        Me.OvalButton2.TabIndex = 83
        Me.OvalButton2.Text = "Clear Inputs"
        Me.OvalButton2.UseVisualStyleBackColor = False
        '
        'NotifCard1
        '
        Me.NotifCard1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NotifCard1.BackColor = System.Drawing.Color.White
        Me.NotifCard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.NotifCard1.IsRead = False
        Me.NotifCard1.JobAssignmentStatus = Nothing
        Me.NotifCard1.Location = New System.Drawing.Point(0, 74)
        Me.NotifCard1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.NotifCard1.Name = "NotifCard1"
        Me.NotifCard1.NotificationTitle = Nothing
        Me.NotifCard1.OverallStatus = Nothing
        Me.NotifCard1.RecentActionDate = New Date(CType(0, Long))
        Me.NotifCard1.Size = New System.Drawing.Size(1033, 150)
        Me.NotifCard1.TabIndex = 84
        '
        'NotificationForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1429, 1100)
        Me.Controls.Add(Me.PageLabel1)
        Me.Controls.Add(Me.SideNavControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "NotificationForm"
        Me.Text = "NotificationForm"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents PageLabel1 As PageLabel
    Friend WithEvents SideNavControl1 As SideNavControl
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents OvalButton1 As OvalButton
    Friend WithEvents OvalButton2 As OvalButton
    Friend WithEvents NotifCard1 As NotifCard
End Class
