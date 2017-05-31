Namespace Services.Authentication
	Public Interface IAuthentication
		ReadOnly Property LoggedIn As Boolean

		Property LoggedAccount As Account

		Function TryLogin( account As Account ) As IEnumerable(Of String)
        Sub Logout()

	End Interface
End Namespace