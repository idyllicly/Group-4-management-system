Imports MySql.Data.MySqlClient

Public Class newUcQuoteManager

    ' ⚠️ CHECK YOUR CONNECTION STRING
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    Private _selectedQuoteID As Integer = 0

    Private Sub newUcQuoteManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateAutoExpire() ' 1. Check for expired quotes immediately on load
        LoadQuotations()   ' 2. Then load the list
        FormatGrid()
    End Sub

    ' ==========================================
    ' 0. AUTO-EXPIRE LOGIC
    ' ==========================================
    Private Sub UpdateAutoExpire()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' SQL Update: Set Status to 'Unresponsive' if it is 'Pending' 
                ' AND the difference between NOW and DateCreated is >= 30 days.
                Dim sql As String = "UPDATE tbl_quotations " &
                                    "SET Status = 'Unresponsive' " &
                                    "WHERE Status = 'Pending' AND DATEDIFF(NOW(), DateCreated) >= 30"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Console.WriteLine("Auto-expire check failed: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 1. LOAD DATA
    ' ==========================================
    Private Sub LoadQuotations()
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' JOIN: Get Client Name and Service Name for display
                Dim sql As String = "SELECT " &
                                    "   Q.QuoteID, " &
                                    "   Q.DateCreated, " &
                                    "   C.ClientName, " &
                                    "   S.ServiceName AS 'Proposed Package', " &
                                    "   Q.AreaSize_Sqm, " &
                                    "   Q.InfestationLevel, " &
                                    "   Q.QuotedPrice, " &
                                    "   Q.Remarks, " &
                                    "   Q.Status " &
                                    "FROM tbl_quotations Q " &
                                    "LEFT JOIN tbl_clients C ON Q.ClientID = C.ClientID " &
                                    "LEFT JOIN tbl_services S ON Q.ProposedService = S.ServiceID " &
                                    "ORDER BY Q.DateCreated DESC"

                Dim da As New MySqlDataAdapter(sql, conn)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvQuotes.DataSource = dt

            Catch ex As Exception
                MessageBox.Show("Error loading quotes: " & ex.Message)
            End Try
        End Using
    End Sub

    ' ==========================================
    ' 2. GRID STYLING
    ' ==========================================
    Private Sub FormatGrid()
        If dgvQuotes.Columns("QuoteID") IsNot Nothing Then dgvQuotes.Columns("QuoteID").Visible = False

        If dgvQuotes.Columns("QuotedPrice") IsNot Nothing Then
            dgvQuotes.Columns("QuotedPrice").DefaultCellStyle.Format = "N2"
            dgvQuotes.Columns("QuotedPrice").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        dgvQuotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgvQuotes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvQuotes.ReadOnly = True
    End Sub

    ' ==========================================
    ' 3. REFRESH BUTTON
    ' ==========================================
    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        UpdateAutoExpire()
        LoadQuotations()
    End Sub

    ' ==========================================
    ' 4. HANDLE SELECTION
    ' ==========================================
    Private Sub dgvQuotes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvQuotes.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvQuotes.Rows(e.RowIndex)
            _selectedQuoteID = Convert.ToInt32(row.Cells("QuoteID").Value)
            ' Optional: If you have an Approve button, enable it here
            ' btnApprove.Enabled = True 
        End If
    End Sub

    ' ==========================================
    ' 5. APPROVE QUOTE (NAVIGATE TO CONTRACT)
    ' ==========================================
    Private Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        If _selectedQuoteID = 0 Then
            MessageBox.Show("Please select a quotation to approve.")
            Exit Sub
        End If

        If MessageBox.Show("Client Accepted? Proceed to create contract?", "Confirm Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then

            ' A. FETCH QUOTE DATA
            Dim clientID As Integer = 0
            Dim serviceID As Integer = 0
            Dim price As Decimal = 0

            Using conn As New MySqlConnection(connString)
                Try
                    conn.Open()
                    Dim sqlGet As String = "SELECT ClientID, ProposedService, QuotedPrice FROM tbl_quotations WHERE QuoteID = @qid"
                    Dim cmdGet As New MySqlCommand(sqlGet, conn)
                    cmdGet.Parameters.AddWithValue("@qid", _selectedQuoteID)

                    Dim dt As New DataTable()
                    Dim da As New MySqlDataAdapter(cmdGet)
                    da.Fill(dt)

                    If dt.Rows.Count > 0 Then
                        Dim row As DataRow = dt.Rows(0)
                        clientID = If(IsDBNull(row("ClientID")), 0, Convert.ToInt32(row("ClientID")))
                        serviceID = If(IsDBNull(row("ProposedService")), 0, Convert.ToInt32(row("ProposedService")))
                        price = Convert.ToDecimal(row("QuotedPrice"))
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error fetching quote: " & ex.Message)
                    Exit Sub
                End Try
            End Using

            ' B. NAVIGATE TO CONTRACT ENTRY
            Dim mainForm As frm_Main = TryCast(Me.ParentForm, frm_Main)

            If mainForm IsNot Nothing Then
                Dim newContractPage As New uc_NewContractEntry()

                ' Pass the data!
                newContractPage.PreFillData(clientID, serviceID, price, _selectedQuoteID)

                ' Switch Screen
                mainForm.LoadPage(newContractPage, "Create Contract (From Quote)")
            Else
                MessageBox.Show("Error: Cannot find Main Form to navigate.")
            End If

        End If
    End Sub

    ' ==========================================
    ' 6. COLOR CODING
    ' ==========================================
    Private Sub dgvQuotes_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvQuotes.CellFormatting
        If dgvQuotes.Columns(e.ColumnIndex).Name = "Status" Then
            Dim status As String = Convert.ToString(e.Value)
            If status = "Approved" Then
                e.CellStyle.BackColor = Color.LightGreen
                e.CellStyle.ForeColor = Color.DarkGreen
            ElseIf status = "Pending" Then
                e.CellStyle.BackColor = Color.LightYellow
                e.CellStyle.ForeColor = Color.Orange
            ElseIf status = "Unresponsive" Then
                e.CellStyle.BackColor = Color.LightGray
                e.CellStyle.ForeColor = Color.DimGray
            End If
        End If
    End Sub

End Class