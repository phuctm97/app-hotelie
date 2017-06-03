Imports Caliburn.Micro

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Screen

		Public ReadOnly Property ScreenRoomsList As ScreenRoomsListViewModel

		Public Sub New()
			DisplayName = "Danh sách phòng"

			ScreenRoomsList = New ScreenRoomsListViewModel()
		End Sub
	End Class
End Namespace
