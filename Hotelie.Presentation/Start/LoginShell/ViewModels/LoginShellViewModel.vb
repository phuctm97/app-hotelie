Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Start.Login.Models

Namespace Start.LoginShell.ViewModels
	Public Class LoginShellViewModel
		Inherits Screen
		Implements IShell

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
			CommandsBar = New LoginShellCommandsBarViewModel( Me )

			Notification = New Notification()

			ScreenLogin = New ScreenLoginViewModel( Me, authentication )

			ScreenSettings = New ScreenSettingsViewModel()

			DisplayName = "Đăng nhập"

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

		Public ReadOnly Property ScreenLogin As ScreenLoginViewModel

		Public ReadOnly Property ScreenSettings As ScreenSettingsViewModel

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

	End Class
End Namespace
