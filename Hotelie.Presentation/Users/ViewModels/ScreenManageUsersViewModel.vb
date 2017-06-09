Imports Caliburn.Micro
Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Presentation.Common
Imports MaterialDesignThemes.Wpf

Namespace Users.ViewModels
	Public Class ScreenManageUsersViewModel
		Inherits AppScreen

		' Dependencies
		Private ReadOnly _authentication As IAuthentication

		' Bind models
		Public Property Users As IObservableCollection(Of EditableUserModel)

		Public Property SelectedUser As EditableUserModel

		Public ReadOnly Property Username As String
			Get
				Return _authentication.LoggedAccount?.Username
			End Get
		End Property

		Public Sub New( authentication As IAuthentication )
			MyBase.New( ColorZoneMode.PrimaryDark )
			_authentication = authentication

			DisplayName = "Quản lý tài khoản"
		End Sub
	End Class
End Namespace