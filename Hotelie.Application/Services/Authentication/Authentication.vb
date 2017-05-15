Namespace Services.Authentication
	Public Class Authentication
		Implements IAuthentication

		Public Property LoggedAccount As Account Implements IAuthentication.LoggedAccount

		Public ReadOnly Property LoggedIn As Boolean Implements IAuthentication.LoggedIn
			Get
				Return LoggedAccount IsNot Nothing
			End Get
		End Property

		Public Function TryLogin( account As Account ) As IEnumerable(Of String) Implements IAuthentication.TryLogin
			Dim errors = New List(Of String)

			If Not String.Equals( account.Username, "phuctm97" ) Then errors.Add( "Wrong username" )
			If Not String.Equals( account.Password, "123456" ) Then errors.Add( "Wrong password" )

			If Not errors.Any() Then LoggedAccount = account

			Return errors
		End Function
	End Class
End Namespace
