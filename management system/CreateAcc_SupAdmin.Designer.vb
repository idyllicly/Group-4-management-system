<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class CreateAcc_SupAdmin
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.OvalComboBox1 = New management_system.OvalComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.OvalTextBox11 = New management_system.OvalTextBox()
        Me.OvalTextBox10 = New management_system.OvalTextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.OvalTextBox2 = New management_system.OvalTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnBack = New management_system.OvalButton()
        Me.OvalButton3 = New management_system.OvalButton()
        Me.OvalDescriptionBox1 = New management_system.OvalDescriptionBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.OvalTextBox13 = New management_system.OvalTextBox()
        Me.OvalTextBox6 = New management_system.OvalTextBox()
        Me.OvalTextBox5 = New management_system.OvalTextBox()
        Me.OvalTextBox4 = New management_system.OvalTextBox()
        Me.OvalTextBox12 = New management_system.OvalTextBox()
        Me.OvalTextBox9 = New management_system.OvalTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.OvalButton1 = New management_system.OvalButton()
        Me.OvalButton2 = New management_system.OvalButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.PageLabel1 = New management_system.PageLabel()
        Me.OvalTextBox1 = New management_system.OvalTextBox()
        Me.OvalTextBox3 = New management_system.OvalTextBox()
        Me.SideNavControl1 = New management_system.SideNavControl()
        Me.MySqlConnection1 = New MySqlConnector.MySqlConnection()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.White
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Panel4)
        Me.Panel2.Controls.Add(Me.btnBack)
        Me.Panel2.Controls.Add(Me.OvalButton3)
        Me.Panel2.Controls.Add(Me.OvalDescriptionBox1)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.OvalButton1)
        Me.Panel2.Controls.Add(Me.OvalButton2)
        Me.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(37, 35)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1037, 919)
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
        'Panel4
        '
        Me.Panel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.OvalComboBox1)
        Me.Panel4.Controls.Add(Me.Button1)
        Me.Panel4.Controls.Add(Me.PictureBox1)
        Me.Panel4.Controls.Add(Me.OvalTextBox11)
        Me.Panel4.Controls.Add(Me.OvalTextBox10)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.OvalTextBox2)
        Me.Panel4.Controls.Add(Me.Label9)
        Me.Panel4.Controls.Add(Me.Label7)
        Me.Panel4.Location = New System.Drawing.Point(589, 71)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(943, 599)
        Me.Panel4.TabIndex = 106
        '
        'OvalComboBox1
        '
        Me.OvalComboBox1.BackColor = System.Drawing.Color.Transparent
        Me.OvalComboBox1.Location = New System.Drawing.Point(57, 448)
        Me.OvalComboBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.OvalComboBox1.Name = "OvalComboBox1"
        Me.OvalComboBox1.Size = New System.Drawing.Size(385, 52)
        Me.OvalComboBox1.TabIndex = 120
        Me.OvalComboBox1.Text = "Select an Item"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.Location = New System.Drawing.Point(653, 254)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(121, 59)
        Me.Button1.TabIndex = 119
        Me.Button1.Text = "INSERT PHOTO HERE"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Location = New System.Drawing.Point(543, 113)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(351, 346)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 118
        Me.PictureBox1.TabStop = False
        '
        'OvalTextBox11
        '
        Me.OvalTextBox11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox11.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox11.Location = New System.Drawing.Point(57, 360)
        Me.OvalTextBox11.Margin = New System.Windows.Forms.Padding(2)
        Me.OvalTextBox11.Name = "OvalTextBox11"
        Me.OvalTextBox11.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.OvalTextBox11.Size = New System.Drawing.Size(385, 51)
        Me.OvalTextBox11.TabIndex = 117
        '
        'OvalTextBox10
        '
        Me.OvalTextBox10.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox10.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox10.Location = New System.Drawing.Point(57, 274)
        Me.OvalTextBox10.Margin = New System.Windows.Forms.Padding(2)
        Me.OvalTextBox10.Name = "OvalTextBox10"
        Me.OvalTextBox10.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.OvalTextBox10.Size = New System.Drawing.Size(385, 51)
        Me.OvalTextBox10.TabIndex = 116
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(59, 254)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(83, 18)
        Me.Label8.TabIndex = 110
        Me.Label8.Text = "Password"
        '
        'OvalTextBox2
        '
        Me.OvalTextBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox2.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox2.Location = New System.Drawing.Point(57, 184)
        Me.OvalTextBox2.Margin = New System.Windows.Forms.Padding(2)
        Me.OvalTextBox2.Name = "OvalTextBox2"
        Me.OvalTextBox2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OvalTextBox2.Size = New System.Drawing.Size(385, 51)
        Me.OvalTextBox2.TabIndex = 112
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(55, 165)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 18)
        Me.Label9.TabIndex = 109
        Me.Label9.Text = "Username"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(59, 339)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(130, 18)
        Me.Label7.TabIndex = 111
        Me.Label7.Text = "Verify Password"
        '
        'btnBack
        '
        Me.btnBack.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBack.BackColor = System.Drawing.Color.IndianRed
        Me.btnBack.CornerRadius = 15
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(862, 847)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(147, 61)
        Me.btnBack.TabIndex = 126
        Me.btnBack.Text = "Clear Inputs"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'OvalButton3
        '
        Me.OvalButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalButton3.BackColor = System.Drawing.Color.MediumPurple
        Me.OvalButton3.CornerRadius = 15
        Me.OvalButton3.FlatAppearance.BorderSize = 0
        Me.OvalButton3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OvalButton3.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OvalButton3.Location = New System.Drawing.Point(658, 847)
        Me.OvalButton3.Name = "OvalButton3"
        Me.OvalButton3.Size = New System.Drawing.Size(177, 61)
        Me.OvalButton3.TabIndex = 125
        Me.OvalButton3.Text = "Create Account"
        Me.OvalButton3.UseVisualStyleBackColor = False
        '
        'OvalDescriptionBox1
        '
        Me.OvalDescriptionBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalDescriptionBox1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.OvalDescriptionBox1.BorderSize = 3
        Me.OvalDescriptionBox1.CurvedRadius = 10
        Me.OvalDescriptionBox1.Location = New System.Drawing.Point(65, 728)
        Me.OvalDescriptionBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.OvalDescriptionBox1.Multiline = True
        Me.OvalDescriptionBox1.Name = "OvalDescriptionBox1"
        Me.OvalDescriptionBox1.Size = New System.Drawing.Size(944, 109)
        Me.OvalDescriptionBox1.TabIndex = 124
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(62, 705)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(171, 18)
        Me.Label12.TabIndex = 123
        Me.Label12.Text = "Additional Information"
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel5.Controls.Add(Me.OvalTextBox13)
        Me.Panel5.Controls.Add(Me.OvalTextBox6)
        Me.Panel5.Controls.Add(Me.OvalTextBox5)
        Me.Panel5.Controls.Add(Me.OvalTextBox4)
        Me.Panel5.Controls.Add(Me.OvalTextBox12)
        Me.Panel5.Controls.Add(Me.OvalTextBox9)
        Me.Panel5.Controls.Add(Me.Label11)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Location = New System.Drawing.Point(0, 71)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(659, 599)
        Me.Panel5.TabIndex = 100
        '
        'OvalTextBox13
        '
        Me.OvalTextBox13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox13.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox13.Location = New System.Drawing.Point(65, 532)
        Me.OvalTextBox13.Margin = New System.Windows.Forms.Padding(2)
        Me.OvalTextBox13.Name = "OvalTextBox13"
        Me.OvalTextBox13.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OvalTextBox13.Size = New System.Drawing.Size(432, 52)
        Me.OvalTextBox13.TabIndex = 122
        '
        'OvalTextBox6
        '
        Me.OvalTextBox6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox6.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox6.Location = New System.Drawing.Point(65, 448)
        Me.OvalTextBox6.Margin = New System.Windows.Forms.Padding(2)
        Me.OvalTextBox6.Name = "OvalTextBox6"
        Me.OvalTextBox6.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OvalTextBox6.Size = New System.Drawing.Size(432, 52)
        Me.OvalTextBox6.TabIndex = 121
        '
        'OvalTextBox5
        '
        Me.OvalTextBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox5.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox5.Location = New System.Drawing.Point(65, 362)
        Me.OvalTextBox5.Margin = New System.Windows.Forms.Padding(2)
        Me.OvalTextBox5.Name = "OvalTextBox5"
        Me.OvalTextBox5.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OvalTextBox5.Size = New System.Drawing.Size(432, 52)
        Me.OvalTextBox5.TabIndex = 120
        '
        'OvalTextBox4
        '
        Me.OvalTextBox4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox4.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox4.Location = New System.Drawing.Point(65, 274)
        Me.OvalTextBox4.Margin = New System.Windows.Forms.Padding(2)
        Me.OvalTextBox4.Name = "OvalTextBox4"
        Me.OvalTextBox4.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OvalTextBox4.Size = New System.Drawing.Size(432, 52)
        Me.OvalTextBox4.TabIndex = 119
        '
        'OvalTextBox12
        '
        Me.OvalTextBox12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox12.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox12.Location = New System.Drawing.Point(65, 185)
        Me.OvalTextBox12.Margin = New System.Windows.Forms.Padding(2)
        Me.OvalTextBox12.Name = "OvalTextBox12"
        Me.OvalTextBox12.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OvalTextBox12.Size = New System.Drawing.Size(432, 52)
        Me.OvalTextBox12.TabIndex = 118
        '
        'OvalTextBox9
        '
        Me.OvalTextBox9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox9.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox9.Location = New System.Drawing.Point(65, 100)
        Me.OvalTextBox9.Margin = New System.Windows.Forms.Padding(2)
        Me.OvalTextBox9.Name = "OvalTextBox9"
        Me.OvalTextBox9.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OvalTextBox9.Size = New System.Drawing.Size(432, 51)
        Me.OvalTextBox9.TabIndex = 115
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(62, 516)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(46, 18)
        Me.Label11.TabIndex = 111
        Me.Label11.Text = "Viber"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(62, 428)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(149, 18)
        Me.Label2.TabIndex = 110
        Me.Label2.Text = "Facebook Account"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(62, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 18)
        Me.Label3.TabIndex = 106
        Me.Label3.Text = "Name"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(62, 165)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 18)
        Me.Label4.TabIndex = 107
        Me.Label4.Text = "Address"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(62, 342)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(116, 18)
        Me.Label5.TabIndex = 109
        Me.Label5.Text = "Email Address"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(62, 254)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 18)
        Me.Label6.TabIndex = 108
        Me.Label6.Text = "Contact No."
        '
        'OvalButton1
        '
        Me.OvalButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalButton1.BackColor = System.Drawing.Color.Cyan
        Me.OvalButton1.CornerRadius = 15
        Me.OvalButton1.FlatAppearance.BorderSize = 0
        Me.OvalButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.OvalButton1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OvalButton1.Location = New System.Drawing.Point(1191, 853)
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
        Me.OvalButton2.Location = New System.Drawing.Point(1366, 853)
        Me.OvalButton2.Name = "OvalButton2"
        Me.OvalButton2.Size = New System.Drawing.Size(127, 48)
        Me.OvalButton2.TabIndex = 83
        Me.OvalButton2.Text = "Clear Inputs"
        Me.OvalButton2.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.BlueViolet
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Location = New System.Drawing.Point(359, 76)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1107, 1009)
        Me.Panel1.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
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
        Me.PageLabel1.TabIndex = 2
        '
        'OvalTextBox1
        '
        Me.OvalTextBox1.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox1.Location = New System.Drawing.Point(128, 149)
        Me.OvalTextBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.OvalTextBox1.Name = "OvalTextBox1"
        Me.OvalTextBox1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OvalTextBox1.Size = New System.Drawing.Size(315, 35)
        Me.OvalTextBox1.TabIndex = 22
        '
        'OvalTextBox3
        '
        Me.OvalTextBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OvalTextBox3.BackColor = System.Drawing.SystemColors.Control
        Me.OvalTextBox3.BorderColor = System.Drawing.Color.BlueViolet
        Me.OvalTextBox3.Location = New System.Drawing.Point(22, -162)
        Me.OvalTextBox3.Margin = New System.Windows.Forms.Padding(4)
        Me.OvalTextBox3.Name = "OvalTextBox3"
        Me.OvalTextBox3.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.OvalTextBox3.Size = New System.Drawing.Size(489, 35)
        Me.OvalTextBox3.TabIndex = 23
        '
        'SideNavControl1
        '
        Me.SideNavControl1.BackColor = System.Drawing.Color.White
        Me.SideNavControl1.Location = New System.Drawing.Point(0, 111)
        Me.SideNavControl1.Name = "SideNavControl1"
        Me.SideNavControl1.Size = New System.Drawing.Size(362, 977)
        Me.SideNavControl1.TabIndex = 29
        '
        'MySqlConnection1
        '
        Me.MySqlConnection1.ProvideClientCertificatesCallback = Nothing
        Me.MySqlConnection1.ProvidePasswordCallback = Nothing
        Me.MySqlConnection1.RemoteCertificateValidationCallback = Nothing
        '
        'CreateAcc_SupAdmin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1429, 1055)
        Me.Controls.Add(Me.PageLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.SideNavControl1)
        Me.Name = "CreateAcc_SupAdmin"
        Me.Text = " "
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OvalTextBox1 As OvalTextBox
    Friend WithEvents OvalTextBox3 As OvalTextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents OvalButton1 As OvalButton
    Friend WithEvents OvalButton2 As OvalButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents OvalTextBox11 As OvalTextBox
    Friend WithEvents OvalTextBox10 As OvalTextBox
    Friend WithEvents OvalTextBox9 As OvalTextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Panel5 As Panel
    Friend WithEvents OvalTextBox2 As OvalTextBox
    Friend WithEvents OvalTextBox13 As OvalTextBox
    Friend WithEvents OvalTextBox6 As OvalTextBox
    Friend WithEvents OvalTextBox5 As OvalTextBox
    Friend WithEvents OvalTextBox4 As OvalTextBox
    Friend WithEvents OvalTextBox12 As OvalTextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents OvalDescriptionBox1 As OvalDescriptionBox
    Friend WithEvents Label12 As Label
    Friend WithEvents OvalButton3 As OvalButton
    Friend WithEvents btnBack As OvalButton
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents OpenFileDialog1 As OpenFileDialog
    Friend WithEvents Button1 As Button
    Friend WithEvents PageLabel1 As PageLabel
    Friend WithEvents SideNavControl1 As SideNavControl
    Friend WithEvents MySqlConnection1 As MySqlConnector.MySqlConnection
    Friend WithEvents OvalComboBox1 As OvalComboBox
End Class
