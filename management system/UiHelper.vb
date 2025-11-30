Imports System.Windows.Forms
Imports System.Drawing

Public Class UiHelper

    ' This Shared Sub means you can call it without creating a "New UiHelper" every time
    Public Shared Sub StyleDataGrid(ByVal dgv As DataGridView)

        ' 1. BASIC SETTINGS
        dgv.BackgroundColor = Color.White
        dgv.BorderStyle = BorderStyle.None
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.EnableHeadersVisualStyles = False ' Required to change header colors
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.MultiSelect = False

        ' 2. HEADER STYLING (Matches your App Header DarkBlue)
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dgv.ColumnHeadersHeight = 45
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

        ' 3. ROW STYLING
        dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue
        dgv.DefaultCellStyle.SelectionForeColor = Color.Black
        dgv.DefaultCellStyle.Font = New Font("Segoe UI", 9)
        dgv.RowHeadersVisible = False ' Hides the ugly empty box on the left

        ' 4. TEXT WRAPPING & AUTOSIZING (Crucial for your "Full Screen" request)
        dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        dgv.DefaultCellStyle.WrapMode = DataGridViewTriState.True

        ' This forces columns to fill the width of the screen
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

    End Sub
End Class