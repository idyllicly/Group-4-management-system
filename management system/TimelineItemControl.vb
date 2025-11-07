Imports System.Windows.Forms

Public Class TimelineItemControl

    ' This is the constructor. It's automatically created.
    Public Sub New()
        InitializeComponent()
    End Sub

    ' --- THIS IS THE METHOD YOU WERE MISSING ---
    ' It takes data and puts it into the labels on this control.
    Public Sub Populate(ByVal title As String, ByVal description As String, ByVal eventDate As DateTime)

        ' Make sure your labels in the designer are named this:
        lblTitle.Text = title
        lblDescription.Text = description
        lblDate.Text = eventDate.ToShortDateString()

    End Sub

End Class