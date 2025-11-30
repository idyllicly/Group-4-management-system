<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class uc_NewContractEntry
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
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbClient = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbService = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpStart = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.numDuration = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.btnSaveContract = New System.Windows.Forms.Button()
        Me.grpDetails = New System.Windows.Forms.GroupBox()
        Me.cmbFrequency = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.grpTimeline = New System.Windows.Forms.GroupBox()
        Me.lblEndDate = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.grpFinancials = New System.Windows.Forms.GroupBox()
        Me.lblInstallmentAmt = New System.Windows.Forms.Label()
        Me.dtpFirstPayment = New System.Windows.Forms.DateTimePicker()
        Me.lblFirstDue = New System.Windows.Forms.Label()
        Me.cmbPayInterval = New System.Windows.Forms.ComboBox()
        Me.lblInterval = New System.Windows.Forms.Label()
        Me.numInstallments = New System.Windows.Forms.NumericUpDown()
        Me.lblTerms = New System.Windows.Forms.Label()
        Me.cmbPaymentTerms = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        CType(Me.numDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDetails.SuspendLayout()
        Me.grpTimeline.SuspendLayout()
        Me.grpFinancials.SuspendLayout()
        CType(Me.numInstallments, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(21, 16)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(105, 20)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "New Contract"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Select Client:"
        '
        'cmbClient
        '
        Me.cmbClient.FormattingEnabled = True
        Me.cmbClient.Location = New System.Drawing.Point(216, 61)
        Me.cmbClient.Name = "cmbClient"
        Me.cmbClient.Size = New System.Drawing.Size(121, 28)
        Me.cmbClient.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(22, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(114, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Select Service:"
        '
        'cmbService
        '
        Me.cmbService.FormattingEnabled = True
        Me.cmbService.Location = New System.Drawing.Point(216, 104)
        Me.cmbService.Name = "cmbService"
        Me.cmbService.Size = New System.Drawing.Size(121, 28)
        Me.cmbService.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 64)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Start Date:"
        '
        'dtpStart
        '
        Me.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpStart.Location = New System.Drawing.Point(197, 64)
        Me.dtpStart.Name = "dtpStart"
        Me.dtpStart.Size = New System.Drawing.Size(176, 26)
        Me.dtpStart.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 116)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(141, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Duration (Months):"
        '
        'numDuration
        '
        Me.numDuration.Location = New System.Drawing.Point(197, 116)
        Me.numDuration.Maximum = New Decimal(New Integer() {60, 0, 0, 0})
        Me.numDuration.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numDuration.Name = "numDuration"
        Me.numDuration.Size = New System.Drawing.Size(176, 26)
        Me.numDuration.TabIndex = 8
        Me.numDuration.Value = New Decimal(New Integer() {12, 0, 0, 0})
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(204, 20)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Total Contract Value (PHP):"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(216, 39)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(121, 26)
        Me.txtAmount.TabIndex = 10
        Me.txtAmount.Text = "0.00"
        '
        'btnSaveContract
        '
        Me.btnSaveContract.Location = New System.Drawing.Point(25, 655)
        Me.btnSaveContract.Name = "btnSaveContract"
        Me.btnSaveContract.Size = New System.Drawing.Size(861, 103)
        Me.btnSaveContract.TabIndex = 11
        Me.btnSaveContract.Text = "SAVE and GENERATE SCHEDULE"
        Me.btnSaveContract.UseVisualStyleBackColor = True
        '
        'grpDetails
        '
        Me.grpDetails.Controls.Add(Me.cmbFrequency)
        Me.grpDetails.Controls.Add(Me.Label6)
        Me.grpDetails.Controls.Add(Me.Label1)
        Me.grpDetails.Controls.Add(Me.cmbClient)
        Me.grpDetails.Controls.Add(Me.Label2)
        Me.grpDetails.Controls.Add(Me.cmbService)
        Me.grpDetails.Location = New System.Drawing.Point(27, 66)
        Me.grpDetails.Name = "grpDetails"
        Me.grpDetails.Size = New System.Drawing.Size(416, 246)
        Me.grpDetails.TabIndex = 12
        Me.grpDetails.TabStop = False
        Me.grpDetails.Text = "Contract Details"
        '
        'cmbFrequency
        '
        Me.cmbFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFrequency.FormattingEnabled = True
        Me.cmbFrequency.Items.AddRange(New Object() {"Monthly", "Quarterly", "Bi-Monthly", "One-Time"})
        Me.cmbFrequency.Location = New System.Drawing.Point(216, 144)
        Me.cmbFrequency.Name = "cmbFrequency"
        Me.cmbFrequency.Size = New System.Drawing.Size(121, 28)
        Me.cmbFrequency.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(26, 145)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(144, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Service Frequency:"
        '
        'grpTimeline
        '
        Me.grpTimeline.Controls.Add(Me.lblEndDate)
        Me.grpTimeline.Controls.Add(Me.Label7)
        Me.grpTimeline.Controls.Add(Me.Label3)
        Me.grpTimeline.Controls.Add(Me.dtpStart)
        Me.grpTimeline.Controls.Add(Me.Label4)
        Me.grpTimeline.Controls.Add(Me.numDuration)
        Me.grpTimeline.Location = New System.Drawing.Point(493, 66)
        Me.grpTimeline.Name = "grpTimeline"
        Me.grpTimeline.Size = New System.Drawing.Size(393, 246)
        Me.grpTimeline.TabIndex = 13
        Me.grpTimeline.TabStop = False
        Me.grpTimeline.Text = "Timeline & Schedule"
        '
        'lblEndDate
        '
        Me.lblEndDate.AutoSize = True
        Me.lblEndDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEndDate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblEndDate.Location = New System.Drawing.Point(193, 168)
        Me.lblEndDate.Name = "lblEndDate"
        Me.lblEndDate.Size = New System.Drawing.Size(15, 20)
        Me.lblEndDate.TabIndex = 10
        Me.lblEndDate.Text = "-"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(17, 168)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(165, 20)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "End Date (Auto-Calc):"
        '
        'grpFinancials
        '
        Me.grpFinancials.Controls.Add(Me.lblInstallmentAmt)
        Me.grpFinancials.Controls.Add(Me.dtpFirstPayment)
        Me.grpFinancials.Controls.Add(Me.lblFirstDue)
        Me.grpFinancials.Controls.Add(Me.cmbPayInterval)
        Me.grpFinancials.Controls.Add(Me.lblInterval)
        Me.grpFinancials.Controls.Add(Me.numInstallments)
        Me.grpFinancials.Controls.Add(Me.lblTerms)
        Me.grpFinancials.Controls.Add(Me.cmbPaymentTerms)
        Me.grpFinancials.Controls.Add(Me.Label8)
        Me.grpFinancials.Controls.Add(Me.Label5)
        Me.grpFinancials.Controls.Add(Me.txtAmount)
        Me.grpFinancials.Location = New System.Drawing.Point(27, 335)
        Me.grpFinancials.Name = "grpFinancials"
        Me.grpFinancials.Size = New System.Drawing.Size(859, 314)
        Me.grpFinancials.TabIndex = 14
        Me.grpFinancials.TabStop = False
        Me.grpFinancials.Text = "Billing Information"
        '
        'lblInstallmentAmt
        '
        Me.lblInstallmentAmt.AutoSize = True
        Me.lblInstallmentAmt.Location = New System.Drawing.Point(6, 232)
        Me.lblInstallmentAmt.Name = "lblInstallmentAmt"
        Me.lblInstallmentAmt.Size = New System.Drawing.Size(168, 20)
        Me.lblInstallmentAmt.TabIndex = 21
        Me.lblInstallmentAmt.Text = "Amount Per Give: 0.00"
        '
        'dtpFirstPayment
        '
        Me.dtpFirstPayment.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFirstPayment.Location = New System.Drawing.Point(216, 118)
        Me.dtpFirstPayment.Name = "dtpFirstPayment"
        Me.dtpFirstPayment.Size = New System.Drawing.Size(200, 26)
        Me.dtpFirstPayment.TabIndex = 20
        '
        'lblFirstDue
        '
        Me.lblFirstDue.AutoSize = True
        Me.lblFirstDue.Location = New System.Drawing.Point(6, 118)
        Me.lblFirstDue.Name = "lblFirstDue"
        Me.lblFirstDue.Size = New System.Drawing.Size(117, 20)
        Me.lblFirstDue.TabIndex = 19
        Me.lblFirstDue.Text = "First Due Date:"
        '
        'cmbPayInterval
        '
        Me.cmbPayInterval.FormattingEnabled = True
        Me.cmbPayInterval.Items.AddRange(New Object() {"Weekly", "Bi-Weekly", "Monthly"})
        Me.cmbPayInterval.Location = New System.Drawing.Point(216, 193)
        Me.cmbPayInterval.Name = "cmbPayInterval"
        Me.cmbPayInterval.Size = New System.Drawing.Size(121, 28)
        Me.cmbPayInterval.TabIndex = 18
        '
        'lblInterval
        '
        Me.lblInterval.AutoSize = True
        Me.lblInterval.Location = New System.Drawing.Point(6, 196)
        Me.lblInterval.Name = "lblInterval"
        Me.lblInterval.Size = New System.Drawing.Size(131, 20)
        Me.lblInterval.TabIndex = 17
        Me.lblInterval.Text = "Payment Interval:"
        '
        'numInstallments
        '
        Me.numInstallments.Location = New System.Drawing.Point(216, 157)
        Me.numInstallments.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.numInstallments.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numInstallments.Name = "numInstallments"
        Me.numInstallments.Size = New System.Drawing.Size(120, 26)
        Me.numInstallments.TabIndex = 16
        Me.numInstallments.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'lblTerms
        '
        Me.lblTerms.AutoSize = True
        Me.lblTerms.Location = New System.Drawing.Point(6, 159)
        Me.lblTerms.Name = "lblTerms"
        Me.lblTerms.Size = New System.Drawing.Size(127, 20)
        Me.lblTerms.TabIndex = 15
        Me.lblTerms.Text = "Number of Gives"
        '
        'cmbPaymentTerms
        '
        Me.cmbPaymentTerms.FormattingEnabled = True
        Me.cmbPaymentTerms.Items.AddRange(New Object() {"Full Payment", "50% Downpayment", "Quarterly Payments", "Monthly"})
        Me.cmbPaymentTerms.Location = New System.Drawing.Point(216, 80)
        Me.cmbPaymentTerms.Name = "cmbPaymentTerms"
        Me.cmbPaymentTerms.Size = New System.Drawing.Size(121, 28)
        Me.cmbPaymentTerms.TabIndex = 12
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 80)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(123, 20)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Payment Terms:"
        '
        'uc_NewContractEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpFinancials)
        Me.Controls.Add(Me.grpTimeline)
        Me.Controls.Add(Me.grpDetails)
        Me.Controls.Add(Me.btnSaveContract)
        Me.Controls.Add(Me.lblTitle)
        Me.Name = "uc_NewContractEntry"
        Me.Size = New System.Drawing.Size(955, 813)
        CType(Me.numDuration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDetails.ResumeLayout(False)
        Me.grpDetails.PerformLayout()
        Me.grpTimeline.ResumeLayout(False)
        Me.grpTimeline.PerformLayout()
        Me.grpFinancials.ResumeLayout(False)
        Me.grpFinancials.PerformLayout()
        CType(Me.numInstallments, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblTitle As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents cmbClient As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents cmbService As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents dtpStart As DateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents numDuration As NumericUpDown
    Friend WithEvents Label5 As Label
    Friend WithEvents txtAmount As TextBox
    Friend WithEvents btnSaveContract As Button
    Friend WithEvents grpDetails As GroupBox
    Friend WithEvents cmbFrequency As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents grpTimeline As GroupBox
    Friend WithEvents lblEndDate As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents grpFinancials As GroupBox
    Friend WithEvents cmbPaymentTerms As ComboBox
    Friend WithEvents Label8 As Label
    Friend WithEvents lblInterval As Label
    Friend WithEvents numInstallments As NumericUpDown
    Friend WithEvents lblTerms As Label
    Friend WithEvents dtpFirstPayment As DateTimePicker
    Friend WithEvents lblFirstDue As Label
    Friend WithEvents cmbPayInterval As ComboBox
    Friend WithEvents lblInstallmentAmt As Label
End Class
