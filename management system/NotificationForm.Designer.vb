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
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.OvalButton1 = New management_system.OvalButton()
        Me.OvalButton2 = New management_system.OvalButton()
        Me.OvalButton3 = New management_system.OvalButton()
        Me.btnBack = New management_system.OvalButton()
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
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.btnBack)
        Me.Panel2.Controls.Add(Me.OvalButton3)
        Me.Panel2.Controls.Add(Me.Panel5)
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
        Me.Label1.Text = "          Create Account"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel5.Location = New System.Drawing.Point(0, 71)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(659, 613)
        Me.Panel5.TabIndex = 100
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
        'OvalButton3
        '
        Me.OvalButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalButton3.BackColor = System.Drawing.Color.MediumPurple
        Me.OvalButton3.CornerRadius = 15
        Me.OvalButton3.FlatAppearance.BorderSize = 0
        Me.OvalButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OvalButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OvalButton3.Location = New System.Drawing.Point(658, 861)
        Me.OvalButton3.Name = "OvalButton3"
        Me.OvalButton3.Size = New System.Drawing.Size(177, 61)
        Me.OvalButton3.TabIndex = 125
        Me.OvalButton3.Text = "Create Account"
        Me.OvalButton3.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.IndianRed
        Me.btnBack.CornerRadius = 15
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(862, 861)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(147, 61)
        Me.btnBack.TabIndex = 126
        Me.btnBack.Text = "Clear Inputs"
        Me.btnBack.UseVisualStyleBackColor = False
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
    Friend WithEvents btnBack As OvalButton
    Friend WithEvents OvalButton3 As OvalButton
    Friend WithEvents Panel5 As Panel
    Friend WithEvents OvalButton1 As OvalButton
    Friend WithEvents OvalButton2 As OvalButton
End Class
