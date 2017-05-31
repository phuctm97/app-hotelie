Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Conductor(Of IScreen)
		Implements IWorkspace

		Private _isDialogOpen As Boolean

		Public Property IsDialogOpen As Boolean
			Get
				Return _isDialogOpen
			End Get
			Set
				If Equals( Value, _isDialogOpen ) Then Return
				_isDialogOpen = value
				NotifyOfPropertyChange( Function() IsDialogOpen )
			End Set
		End Property

		Public Sub New()
			DisplayName = "Danh sách phòng"
			IsDialogOpen = False
		End Sub

		Public Sub ShowRoomDetailDialog()
			ActivateItem( New RoomDetailViewModel() )
			IsDialogOpen = True
		End Sub

		Public Sub ShowAddRoomDialog()
			ActivateItem( New AddRoomViewModel() )
			IsDialogOpen = True
		End Sub
	End Class
End Namespace
