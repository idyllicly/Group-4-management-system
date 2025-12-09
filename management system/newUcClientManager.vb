Imports MySql.Data.MySqlClient

Public Class newUcClientManager

    ' CONNECTION STRING
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    Private _selectedID As Integer = 0
    Private _selectedName As String = ""
    Private _selectedAddress As String = ""

    Private Sub newUcClientManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadClients("")
        ClearConnectedInfo()
    End Sub

    ' ==========================================
    ' 1. LOAD CLIENT LIST (Data Grid)
    ' ==========================================
    Private Sub LoadClients(searchText As String)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' UPDATED COLUMNS:
                ' 1. Shows only Name, Location (City/Brgy), and Contract Count
                ' 2. Removed Phone, Email, Street (as requested)
                Dim sql As String = "SELECT ClientID, " &
                                    "CONCAT(ClientFirstName, ' ', IFNULL(ClientMiddleName, ''), ' ', ClientLastName) AS 'Client Name', " &
                                    "Barangay, " &
                                    "City, " &
                                    "(SELECT COUNT(*) FROM tbl_contracts WHERE tbl_contracts.ClientID = tbl_clients.ClientID AND ContractStatus = 'Active') AS 'Active Contracts' " &
                                    "FROM tbl_clients"

                ' Search Logic: We still search hidden fields (like Phone) so you can find them!
                If searchText <> "" Then
                    sql &= " WHERE ClientFirstName LIKE @s OR ClientLastName LIKE @s OR ContactNumber LIKE @s OR City LIKE @s"
                End If

                ' SORT ORDER: ClientID DESC means "Newest Added First"
                sql &= " ORDER BY ClientID DESC"

                Dim da As New MySqlDataAdapter(sql, conn)
                If searchText <> "" Then
                    da.SelectCommand.Parameters.AddWithValue("@s", "%" & searchText & "%")
                End If

                Dim dt As New DataTable()
                da.Fill(dt)

                dgvClientList.DataSource = dt

                ' --- STYLING ---
                ' Hide ID
                If dgvClientList.Columns("ClientID") IsNot Nothing Then
                    dgvClientList.Columns("ClientID").Visible = False
                End If

                ' Make "Active Contracts" small and centered
                If dgvClientList.Columns("Active Contracts") IsNot Nothing Then
                    dgvClientList.Columns("Active Contracts").Width = 80
                    dgvClientList.Columns("Active Contracts").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                End If

                dgvClientList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                LoadConnectedInfo(_selectedID)
            Catch ex As Exception
                MessageBox.Show("Error loading clients: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadClients(txtSearch.Text)
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadClients("")
        ClearConnectedInfo()
    End Sub

    ' ==========================================
    ' 2. LOAD CONNECTED INFO (The Dashboard)
    ' ==========================================
    Private Sub dgvClientList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientList.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvClientList.Rows(e.RowIndex)
            _selectedID = Convert.ToInt32(row.Cells("ClientID").Value)
            _selectedName = row.Cells("Client Name").Value.ToString()

            ' Enable Action Buttons
            btnEditClient.Enabled = True
            btnCreateInquiry.Enabled = True

            LoadConnectedInfo(_selectedID)
        End If
    End Sub

    Private Sub LoadConnectedInfo(clientId As Integer)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' --- A. GET ALL CLIENT DETAILS ---
                Dim sqlClient As String = "SELECT * FROM tbl_clients WHERE ClientID = @id"
                Dim cmdClient As New MySqlCommand(sqlClient, conn)
                cmdClient.Parameters.AddWithValue("@id", clientId)
                Dim reader As MySqlDataReader = cmdClient.ExecuteReader()

                If reader.Read() Then
                    ' 1. MAIN NAME
                    lblFullName.Text = reader("ClientFirstName").ToString() & " " & reader("ClientLastName").ToString()

                    ' 2. CONTACT PERSON (The actual human to talk to)
                    Dim contactPerson As String = reader("ContactFirstName").ToString() & " " & reader("ContactLastName").ToString()
                    lblContactInfo.Text = "👤 Contact Person: " & contactPerson

                    ' 3. COMMUNICATION (Phone & Email combined)
                    lblEmail.Text = "📞 " & reader("ContactNumber").ToString() & vbCrLf &
                                    "✉️ " & reader("Email").ToString()

                    ' 4. FULL ADDRESS (Street + Brgy + City)
                    Dim street As String = reader("StreetAddress").ToString()
                    Dim brgy As String = reader("Barangay").ToString()
                    Dim city As String = reader("City").ToString()
                    lblFullAddress.Text = "🏠 " & street & vbCrLf & "     " & brgy & ", " & city
                End If
                reader.Close()

                ' --- B. CONTRACT STATS ---
                Dim sqlStats As String = "SELECT " &
                                         "(SELECT COUNT(*) FROM tbl_contracts WHERE ClientID = @id AND ContractStatus = 'Active') as ActiveCon, " &
                                         "(SELECT IFNULL(SUM(TotalAmount), 0) FROM tbl_contracts WHERE ClientID = @id) as LifetimeValue "

                Dim cmdStats As New MySqlCommand(sqlStats, conn)
                cmdStats.Parameters.AddWithValue("@id", clientId)
                Dim rStats As MySqlDataReader = cmdStats.ExecuteReader()

                If rStats.Read() Then
                    lblStats.Text = "Active Contracts: " & rStats("ActiveCon").ToString() & vbCrLf &
                                    "Total Value: ₱" & Convert.ToDecimal(rStats("LifetimeValue")).ToString("N2")
                End If
                rStats.Close()

                ' --- C. NEXT VISIT ---
                Dim sqlJob As String = "SELECT ScheduledDate, JobType, Status FROM tbl_joborders " &
                                       "WHERE (ClientID = @id OR ContractID IN (SELECT ContractID FROM tbl_contracts WHERE ClientID = @id)) " &
                                       "AND Status = 'Pending' " &
                                       "ORDER BY ScheduledDate ASC LIMIT 1"

                Dim cmdJob As New MySqlCommand(sqlJob, conn)
                cmdJob.Parameters.AddWithValue("@id", clientId)
                Dim rJob As MySqlDataReader = cmdJob.ExecuteReader()

                If rJob.Read() Then
                    Dim jobDate As Date = Convert.ToDateTime(rJob("ScheduledDate"))
                    lblRecentActivity.Text = "📅 NEXT VISIT: " & jobDate.ToShortDateString() & vbCrLf &
                                             "Type: " & rJob("JobType").ToString()
                    lblRecentActivity.ForeColor = Color.DarkGreen
                Else
                    lblRecentActivity.Text = "No upcoming visits."
                    lblRecentActivity.ForeColor = Color.Gray
                End If
                rJob.Close()

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End Using
    End Sub

    Private Sub ClearConnectedInfo()
        lblFullName.Text = "Select a Client"
        lblFullAddress.Text = "---"
        lblContactInfo.Text = "---"
        lblEmail.Text = "---"
        lblStats.Text = "---"
        lblRecentActivity.Text = "---"

        lblRecentActivity.ForeColor = Color.Black

        btnEditClient.Enabled = False
        btnCreateInquiry.Enabled = False
    End Sub

    ' ==========================================
    ' 3. BUTTONS
    ' ==========================================
    Private Sub btnAddClient_Click(sender As Object, e As EventArgs) Handles btnAddClient.Click
        Dim frm As New frmClientEntry()
        frm.ClientID = 0
        If frm.ShowDialog() = DialogResult.OK Then
            LoadClients(txtSearch.Text)
        End If
    End Sub

    Private Sub btnEditClient_Click(sender As Object, e As EventArgs) Handles btnEditClient.Click
        If _selectedID = 0 Then Exit Sub
        Dim frm As New frmClientEntry()
        frm.ClientID = _selectedID
        If frm.ShowDialog() = DialogResult.OK Then
            LoadClients(txtSearch.Text)
            LoadConnectedInfo(_selectedID)
        End If
    End Sub

    Private Sub btnCreateInquiry_Click(sender As Object, e As EventArgs) Handles btnCreateInquiry.Click
        If _selectedID = 0 Then
            MessageBox.Show("Please select a client first.")
            Exit Sub
        End If

        Dim frm As New frmInquiryPopup()
        frm.TargetClientID = _selectedID
        frm.TargetClientName = _selectedName
        frm.TargetClientAddress = _selectedAddress

        If frm.ShowDialog() = DialogResult.OK Then
            LoadConnectedInfo(_selectedID) ' Refresh to show new Quote count
        End If
    End Sub

    Private Sub pnlAllInfo_Enter(sender As Object, e As EventArgs) Handles pnlAllInfo.Enter

    End Sub

    ' ==========================================
    ' NAVIGATION BUTTONS
    ' ==========================================

    ' 1. GO TO CONTRACTS
    Private Sub btnGoContracts_Click(sender As Object, e As EventArgs) Handles btnGoContracts.Click
        If _selectedID = 0 Then
            MessageBox.Show("Please select a client first.")
            Exit Sub
        End If

        Dim main As frm_Main = CType(Application.OpenForms("frm_Main"), frm_Main)
        If main IsNot Nothing Then
            Dim uc As New newUcContractManager()
            uc.PresetClientID = _selectedID ' Filter by this client
            main.LoadPage(uc, "Client Contracts: " & _selectedName)
        End If
    End Sub

    ' 2. GO TO OCULARS (QUOTES)
    Private Sub btnGoOculars_Click(sender As Object, e As EventArgs) Handles btnGoOculars.Click
        If _selectedID = 0 Then
            MessageBox.Show("Please select a client first.")
            Exit Sub
        End If

        Dim main As frm_Main = CType(Application.OpenForms("frm_Main"), frm_Main)
        If main IsNot Nothing Then
            Dim uc As New newUcQuoteManager()
            uc.PresetClientID = _selectedID ' Filter by this client
            main.LoadPage(uc, "Oculars & Quotes: " & _selectedName)
        End If
    End Sub

    ' 3. GO TO BILLING (Smart Logic)
    ' This finds the ACTIVE Contract first, because Billing works by Contract ID
    Private Sub btnGoBilling_Click(sender As Object, e As EventArgs) Handles btnGoBilling.Click
        If _selectedID = 0 Then
            MessageBox.Show("Please select a client first.")
            Exit Sub
        End If

        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' Find the most recent ACTIVE contract for this client
                Dim sql As String = "SELECT ContractID FROM tbl_contracts WHERE ClientID = @id AND ContractStatus = 'Active' ORDER BY ContractID DESC LIMIT 1"
                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@id", _selectedID)

                Dim result = cmd.ExecuteScalar()

                If result IsNot Nothing Then
                    Dim activeContractID As Integer = Convert.ToInt32(result)

                    ' Navigate to Billing with that Contract pre-selected
                    Dim main As frm_Main = CType(Application.OpenForms("frm_Main"), frm_Main)
                    If main IsNot Nothing Then
                        Dim uc As New newUcBilling()
                        uc.PresetContractID = activeContractID
                        main.LoadPage(uc, "Billing: " & _selectedName)
                    End If
                Else
                    MessageBox.Show("This client has no Active Contract to bill.", "No Active Contract", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

            Catch ex As Exception
                MessageBox.Show("Error finding contract: " & ex.Message)
            End Try
        End Using
    End Sub
End Class