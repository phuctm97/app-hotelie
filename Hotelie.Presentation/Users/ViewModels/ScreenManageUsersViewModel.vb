Imports Caliburn.Micro
Imports Hotelie.Application.Users.Commands
Imports Hotelie.Application.Users.Factories
Imports Hotelie.Application.Users.Queries
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports MaterialDesignThemes.Wpf

Namespace Users.ViewModels
	Public Class ScreenManageUsersViewModel
		Inherits AppScreenHasSavingAndDeleting
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _getAllUsersQuery As IGetAllUsersQuery
		Private ReadOnly _updateUserPermissionCommand As IUpdateUserPermissionCommand
		Private ReadOnly _removeUserCommand As IRemoveUserCommand
		Private ReadOnly _createUserFactory As ICreateUserFactory

		' Backing fields
		Private _selectedUser As EditableUserModel
		Private ReadOnly _originalSelectedUser As EditableUserModel

		' Bind models

		Public Property SelectedUser As EditableUserModel
			Get
				Return _selectedUser
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _selectedUser ) Then Return
				OnSelectedUserChanging( Value )
			End Set
		End Property

		Public ReadOnly Property CanEditUser As Boolean
			Get
				If IsNothing( SelectedUser ) Then Return False
				Return Not String.Equals( SelectedUser.Username, "admin" )
			End Get
		End Property

		' Bind data

		Public ReadOnly Property Users As IObservableCollection(Of EditableUserModel)

		Public Sub New( getAllUsersQuery As IGetAllUsersQuery,
		                updateUserPermissionCommand As IUpdateUserPermissionCommand,
		                removeUserCommand As IRemoveUserCommand,
		                createUserFactory As ICreateUserFactory )
			MyBase.New( ColorZoneMode.PrimaryDark )
			_getAllUsersQuery = getAllUsersQuery
			_updateUserPermissionCommand = updateUserPermissionCommand
			_removeUserCommand = removeUserCommand
			_createUserFactory = createUserFactory
			_originalSelectedUser = New EditableUserModel()

			DisplayName = "Quản lý tài khoản"
			Users = New BindableCollection(Of EditableUserModel)
		End Sub

		' Show

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
			SelectedUser = Users.FirstOrDefault()
		End Sub

		Public Overrides Async Function ShowAsync() As Task
			ShowStaticWindowLoadingDialog()
			Users.Clear()
			Users.AddRange( (Await _getAllUsersQuery.ExecuteAsync()).Select( Function( u ) New EditableUserModel With {
				                                                               .Username=u.UserName,
				                                                               .CouldAddLease=u.CouldAddLease,
				                                                               .CouldConfigRoom=u.CouldConfigRoom,
				                                                               .CouldEditLease=u.CouldEditLease,
				                                                               .CouldEditRule=u.CouldEditRule,
				                                                               .CouldManageUser=u.CouldManageUser,
				                                                               .CouldRemoveLease=u.CouldRemoveLease} ) )
			SelectedUser = Users.FirstOrDefault()
			CloseStaticWindowDialog()
		End Function

		' Exit

		Public Overrides ReadOnly Property IsEdited As Boolean
			Get
				Return (Not String.Equals( SelectedUser.Username, _originalSelectedUser.Username )) Or
				       (Not Equals( SelectedUser.CouldAddLease, _originalSelectedUser.CouldAddLease )) Or
				       (Not Equals( SelectedUser.CouldConfigRoom, _originalSelectedUser.CouldConfigRoom )) Or
				       (Not Equals( SelectedUser.CouldEditLease, _originalSelectedUser.CouldEditLease )) Or
				       (Not Equals( SelectedUser.CouldEditRule, _originalSelectedUser.CouldEditRule )) Or
				       (Not Equals( SelectedUser.CouldManageUser, _originalSelectedUser.CouldManageUser )) Or
				       (Not Equals( SelectedUser.CouldRemoveLease, _originalSelectedUser.CouldRemoveLease ))
			End Get
		End Property

		Private Async Sub OnSelectedUserChanging( value As EditableUserModel )
			If (_selectedUser IsNot Nothing) And IsEdited And CanEditUser
				If Await AskForSaveUser()
					Await ActualSaveAsync()
				Else
					RollbackSelectedUser()
				End If
			End If

			_selectedUser = value
			UpdateOriginalUser()

			NotifyOfPropertyChange( Function() SelectedUser )
			NotifyOfPropertyChange( Function() CanEditUser )
		End Sub

		Private Async Function AskForSaveUser() As Task(Of Boolean)
			Dim dialog =
				    New TwoButtonDialog(
					    $"Các thay đổi trên tài khoản {SelectedUser.Username _
					                       } chưa được lưu. Lưu?",
					    "LƯU",
					    "HỦY",
					    True,
					    False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "LƯU" ) Then Return True
			Return False
		End Function

		Public Overrides Async Function CanExit( Optional message As String = "Thoát mà không lưu các thay đổi?",
		                                         Optional buttonExit As String = "THOÁT",
		                                         Optional buttonSave As String = "LƯU",
		                                         Optional buttonCancel As String = "HỦY" ) As Task(Of Int32)
			If Await AskForSaveUser()
				Return 1
			End If

			Return 0
		End Function

		' Save

		Private Sub UpdateOriginalUser()
			If SelectedUser Is Nothing Then Return

			_originalSelectedUser.Username = SelectedUser.Username
			_originalSelectedUser.CouldAddLease = SelectedUser.CouldAddLease
			_originalSelectedUser.CouldConfigRoom = SelectedUser.CouldConfigRoom
			_originalSelectedUser.CouldEditLease = SelectedUser.CouldEditLease
			_originalSelectedUser.CouldEditRule = SelectedUser.CouldEditRule
			_originalSelectedUser.CouldManageUser = SelectedUser.CouldManageUser
			_originalSelectedUser.CouldRemoveLease = SelectedUser.CouldRemoveLease
		End Sub

		Private Sub RollbackSelectedUser()
			If _originalSelectedUser Is Nothing Then Return

			SelectedUser.Username = _originalSelectedUser.Username
			SelectedUser.CouldAddLease = _originalSelectedUser.CouldAddLease
			SelectedUser.CouldConfigRoom = _originalSelectedUser.CouldConfigRoom
			SelectedUser.CouldEditLease = _originalSelectedUser.CouldEditLease
			SelectedUser.CouldEditRule = _originalSelectedUser.CouldEditRule
			SelectedUser.CouldManageUser = _originalSelectedUser.CouldManageUser
			SelectedUser.CouldRemoveLease = _originalSelectedUser.CouldRemoveLease
		End Sub

		Public Overrides Function CanSave() As Task(Of Boolean)
			Return Task.FromResult( CanEditUser )
		End Function

		Public Overrides Async Function ActualSaveAsync() As Task
			ShowStaticWindowLoadingDialog()

			Dim err = Await _updateUserPermissionCommand.ExecuteAsync( SelectedUser.Username,
			                                                           SelectedUser.CouldConfigRoom,
			                                                           SelectedUser.CouldAddLease,
			                                                           SelectedUser.CouldEditLease,
			                                                           SelectedUser.CouldRemoveLease,
			                                                           SelectedUser.CouldManageUser,
			                                                           SelectedUser.CouldEditRule )
			CloseStaticWindowDialog()

			If Not String.IsNullOrEmpty( err )
				'save fail
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error, err )
			Else
				'save success
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Ok, "Cập nhật thành công" )
				UpdateOriginalUser()
			End If
		End Function

		' Delete
		Public Overrides Async Function CanDelete() As Task(Of Boolean)
			If Not CanEditUser Then Return False

			Dim dialog =
				    New TwoButtonDialog(
					    $"Bạn có chắc muốn xóa tài khoản {SelectedUser.Username _
					                       }? Thao tác này sẽ được lưu trực tiếp va không thể khôi phục lại được",
					    "XÓA",
					    "HỦY",
					    True,
					    False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "XÓA" ) Then Return True
			Return False
		End Function

		Public Overrides Async Function ActualDeleteAsync() As Task
			ShowStaticWindowLoadingDialog()

			Dim err = Await _removeUserCommand.ExecuteAsync( SelectedUser.Username )

			CloseStaticWindowDialog()

			If Not String.IsNullOrEmpty( err )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error, err )
			Else
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Ok, $"Đã xóa {SelectedUser.Username}" )
				Users.Remove( SelectedUser )
				SelectedUser = Users.FirstOrDefault()
			End If
		End Function
	End Class
End Namespace