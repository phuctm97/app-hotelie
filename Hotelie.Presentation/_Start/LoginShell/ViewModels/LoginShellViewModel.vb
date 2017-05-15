Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common

Namespace Start.LoginShell.ViewModels
	Public Class LoginShellViewModel
		Implements IShell

		Private ReadOnly _authentication As IAuthentication

		Public Property DisplayName As String Implements IHaveDisplayName.DisplayName

		Public Property Parent As Object Implements IChild.Parent

		Public Sub New( authentication As IAuthentication )
			_authentication = authentication

			DisplayName = "Login"
		End Sub

		Public Sub Login( username As String,
		                  password As String )
			Dim errors = _authentication.TryLogin( New Account With {.Username=username, .Password=password} ).ToList()

			If Not errors.Any()
				CType(Parent, IMainWindow).ShowWorkspaceShell()
			Else
				For Each [error] As String In errors
					MessageBox.Show( [error] )
				Next
			End If
		End Sub

		Public Function CanLogin( username As String,
		                          password As String ) As Boolean
			Return username.Length >= 6 And password.Length >= 6
		End Function
	End Class
End Namespace
