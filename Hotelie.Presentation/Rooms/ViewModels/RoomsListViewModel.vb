Imports Caliburn.Micro

Namespace Rooms.ViewModels
	Public Class RoomsListViewModel
		Inherits Screen
		Implements IScreen

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			DisplayName = "Danh sách phòng"
		End Sub

	End Class
End Namespace