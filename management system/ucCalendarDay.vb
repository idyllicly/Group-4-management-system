Public Class ucCalendarDay
    Public Property DayDate As Date
    Public Event DayClicked(selectedDate As Date)

    ' 1. IN THE "New()" SUB: Change the border style
    Public Sub New()
        InitializeComponent()
        ' Remove the default black border if it's set in properties
        Me.BackColor = Color.White
        Me.Padding = New Padding(1) ' Creates a small gap for the background to show through as a border
    End Sub

    Public Sub ClearData()
        lblDayNumber.Text = ""
        If flpContent IsNot Nothing Then flpContent.Controls.Clear()
        Me.BackColor = Color.White
    End Sub

    Public Sub AddJobSummary(count As Integer, jobType As String)
        If flpContent Is Nothing Then Return

        Dim lbl As New Label()
        lbl.Text = $"{count} {jobType}"
        lbl.AutoSize = False
        lbl.Width = Me.Width - 4
        lbl.Height = 16
        lbl.Font = New Font("Segoe UI", 7, FontStyle.Regular)
        lbl.TextAlign = ContentAlignment.MiddleLeft
        lbl.Margin = New Padding(1)
        lbl.Cursor = Cursors.Hand

        ' Color coding
        ' 2. IN "AddJobSummary": Update Pill Colors to match the Blue Theme
        Select Case jobType.ToLower()
            Case "service"
                ' Soft Blue Pill
                lbl.BackColor = Color.FromArgb(219, 234, 254)
                lbl.ForeColor = Color.FromArgb(30, 64, 175)
            Case "inspection"
                ' Soft Gold Pill (Complementary to Blue)
                lbl.BackColor = Color.FromArgb(254, 249, 195)
                lbl.ForeColor = Color.FromArgb(161, 98, 7)
            Case Else
                ' Soft Gray
                lbl.BackColor = Color.FromArgb(241, 245, 249)
                lbl.ForeColor = Color.FromArgb(71, 85, 105)
        End Select

        ' FIX: Ensure the label triggers the click event too
        AddHandler lbl.Click, AddressOf Element_Click
        flpContent.Controls.Add(lbl)
    End Sub

    Public Sub SetDay(d As Integer, fullDate As Date)
        lblDayNumber.Text = d.ToString()
        DayDate = fullDate
    End Sub

    ' FIX: Handle clicks from ALL sources (Base, Label, Panel)
    Private Sub Element_Click(sender As Object, e As EventArgs) Handles MyBase.Click, lblDayNumber.Click
        RaiseEvent DayClicked(DayDate)
    End Sub
End Class