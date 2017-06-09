Imports Caliburn.Micro
Imports Hotelie.Presentation.Common
Imports MaterialDesignThemes.Wpf

Namespace Users.ViewModels
	Public Class ScreenManageUsersViewModel
		Inherits AppScreen

		' Bind models
		Public Property Users As IObservableCollection(Of EditableUserModel)

		Public Property SelectedUser As EditableUserModel

		Public Sub New()
			MyBase.New( ColorZoneMode.PrimaryDark )

			DisplayName = "Quản lý tài khoản"
		End Sub
	End Class
End Namespace