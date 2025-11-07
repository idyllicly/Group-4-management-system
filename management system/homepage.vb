
Imports Transitions

Public Class homepage

    Private isMenuOpen As Boolean = False


    Private Sub NavigationControl2_Load(sender As Object, e As EventArgs) Handles NavigationControl2.Load
        NavigationControl2.BringToFront()
    End Sub

    Private Sub homepage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub NavigationControl2_BurgerMenuClicked(sender As Object, e As EventArgs) Handles NavigationControl2.BurgerMenuClicked
        ' This line toggles the visibility
        'menu.Visible = Not menu.Visible

        ' This is important to make sure it appears on top of other controls
        'menu.BringToFront()

        'try
        AppLogic.HandleBurgerClick(isMenuOpen, menu, NavigationControl2)
    End Sub

    Private Sub menu_Load(sender As Object, e As EventArgs) Handles menu.Load

    End Sub
End Class