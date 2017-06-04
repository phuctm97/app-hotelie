Imports Hotelie.Application.Services.Authentication

Namespace Tests.Services.Authentication
	Public Class Authentication
		Implements IAuthentication

		Public Property LoggedAccount As Account Implements IAuthentication.LoggedAccount

		Public Property LoggedIn As Boolean Implements IAuthentication.LoggedIn

		Public Sub Logout() Implements IAuthentication.Logout
			LoggedAccount = Nothing
			LoggedIn = False
		End Sub

		Public Iterator Function TryLogin( username As String,
		                                   password As String ) As IEnumerable(Of String) Implements IAuthentication.TryLogin
			If username <> "username"
				Yield "Tài khoản không tồn tại"
			End If

			If password <> "password"
				Yield "Sai mật khẩu"
			End If

			LoggedAccount = New Account With {.Username = username}
		End Function

		Public Async Function TryLoginAsync( username As String,
		                                     password As String ) As Task(Of IEnumerable(Of String)) _
			Implements IAuthentication.TryLoginAsync
			Return Await Task.Run( Function() TryLoginAsync( username, password ) )
		End Function
	End Class
End Namespace
