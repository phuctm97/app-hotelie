Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Screen

		Private _displayCode As Integer

		Public ReadOnly Property ScreenRoomsList As ScreenRoomsListViewModel

		Public ReadOnly Property ScreenRoomDetail As ScreenRoomDetailViewModel

		Public ReadOnly Property ScreenAddRoom As ScreenAddRoomViewModel

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
		                updateRoomCommand As IUpdateRoomCommand )
			DisplayName = "Danh sách phòng"

			' TODO: delay for loading screens
			ScreenRoomsList = New ScreenRoomsListViewModel( getRoomsListQuery, getRoomCategoriesListQuery )

			ScreenRoomDetail = New ScreenRoomDetailViewModel( Me, getRoomCategoriesListQuery, updateRoomCommand )

			ScreenAddRoom = New ScreenAddRoomViewModel( Me, getRoomCategoriesListQuery )

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
			ScreenAddRoom.Reset()
			DisplayCode = 2
		End Sub
	End Class
End Namespace
