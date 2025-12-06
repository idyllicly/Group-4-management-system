Imports MySql.Data.MySqlClient

' ==========================================
' UI REQUIREMENT NOTES:
' 1. dgvClientList: The main DataGridView.
' 2. pnlAllInfo: A Panel/GroupBox for the "Connected Info" area.
'    Inside pnlAllInfo, add these Labels:
'      - lblFullName (Large Font)
'      - lblFullAddress
'      - lblContactInfo
'      - lblEmail
'      - lblStats (For "Active Contracts", "Total Spent", etc.)
'      - lblRecentActivity (For "Last Job", "Pending Job")
' 3. Buttons:
'      - btnAddClient
'      - btnEditClient
'      - btnCreateInquiry (The new popup trigger)
'      - btnRefresh
'      - txtSearch (TextBox)
' ==========================================

Public Class newUcClientManager

    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"
    Private _selectedID As Integer = 0
    Private _selectedName As String = ""
    Private _selectedAddress As String = ""

    Private Sub newUcClientManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadClients("")
        ClearConnectedInfo()
    End Sub

    ' === 1. LOAD IMPORTANT INFO ONLY ===
    Private Sub LoadClients(searchText As String)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' UPDATED SQL: Concatenate First/Last names for display in the grid
                ' We use COALESCE to ensure we don't get errors if a middle name is missing
                Dim sql As String = "SELECT ClientID, " &
                                    "CONCAT(ClientFirstName, ' ', ClientLastName) AS ClientName, " &
                                    "CONCAT(ContactFirstName, ' ', ContactLastName) AS ContactPerson, " &
                                    "ContactNumber, City, Barangay " &
                                    "FROM tbl_clients"

                If searchText <> "" Then
                    ' Search across new name fields
                    sql &= " WHERE ClientFirstName LIKE @s OR ClientLastName LIKE @s OR City LIKE @s OR Barangay LIKE @s"
                End If

                Dim da As New MySqlDataAdapter(sql, conn)
                If searchText <> "" Then
                    da.SelectCommand.Parameters.AddWithValue("@s", "%" & searchText & "%")
                End If

                Dim dt As New DataTable()
                da.Fill(dt)

                dgvClientList.DataSource = dt

                ' Hide ID, but keep it for logic
                If dgvClientList.Columns("ClientID") IsNot Nothing Then
                    dgvClientList.Columns("ClientID").Visible = False
                End If

                ' Styling the Grid to look cleaner
                dgvClientList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

            Catch ex As Exception
                MessageBox.Show("Error loading clients: " & ex.Message)
            End Try
        End Using
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        LoadClients(txtSearch.Text)
    End Sub

    ' === 2. THE "ALL INFO CONNECTED" SCANNER ===
    Private Sub dgvClientList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientList.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvClientList.Rows(e.RowIndex)
            _selectedID = Convert.ToInt32(row.Cells("ClientID").Value)
            _selectedName = row.Cells("ClientName").Value.ToString()

            ' Enable buttons
            btnEditClient.Enabled = True
            btnCreateInquiry.Enabled = True

            ' Trigger the deep scan of connected info
            LoadConnectedInfo(_selectedID)
        End If
    End Sub

    Private Sub LoadConnectedInfo(clientId As Integer)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()

                ' -- A. GET FULL DETAILS (Address, Email) --
                Dim sqlClient As String = "SELECT * FROM tbl_clients WHERE ClientID = @id"
                Dim cmdClient As New MySqlCommand(sqlClient, conn)
                cmdClient.Parameters.AddWithValue("@id", clientId)
                Dim reader As MySqlDataReader = cmdClient.ExecuteReader()

                If reader.Read() Then
                    ' UPDATED: Construct names from split columns
                    Dim cName As String = reader("ClientFirstName").ToString() & " " & reader("ClientLastName").ToString()
                    Dim contactName As String = reader("ContactFirstName").ToString() & " " & reader("ContactLastName").ToString()

                    lblFullName.Text = cName
                    lblContactInfo.Text = "Person: " & contactName & " | Ph: " & reader("ContactNumber").ToString()
                    lblEmail.Text = "Email: " & reader("Email").ToString()

                    ' Build Full Address
                    Dim street As String = reader("StreetAddress").ToString()
                    Dim brgy As String = reader("Barangay").ToString()
                    Dim city As String = reader("City").ToString()
                    _selectedAddress = street & ", " & brgy & ", " & city
                    lblFullAddress.Text = _selectedAddress
                End If
                reader.Close()

                ' -- B. SCAN CONTRACTS (Active Count & Balance) --
                ' Using the view 'view_contract_details' if available, or raw table
                Dim sqlContracts As String = "SELECT COUNT(*) as ActiveCount, SUM(BalanceRemaining) as TotalBalance " &
                                             "FROM view_contract_details WHERE ClientID = @id AND BalanceRemaining > 0"

                Dim cmdCon As New MySqlCommand(sqlContracts, conn)
                cmdCon.Parameters.AddWithValue("@id", clientId)
                Dim rCon As MySqlDataReader = cmdCon.ExecuteReader()

                Dim activeCount As Integer = 0
                Dim totalBalance As Decimal = 0

                If rCon.Read() Then
                    If Not IsDBNull(rCon("ActiveCount")) Then activeCount = Convert.ToInt32(rCon("ActiveCount"))
                    If Not IsDBNull(rCon("TotalBalance")) Then totalBalance = Convert.ToDecimal(rCon("TotalBalance"))
                End If
                rCon.Close()

                ' -- C. SCAN JOBS (Last Visit & Next Scheduled) --
                ' UPDATED: Removed ClientID_TempLink. Uses proper ClientID or Contract linkage.
                Dim sqlJobs As String = "SELECT ScheduledDate, Status, JobType FROM tbl_joborders " &
                                        "WHERE ClientID = @id OR ContractID IN (SELECT ContractID FROM tbl_contracts WHERE ClientID = @id) " &
                                        "ORDER BY ScheduledDate DESC LIMIT 1"

                Dim cmdJob As New MySqlCommand(sqlJobs, conn)
                cmdJob.Parameters.AddWithValue("@id", clientId)
                Dim rJob As MySqlDataReader = cmdJob.ExecuteReader()

                If rJob.Read() Then
                    Dim jobDate As Date = Convert.ToDateTime(rJob("ScheduledDate"))
                    Dim status As String = rJob("Status").ToString()
                    Dim type As String = rJob("JobType").ToString()
                    lblRecentActivity.Text = "Latest Activity: " & type & " on " & jobDate.ToShortDateString() & " [" & status & "]"
                Else
                    lblRecentActivity.Text = "No Job History found."
                End If
                rJob.Close()

            Catch ex As Exception
                lblStats.Text = "Error loading details."
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
        btnEditClient.Enabled = False
        btnCreateInquiry.Enabled = False
    End Sub

    ' === 3. BUTTONS & POP-UPS ===

    Private Sub btnAddClient_Click(sender As Object, e As EventArgs) Handles btnAddClient.Click
        ' Open the Entry Form in ADD mode (ID=0)
        Dim frm As New frmClientEntry()
        frm.ClientID = 0
        If frm.ShowDialog() = DialogResult.OK Then
            LoadClients(txtSearch.Text) ' Refresh list
        End If
    End Sub

    Private Sub btnEditClient_Click(sender As Object, e As EventArgs) Handles btnEditClient.Click
        If _selectedID = 0 Then Exit Sub

        ' Open the Entry Form in EDIT mode
        Dim frm As New frmClientEntry()
        frm.ClientID = _selectedID
        If frm.ShowDialog() = DialogResult.OK Then
            LoadClients(txtSearch.Text)
            LoadConnectedInfo(_selectedID) ' Refresh details panel
        End If
    End Sub

    Private Sub btnCreateInquiry_Click(sender As Object, e As EventArgs) Handles btnCreateInquiry.Click
        If _selectedID = 0 Then
            MessageBox.Show("Please select a client from the list first.")
            Exit Sub
        End If

        ' Open the Inquiry Popup with pre-filled data
        Dim frm As New frmInquiryPopup()
        frm.TargetClientID = _selectedID
        frm.TargetClientName = _selectedName
        frm.TargetClientAddress = _selectedAddress

        If frm.ShowDialog() = DialogResult.OK Then
            ' Maybe refresh recent activity to show the new Pending Inspection?
            LoadConnectedInfo(_selectedID)
        End If
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadClients("")
        ClearConnectedInfo()
    End Sub

End Class