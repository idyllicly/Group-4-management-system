Imports System.Windows.Forms
Imports System.Drawing
Imports MySql.Data.MySqlClient
Imports ClosedXML.Excel
Imports System.IO

Public Class newUcDataSync
    ' Database Connection String
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' Variable to hold the raw data from Excel
    Private loadedDataTable As DataTable

    ' =========================================================================
    ' 1. LOAD FILE LOGIC
    ' =========================================================================

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
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
                        ' Create Columns
                        For Each cell In row.Cells()
                            loadedDataTable.Columns.Add(cell.Value.ToString())
                        Next
                        firstRow = False
                    Else
                        ' Add Data
                        loadedDataTable.Rows.Add()
                        Dim i As Integer = 0
                        For Each cell In row.Cells(1, loadedDataTable.Columns.Count)
                            loadedDataTable.Rows(loadedDataTable.Rows.Count - 1)(i) = cell.Value.ToString()
                            i += 1
                        Next
                    End If
                Next
            End Using

            ' Show in Grid
            dgvPreview.DataSource = loadedDataTable
            lblStatus.Text = $"Loaded {loadedDataTable.Rows.Count} rows."
            lblStatus.ForeColor = Color.Blue
            btnImport.Enabled = True

        Catch ex As Exception
            MessageBox.Show("Error reading Excel file: " & ex.Message)
        End Try
    End Sub

    ' =========================================================================
    ' 2. MAIN IMPORT DISPATCHER
    ' =========================================================================

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        If loadedDataTable Is Nothing OrElse loadedDataTable.Rows.Count = 0 Then
            MessageBox.Show("Please load an Excel file first.")
            Return
        End If

        ' Disable UI
        btnImport.Enabled = False
        pbImport.Visible = True
        pbImport.Value = 0
        pbImport.Maximum = loadedDataTable.Rows.Count

        Dim successCount As Integer = 0
        Dim profile As String = cboImportProfile.Text

        Using conn As New MySqlConnection(connString)
            conn.Open()
            Dim trans As MySqlTransaction = conn.BeginTransaction()

            Try
                ' Switch logic based on file type
                Select Case profile
                    Case "Baiting System"
                        successCount = ImportBaiting(conn, trans)
                    Case "Termite Control / Soil Poisoning"
                        successCount = ImportTermite(conn, trans)
                    Case "General Pest Control (GPC)"
                        successCount = ImportGPC(conn, trans)
                    Case "Ocular / Inspection"
                        successCount = ImportOcular(conn, trans)
                    Case Else
                        MessageBox.Show("Please select an Import Profile from the dropdown.")
                        trans.Rollback()
                        Return
                End Select

                trans.Commit()
                MessageBox.Show($"Success! Imported {successCount} records.", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As Exception
                trans.Rollback()
                MessageBox.Show("Critical Error: " & ex.Message & vbCrLf & "All changes rolled back.", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                ' Reset UI
                btnImport.Enabled = True
                pbImport.Visible = False
                lblStatus.Text = "Ready."
            End Try
        End Using
    End Sub

    ' =========================================================================
    ' 3. SPECIFIC IMPORT LOGIC
    ' =========================================================================

    ' A. BAITING SYSTEM IMPORT
    Private Function ImportBaiting(conn As MySqlConnection, trans As MySqlTransaction) As Integer
        Dim count As Integer = 0
        Dim serviceID As Integer = 1 ' Baiting ID

        For Each row As DataRow In loadedDataTable.Rows
            Try
                Dim name As String = GetValueByHeader(row, "Name")
                Dim address As String = GetValueByHeader(row, "Address", "Location")

                If String.IsNullOrWhiteSpace(name) Then Continue For

                Dim clientID As Integer = UpsertClient(conn, trans, name, address, "")
                Dim contractID As Integer = UpsertContract(conn, trans, clientID, serviceID, "", "0")

                ' Loop columns to find visits
                For Each col As DataColumn In loadedDataTable.Columns
                    Dim header As String = col.ColumnName.ToLower()
                    If header.Contains("visit") Or header.Contains("bait") Then
                        Dim dateVal As String = row(col).ToString()
                        Dim visitDate As Date
                        If Date.TryParse(dateVal, visitDate) Then
                            Dim status As String = If(visitDate < Date.Today, "Completed", "Pending")
                            InsertJobOrder(conn, trans, clientID, contractID, visitDate, "Inspection", 1, status)
                        End If
                    End If
                Next
                count += 1
                pbImport.PerformStep()
            Catch : End Try
        Next
        Return count
    End Function

    ' B. TERMITE / SOIL POISONING IMPORT
    Private Function ImportTermite(conn As MySqlConnection, trans As MySqlTransaction) As Integer
        Dim count As Integer = 0
        Dim serviceID As Integer = 2 ' Termite ID

        For Each row As DataRow In loadedDataTable.Rows
            Try
                Dim name As String = GetValueByHeader(row, "Name")
                Dim contact As String = GetValueByHeader(row, "Contact")
                Dim startDateStr As String = GetValueByHeader(row, "Start", "Contract Date")
                Dim amountStr As String = GetValueByHeader(row, "Payment", "Amount")

                If String.IsNullOrWhiteSpace(name) Then Continue For

                Dim clientID As Integer = UpsertClient(conn, trans, name, "", contact)
                Dim contractID As Integer = UpsertContract(conn, trans, clientID, serviceID, startDateStr, amountStr)

                ' Find Next Service
                Dim nextSvc As String = GetValueByHeader(row, "Next Service", "Remarks")
                ' Clean up date strings like "10/30/2026 (M&V)"
                Dim cleanDateStr = System.Text.RegularExpressions.Regex.Split(nextSvc, "\s|\(")(0)
                Dim jobDate As Date

                If Date.TryParse(cleanDateStr, jobDate) Then
                    InsertJobOrder(conn, trans, clientID, contractID, jobDate, "Service", 1)
                End If

                count += 1
                pbImport.PerformStep()
            Catch : End Try
        Next
        Return count
    End Function

    ' C. GPC IMPORT
    Private Function ImportGPC(conn As MySqlConnection, trans As MySqlTransaction) As Integer
        Dim count As Integer = 0
        Dim serviceID As Integer = 7 ' GPC ID

        For Each row As DataRow In loadedDataTable.Rows
            Try
                Dim name As String = GetValueByHeader(row, "Client", "Name")
                Dim contact As String = GetValueByHeader(row, "Contact")
                Dim startStr As String = GetValueByHeader(row, "START")
                Dim amountStr As String = GetValueByHeader(row, "Payment")

                If String.IsNullOrWhiteSpace(name) Then Continue For

                Dim clientID As Integer = UpsertClient(conn, trans, name, "", contact)
                Dim contractID As Integer = UpsertContract(conn, trans, clientID, serviceID, startStr, amountStr)

                ' Look for "Next Service"
                Dim nextSvcStr As String = GetValueByHeader(row, "Next Service", "Upcoming")
                Dim nextDate As Date
                If Date.TryParse(nextSvcStr, nextDate) Then
                    InsertJobOrder(conn, trans, clientID, contractID, nextDate, "Service", 1)
                End If

                count += 1
                pbImport.PerformStep()
            Catch : End Try
        Next
        Return count
    End Function

    ' D. OCULAR / INSPECTION IMPORT
    Private Function ImportOcular(conn As MySqlConnection, trans As MySqlTransaction) As Integer
        Dim count As Integer = 0
        ' No Contract needed

        For Each row As DataRow In loadedDataTable.Rows
            Try
                Dim name As String = GetValueByHeader(row, "NAME")
                Dim dateVal As String = GetValueByHeader(row, "DATE")
                Dim updateCol As String = GetValueByHeader(row, "UPDATE")

                If String.IsNullOrWhiteSpace(name) Then Continue For

                Dim clientID As Integer = UpsertClient(conn, trans, name, "", "")

                ' Map Status
                Dim status As String = "Pending"
                If updateCol.ToUpper().Contains("POSITIVE") Or updateCol.ToUpper().Contains("DONE") Then status = "Completed"
                If updateCol.ToUpper().Contains("NO RESP") Then status = "Unresponsive"

                Dim jobDate As Date
                If Date.TryParse(dateVal, jobDate) Then
                    ' Pass 0 as ContractID
                    InsertJobOrder(conn, trans, clientID, 0, jobDate, "Inspection", 1, status)
                End If

                count += 1
                pbImport.PerformStep()
            Catch : End Try
        Next
        Return count
    End Function

    ' =========================================================================
    ' 4. DATABASE HELPER FUNCTIONS
    ' =========================================================================

    Private Function UpsertClient(conn As MySqlConnection, trans As MySqlTransaction, rawName As String, addr As String, contact As String) As Integer
        ' Split First and Last Name
        Dim parts As String() = rawName.Trim().Split(" "c)
        Dim lname As String = ""
        Dim fname As String = rawName

        If parts.Length > 1 Then
            lname = parts(parts.Length - 1)
            fname = rawName.Substring(0, rawName.LastIndexOf(" "c)).Trim()
        End If

        ' Check if client exists
        Dim checkSql As String = "SELECT ClientID FROM tbl_clients WHERE ClientLastName = @l AND ClientFirstName = @f LIMIT 1"
        Dim checkCmd As New MySqlCommand(checkSql, conn, trans)
        checkCmd.Parameters.AddWithValue("@l", lname)
        checkCmd.Parameters.AddWithValue("@f", fname)

        Dim result = checkCmd.ExecuteScalar()

        If result IsNot Nothing Then
            ' Update Contact/Address if provided
            If Not String.IsNullOrEmpty(addr) Or Not String.IsNullOrEmpty(contact) Then
                Dim updSql As String = "UPDATE tbl_clients SET StreetAddress = COALESCE(StreetAddress, @a), ContactNumber = COALESCE(ContactNumber, @c) WHERE ClientID = @id"
                Dim updCmd As New MySqlCommand(updSql, conn, trans)
                updCmd.Parameters.AddWithValue("@a", If(String.IsNullOrEmpty(addr), DBNull.Value, addr))
                updCmd.Parameters.AddWithValue("@c", If(String.IsNullOrEmpty(contact), DBNull.Value, contact))
                updCmd.Parameters.AddWithValue("@id", result)
                updCmd.ExecuteNonQuery()
            End If
            Return Convert.ToInt32(result)
        Else
            ' Insert New
            Dim insSql As String = "INSERT INTO tbl_clients (ClientFirstName, ClientLastName, StreetAddress, ContactNumber) VALUES (@f, @l, @a, @c); SELECT LAST_INSERT_ID();"
            Dim insCmd As New MySqlCommand(insSql, conn, trans)
            insCmd.Parameters.AddWithValue("@f", fname)
            insCmd.Parameters.AddWithValue("@l", lname)
            insCmd.Parameters.AddWithValue("@a", If(String.IsNullOrEmpty(addr), DBNull.Value, addr))
            insCmd.Parameters.AddWithValue("@c", If(String.IsNullOrEmpty(contact), DBNull.Value, contact))
            Return Convert.ToInt32(insCmd.ExecuteScalar())
        End If
    End Function

    Private Function UpsertContract(conn As MySqlConnection, trans As MySqlTransaction, cid As Integer, sid As Integer, startStr As String, amountStr As String) As Integer
        ' Parse Date
        Dim sDate As Date
        If Not Date.TryParse(startStr, sDate) Then sDate = Date.Today

        ' Parse Amount
        Dim amt As Decimal = 0
        Dim cleanAmt As String = System.Text.RegularExpressions.Regex.Replace(amountStr, "[^0-9.]", "")
        Decimal.TryParse(cleanAmt, amt)

        ' Check Duplicate Active Contract
        Dim checkSql As String = "SELECT ContractID FROM tbl_contracts WHERE ClientID = @c AND ServiceID = @s AND ContractStatus = 'Active' LIMIT 1"
        Dim checkCmd As New MySqlCommand(checkSql, conn, trans)
        checkCmd.Parameters.AddWithValue("@c", cid)
        checkCmd.Parameters.AddWithValue("@s", sid)
        Dim result = checkCmd.ExecuteScalar()

        If result IsNot Nothing Then
            Return Convert.ToInt32(result)
        Else
            Dim insSql As String = "INSERT INTO tbl_contracts (ClientID, ServiceID, StartDate, TotalAmount, ContractStatus, ServiceFrequency) VALUES (@c, @s, @d, @a, 'Active', 'Monthly'); SELECT LAST_INSERT_ID();"
            Dim insCmd As New MySqlCommand(insSql, conn, trans)
            insCmd.Parameters.AddWithValue("@c", cid)
            insCmd.Parameters.AddWithValue("@s", sid)
            insCmd.Parameters.AddWithValue("@d", sDate)
            insCmd.Parameters.AddWithValue("@a", amt)
            Return Convert.ToInt32(insCmd.ExecuteScalar())
        End If
    End Function

    Private Sub InsertJobOrder(conn As MySqlConnection, trans As MySqlTransaction, cid As Integer, conId As Integer, sDate As Date, type As String, visit As Integer, Optional status As String = "Pending")
        ' Check Duplicate Job
        Dim checkSql As String = "SELECT JobID FROM tbl_joborders WHERE ClientID = @c AND ScheduledDate = @d"
        Dim checkCmd As New MySqlCommand(checkSql, conn, trans)
        checkCmd.Parameters.AddWithValue("@c", cid)
        checkCmd.Parameters.AddWithValue("@d", sDate)

        If checkCmd.ExecuteScalar() Is Nothing Then
            Dim sql As String = "INSERT INTO tbl_joborders (ClientID, ContractID, ScheduledDate, JobType, Status, VisitNumber) VALUES (@c, @con, @d, @t, @s, @v)"
            Dim cmd As New MySqlCommand(sql, conn, trans)
            cmd.Parameters.AddWithValue("@c", cid)
            cmd.Parameters.AddWithValue("@con", If(conId = 0, DBNull.Value, conId))
            cmd.Parameters.AddWithValue("@d", sDate)
            cmd.Parameters.AddWithValue("@t", type)
            cmd.Parameters.AddWithValue("@s", status)
            cmd.Parameters.AddWithValue("@v", visit)
            cmd.ExecuteNonQuery()
        End If
    End Sub

    ' Helper to safely get value from DataRow by fuzzy column match
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

    ' =========================================================================
    ' 5. EXPORT LOGIC
    ' =========================================================================

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim wb As New XLWorkbook()
            Dim hasData As Boolean = False

            Using conn As New MySqlConnection(connString)
                conn.Open()

                ' Export Clients
                If chkExportClients.Checked Then
                    Dim dt As New DataTable()
                    Dim sql As String = "SELECT ClientFirstName, ClientLastName, ContactNumber, StreetAddress, City FROM tbl_clients"
                    Dim adp As New MySqlDataAdapter(sql, conn)
                    adp.Fill(dt)
                    If dt.Rows.Count > 0 Then
                        wb.Worksheets.Add(dt, "Clients").Columns().AdjustToContents()
                        hasData = True
                    End If
                End If

                ' Export Contracts
                If chkExportContracts.Checked Then
                    Dim dt As New DataTable()
                    Dim sql As String = "SELECT CONCAT(c.ClientFirstName, ' ', c.ClientLastName) AS Client, " &
                                        "s.ServiceName, con.StartDate, con.TotalAmount " &
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

                ' Export Jobs
                If chkExportJobs.Checked Then
                    Dim dt As New DataTable()
                    Dim sql As String = "SELECT j.ScheduledDate, j.JobType, j.VisitNumber, j.Status, " &
                                        "CONCAT(c.ClientFirstName, ' ', c.ClientLastName) AS Client, c.StreetAddress " &
                                        "FROM tbl_joborders j " &
                                        "LEFT JOIN tbl_clients c ON j.ClientID = c.ClientID " &
                                        "WHERE j.ScheduledDate BETWEEN @d1 AND @d2 ORDER BY j.ScheduledDate ASC"

                    Dim cmd As New MySqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@d1", dtpFrom.Value.Date)
                    cmd.Parameters.AddWithValue("@d2", dtpTo.Value.Date.AddDays(1).AddSeconds(-1))

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