Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Screen
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _getRoomsListQuery As IGetRoomsListQuery
		Private ReadOnly _getRoomCategoriesListQuery As IGetRoomCategoriesListQuery

		Private _displayCode As Integer

		Public Property ScreenRoomsList As ScreenRoomsListViewModel

		Public Property ScreenRoomDetail As ScreenRoomDetailViewModel

		Public Property ScreenAddRoom As ScreenAddRoomViewModel

		Public Property DisplayCode As Integer
			Get
				Return _displayCode
			End Get
			Set
				If Equals( Value, _displayCode ) Then Return
				_displayCode = value
				NotifyOfPropertyChange( Function() DisplayCode )
			End Set
		End Property

		Public Sub New( getRoomsListQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery,
		                updateRoomCommand As IUpdateRoomCommand,
		                removeRoomCommand As IRemoveRoomCommand,
		                inventory As IInventory )
			_getRoomsListQuery = getRoomsListQuery
			_getRoomCategoriesListQuery = getRoomCategoriesListQuery

			ScreenRoomDetail = New ScreenRoomDetailViewModel( Me,
			                                                  getRoomCategoriesListQuery,
			                                                  updateRoomCommand,
			                                                  removeRoomCommand,
			                                                  inventory )

			ScreenAddRoom = New ScreenAddRoomViewModel( Me, getRoomCategoriesListQuery )

			DisplayName = "Danh sách phòng"

			DisplayCode = - 1
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			InitAsync()
		End Sub

		Private Async Sub InitAsync()
			ShowStaticWindowLoadingDialog()
			ScreenRoomsList = Await ScreenRoomsListViewModel.CreateAsync( _getRoomsListQuery, _getRoomCategoriesListQuery )
			NotifyOfPropertyChange( Function() ScreenRoomsList )
			CloseStaticWindowDialog()

			DisplayCode = 0
		End Sub

		Public Sub NavigateToScreenRoomsList()
			DisplayCode = 0
		End Sub

		Public Sub NavigateToScreenRoomDetail( room As RoomModel )
			If IsNothing( room ) Then Return

			ScreenRoomDetail.SetRoom( room.Id, room.Name, room.CategoryId, room.Note, room.State )
			DisplayCode = 1
		End Sub

		Public Sub NavigateToScreenAddRoom()
			DisplayCode = 2
		End Sub
	End Class
End Namespace
