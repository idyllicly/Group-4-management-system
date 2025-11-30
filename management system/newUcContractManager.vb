Imports MySql.Data.MySqlClient

Public Class newUcContractManager

    ' Database Connection String
    Dim connString As String = "server=localhost;user id=root;password=;database=db_rrcms;"

    ' Variable to hold the ID of the contract the user clicked on
    Private _selectedContractID As Integer = 0

    ' 1. Load Data when the Form opens
    Private Sub newUcContractManager_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadContractList("")
    End Sub

    ' 2. Function to Load the Master List
    Private Sub LoadContractList(searchTerm As String)
        Using conn As New MySqlConnection(connString)
            Try
                conn.Open()
                ' We JOIN tables to get readable names instead of just IDs
                Dim sql As String = "SELECT " &
                                    "   Con.ContractID, " &
                                    "   Cl.ClientName, " &
                                    "   S.ServiceName, " &
                                    "   Con.ContractStatus, " &
                                    "   Con.StartDate, " &
                                    "   Con.TotalAmount, " &
                                    "   Con.BalanceRemaining " &
                                    "FROM tbl_contracts Con " &
                                    "LEFT JOIN tbl_clients Cl ON Con.ClientID = Cl.ClientID " &
                                    "LEFT JOIN tbl_services S ON Con.ServiceID = S.ServiceID " &
                                    "WHERE Cl.ClientName LIKE @search " &
                                    "ORDER BY Con.ContractID DESC"

                Dim cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddWithValue("@search", "%" & searchTerm & "%")

                Dim da As New MySqlDataAdapter(cmd)
                Dim dt As New DataTable()
                da.Fill(dt)

                dgvContracts.DataSource = dt

                ' Hide ID column (optional) but keep it for logic
                If dgvContracts.Columns("ContractID") IsNot Nothing Then
                    dgvContracts.Columns("ContractID").Visible = False
                End If

            Catch ex As Exception
                MessageBox.Show("Error loading contracts: " & ex.Message)
            End Try
        End Using
    End Sub

    ' 3. Search Button
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        LoadContractList(txtSearch.Text.Trim())
    End Sub

    ' 4. WHEN USER CLICKS A ROW -> LOAD DETAILS TABS
    Private Sub dgvContracts_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvContracts.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = dgvContracts.Rows(e.RowIndex)
            _selectedContractID = Convert.ToInt32(row.Cells("ContractID").Value)

            ' Load the specific tabs
            LoadOverview(row)
            LoadSchedule(_selectedContractID)
            LoadJobHistory(_selectedContractID)
            LoadPaymentHistory(_selectedContractID)
        End If
    End Sub

    ' === DETAIL LOADING SUBS ===

    Private Sub LoadOverview(row As DataGridViewRow)
        ' Assuming you added these labels to tpOverview
        ' You might need to make these public or friend in designer if they aren't accessible
        ' For this example, ensure you have Labels named accordingly on the Overview tab

        lblClientName.Text = "Client: " & row.Cells("ClientName").Value.ToString()
        lblService.Text = "Service: " & row.Cells("ServiceName").Value.ToString()
        lblStatus.Text = "Status: " & row.Cells("ContractStatus").Value.ToString()
        lblTotalAmount.Text = "Total: " & row.Cells("TotalAmount").Value.ToString()
        lblBalance.Text = "Balance: " & row.Cells("BalanceRemaining").Value.ToString()
    End Sub

    Private Sub LoadSchedule(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            Dim sql As String = "SELECT DueDate, AmountDue, Status, InstallmentNumber FROM tbl_paymentschedule WHERE ContractID = @id ORDER BY DueDate ASC"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", contractID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvSchedule.DataSource = dt
        End Using
    End Sub

    Private Sub LoadJobHistory(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            ' CORRECTED SQL: Changed "Status" to "J.Status" to avoid ambiguity
            Dim sql As String = "SELECT VisitNumber, ScheduledDate, J.Status, CONCAT(T.FirstName, ' ', T.LastName) as Tech " &
                                "FROM tbl_joborders J " &
                                "LEFT JOIN tbl_technicians T ON J.TechnicianID = T.TechnicianID " &
                                "WHERE J.ContractID = @id ORDER BY VisitNumber ASC"

            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", contractID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvJobHistory.DataSource = dt
        End Using
    End Sub

    Private Sub LoadPaymentHistory(contractID As Integer)
        Using conn As New MySqlConnection(connString)
            ' Shows actual money received
            Dim sql As String = "SELECT PaymentDate, AmountPaid, OR_Number FROM tbl_payments WHERE ContractID = @id ORDER BY PaymentDate DESC"
            Dim cmd As New MySqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@id", contractID)
            Dim da As New MySqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            dgvPayments.DataSource = dt
        End Using
    End Sub

    ' 5. Create New Contract Button
    Private Sub btnNewContract_Click(sender As Object, e As EventArgs) Handles btnNewContract.Click
        ' This uses your existing Helper function in the parent form to switch pages
        ' We assume the parent is frm_Main
        Dim parentForm As frm_Main = TryCast(Me.ParentForm.ParentForm, frm_Main)
        ' Note: ParentForm might need to be accessed via Me.Parent depending on nesting. 
        ' Since this UC is inside pnlContent, the immediate parent is pnlContent, its parent is TableLayout, then Form.

        ' Easier way provided in your newUcSideNav logic:
        Dim main As frm_Main = CType(Application.OpenForms("frm_Main"), frm_Main)
        If main IsNot Nothing Then
            ' Load your EXISTING creation control
            main.LoadPage(New uc_NewContractEntry(), "Create New Contract")
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class