Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Start.Login.Models

Namespace Start.LoginShell.ViewModels
	Public Class LoginShellViewModel
		Inherits Screen
		Implements IShell

		' Dependencies

		Private ReadOnly _authentication As IAuthentication

		' Display property backing fields

		Private _notification As Notification
		Private _displayCode As Integer

		' Parent window

		Public Property ParentWindow As IMainWindow Implements IChild(Of IMainWindow).Parent
			Get
				Return CType(Parent, IMainWindow)
			End Get
			Set
				Parent = Value
			End Set
		End Property

		' Initialization

		Public Sub New( authentication As IAuthentication )
			_authentication = authentication

			CommandsBar = New LoginShellCommandsBarViewModel( Me )

			Notification = New Notification()

			LoginForm = New LoginFormViewModel()

			SettingsForm = New SettingsFormViewModel()
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			DisplayName = "Login"
			DisplayCode = 0
			HideNotification()
		End Sub

		' Display properties

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

		Public Sub ShowNotification( type As NotificationType,
		                             text As String )
			Notification.Type = type
			Notification.Text = text
		End Sub

		Public Sub HideNotification()
			Notification.Type = NotificationType.None
			Notification.Text = String.Empty
		End Sub

		Public ReadOnly Property LoginForm As LoginFormViewModel

		Public ReadOnly Property SettingsForm As SettingsFormViewModel

		Public ReadOnly Property CommandsBar As IWindowCommandsBar Implements IShell.CommandsBar

		Public Property DisplayCode As Integer
			Get
				Return _displayCode
			End Get
			Set
				If Equals( value, _displayCode ) Then Return
				_displayCode = value
				NotifyOfPropertyChange( Function() DisplayCode )
			End Set
		End Property

		' Login

		Public Sub TryLogin( username As String,
		                     password As String )

			' short username
			If username.Length = 0
				ShowNotification( NotificationType.Information, "Nhập tên tài khoản để đăng nhập" )
				Return
			End If

			' short password
			If password.Length = 0
				ShowNotification( NotificationType.Information, "Nhập mật khẩu để đăng nhập" )
				Return
			End If

			' login
			Dim err = _authentication.TryLogin( New Account With {.Username=username, .Password=password} ).FirstOrDefault()

			If String.IsNullOrEmpty( err )
				' success
				ParentWindow.SwitchShell( "workspace-shell" )
			Else
				' fail
				ShowNotification( NotificationType.Error, err )
			End If
		End Sub
	End Class
End Namespace
