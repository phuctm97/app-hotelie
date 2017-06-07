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
		Inherits AppScreenHasSaving
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

		Public Async Sub PreviewExit()
			If CheckForPendingChanges()
				Dim result = Await ConfirmExit()

				If Equals( result, 1 )
					PreviewSave()
					Return
				ElseIf Equals( result, 2 )
					Return
				End If
			End If

			[Exit]()
		End Sub

		Public Async Sub PreviewExitAsync()
			If CheckForPendingChanges()
				Dim result = Await ConfirmExit()

				If Equals( result, 1 )
					PreviewSaveAsync()
					Return
				ElseIf Equals( result, 2 )
					Return
				End If
			End If

			[Exit]()
		End Sub

		Private Sub [Exit]()
			ParentWorkspace.NavigateToScreenRoomsList()
			ResetValues()
		End Sub

		Private Function CheckForPendingChanges() As Boolean
			Return (Not String.IsNullOrEmpty( _roomId )) And
			       ((Not String.Equals( _roomName, _originalRoomName )) Or
			        (Not String.Equals( _roomCategory.Id, _originalRoomCategoryId )) Or
			        (Not String.Equals( _roomNote, _originalRoomNote )))
		End Function

		Private Async Function ConfirmExit() As Task(Of Integer)
			' show dialog
			Dim dialog = New ThreeButtonDialog( "Thoát mà không lưu các thay đổi?",
			                                    "THOÁT",
			                                    "LƯU & THOÁT",
			                                    "HỦY",
			                                    False,
			                                    True,
			                                    False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "THOÁT" ) Then Return 0
			If String.Equals( result, "HỦY" ) Then Return 2
			Return 1
		End Function

		' Save
		Public Sub PreviewSave()
			If ValidateData()
				Save()
			End If
		End Sub

		Public Sub PreviewSaveAsync()
			If ValidateData()
				SaveAsync()
			End If
		End Sub

		Private Sub Save()
			' try update
			Dim err = _updateRoomCommand.Execute( _roomId, _roomName, _roomCategory.Id, _roomNote, _roomState )

			If String.IsNullOrEmpty( err )
				OnSaveSuccess()
			Else
				OnSaveFail( err )
			End If
		End Sub

		Private Sub OnSaveSuccess()
			_inventory.OnRoomUpdated( _roomId )
			[Exit]()
		End Sub

		Private Async Sub SaveAsync()
			' try update
			ShowStaticWindowLoadingDialog()

			Dim err = Await _updateRoomCommand.ExecuteAsync( _roomId, _roomName, _roomCategory.Id, _roomNote, _roomState )

			If String.IsNullOrEmpty( err )
				Await OnSaveSuccessAsync()
			Else
				OnSaveFail( err )
			End If

			CloseStaticWindowDialog()
		End Sub

		Private Async Function OnSaveSuccessAsync() As Task
			Await _inventory.OnRoomUpdatedAsync( _roomId )
			[Exit]()
		End Function

		Private Sub OnSaveFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		Private Function ValidateData() As Boolean
			If String.IsNullOrWhiteSpace( RoomName )
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Vui lòng nhập tên phòng!" )
				Return False
			End If

			Return True
		End Function

		' Delete
		Public Async Sub PreviewDelete()
			If Not ValidateDeleting() Then Return
			Dim result = Await ConfirmDelete()

			If Equals( result, 0 )
				Delete()
			End If
		End Sub

		Public Async Sub PreviewDeleteAsync()
			If Not ValidateDeleting() Then Return
			Dim result = Await ConfirmDelete()

			If Equals( result, 0 )
				DeleteAsync()
			End If
		End Sub

		Private Async Function ConfirmDelete() As Task(Of Integer)
			' show dialog
			Dim dialog = New TwoButtonDialog( "Xóa phòng. Tiếp tục?",
			                                  "XÓA",
			                                  "HỦY",
			                                  True,
			                                  False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "XÓA" ) Then Return 0
			Return 1
		End Function

		Private Sub Delete()
			' try update
			Dim err = _removeRoomCommand.Execute( _roomId )

			If String.IsNullOrEmpty( err )
				OnDeleteSuccess()
			Else
				OnDeleteFail( err )
			End If
		End Sub

		Private Sub OnDeleteSuccess()
			_inventory.OnRoomRemoved( _roomId )
			[Exit]()
		End Sub

		Private Async Sub DeleteAsync()
			If Not ValidateDeleting() Then Return
			' try update
			ShowStaticWindowLoadingDialog()
			Dim err = Await _removeRoomCommand.ExecuteAsync( _roomId )

			If String.IsNullOrEmpty( err )
				Await OnDeleteSuccessAsync()
			Else
				OnDeleteFail( err )
			End If

			CloseStaticWindowDialog()
		End Sub

		Private Async Function OnDeleteSuccessAsync() As Task
			Await _inventory.OnRoomRemovedAsync( _roomId )
			[Exit]()
		End Function

		Private Sub OnDeleteFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		Private Function ValidateDeleting() As Boolean
			If RoomState = 1
				ShowStaticBottomNotification( StaticNotificationType.Information, "Không thể xóa một phòng đang được thuê" )
				Return False
			End If
			Return True
		End Function

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