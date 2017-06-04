Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Infrastructure
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Rooms.ViewModels
	Public Class ScreenRoomDetailViewModel
		Inherits PropertyChangedBase
		Implements IChild(Of RoomsWorkspaceViewModel),
		           INeedWindowModals,
		           IRoomPresenter

		' Dependencies
		Private ReadOnly _getRoomCategoriesListQuery As IGetRoomCategoriesListQuery
		Private ReadOnly _updateRoomCommand As IUpdateRoomCommand
		Private ReadOnly _removeRoomCommand As IRemoveRoomCommand
		Private ReadOnly _inventory As IInventory

		' Backing fields
		Private _roomId As String
		Private _roomName As String
		Private _roomCategory As RoomCategoryModel
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
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery,
		                updateRoomCommand As IUpdateRoomCommand,
		                removeRoomCommand As IRemoveRoomCommand,
		                inventory As IInventory )

			ParentWorkspace = workspace
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery
			_updateRoomCommand = updateRoomCommand
			_removeRoomCommand = removeRoomCommand
			_inventory = inventory
			RegisterInventory()

			RoomCategories = New BindableCollection(Of RoomCategoryModel)
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
			_roomName = "Chưa có tên"
			_roomCategory = RoomCategories.FirstOrDefault()
			_roomNote = String.Empty
			_roomState = 0

			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Sub

		' Data
		Public Property RoomName As String
			Get
				Return _roomName
			End Get
			Set
				If String.Equals( Value, _roomName ) Then Return
				_roomName = value
				NotifyOfPropertyChange( Function() RoomName )
			End Set
		End Property

		Public Property RoomCategory As RoomCategoryModel
			Get
				Return _roomCategory
			End Get
			Set
				If Equals( Value, _roomCategory ) Then Return
				_roomCategory = value
				NotifyOfPropertyChange( Function() RoomCategory )
			End Set
		End Property

		Public Property RoomNote As String
			Get
				Return _roomNote
			End Get
			Set
				If String.Equals( Value, _roomNote ) Then Return
				_roomNote = value
				NotifyOfPropertyChange( Function() RoomNote )
			End Set
		End Property

		Public Property RoomState As Integer
			Get
				Return _roomState
			End Get
			Set
				If Equals( Value, _roomState ) Then Return
				_roomState = value
				NotifyOfPropertyChange( Function() RoomState )
			End Set
		End Property

		' ReSharper disable once CollectionNeverUpdated.Global
		' ReSharper disable once UnassignedGetOnlyAutoProperty
		Public ReadOnly Property RoomCategories As IObservableCollection(Of RoomCategoryModel)

		Public Sub SetRoom( id As String,
		                    name As String,
		                    categoryId As String,
		                    note As String,
		                    state As Integer )
			' Bind values
			_roomId = id
			RoomName = name
			RoomCategory = RoomCategories.FirstOrDefault( Function( c ) Equals( c.Id, categoryId ) )
			RoomState = state
			RoomNote = note

			' Backup old values
			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Sub

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

		Private Sub [Exit]()
			ResetValues()
			ParentWorkspace.NavigateToScreenRoomsList()
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
				SaveAsync()
			End If
		End Sub

		Private Sub Save()
			' try update
			_updateRoomCommand.Execute( _roomId, _roomName, _roomCategory.Id, _roomNote, _roomState )
			_inventory.OnRoomUpdated( _roomId, _roomName, _roomCategory.Id, _roomNote, _roomState )
			[Exit]()
		End Sub

		Private Async Sub SaveAsync()
			' try update
			ShowStaticWindowLoadingDialog()

			Await _updateRoomCommand.ExecuteAsync( _roomId, _roomName, _roomCategory.Id, _roomNote, _roomState )
			_inventory.OnRoomUpdated( _roomId, _roomName, _roomCategory.Id, _roomNote, _roomState )

			CloseStaticWindowDialog()

			[Exit]()
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
			_removeRoomCommand.Execute( _roomId )
			_inventory.OnRoomRemoved( _roomId )
			[Exit]()
		End Sub

		Private Async Sub DeleteAsync()
			' try update
			ShowStaticWindowLoadingDialog()
			Await _removeRoomCommand.ExecuteAsync( _roomId )
			_inventory.OnRoomRemoved( _roomId )
			CloseStaticWindowDialog()
			[Exit]()
		End Sub

		' Infrastructure

		Public Sub OnRoomUpdated( id As String,
		                          name As String,
		                          categoryId As String,
		                          note As String,
		                          state As Int32 ) Implements IRoomPresenter.OnRoomUpdated
			If String.IsNullOrEmpty( _roomId ) Then Return
			If String.IsNullOrEmpty( id ) Then Return
			If Not String.Equals( _roomId, id ) Then Return

			RoomName = name
			RoomCategory = RoomCategories.FirstOrDefault( Function( c ) c.Id = categoryId )
			If IsNothing( RoomCategory ) Then Throw New EntryPointNotFoundException()
			RoomNote = note
			RoomState = state
		End Sub
	End Class
End Namespace