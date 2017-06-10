Imports Caliburn.Micro
Imports Hotelie.Application.Users.Commands
Imports Hotelie.Application.Users.Queries
Imports Hotelie.Presentation.Common
Imports MaterialDesignThemes.Wpf

Namespace Users.ViewModels
	Public Class ScreenManageUsersViewModel
		Inherits AppScreen

		' Dependencies
		Private ReadOnly _getAllUsersQuery As IGetAllUsersQuery
		Private ReadOnly _updateUserPermissionCommand As IUpdateUserPermissionCommand
		Private ReadOnly _removeUserCommand As IRemoveUserCommand

		' Backing fields
		Private _selectedUser As EditableUserModel

		' Bind models

		Public Property SelectedUser As EditableUserModel
			Get
				Return _selectedUser
			End Get
			Set
				If Equals( Value, _selectedUser ) Then Return
				_selectedUser = value
				NotifyOfPropertyChange( Function() SelectedUser )
			End Set
		End Property

		' Bind data
		Public ReadOnly Property Users As IObservableCollection(Of EditableUserModel)

		Public Sub New( getAllUsersQuery As IGetAllUsersQuery,
		                updateUserPermissionCommand As IUpdateUserPermissionCommand,
		                removeUserCommand As IRemoveUserCommand )
			MyBase.New( ColorZoneMode.PrimaryDark )
			_getAllUsersQuery = getAllUsersQuery
			_updateUserPermissionCommand = updateUserPermissionCommand
			_removeUserCommand = removeUserCommand

			DisplayName = "Quản lý tài khoản"

			Users = New BindableCollection(Of EditableUserModel)
		End Sub

		Public Overrides Sub Show()
			Users.Clear()
			Users.AddRange( _getAllUsersQuery.Execute().Select( Function( u ) New EditableUserModel With {
				                                                  .Username=u.UserName,
				                                                  .CouldAddLease=u.CouldAddLease,
				                                                  .CouldConfigRoom=u.CouldConfigRoom,
				                                                  .CouldEditLease=u.CouldEditLease,
				                                                  .CouldEditRule=u.CouldEditRule,
				                                                  .CouldManageUser=u.CouldManageUser,
				                                                  .CouldRemoveLease=u.CouldRemoveLease} ) )
		End Sub

		Public Overrides Async Function ShowAsync() As Task
			Users.Clear()
			Users.AddRange( (Await _getAllUsersQuery.ExecuteAsync()).Select( Function( u ) New EditableUserModel With {
				                                                               .Username=u.UserName,
				                                                               .CouldAddLease=u.CouldAddLease,
				                                                               .CouldConfigRoom=u.CouldConfigRoom,
				                                                               .CouldEditLease=u.CouldEditLease,
				                                                               .CouldEditRule=u.CouldEditRule,
				                                                               .CouldManageUser=u.CouldManageUser,
				                                                               .CouldRemoveLease=u.CouldRemoveLease} ) )
		End Function
	End Class
End Namespace