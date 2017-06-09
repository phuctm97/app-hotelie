Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Start.LoginShell.ViewModels
	Public Class ScreenLoginViewModel
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _authentication As IAuthentication

		' Bind models
		Public ReadOnly Property InitialAccount As String

		Public ReadOnly Property InitialPassword As String

		' Initilization
		Public Sub New( authentication As IAuthentication )
			_authentication = authentication
			InitialAccount = My.Settings.SavedAccount
			InitialPassword = My.Settings.SavedPassword
		End Sub

		' Login
		Public Sub Login( username As String,
		                  password As String,
		                  rememberAccount As Boolean )
			If Not ValidateAccount( username, password ) Then Return

			ActualLoginAsync( username, password, rememberAccount )
		End Sub

		Private Sub ActualLogin( username As String,
		                         password As String,
		                         rememberAccount As Boolean )
			Dim err As String
			Try
				' try login
				err = _authentication.TryLogin( username, password ).FirstOrDefault()

			Catch ex As DatabaseConnectionFailedException
				' connection errors
				err = "Mất kết nối máy chủ. Không thể đăng nhập!"

			Catch ex As Exception
				' other errors
				err = ex.Message
			End Try

			If String.IsNullOrEmpty( err )
				' success
				OnLoginSuccess( username, password, rememberAccount )
			Else
				' fail
				OnLoginFail( err )
			End If
		End Sub

		Private Async Sub ActualLoginAsync( username As String,
		                                    password As String,
		                                    rememberAccount As Boolean )
			Dim err As String
			Try
				' ry login
				ShowStaticWindowDialog( New LoadingDialog() )
				err = (Await _authentication.TryLoginAsync( username, password )).FirstOrDefault()
				CloseStaticWindowDialog()

			Catch ex As DatabaseConnectionFailedException
				'connection errors
				err = "Mất kết nối máy chủ. Không thể đăng nhập!"

			Catch ex As Exception
				'other errors
				err = ex.Message
			End Try

			If String.IsNullOrEmpty( err )
				'success
				OnLoginSuccess( username, password, rememberAccount )
			Else
				'fail
				OnLoginFail( err )
			End If
		End Sub

		Private Function ValidateAccount( username As String,
		                                  password As String ) As Boolean
			' short username
			If username.Length = 0
				ShowStaticTopNotification( StaticNotificationType.Information,
				                           "Nhập tên tài khoản để đăng nhập" )
				Return False
			End If

			' short password
			If password.Length = 0
				ShowStaticTopNotification( StaticNotificationType.Information,
				                           "Nhập mật khẩu để đăng nhập" )
				Return False
			End If

			Return True
		End Function

		' Login results
		Private Sub OnLoginSuccess( username As String,
		                            password As String,
		                            rememberAccount As Boolean )
			IoC.Get(Of IMainWindow).SwitchShell( "workspace-shell" )

			If rememberAccount
				My.Settings.SavedAccount = username
				My.Settings.SavedPassword = password
			Else
				My.Settings.SavedAccount = String.Empty
				My.Settings.SavedPassword = String.Empty
			End If

			My.Settings.Save()
		End Sub

		Private Sub OnLoginFail( err As String )
			ShowStaticTopNotification( StaticNotificationType.Error, err )
		End Sub
	End Class
End Namespace
