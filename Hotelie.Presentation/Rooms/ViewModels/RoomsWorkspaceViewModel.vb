Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Screen

		Public ReadOnly Property ScreenRoomsList As ScreenRoomsListViewModel

		Public Sub New( getRoomsListQuery As IGetRoomsListQuery,
		                getRoomCategoriesListQuery As IGetRoomCategoriesListQuery )
			DisplayName = "Danh sách phòng"

			ScreenRoomsList = New ScreenRoomsListViewModel( getRoomsListQuery, getRoomCategoriesListQuery )
		End Sub
	End Class
End Namespace
