Imports Hotelie.Application.Services.Authentication

Namespace Tests
	Public Class Authentication
		Implements IAuthentication

		Public Property LoggedAccount As Account Implements IAuthentication.LoggedAccount

		Public Property LoggedIn As Boolean Implements IAuthentication.LoggedIn

		Public Iterator Function TryLogin( account As Account ) As IEnumerable(Of String) Implements IAuthentication.TryLogin
			If account.Username <> "username"
				Yield "Tài khoản không tồn tại"
			End If

			If account.Password <> "password"
				Yield "Sai mật khẩu"
			End If

			LoggedAccount = New Account With {.Username = account.Username, .Password = account.Password}
		End Function
	End Class
End Namespace
