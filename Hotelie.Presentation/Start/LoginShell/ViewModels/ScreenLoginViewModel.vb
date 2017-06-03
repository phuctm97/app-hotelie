Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Start.LoginShell.ViewModels
	Public Class ScreenLoginViewModel
		' Dependencies

		Private ReadOnly _authentication As IAuthentication

		' Initilization

		Public Sub New( authentication As IAuthentication )
			_authentication = authentication
		End Sub

		Public Sub Login( username As String,
		                  password As String )

			' short username
			If username.Length = 0
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Information, "Nhập tên tài khoản để đăng nhập" )
				Return
			End If

			' short password
			If password.Length = 0
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Information, "Nhập mật khẩu để đăng nhập" )
				Return
			End If

			' login

			Dim err = String.Empty
			Try
				err = _authentication.TryLogin( username, password ).FirstOrDefault()
			Catch ex As DatabaseConnectionFailedException
				err = "Mất kết nối máy chủ. Không thể đăng nhập!"
			Catch ex As Exception
				err = ex.Message
			End Try

			If String.IsNullOrEmpty( err )
				' success
				IoC.Get(Of IMainWindow).SwitchShell( "workspace-shell" )
			Else
				' fail
				IoC.Get(Of IMainWindow).ShowStaticNotification( StaticNotificationType.Error, err )
			End If
		End Sub
	End Class
End Namespace
