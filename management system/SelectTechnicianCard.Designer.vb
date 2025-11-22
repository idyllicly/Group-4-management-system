<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SelectTechnicianCard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SelectTechnicianCard))
        Me.btnBack = New management_system.OvalButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.OvalButton1 = New management_system.OvalButton()
        Me.MainPanel = New System.Windows.Forms.Panel()
        Me.TechPanel = New System.Windows.Forms.Panel()
        Me.TechDesc = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TechName = New System.Windows.Forms.Label()
        Me.PicturePanel = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.TechViber = New System.Windows.Forms.Label()
        Me.TechFB = New System.Windows.Forms.Label()
        Me.TechEmail = New System.Windows.Forms.Label()
        Me.TechNumber = New System.Windows.Forms.Label()
        Me.TechPicture = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        Me.MainPanel.SuspendLayout()
        Me.TechPanel.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.PicturePanel.SuspendLayout()
        CType(Me.TechPicture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.IndianRed
        Me.btnBack.CornerRadius = 15
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(1034, 13)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(147, 61)
        Me.btnBack.TabIndex = 0
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.OvalButton1)
        Me.Panel1.Controls.Add(Me.btnBack)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 628)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1228, 86)
        Me.Panel1.TabIndex = 1
        '
        'OvalButton1
        '
        Me.OvalButton1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalButton1.BackColor = System.Drawing.Color.MediumPurple
        Me.OvalButton1.CornerRadius = 15
        Me.OvalButton1.FlatAppearance.BorderSize = 0
        Me.OvalButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OvalButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OvalButton1.Location = New System.Drawing.Point(831, 13)
        Me.OvalButton1.Name = "OvalButton1"
        Me.OvalButton1.Size = New System.Drawing.Size(177, 61)
        Me.OvalButton1.TabIndex = 1
        Me.OvalButton1.Text = "Assign and Submit"
        Me.OvalButton1.UseVisualStyleBackColor = False
        '
        'MainPanel
        '
        Me.MainPanel.Controls.Add(Me.TechPanel)
        Me.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainPanel.Location = New System.Drawing.Point(0, 0)
        Me.MainPanel.Name = "MainPanel"
        Me.MainPanel.Size = New System.Drawing.Size(1228, 628)
        Me.MainPanel.TabIndex = 2
        '
        'TechPanel
        '
        Me.TechPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TechPanel.BackColor = System.Drawing.Color.LightSteelBlue
        Me.TechPanel.Controls.Add(Me.TechDesc)
        Me.TechPanel.Controls.Add(Me.Panel2)
        Me.TechPanel.Controls.Add(Me.PicturePanel)
        Me.TechPanel.Location = New System.Drawing.Point(63, 49)
        Me.TechPanel.Name = "TechPanel"
        Me.TechPanel.Size = New System.Drawing.Size(1100, 528)
        Me.TechPanel.TabIndex = 0
        '
        'TechDesc
        '
        Me.TechDesc.AutoSize = True
        Me.TechDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TechDesc.Location = New System.Drawing.Point(332, 174)
        Me.TechDesc.MaximumSize = New System.Drawing.Size(1050, 0)
        Me.TechDesc.Name = "TechDesc"
        Me.TechDesc.Size = New System.Drawing.Size(1020, 325)
        Me.TechDesc.TabIndex = 2
        Me.TechDesc.Text = resources.GetString("TechDesc.Text")
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.TechName)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(312, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(788, 154)
        Me.Panel2.TabIndex = 1
        '
        'TechName
        '
        Me.TechName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.TechName.AutoSize = True
        Me.TechName.Font = New System.Drawing.Font("Microsoft YaHei", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TechName.Location = New System.Drawing.Point(41, 73)
        Me.TechName.Name = "TechName"
        Me.TechName.Size = New System.Drawing.Size(105, 40)
        Me.TechName.TabIndex = 1
        Me.TechName.Text = "Name"
        '
        'PicturePanel
        '
        Me.PicturePanel.BackColor = System.Drawing.Color.MediumPurple
        Me.PicturePanel.Controls.Add(Me.Panel6)
        Me.PicturePanel.Controls.Add(Me.Panel5)
        Me.PicturePanel.Controls.Add(Me.Panel4)
        Me.PicturePanel.Controls.Add(Me.Panel3)
        Me.PicturePanel.Controls.Add(Me.TechViber)
        Me.PicturePanel.Controls.Add(Me.TechFB)
        Me.PicturePanel.Controls.Add(Me.TechEmail)
        Me.PicturePanel.Controls.Add(Me.TechNumber)
        Me.PicturePanel.Controls.Add(Me.TechPicture)
        Me.PicturePanel.Dock = System.Windows.Forms.DockStyle.Left
        Me.PicturePanel.Location = New System.Drawing.Point(0, 0)
        Me.PicturePanel.Name = "PicturePanel"
        Me.PicturePanel.Size = New System.Drawing.Size(312, 528)
        Me.PicturePanel.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.BackgroundImage = Global.management_system.My.Resources.Resources._15452805
        Me.Panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel6.Location = New System.Drawing.Point(12, 470)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(35, 29)
        Me.Panel6.TabIndex = 6
        '
        'Panel5
        '
        Me.Panel5.BackgroundImage = Global.management_system.My.Resources.Resources.facebook_logo_icon_free_png
        Me.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel5.Location = New System.Drawing.Point(12, 431)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(35, 29)
        Me.Panel5.TabIndex = 6
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.BackgroundImage = Global.management_system.My.Resources.Resources.envelope_letter_icon_symbol_png
        Me.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel4.Location = New System.Drawing.Point(12, 390)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(35, 29)
        Me.Panel4.TabIndex = 6
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = Global.management_system.My.Resources.Resources.contact_us_icon_png_21
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Location = New System.Drawing.Point(12, 349)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(35, 29)
        Me.Panel3.TabIndex = 5
        '
        'TechViber
        '
        Me.TechViber.AutoSize = True
        Me.TechViber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TechViber.Location = New System.Drawing.Point(53, 470)
        Me.TechViber.Name = "TechViber"
        Me.TechViber.Size = New System.Drawing.Size(58, 25)
        Me.TechViber.TabIndex = 4
        Me.TechViber.Text = "Viber"
        '
        'TechFB
        '
        Me.TechFB.AutoSize = True
        Me.TechFB.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TechFB.Location = New System.Drawing.Point(53, 435)
        Me.TechFB.Name = "TechFB"
        Me.TechFB.Size = New System.Drawing.Size(99, 25)
        Me.TechFB.TabIndex = 3
        Me.TechFB.Text = "Facebook"
        '
        'TechEmail
        '
        Me.TechEmail.AutoSize = True
        Me.TechEmail.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TechEmail.Location = New System.Drawing.Point(53, 394)
        Me.TechEmail.Name = "TechEmail"
        Me.TechEmail.Size = New System.Drawing.Size(60, 25)
        Me.TechEmail.TabIndex = 2
        Me.TechEmail.Text = "Email"
        '
        'TechNumber
        '
        Me.TechNumber.AutoSize = True
        Me.TechNumber.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TechNumber.Location = New System.Drawing.Point(53, 353)
        Me.TechNumber.Name = "TechNumber"
        Me.TechNumber.Size = New System.Drawing.Size(115, 25)
        Me.TechNumber.TabIndex = 1
        Me.TechNumber.Text = "Contact No."
        '
        'TechPicture
        '
        Me.TechPicture.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TechPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TechPicture.Location = New System.Drawing.Point(29, 53)
        Me.TechPicture.Name = "TechPicture"
        Me.TechPicture.Size = New System.Drawing.Size(260, 221)
        Me.TechPicture.TabIndex = 0
        Me.TechPicture.TabStop = False
        '
        'SelectTechnicianCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(1228, 714)
        Me.ControlBox = False
        Me.Controls.Add(Me.MainPanel)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "SelectTechnicianCard"
        Me.Text = "SelectTechnicianCard"
        Me.Panel1.ResumeLayout(False)
        Me.MainPanel.ResumeLayout(False)
        Me.TechPanel.ResumeLayout(False)
        Me.TechPanel.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.PicturePanel.ResumeLayout(False)
        Me.PicturePanel.PerformLayout()
        CType(Me.TechPicture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnBack As OvalButton
    Friend WithEvents Panel1 As Panel
    Friend WithEvents OvalButton1 As OvalButton
    Friend WithEvents MainPanel As Panel
    Friend WithEvents TechPanel As Panel
    Friend WithEvents PicturePanel As Panel
    Friend WithEvents TechPicture As PictureBox
    Friend WithEvents TechName As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TechViber As Label
    Friend WithEvents TechFB As Label
    Friend WithEvents TechEmail As Label
    Friend WithEvents TechNumber As Label
    Friend WithEvents TechDesc As Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
End Class
