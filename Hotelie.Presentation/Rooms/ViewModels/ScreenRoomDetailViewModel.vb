Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Rooms.ViewModels
	Public Class ScreenRoomDetailViewModel
		Inherits PropertyChangedBase
		Implements IChild(Of RoomsWorkspaceViewModel)
		Implements INeedWindowModals

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
		Sub New( workspace As RoomsWorkspaceViewModel,
		         getRoomCategoriesListQuery As IGetRoomCategoriesListQuery,
		         updateRoomCommand As IUpdateRoomCommand,
		         removeRoomCommand As IRemoveRoomCommand,
		         inventory As IInventory )

			ParentWorkspace = workspace
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery
			_updateRoomCommand = updateRoomCommand
			_removeRoomCommand = removeRoomCommand
			_inventory = inventory

			InitRoomCategories( RoomCategories )

			InitValues()
		End Sub

		Private Sub InitValues()
			ResetValues()
		End Sub

		Private Sub InitRoomCategories( ByRef roomCategoryCollection As IObservableCollection(Of RoomCategoryModel) )
			roomCategoryCollection = New BindableCollection(Of RoomCategoryModel)
			roomCategoryCollection.AddRange( _getRoomCategoriesListQuery.Execute() )
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
			_roomName = "Chưa có tên"
			_roomCategory = RoomCategories.FirstOrDefault()
			_roomNote = String.Empty
			_roomState = 0

			_originalRoomName = _roomName
			_originalRoomCategoryId = _roomCategory.Id
			_originalRoomNote = _roomNote
		End Sub

		Public Async Function PreviewExit() As Task(Of Integer)
			If CheckForPendingChanges()
				Dim result = Await ConfirmExit()

				If Equals( result, 1 )
					Return Await PreviewSave()
				ElseIf Equals( result, 2 )
					Return 0
				End If
			End If

			[Exit]()
			Return 0
		End Function

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
		Public Async Function PreviewSave() As Task(Of Integer)
			If ValidateData() Then Await Save()
			Return 0
		End Function

		Private Async Function Save() As Task(Of Integer)
			' try update
			ShowStaticWindowDialog( New LoadingDialog() )

			Await _updateRoomCommand.ExecuteAsync( _roomId, _roomName, _roomCategory.Id, _roomNote, _roomState )
			_inventory.OnRoomUpdated( _roomId, _roomName, _roomCategory.Id, _roomNote, _roomState )

			CloseStaticWindowDialog()

			[Exit]()
			Return 0
		End Function

		Private Function ValidateData() As Boolean
			If String.IsNullOrWhiteSpace( RoomName )
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Vui lòng nhập tên phòng!" )
				Return False
			End If

			Return True
		End Function

		' Delete
		Public Async Function PreviewDelete() As Task(Of Integer)
			Dim result = Await ConfirmDelete()

			If Equals( result, 0 ) Then Await Delete()

			Return 0
		End Function

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

		Private Async Function Delete() As Task(Of Integer)
			' try update
			ShowStaticWindowDialog( New LoadingDialog() )

			Await _removeRoomCommand.ExecuteAsync( _roomId )
			_inventory.OnRoomRemoved( _roomId )

			CloseStaticWindowDialog()

			[Exit]()
			Return 0
		End Function
	End Class
End Namespace