Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands
Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Infrastructure
Imports Hotelie.Presentation.Rooms.Models
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Rooms.ViewModels
	Public Class ScreenRoomDetailViewModel
		Inherits AppScreenHasSavingAndDeleting
		Implements IChild(Of RoomsWorkspaceViewModel),
		           INeedWindowModals,
		           IRoomPresenter

		' Dependencies
		Private ReadOnly _getAllRoomsCategoriesQuery As IGetAllRoomCategoriesQuery
		Private ReadOnly _getRoomQuery As IGetRoomQuery
		Private ReadOnly _updateRoomCommand As IUpdateRoomCommand
		Private ReadOnly _removeRoomCommand As IRemoveRoomCommand
		Private ReadOnly _inventory As IInventory

		' Backing fields
		Private _originalRoomName As String
		Private _originalRoomCategoryId As String
		Private _originalRoomNote As String

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent

		' Initialization
		Public Sub New( workspace As RoomsWorkspaceViewModel,
		                getAllRoomsCategoriesQuery As IGetAllRoomCategoriesQuery,
		                getRoomQuery As IGetRoomQuery,
		                updateRoomCommand As IUpdateRoomCommand,
		                removeRoomCommand As IRemoveRoomCommand,
		                inventory As IInventory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ParentWorkspace = workspace
			_getAllRoomsCategoriesQuery = getAllRoomsCategoriesQuery
			_getRoomQuery = getRoomQuery
			_updateRoomCommand = updateRoomCommand
			_removeRoomCommand = removeRoomCommand
			_inventory = inventory
			RegisterInventory()

			Categories = New BindableCollection(Of RoomCategoryModel)
			Room = New EditableRoomModel()
		End Sub

		Public Sub Init()
			InitRoomCategories()
			InitValues()
		End Sub

		Private Sub InitRoomCategories()
			Categories.Clear()
			Categories.AddRange( _getAllRoomsCategoriesQuery.Execute() )
		End Sub

		Private Async Function InitRoomCategoriesAsync() As Task
			Categories.Clear()
			Categories.AddRange( Await _getAllRoomsCategoriesQuery.ExecuteAsync() )
		End Function

		Private Sub InitValues()
			Room.Id = String.Empty
			Room.Name = String.Empty
			Room.Note = String.Empty
			Room.Category = Categories.FirstOrDefault()
			Room.State = 0

			_originalRoomName = Room.Name
			_originalRoomCategoryId = Room.Category.Id
			_originalRoomNote = Room.Note
		End Sub

		' Binding model
		Public ReadOnly Property Room As EditableRoomModel

		' Binding data
		' ReSharper disable once CollectionNeverUpdated.Global
		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property Categories As IObservableCollection(Of RoomCategoryModel)

		Public Sub SetRoom( id As String )
			Dim model = _getRoomQuery.Execute( id )
			Dim category = Categories.FirstOrDefault( Function( c ) Equals( c.Id, model.Category.Id ) )
			If IsNothing( category )
				ShowStaticBottomNotification( StaticNotificationType.Warning,
				                              "Không tìm thấy loại phòng tương ứng trong danh sách loại phòng có thể thay đổi" )
				Return
			End If

			' Update bind models
			Room.Id = id
			Room.Name = model.Name
			Room.Category = category
			Room.State = model.State
			Room.Note = model.Note

			' Backup old values
			_originalRoomName = Room.Name
			_originalRoomCategoryId = Room.Category.Id
			_originalRoomNote = Room.Note
		End Sub

		Public Async Function SetRoomAsync( id As String ) As Task
			ShowStaticWindowLoadingDialog()
			Dim model = Await _getRoomQuery.ExecuteAsync( id )
			CloseStaticWindowDialog()

			Dim category = Categories.FirstOrDefault( Function( c ) Equals( c.Id, model.Category.Id ) )
			If IsNothing( category )
				ShowStaticBottomNotification( StaticNotificationType.Warning,
				                              "Không tìm thấy loại phòng tương ứng trong danh sách loại phòng có thể thay đổi" )
				Return
			End If

			' Update bind models
			Room.Id = id
			Room.Name = model.Name
			Room.Category = category
			Room.State = model.State
			Room.Note = model.Note

			' Backup old values
			_originalRoomName = Room.Name
			_originalRoomCategoryId = Room.Category.Id
			_originalRoomNote = Room.Note
		End Function

		' Exit
		Private Sub ResetValues()
			Room.Id = String.Empty
			Room.Name = String.Empty
			Room.Category = Categories.FirstOrDefault()
			Room.Note = String.Empty
			Room.State = 0

			_originalRoomName = Room.Name
			_originalRoomCategoryId = Room.Category.Id
			_originalRoomNote = Room.Note
		End Sub

		Public Overrides ReadOnly Property IsEdited As Boolean
			Get
				Return CheckForPendingChanges()
			End Get
		End Property

		Private Function CheckForPendingChanges() As Boolean
			Return (Not IsNothing( Room )) AndAlso
			       ((Not String.IsNullOrEmpty( Room.Id )) And
			        ((Not String.Equals( Room.Name, _originalRoomName )) Or
			         (Not String.Equals( Room.Category.Id, _originalRoomCategoryId )) Or
			         (Not String.Equals( Room.Note, _originalRoomNote ))))
		End Function

		Public Overrides Function ActualExitAsync() As Task
			ResetValues()

			ParentWorkspace.NavigateToScreenRoomsList()
			Return MyBase.ActualExitAsync()
		End Function

		' Save
		Public Overrides Function CanSave() As Task(Of Boolean)
			If String.IsNullOrWhiteSpace( Room.Name )
				ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng nhập tên phòng!" )
				Return Task.FromResult( False )
			End If

			Return Task.FromResult( True )
		End Function

		Public Overrides Async Function ActualSaveAsync() As Task
			' try update
			ShowStaticWindowLoadingDialog()

			Dim err = Await _updateRoomCommand.ExecuteAsync( Room.Id, Room.Name, Room.Category.Id, Room.Note, Room.State )

			If String.IsNullOrEmpty( err )
				Await OnSaveSuccessAsync()
			Else
				OnSaveFail( err )
			End If

			CloseStaticWindowDialog()
		End Function

		Private Async Function OnSaveSuccessAsync() As Task
			Await _inventory.OnRoomUpdatedAsync( Room.Id )
			Await ActualExitAsync()
		End Function

		Private Sub OnSaveFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		' Delete
		Public Overrides Async Function CanDelete() As Task(Of Boolean)
			If Room.State = 1
				ShowStaticBottomNotification( StaticNotificationType.Information, "Không thể xóa một phòng đang được thuê" )
				Return False
			End If

			Return Await MyBase.CanDelete()
		End Function

		Public Overrides Async Function ActualDeleteAsync() As Task
			' try delete
			ShowStaticWindowLoadingDialog()
			Dim err = Await _removeRoomCommand.ExecuteAsync( Room.Id )

			If String.IsNullOrEmpty( err )
				Await OnDeleteSuccessAsync()
			Else
				OnDeleteFail( err )
			End If

			CloseStaticWindowDialog()
		End Function

		Private Async Function OnDeleteSuccessAsync() As Task
			Await _inventory.OnRoomRemovedAsync( Room.Id )
			Await ActualExitAsync()
		End Function

		Private Sub OnDeleteFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		' Add lease
		Public Sub AddLease()
			If CanAddLease()
				ParentWorkspace.ParentShell.NavigateToScreenAddLease( Room.Id )
			End If
		End Sub

		Public Function CanAddLease() As Boolean
			If String.IsNullOrWhiteSpace( Room.Id )
				ShowStaticBottomNotification( StaticNotificationType.Error,
				                              "Phòng không tồn tại" )
				Return False
			End If

			If Room.State <> 0
				ShowStaticBottomNotification( StaticNotificationType.Error,
				                              "Không thể thuê. Phòng đã được thuê trước đó" )
				Return False
			End If
			Return True
		End Function

		' Infrastructure
		Public Sub OnRoomUpdated( model As RoomModel ) Implements IRoomPresenter.OnRoomUpdated
			If String.IsNullOrEmpty( Room.Id ) Then Return
			If String.IsNullOrEmpty( model.Id ) Then Return
			If Not String.Equals( Room.Id, model.Id ) Then Return

			Dim category = Categories.FirstOrDefault( Function( c ) c.Id = model.Category.Id )
			If IsNothing( category )
				ShowStaticBottomNotification( StaticNotificationType.Warning,
				                              "Không tìm thấy loại phòng {} trong danh sách loại phòng có thể chọn để cập nhật" )
				Return
			End If

			Room.Name = model.Name
			Room.Category = category
			Room.Note = model.Note
			Room.State = model.State
		End Sub
	End Class
End Namespace