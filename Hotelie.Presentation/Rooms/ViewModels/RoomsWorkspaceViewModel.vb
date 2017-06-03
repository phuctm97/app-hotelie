Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Screen

		Private _displayCode As Integer

		Public ReadOnly Property ScreenRoomsList As ScreenRoomsListViewModel

		Public ReadOnly Property ScreenRoomDetail As ScreenRoomDetailViewModel

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
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery )
			DisplayName = "Danh sách phòng"

			ScreenRoomsList = New ScreenRoomsListViewModel( getRoomsListQuery, getRoomCategoriesListQuery )

			ScreenRoomDetail = New ScreenRoomDetailViewModel( getRoomCategoriesListQuery )

			DisplayCode = 0
		End Sub

		Public Sub NavigateToScreenRoomsList()
			DisplayCode = 0
		End Sub

		Public Sub NavigateToScreenRoomDetail( room As RoomModel )
			If IsNothing(room) Then Return

			ScreenRoomDetail.SetRoom( room.Id, room.Name, room.CategoryId, room.State, room.Note )
			DisplayCode = 1
		End Sub
	End Class
End Namespace
