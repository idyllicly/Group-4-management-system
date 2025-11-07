Imports System.Windows.Forms
' This new Imports is required
Imports Transitions

Module AppLogic
    Public Sub HandleBurgerClick(ByRef isMenuOpen As Boolean, menu As Control, topBar As Control)
        ' 1. Toggle the state
        isMenuOpen = Not isMenuOpen

        ' 2. Define where the menu should stop
        Dim targetY As Integer
        If isMenuOpen Then
            ' --- OPENING ---
            targetY = topBar.Height ' Stop below the top bar
        Else
            ' --- CLOSING ---
            targetY = -menu.Height ' Move fully off-screen
        End If

        ' 3. --- THIS IS THE MAGIC ---
        ' Animate the "Top" property of the menu, to the targetY position,
        ' using an Ease-In-Ease-Out style, over 400 milliseconds.
        Dim t As New Transition(New TransitionType_EaseInEaseOut(400))
        t.add(menu, "Top", targetY)
        t.run()

    End Sub

End Module