Imports System.Windows.Forms
Imports System.Drawing
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel
Imports System.IO

Public Class newUcDataSync
    Inherits UserControl

    ' Database Connection
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' UI Controls (Declared here so Logic can access them)
    Private WithEvents tcMain As TabControl
    Private tabImport As TabPage
    Private tabExport As TabPage

    ' Import Controls
    Private WithEvents btnLoadFile As Button
    Private WithEvents btnProcessImport As Button
    Private dgvPreview As DataGridView
    Private lblStatus As Label
    Private txtFilePath As TextBox
    Private cboServiceType As ComboBox
    Private loadedDataTable As DataTable

    ' Export Controls
    Private chkExportClients As CheckBox
    Private chkExportContracts As CheckBox
    Private chkExportJobs As CheckBox
    Private dtpFrom As DateTimePicker
    Private dtpTo As DateTimePicker
    Private WithEvents btnExport As Button
    Private grpExportOptions As GroupBox

    Public Sub New()
        ' Initialize the new UI Layout
        SetupUI()
    End Sub

    ' =========================================================================
    ' NEW UI SETUP (Responsive Design)
    ' =========================================================================
    Private Sub SetupUI()
        ' 1. Main UserControl Settings
        Me.Size = New Size(1000, 700)
        Me.BackColor = Color.White
        Me.Font = New Font("Segoe UI", 10)

        ' 2. Initialize Tab Control
        tcMain = New TabControl() With {
            .Dock = DockStyle.Fill,
            .ItemSize = New Size(120, 30),
            .SizeMode = TabSizeMode.Fixed
        }

        tabImport = New TabPage("Import Data") With {.BackColor = Color.White}
        tabExport = New TabPage("Export Database") With {.BackColor = Color.WhiteSmoke}

        ' --- IMPORT TAB LAYOUT ---
        Dim pnlImportContainer As New TableLayoutPanel() With {
            .Dock = DockStyle.Fill,
            .RowCount = 2,
            .ColumnCount = 1
        }

        pnlImportContainer.RowStyles.Add(New RowStyle(SizeType.AutoSize))
        pnlImportContainer.RowStyles.Add(New RowStyle(SizeType.Percent, 100))

        ' Header GroupBox
        Dim grpHeader As New GroupBox() With {
            .Text = "Import Configuration",
            .Dock = DockStyle.Fill,
            .Margin = New Padding(10),
            .AutoSize = True
        }

        ' Grid inside header
        Dim tlpHeader As New TableLayoutPanel() With {
            .Dock = DockStyle.Fill,
            .RowCount = 3,
            .ColumnCount = 2,
            .Padding = New Padding(10),
            .AutoSize = True
        }
        tlpHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 60))
        tlpHeader.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 40))

        tlpHeader.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Row 1: Labels
        tlpHeader.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Row 2: Inputs
        tlpHeader.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Row 3: Status

        ' Labels
        Dim lblFile As New Label With {
            .Text = "1. Select Excel File:", .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .Margin = New Padding(0, 0, 0, 5)
        }
        Dim lblServiceType As New Label With {
            .Text = "2. Select Service Type (Mapping):", .AutoSize = True,
            .Font = New Font("Segoe UI", 9, FontStyle.Bold),
            .Margin = New Padding(0, 0, 0, 5)
        }

        ' Input Panels
        Dim pnlFile As New TableLayoutPanel() With {
            .Dock = DockStyle.Fill, .RowCount = 1, .ColumnCount = 2,
            .Margin = New Padding(0, 0, 0, 15),
            .AutoSize = True
        }
        pnlFile.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
        pnlFile.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))

        Dim pnlService As New TableLayoutPanel() With {
            .Dock = DockStyle.Fill, .RowCount = 1, .ColumnCount = 2,
            .Margin = New Padding(0, 0, 0, 15),
            .AutoSize = True
        }
        pnlService.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 100))
        pnlService.ColumnStyles.Add(New ColumnStyle(SizeType.AutoSize))

        ' Controls
        txtFilePath = New TextBox With {
            .Dock = DockStyle.Fill, .ReadOnly = True, .BackColor = Color.WhiteSmoke,
            .Font = New Font("Segoe UI", 11)
        }
        btnLoadFile = New Button With {
            .Text = "Browse...",
            .Width = 100,
            .Height = 38,
            .BackColor = Color.DodgerBlue, .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand,
            .Margin = New Padding(5, 0, 0, 0)
        }

        ' --- FIXED COMBOBOX INITIALIZATION ---
        cboServiceType = New ComboBox With {
            .Dock = DockStyle.Fill, .DropDownStyle = ComboBoxStyle.DropDownList,
            .Font = New Font("Segoe UI", 11)
        }
        ' Added these lines back:
        cboServiceType.Items.AddRange({"Termite Control / Soil Poisoning", "Baiting System", "General Pest Control"})
        cboServiceType.SelectedIndex = 0
        ' -------------------------------------

        btnProcessImport = New Button With {
            .Text = "START IMPORT",
            .Width = 140,
            .Height = 38,
            .BackColor = Color.SeaGreen, .ForeColor = Color.White,
            .FlatStyle = FlatStyle.Flat, .Enabled = False, .Cursor = Cursors.Hand,
            .Margin = New Padding(5, 0, 0, 0)
        }

        ' Add to Panels
        pnlFile.Controls.Add(txtFilePath, 0, 0)
        pnlFile.Controls.Add(btnLoadFile, 1, 0)
        pnlService.Controls.Add(cboServiceType, 0, 0)
        pnlService.Controls.Add(btnProcessImport, 1, 0)

        ' Status Label
        lblStatus = New Label With {
            .Text = "Ready...", .AutoSize = True, .ForeColor = Color.DimGray,
            .Margin = New Padding(0, 5, 0, 0)
        }

        ' Add to Header Grid
        tlpHeader.Controls.Add(lblFile, 0, 0)
        tlpHeader.Controls.Add(lblServiceType, 1, 0)
        tlpHeader.Controls.Add(pnlFile, 0, 1)
        tlpHeader.Controls.Add(pnlService, 1, 1)
        tlpHeader.Controls.Add(lblStatus, 0, 2)
        tlpHeader.SetColumnSpan(lblStatus, 2)

        grpHeader.Controls.Add(tlpHeader)

        ' DataGridView
        dgvPreview = New DataGridView With {
            .Dock = DockStyle.Fill, .BackgroundColor = Color.White, .BorderStyle = BorderStyle.None,
            .ReadOnly = True, .ColumnHeadersHeight = 35, .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        }

        pnlImportContainer.Controls.Add(grpHeader, 0, 0)
        pnlImportContainer.Controls.Add(dgvPreview, 0, 1)
        tabImport.Controls.Add(pnlImportContainer)

        ' --- EXPORT TAB LAYOUT ---
        Dim tlpExport As New TableLayoutPanel() With {.Dock = DockStyle.Fill, .RowCount = 3, .ColumnCount = 3}
        tlpExport.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        tlpExport.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 550))
        tlpExport.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50))
        tlpExport.RowStyles.Add(New RowStyle(SizeType.Percent, 50))
        tlpExport.RowStyles.Add(New RowStyle(SizeType.Absolute, 450))
        tlpExport.RowStyles.Add(New RowStyle(SizeType.Percent, 50))

        grpExportOptions = New GroupBox With {
            .Text = "Export Configuration", .Dock = DockStyle.Fill,
            .Font = New Font("Segoe UI", 11, FontStyle.Bold), .BackColor = Color.White, .Padding = New Padding(20)
        }

        Dim flpExportItems As New FlowLayoutPanel() With {
            .Dock = DockStyle.Fill, .FlowDirection = FlowDirection.TopDown, .WrapContents = False, .AutoScroll = True
        }

        chkExportClients = New CheckBox With {.Text = "Client Master List", .AutoSize = True, .Checked = True, .Margin = New Padding(0, 0, 0, 15)}
        chkExportContracts = New CheckBox With {.Text = "Active Contracts", .AutoSize = True, .Margin = New Padding(0, 0, 0, 15)}
        chkExportJobs = New CheckBox With {.Text = "Job Schedules (Dates)", .AutoSize = True, .Margin = New Padding(0, 0, 0, 5)}

        Dim pnlDateRange As New FlowLayoutPanel() With {.AutoSize = True, .FlowDirection = FlowDirection.LeftToRight}
        Dim lblRange As New Label With {.Text = "Date Range:", .AutoSize = True, .Font = New Font("Segoe UI", 9), .Margin = New Padding(0, 5, 10, 0)}
        dtpFrom = New DateTimePicker With {.Format = DateTimePickerFormat.Short, .Width = 120, .Font = New Font("Segoe UI", 9)}
        Dim lblTo As New Label With {.Text = " to ", .AutoSize = True, .Margin = New Padding(5, 5, 5, 0)}
        dtpTo = New DateTimePicker With {.Format = DateTimePickerFormat.Short, .Width = 120, .Font = New Font("Segoe UI", 9)}

        pnlDateRange.Controls.AddRange({lblRange, dtpFrom, lblTo, dtpTo})

        btnExport = New Button With {
            .Text = "EXPORT DATA", .Height = 50, .Width = 250, .BackColor = Color.Teal,
            .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat, .Cursor = Cursors.Hand, .Margin = New Padding(0, 30, 0, 0)
        }

        flpExportItems.Controls.AddRange({chkExportClients, chkExportContracts, chkExportJobs, pnlDateRange, btnExport})
        grpExportOptions.Controls.Add(flpExportItems)
        tlpExport.Controls.Add(grpExportOptions, 1, 1)
        tabExport.Controls.Add(tlpExport)

        tcMain.TabPages.Add(tabImport)
        tcMain.TabPages.Add(tabExport)
        Me.Controls.Add(tcMain)
    End Sub

    ' =========================================================================
    ' IMPORT LOGIC (Preserved from Original)
    ' =========================================================================

    Private Sub btnLoadFile_Click(sender As Object, e As EventArgs) Handles btnLoadFile.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Excel Files|*.xlsx;*.xls"
            If ofd.ShowDialog() = DialogResult.OK Then
                txtFilePath.Text = ofd.FileName
                LoadExcelPreview(ofd.FileName)
            End If
        End Using
    End Sub

    Private Sub LoadExcelPreview(path As String)
        Try
            loadedDataTable = New DataTable()
            Using workbook As New XLWorkbook(path)
                Dim worksheet = workbook.Worksheet(1)
                Dim firstRow = True
                For Each row In worksheet.RowsUsed()
                    If firstRow Then
                        For Each cell In row.Cells()
                            loadedDataTable.Columns.Add(cell.Value.ToString())
                        Next
                        firstRow = False
                    Else
                        loadedDataTable.Rows.Add()
                        Dim i As Integer = 0
                        For Each cell In row.Cells(1, loadedDataTable.Columns.Count)
                            loadedDataTable.Rows(loadedDataTable.Rows.Count - 1)(i) = cell.Value.ToString()
                            i += 1
                        Next
                    End If
                Next
            End Using
            dgvPreview.DataSource = loadedDataTable
            btnProcessImport.Enabled = True
            lblStatus.Text = $"Loaded {loadedDataTable.Rows.Count} rows."
            lblStatus.ForeColor = Color.Blue
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnProcessImport_Click(sender As Object, e As EventArgs) Handles btnProcessImport.Click
        If loadedDataTable Is Nothing Then Return

        Dim successCount As Integer = 0
        Dim selectedService As String = cboServiceType.Text

        Using conn As New MySqlConnection(connString)
            conn.Open()

            Dim serviceID As Integer = 0
            If selectedService.Contains("Baiting") Then serviceID = 1
            If selectedService.Contains("Termite") Then serviceID = 2
            If selectedService.Contains("General") Then serviceID = 7

            For Each row As DataRow In loadedDataTable.Rows
                Try
                    ' 2. BASIC MAPPING
                    Dim clientName As String = GetValueByHeader(row, "Name", "Client Name", "Client")
                    Dim address As String = GetValueByHeader(row, "Address", "Location", "Site Address")
                    Dim contact As String = GetValueByHeader(row, "Contact", "Contact #", "No.", "Phone")
                    Dim contractStart As String = GetValueByHeader(row, "Contract Date", "Date", "Start Date")
                    Dim paymentAmount As String = GetValueByHeader(row, "Payment", "Amount", "Contract Price")
                    Dim paymentTerms As String = GetValueByHeader(row, "Terms", "Terms of Payment")

                    If String.IsNullOrWhiteSpace(clientName) Then Continue For

                    ' 3. INSERT CLIENT
                    Dim clientID As Integer = UpsertClient(conn, clientName, address, contact)

                    ' 4. INSERT CONTRACT
                    Dim contractID As Integer = 0
                    If Not String.IsNullOrWhiteSpace(contractStart) Then
                        Dim sDate As DateTime
                        If DateTime.TryParse(contractStart, sDate) Then
                            Dim amt As Decimal = 0
                            Decimal.TryParse(paymentAmount, amt)
                            contractID = UpsertContract(conn, clientID, serviceID, sDate, amt, paymentTerms)
                        End If
                    End If

                    If contractID = 0 Then contractID = GetActiveContractID(conn, clientID)

                    ' 5. HANDLE SCHEDULES
                    If contractID > 0 Then

                        ' A. BAITING LOGIC
                        If selectedService.Contains("Baiting") Then
                            For Each col As DataColumn In loadedDataTable.Columns
                                Dim header As String = col.ColumnName.ToLower()
                                If header.Contains("visit") Or header.Contains("bait") Then
                                    Dim dateVal As String = row(col).ToString()
                                    If Not String.IsNullOrWhiteSpace(dateVal) Then
                                        Dim visitDate As DateTime
                                        If DateTime.TryParse(dateVal, visitDate) Then
                                            Dim visitNum As Integer = ExtractVisitNumber(header)
                                            InsertJobOrder(conn, clientID, contractID, visitDate, "Inspection", visitNum)
                                        End If
                                    End If
                                End If
                            Next

                            ' B. TERMITE / GPC LOGIC
                        Else
                            Dim nextService As String = GetValueByHeader(row, "Next Service", "Next Schedule", "Upcoming")
                            If Not String.IsNullOrWhiteSpace(nextService) Then
                                Dim schedDate As DateTime
                                If DateTime.TryParse(nextService, schedDate) Then
                                    InsertJobOrder(conn, clientID, contractID, schedDate, "Service", 1)
                                End If
                            End If
                        End If
                    End If

                    successCount += 1

                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            Next
        End Using

        MessageBox.Show($"Import Complete! Processed {successCount} records.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' --- HELPER FUNCTIONS ---

    Private Function GetValueByHeader(row As DataRow, ParamArray possibleHeaders() As String) As String
        For Each header As String In possibleHeaders
            For Each col As DataColumn In row.Table.Columns
                If col.ColumnName.ToLower().Contains(header.ToLower()) Then
                    Return row(col).ToString()
                End If
            Next
        Next
        Return String.Empty
    End Function

    Private Function ExtractVisitNumber(header As String) As Integer
        If header.Contains("1") Then Return 1
        If header.Contains("2") Then Return 2
        If header.Contains("3") Then Return 3
        If header.Contains("4") Then Return 4
        If header.Contains("5") Then Return 5
        Return 1
    End Function

    Private Function UpsertClient(conn As MySqlConnection, name As String, addr As String, contact As String) As Integer
        Dim checkCmd As New MySqlCommand("SELECT ClientID FROM tbl_clients WHERE ClientName = @name", conn)
        checkCmd.Parameters.AddWithValue("@name", name)
        Dim result = checkCmd.ExecuteScalar()

        If result IsNot Nothing Then
            If Not String.IsNullOrEmpty(addr) Or Not String.IsNullOrEmpty(contact) Then
                Dim updSql As String = "UPDATE tbl_clients SET StreetAddress = COALESCE(@addr, StreetAddress), ContactNumber = COALESCE(@contact, ContactNumber) WHERE ClientID = @id"
                Dim updCmd As New MySqlCommand(updSql, conn)
                updCmd.Parameters.AddWithValue("@addr", If(String.IsNullOrEmpty(addr), DBNull.Value, addr))
                updCmd.Parameters.AddWithValue("@contact", If(String.IsNullOrEmpty(contact), DBNull.Value, contact))
                updCmd.Parameters.AddWithValue("@id", result)
                updCmd.ExecuteNonQuery()
            End If
            Return Convert.ToInt32(result)
        Else
            Dim sql As String = "INSERT INTO tbl_clients (ClientName, StreetAddress, ContactNumber) VALUES (@name, @addr, @contact); SELECT LAST_INSERT_ID();"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@name", name)
            cmd.Parameters.AddWithValue("@addr", addr)
            cmd.Parameters.AddWithValue("@contact", contact)
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End If
    End Function

    Private Function UpsertContract(conn As MySqlConnection, clientId As Integer, serviceId As Integer, startDate As Date, amount As Decimal, terms As String) As Integer
        Dim checkSql As String = "SELECT ContractID FROM tbl_contracts WHERE ClientID = @cid AND ServiceID = @sid AND StartDate = @date"
        Dim cmd As New MySqlCommand(checkSql, conn)
        cmd.Parameters.AddWithValue("@cid", clientId)
        cmd.Parameters.AddWithValue("@sid", serviceId)
        cmd.Parameters.AddWithValue("@date", startDate)
        Dim result = cmd.ExecuteScalar()

        If result IsNot Nothing Then
            Return Convert.ToInt32(result)
        Else
            Dim insSql As String = "INSERT INTO tbl_contracts (ClientID, ServiceID, StartDate, TotalAmount, PaymentTerms, ContractStatus) VALUES (@cid, @sid, @date, @amt, @terms, 'Active'); SELECT LAST_INSERT_ID();"
            Dim insCmd As New MySqlCommand(insSql, conn)
            insCmd.Parameters.AddWithValue("@cid", clientId)
            insCmd.Parameters.AddWithValue("@sid", serviceId)
            insCmd.Parameters.AddWithValue("@date", startDate)
            insCmd.Parameters.AddWithValue("@amt", amount)
            insCmd.Parameters.AddWithValue("@terms", terms)
            Return Convert.ToInt32(insCmd.ExecuteScalar())
        End If
    End Function

    Private Function GetActiveContractID(conn As MySqlConnection, clientID As Integer) As Integer
        Dim cmd As New MySqlCommand("SELECT ContractID FROM tbl_contracts WHERE ClientID = @cid AND ContractStatus = 'Active' ORDER BY ContractID DESC LIMIT 1", conn)
        cmd.Parameters.AddWithValue("@cid", clientID)
        Dim res = cmd.ExecuteScalar()
        If res IsNot Nothing Then Return Convert.ToInt32(res)
        Return 0
    End Function

    Private Sub InsertJobOrder(conn As MySqlConnection, clientId As Integer, contractId As Integer, schedDate As Date, type As String, visitNum As Integer)
        ' Prevent Duplicate Job Orders
        Dim checkSql As String = "SELECT JobID FROM tbl_joborders WHERE ContractID = @cid AND ScheduledDate = @date"
        Dim cmd As New MySqlCommand(checkSql, conn)
        cmd.Parameters.AddWithValue("@cid", contractId)
        cmd.Parameters.AddWithValue("@date", schedDate)

        If cmd.ExecuteScalar() Is Nothing Then
            Dim insSql As String = "INSERT INTO tbl_joborders (ClientID, ContractID, ScheduledDate, JobType, Status, VisitNumber) VALUES (@uid, @cid, @date, @type, 'Pending', @vis)"
            Dim insCmd As New MySqlCommand(insSql, conn)
            insCmd.Parameters.AddWithValue("@uid", clientId)
            insCmd.Parameters.AddWithValue("@cid", contractId)
            insCmd.Parameters.AddWithValue("@date", schedDate)
            insCmd.Parameters.AddWithValue("@type", type)
            insCmd.Parameters.AddWithValue("@vis", visitNum)
            insCmd.ExecuteNonQuery()
        End If
    End Sub

    ' =========================================================================
    ' EXPORT LOGIC (Preserved from Original)
    ' =========================================================================

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim wb As New XLWorkbook()
            Dim hasData As Boolean = False

            Using conn As New MySqlConnection(connString)
                conn.Open()

                If chkExportClients.Checked Then
                    Dim dt As New DataTable()
                    Dim adp As New MySqlDataAdapter("SELECT ClientName, ContactPerson, ContactNumber, StreetAddress, City FROM tbl_clients", conn)
                    adp.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        wb.Worksheets.Add(dt, "Clients").Columns().AdjustToContents()
                        hasData = True
                    End If
                End If

                If chkExportContracts.Checked Then
                    Dim dt As New DataTable()
                    Dim sql As String = "SELECT c.ClientName, s.ServiceName, con.StartDate, con.TotalAmount, con.PaymentTerms, con.ContractStatus " &
                                        "FROM tbl_contracts con " &
                                        "JOIN tbl_clients c ON con.ClientID = c.ClientID " &
                                        "JOIN tbl_services s ON con.ServiceID = s.ServiceID " &
                                        "WHERE con.ContractStatus = 'Active'"
                    Dim adp As New MySqlDataAdapter(sql, conn)
                    adp.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        wb.Worksheets.Add(dt, "Contracts").Columns().AdjustToContents()
                        hasData = True
                    End If
                End If

                If chkExportJobs.Checked Then
                    Dim dt As New DataTable()
                    Dim sql As String = "SELECT j.ScheduledDate, j.JobType, j.VisitNumber, j.Status, c.ClientName, c.StreetAddress " &
                                        "FROM tbl_joborders j " &
                                        "LEFT JOIN tbl_contracts con ON j.ContractID = con.ContractID " &
                                        "LEFT JOIN tbl_clients c ON con.ClientID = c.ClientID " &
                                        "WHERE j.ScheduledDate BETWEEN @d1 AND @d2 ORDER BY j.ScheduledDate ASC"
                    Dim cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@d1", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@d2", dtpTo.Value.Date)
                    Dim adp As New MySqlDataAdapter(cmd)
                    adp.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        wb.Worksheets.Add(dt, "Job Schedules").Columns().AdjustToContents()
                        hasData = True
                    End If
                End If
            End Using

            If hasData Then
                Dim sfd As New SaveFileDialog()
                sfd.Filter = "Excel Workbook|*.xlsx"
                sfd.FileName = $"RRCMS_Data_{DateTime.Now:yyyy-MM-dd}.xlsx"
                If sfd.ShowDialog() = DialogResult.OK Then
                    wb.SaveAs(sfd.FileName)
                    MessageBox.Show("Export Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                MessageBox.Show("No data found to export.", "Warning")
            End If

        Catch ex As Exception
            MessageBox.Show("Export Error: " & ex.Message)
        End Try
    End Sub

End Class