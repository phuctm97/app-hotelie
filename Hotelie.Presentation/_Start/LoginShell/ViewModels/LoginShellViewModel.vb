Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Start.Login.Models

Namespace Start.LoginShell.ViewModels
	Public Class LoginShellViewModel
		Inherits Screen
		Implements IShell

		Private ReadOnly _authentication As IAuthentication

		Private _isLoginFormVisible As Boolean
		Private _isSettingsFormVisible As Boolean
		Private _notification As Notification

		Public Property ParentWindow As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return CType(Parent, IMainWindow)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		Public Sub New( authentication As IAuthentication )
			_authentication = authentication

			InitializeComponents()
		End Sub

		Private Sub InitializeComponents()
			DisplayName = "Login"

			IsLoginFormVisible = True
			IsSettingsFormVisible = False

			Notification = New Notification With {.Text=String.Empty, .Type=NotificationType.None}			
		End Sub

		' Notification
		Public Property Notification As Notification
			Get
				Return _notification
			End Get
			Set
				If Equals( Value, _notification ) Then Return

				_notification = value
				NotifyOfPropertyChange( Function() Notification )
			End Set
		End Property

		' Forms display
		Public Property IsLoginFormVisible As Boolean
			Get
				Return _isLoginFormVisible
			End Get
			Set
				If Equals( Value, _isLoginFormVisible ) Then Return
				_isLoginFormVisible = value
				NotifyOfPropertyChange( Function() IsLoginFormVisible )
			End Set
		End Property

		Public Property IsSettingsFormVisible As Boolean
			Get
				Return _isSettingsFormVisible
			End Get
			Set
				If Equals( Value, _isSettingsFormVisible ) Then Return
				_isSettingsFormVisible = value
				NotifyOfPropertyChange( Function() IsSettingsFormVisible )
			End Set
		End Property

		Public Sub ToggleDisplayForm()
			IsLoginFormVisible = Not IsLoginFormVisible
			IsSettingsFormVisible = Not IsSettingsFormVisible
		End Sub

		' Window commands
		Public Sub ToggleWindowZoomState()
			ParentWindow.ToggleZoomState()
		End Sub

		Public Sub HideWindow()
			ParentWindow.Hide()
		End Sub

		Public Sub CloseWindow()
			ParentWindow.Close()
		End Sub

		' Login
		Public Sub TryLogin( username As String,
		                     password As String )

			' short username
			If username.Length = 0
				Notification.Type = NotificationType.Information
				Notification.Text = "Nhập tên tài khoản để đăng nhập"
				Return
			End If

			' short password
			If password.Length = 0
				Notification.Type = NotificationType.Information
				Notification.Text = "Nhập mật khẩu để đăng nhập"
				Return
			End If

			' login
			Dim err = _authentication.TryLogin( New Account With {.Username=username, .Password=password} ).FirstOrDefault()

			If String.IsNullOrEmpty( err )
				' success
				ParentWindow.ShowWorkspaceShell()
			Else
				' fail
				Notification.Type = NotificationType.Error
				Notification.Text = err
			End If
		End Sub
	End Class
End Namespace
