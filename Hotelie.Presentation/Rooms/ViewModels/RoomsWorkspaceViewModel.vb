Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Presentation.Common

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Conductor(Of IScreen)
		Implements IWorkspace

		Private _isDialogOpen As Boolean
		Private ReadOnly _getRoomsListQuery As IGetRoomsListQuery

		Public Sub New( getRoomsListQuery As IGetRoomsListQuery )
			_getRoomsListQuery = getRoomsListQuery

			DisplayName = "Danh sách phòng"
			IsDialogOpen = False

			Rooms = New BindableCollection(Of RoomModel)
		End Sub

		Protected Overrides Sub OnViewLoaded( view As Object )
			MyBase.OnViewLoaded( view )

			Rooms.AddRange( _getRoomsListQuery.Execute() )
		End Sub

		' Rooms
		Public ReadOnly Property Rooms As IObservableCollection(Of RoomModel)

		' Dialog

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
