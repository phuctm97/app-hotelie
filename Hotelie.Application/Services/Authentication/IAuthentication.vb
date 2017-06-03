Namespace Services.Authentication
	Public Interface IAuthentication
		ReadOnly Property LoggedIn As Boolean

		Property LoggedAccount As Account

		Function TryLogin( username As String, password As String ) As IEnumerable(Of String)
        Sub Logout()

	End Interface
End Namespace