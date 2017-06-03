﻿Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
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
				IoC.Get(Of IMainWindow).ShowNotification( NotificationType.Information, "Nhập tên tài khoản để đăng nhập" )
				Return
			End If

			' short password
			If password.Length = 0
				IoC.Get(Of IMainWindow).ShowNotification( NotificationType.Information, "Nhập mật khẩu để đăng nhập" )
				Return
			End If

			' login
			Dim err = _authentication.TryLogin( New Account With {.Username=username, .Password=password} ).FirstOrDefault()

			If String.IsNullOrEmpty( err )
				' success
				IoC.Get(Of IMainWindow).SwitchShell( "workspace-shell" )
			Else
				' fail
				IoC.Get(Of IMainWindow).ShowNotification( NotificationType.Error, err )
			End If
		End Sub
	End Class
End Namespace
