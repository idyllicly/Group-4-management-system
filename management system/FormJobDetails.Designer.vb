<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormJobDetails
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnAssign = New System.Windows.Forms.Button()
        Me.btnMarkComplete = New System.Windows.Forms.Button()
        Me.btnRevisit = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.descriptionText = New System.Windows.Forms.Label()
        Me.description = New System.Windows.Forms.Label()
        Me.scheduledTime = New System.Windows.Forms.Label()
        Me.scheduledDate = New System.Windows.Forms.Label()
        Me.contact = New System.Windows.Forms.Label()
        Me.address = New System.Windows.Forms.Label()
        Me.jobType = New System.Windows.Forms.Label()
        Me.clientName = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.SideNavControl1 = New management_system.SideNavControl()
        Me.PageLabel1 = New management_system.PageLabel()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblStatus)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Controls.Add(Me.descriptionText)
        Me.Panel1.Controls.Add(Me.description)
        Me.Panel1.Controls.Add(Me.scheduledTime)
        Me.Panel1.Controls.Add(Me.scheduledDate)
        Me.Panel1.Controls.Add(Me.contact)
        Me.Panel1.Controls.Add(Me.address)
        Me.Panel1.Controls.Add(Me.jobType)
        Me.Panel1.Controls.Add(Me.clientName)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(420, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1126, 902)
        Me.Panel1.TabIndex = 0
        '
        'lblStatus
        '
        Me.lblStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(810, 17)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(98, 32)
        Me.lblStatus.TabIndex = 13
        Me.lblStatus.Text = "status:"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnAssign)
        Me.Panel2.Controls.Add(Me.btnMarkComplete)
        Me.Panel2.Controls.Add(Me.btnRevisit)
        Me.Panel2.Controls.Add(Me.btnEdit)
        Me.Panel2.Controls.Add(Me.btnDelete)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 802)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1126, 100)
        Me.Panel2.TabIndex = 12
        '
        'btnAssign
        '
        Me.btnAssign.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAssign.AutoSize = True
        Me.btnAssign.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnAssign.Location = New System.Drawing.Point(959, 3)
        Me.btnAssign.Name = "btnAssign"
        Me.btnAssign.Size = New System.Drawing.Size(141, 30)
        Me.btnAssign.TabIndex = 4
        Me.btnAssign.Text = "assign technician"
        Me.btnAssign.UseVisualStyleBackColor = True
        '
        'btnMarkComplete
        '
        Me.btnMarkComplete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnMarkComplete.AutoSize = True
        Me.btnMarkComplete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnMarkComplete.Location = New System.Drawing.Point(22, 46)
        Me.btnMarkComplete.Name = "btnMarkComplete"
        Me.btnMarkComplete.Size = New System.Drawing.Size(144, 30)
        Me.btnMarkComplete.TabIndex = 3
        Me.btnMarkComplete.Text = "mark as complete"
        Me.btnMarkComplete.UseVisualStyleBackColor = True
        '
        'btnRevisit
        '
        Me.btnRevisit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRevisit.AutoSize = True
        Me.btnRevisit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnRevisit.Location = New System.Drawing.Point(885, 46)
        Me.btnRevisit.Name = "btnRevisit"
        Me.btnRevisit.Size = New System.Drawing.Size(59, 30)
        Me.btnRevisit.TabIndex = 0
        Me.btnRevisit.Text = "revisit"
        Me.btnRevisit.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.AutoSize = True
        Me.btnEdit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnEdit.Location = New System.Drawing.Point(980, 46)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(45, 30)
        Me.btnEdit.TabIndex = 1
        Me.btnEdit.Text = "edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.AutoSize = True
        Me.btnDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.btnDelete.Location = New System.Drawing.Point(1041, 46)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(65, 30)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "cancel"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'descriptionText
        '
        Me.descriptionText.AutoSize = True
        Me.descriptionText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.descriptionText.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.descriptionText.Location = New System.Drawing.Point(34, 313)
        Me.descriptionText.Name = "descriptionText"
        Me.descriptionText.Size = New System.Drawing.Size(144, 27)
        Me.descriptionText.TabIndex = 11
        Me.descriptionText.Text = "description text"
        '
        'description
        '
        Me.description.AutoSize = True
        Me.description.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.description.Location = New System.Drawing.Point(34, 278)
        Me.description.Name = "description"
        Me.description.Size = New System.Drawing.Size(106, 25)
        Me.description.TabIndex = 10
        Me.description.Text = "description"
        '
        'scheduledTime
        '
        Me.scheduledTime.AutoSize = True
        Me.scheduledTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.scheduledTime.Location = New System.Drawing.Point(34, 244)
        Me.scheduledTime.Name = "scheduledTime"
        Me.scheduledTime.Size = New System.Drawing.Size(143, 25)
        Me.scheduledTime.TabIndex = 9
        Me.scheduledTime.Text = "scheduled time"
        '
        'scheduledDate
        '
        Me.scheduledDate.AutoSize = True
        Me.scheduledDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.scheduledDate.Location = New System.Drawing.Point(34, 210)
        Me.scheduledDate.Name = "scheduledDate"
        Me.scheduledDate.Size = New System.Drawing.Size(145, 25)
        Me.scheduledDate.TabIndex = 8
        Me.scheduledDate.Text = "scheduled date"
        '
        'contact
        '
        Me.contact.AutoSize = True
        Me.contact.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.contact.Location = New System.Drawing.Point(34, 177)
        Me.contact.Name = "contact"
        Me.contact.Size = New System.Drawing.Size(107, 25)
        Me.contact.TabIndex = 7
        Me.contact.Text = "contact no."
        '
        'address
        '
        Me.address.AutoSize = True
        Me.address.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.address.Location = New System.Drawing.Point(34, 124)
        Me.address.Name = "address"
        Me.address.Size = New System.Drawing.Size(107, 29)
        Me.address.TabIndex = 6
        Me.address.Text = "address"
        '
        'jobType
        '
        Me.jobType.AutoSize = True
        Me.jobType.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.jobType.Location = New System.Drawing.Point(34, 88)
        Me.jobType.Name = "jobType"
        Me.jobType.Size = New System.Drawing.Size(106, 29)
        Me.jobType.TabIndex = 5
        Me.jobType.Text = "job type"
        '
        'clientName
        '
        Me.clientName.AutoSize = True
        Me.clientName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.clientName.Location = New System.Drawing.Point(33, 33)
        Me.clientName.Name = "clientName"
        Me.clientName.Size = New System.Drawing.Size(171, 32)
        Me.clientName.TabIndex = 4
        Me.clientName.Text = "client name"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 417.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SideNavControl1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 142)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(1549, 908)
        Me.TableLayoutPanel1.TabIndex = 3
        '
        'SideNavControl1
        '
        Me.SideNavControl1.BackColor = System.Drawing.Color.White
        Me.SideNavControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SideNavControl1.Location = New System.Drawing.Point(3, 4)
        Me.SideNavControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.SideNavControl1.Name = "SideNavControl1"
        Me.SideNavControl1.Size = New System.Drawing.Size(411, 900)
        Me.SideNavControl1.TabIndex = 1
        '
        'PageLabel1
        '
        Me.PageLabel1.BackColor = System.Drawing.Color.MediumPurple
        Me.PageLabel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.PageLabel1.Location = New System.Drawing.Point(0, 0)
        Me.PageLabel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PageLabel1.Name = "PageLabel1"
        Me.PageLabel1.Size = New System.Drawing.Size(1549, 142)
        Me.PageLabel1.TabIndex = 2
        '
        'FormJobDetails
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1549, 1050)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.PageLabel1)
        Me.Name = "FormJobDetails"
        Me.Text = "FormJobDetails"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents contact As Label
    Friend WithEvents address As Label
    Friend WithEvents jobType As Label
    Friend WithEvents clientName As Label
    Friend WithEvents btnMarkComplete As Button
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnEdit As Button
    Friend WithEvents btnRevisit As Button
    Friend WithEvents scheduledDate As Label
    Friend WithEvents descriptionText As Label
    Friend WithEvents description As Label
    Friend WithEvents scheduledTime As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PageLabel1 As PageLabel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents SideNavControl1 As SideNavControl
    Friend WithEvents btnAssign As Button
    Friend WithEvents lblStatus As Label
End Class
