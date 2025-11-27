Public Class UC_JobCard
    ' Setup the visuals
    Public Sub SetData(title As String, time As String, bgColor As Color)
        ' 1. Set the Text
        lblTitle.Text = title
        lblTime.Text = time

        ' 2. Set the Background Color
        Me.BackColor = bgColor

        ' 3. Styling & Dimensions
        Me.Size = New Size(350, 75) ' Increased height slightly to fit both lines comfortably
        Me.Padding = New Padding(10)

        ' 4. FIX: Force the Label Positions (Layout)
        ' This ensures they don't overlap even if they are stacked in the Designer

        ' Title goes to Top-Left
        lblTitle.Location = New Point(15, 10)
        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        lblTitle.ForeColor = Color.Black
        lblTitle.BackColor = Color.Transparent ' Ensure it doesn't block background

        ' Time goes Below the Title
        lblTime.Location = New Point(15, 35)
        lblTime.AutoSize = True
        lblTime.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        lblTime.ForeColor = Color.Black
        lblTime.BackColor = Color.Transparent
    End Sub
End Class