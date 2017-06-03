Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Start.Login.Models

Namespace Start.LoginShell.ViewModels
	Public Class ScreenLoginViewModel
		Implements IChild(Of LoginShellViewModel)

		' Dependencies

		Private ReadOnly _authentication As IAuthentication

		' Shell

		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentShell As LoginShellViewModel Implements IChild(Of LoginShellViewModel).Parent
			Get
				Return CType(Parent, LoginShellViewModel)
			End Get
			Set
				Parent = value
			End Set
		End Property

		' Initilization

		Public Sub New( shell As LoginShellViewModel,
		                authentication As IAuthentication )
			ParentShell = shell
			_authentication = authentication
		End Sub

		Public Sub Login( username As String,
		                  password As String )

			' short username
			If username.Length = 0
				ParentShell.ShowNotification( NotificationType.Information, "Nhập tên tài khoản để đăng nhập" )
				Return
			End If

			' short password
			If password.Length = 0
				ParentShell.ShowNotification( NotificationType.Information, "Nhập mật khẩu để đăng nhập" )
				Return
			End If

			' login
			Dim err = _authentication.TryLogin( New Account With {.Username=username, .Password=password} ).FirstOrDefault()

			If String.IsNullOrEmpty( err )
				' success
				ParentShell.ParentWindow.SwitchShell( "workspace-shell" )
			Else
				' fail
				ParentShell.ShowNotification( NotificationType.Error, err )
			End If
		End Sub
	End Class
End Namespace
