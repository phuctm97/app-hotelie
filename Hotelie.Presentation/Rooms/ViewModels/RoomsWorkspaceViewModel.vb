Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Implements IWorkspace

		Public Property DisplayName As String Implements IHaveDisplayName.DisplayName

		Public Sub New()
			DisplayName = "Danh sách phòng"
		End Sub

	End Class
End Namespace
