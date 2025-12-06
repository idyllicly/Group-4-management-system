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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(newUcPageHeader))
        Me.lblPageTitle = New System.Windows.Forms.Label()
        Me.btnBell = New System.Windows.Forms.PictureBox()
        Me.lblBadge = New System.Windows.Forms.Label()
        Me.timerNotifCheck = New System.Windows.Forms.Timer(Me.components)
        CType(Me.btnBell, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'btnBell
        '
        Me.btnBell.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBell.Image = CType(resources.GetObject("btnBell.Image"), System.Drawing.Image)
        Me.btnBell.Location = New System.Drawing.Point(861, 227)
        Me.btnBell.Name = "btnBell"
        Me.btnBell.Size = New System.Drawing.Size(120, 118)
        Me.btnBell.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.btnBell.TabIndex = 4
        Me.btnBell.TabStop = False
        '
        'lblBadge
        '
        Me.lblBadge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBadge.BackColor = System.Drawing.Color.Crimson
        Me.lblBadge.ForeColor = System.Drawing.Color.White
        Me.lblBadge.Location = New System.Drawing.Point(935, 253)
        Me.lblBadge.Name = "lblBadge"
        Me.lblBadge.Size = New System.Drawing.Size(27, 25)
        Me.lblBadge.TabIndex = 5
        '
        'timerNotifCheck
        '
        Me.timerNotifCheck.Interval = 5000
        '
        'newUcPageHeader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkBlue
        Me.Controls.Add(Me.lblBadge)
        Me.Controls.Add(Me.btnBell)
        Me.Controls.Add(Me.lblPageTitle)
        Me.Name = "newUcPageHeader"
        Me.Size = New System.Drawing.Size(1024, 376)
        CType(Me.btnBell, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents lblPageTitle As Label
    Friend WithEvents btnBell As PictureBox
    Friend WithEvents lblBadge As Label
    Friend WithEvents timerNotifCheck As Timer
End Class
