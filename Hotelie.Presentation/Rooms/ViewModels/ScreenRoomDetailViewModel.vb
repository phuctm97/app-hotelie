Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomData
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Infrastructure
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Rooms.ViewModels
	Public Class ScreenRoomDetailViewModel
		Inherits AppScreenHasSavingAndDeleting
		Implements IChild(Of RoomsWorkspaceViewModel),
		           INeedWindowModals,
		           IRoomPresenter

		' Dependencies
		Private ReadOnly _getRoomCategoriesListQuery As IGetRoomCategoriesListQuery
		Private ReadOnly _getRoomDataQuery As IGetRoomDataQuery
		Private ReadOnly _updateRoomCommand As IUpdateRoomCommand
		Private ReadOnly _removeRoomCommand As IRemoveRoomCommand
		Private ReadOnly _inventory As IInventory

		' Backing fields
		Private _roomId As String
		Private _roomName As String
		Private _roomCategory As RoomCategoriesListItemModel
		Private _roomNote As String
		Private _roomState As Integer

		Private _originalRoomName As String
		Private _originalRoomCategoryId As String
		Private _originalRoomNote As String

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As RoomsWorkspaceViewModel Implements IChild(Of RoomsWorkspaceViewModel).Parent

		' Initialization
		Public Sub New( workspace As RoomsWorkspaceViewModel,
		                getRoomDataQuery As IGetRoomDataQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery,
		                updateRoomCommand As IUpdateRoomCommand,
		                removeRoomCommand As IRemoveRoomCommand,
		                inventory As IInventory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ParentWorkspace = workspace
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery
			_getRoomDataQuery = getRoomDataQuery
			_updateRoomCommand = updateRoomCommand
			_removeRoomCommand = removeRoomCommand
			_inventory = inventory
			RegisterInventory()

			RoomCategories = New BindableCollection(Of RoomCategoriesListItemModel)
		End Sub

		Public Sub Init()
			InitRoomCategories()
			InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Await InitRoomCategoriesAsync()
			InitValues()
		End Function

		Private Sub InitRoomCategories()
			RoomCategories.Clear()
			RoomCategories.AddRange( _getRoomCategoriesListQuery.Execute() )
		End Sub

		Private Async Function InitRoomCategoriesAsync() As Task
			RoomCategories.Clear()
			RoomCategories.AddRange( Await _getRoomCategoriesListQuery.ExecuteAsync() )
		End Function

		Private Sub InitValues()
			_roomId = String.Empty
			RoomName = "Chưa có tên"
			RoomCategory = RoomCategories.FirstOrDefault()
			RoomNote = String.Empty
			RoomState = 0

			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Sub

		' Binding model
		Public Property RoomName As String
			Get
				Return _roomName
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _roomName ) Then Return
				_roomName = value
				NotifyOfPropertyChange( Function() RoomName )
			End Set
		End Property

		Public Property RoomCategory As RoomCategoriesListItemModel
			Get
				Return _roomCategory
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _roomCategory ) Then Return
				_roomCategory = value
				NotifyOfPropertyChange( Function() RoomCategory )
			End Set
		End Property

		Public Property RoomNote As String
			Get
				Return _roomNote
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _roomNote ) Then Return
				_roomNote = value
				NotifyOfPropertyChange( Function() RoomNote )
			End Set
		End Property

		Public Property RoomState As Integer
			Get
				Return _roomState
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _roomState ) Then Return
				_roomState = value
				NotifyOfPropertyChange( Function() RoomState )
			End Set
		End Property

		' Binding data
		' ReSharper disable once CollectionNeverUpdated.Global
		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoriesListItemModel)

		Public Sub SetRoom( id As String )
			Dim model = _getRoomDataQuery.Execute( id )
			Dim category = RoomCategories.FirstOrDefault( Function( c ) Equals( c.Id, model.Category.Id ) )
			If IsNothing( category ) Then Throw New EntryPointNotFoundException()

			' Bind values
			_roomId = id
			RoomName = model.Name
			RoomCategory = category
			RoomState = model.State
			RoomNote = model.Note

			' Backup old values
			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Sub

		Public Async Function SetRoomAsync( id As String ) As Task
			ShowStaticWindowLoadingDialog()
			Dim model = Await _getRoomDataQuery.ExecuteAsync( id )
			CloseStaticWindowDialog()

			Dim category = RoomCategories.FirstOrDefault( Function( c ) Equals( c.Id, model.Category.Id ) )
			If IsNothing( category ) Then Throw New EntryPointNotFoundException()

			' Bind values
			_roomId = id
			RoomName = model.Name
			RoomCategory = category
			RoomState = model.State
			RoomNote = model.Note

			' Backup old values
			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Function

		' Exit
		Private Sub ResetValues()
			_roomId = String.Empty
			RoomName = "Chưa có tên"
			RoomCategory = RoomCategories.FirstOrDefault()
			RoomNote = String.Empty
			RoomState = 0

			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Sub

		Public Overrides ReadOnly Property IsEdited As Boolean
			Get
				Return CheckForPendingChanges()
			End Get
		End Property

		Private Function CheckForPendingChanges() As Boolean
			Return (Not String.IsNullOrEmpty( _roomId )) And
			       ((Not String.Equals( _roomName, _originalRoomName )) Or
			        (Not String.Equals( _roomCategory.Id, _originalRoomCategoryId )) Or
			        (Not String.Equals( _roomNote, _originalRoomNote )))
		End Function

		Public Overrides Function ActualExitAsync() As Task
			ResetValues()

			ParentWorkspace.NavigateToScreenRoomsList()
			Return MyBase.ActualExitAsync()
		End Function

		' Save
		Public Overrides Function CanSave() As Task(Of Boolean)
			If String.IsNullOrWhiteSpace( RoomName )
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Vui lòng nhập tên phòng!" )
				Return Task.FromResult( False )
			End If

			Return Task.FromResult( True )
		End Function

		Public Overrides Async Function ActualSaveAsync() As Task
			' try update
			ShowStaticWindowLoadingDialog()

			Dim err = Await _updateRoomCommand.ExecuteAsync( _roomId, _roomName, _roomCategory.Id, _roomNote, _roomState )

			If String.IsNullOrEmpty( err )
				Await OnSaveSuccessAsync()
			Else
				OnSaveFail( err )
			End If

			CloseStaticWindowDialog()
		End Function

		Private Async Function OnSaveSuccessAsync() As Task
			Await _inventory.OnRoomUpdatedAsync( _roomId )
			Await ActualExitAsync()
		End Function

		Private Sub OnSaveFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		' Delete
		Public Overrides Async Function CanDelete() As Task(Of Boolean)
			If RoomState = 1
				ShowStaticBottomNotification( StaticNotificationType.Information, "Không thể xóa một phòng đang được thuê" )
				Return False
			End If

			Return Await MyBase.CanDelete()
		End Function

		Public Overrides Async Function ActualDeleteAsync() As Task
			' try delete
			ShowStaticWindowLoadingDialog()
			Dim err = Await _removeRoomCommand.ExecuteAsync( _roomId )

			If String.IsNullOrEmpty( err )
				Await OnDeleteSuccessAsync()
			Else
				OnDeleteFail( err )
			End If

			CloseStaticWindowDialog()
		End Function

		Private Async Function OnDeleteSuccessAsync() As Task
			Await _inventory.OnRoomRemovedAsync( _roomId )
			Await ActualExitAsync()
		End Function

		Private Sub OnDeleteFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		' Infrastructure
		Public Sub OnRoomUpdated( model As RoomModel ) Implements IRoomPresenter.OnRoomUpdated
			If String.IsNullOrEmpty( _roomId ) Then Return
			If String.IsNullOrEmpty( model.Id ) Then Return
			If Not String.Equals( _roomId, model.Id ) Then Return

			Dim category = RoomCategories.FirstOrDefault( Function( c ) c.Id = model.Category.Id )
			If IsNothing( category ) Then Throw New EntryPointNotFoundException()

			RoomName = model.Name
			RoomCategory = category
			RoomNote = model.Note
			RoomState = model.State
		End Sub
	End Class
End Namespace