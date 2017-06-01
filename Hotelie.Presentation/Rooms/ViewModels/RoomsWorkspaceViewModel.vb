Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Presentation.Common

Namespace Rooms.ViewModels
	Public Class RoomsWorkspaceViewModel
		Inherits Conductor(Of IScreen)
		Implements IWorkspace

		Private _isTopDrawerOpen As Boolean
		Private ReadOnly _getRoomsListQuery As IGetRoomsListQuery

		Public Sub New( getRoomsListQuery As IGetRoomsListQuery )
			_getRoomsListQuery = getRoomsListQuery

			DisplayName = "Danh sách phòng"
			IsTopDrawerOpen = False

			Rooms = New BindableCollection(Of RoomModel)
		End Sub

		Protected Overrides Sub OnInitialize()
			MyBase.OnInitialize()

			Rooms.AddRange( _getRoomsListQuery.Execute() )
		End Sub

		Protected Overrides Sub OnViewReady( view As Object )
			MyBase.OnViewReady( view )

			FilterRooms( String.Empty )
		End Sub

		' Rooms
		Public ReadOnly Property Rooms As IObservableCollection(Of RoomModel)

		Public Sub FilterRooms( namePrefix As String )
			namePrefix = namePrefix.ToLower()

			For Each room As RoomModel In Rooms
				If room.Name.ToLower().Contains( namePrefix )
					room.IsVisible = True
				Else
					room.IsVisible = False
				End If
			Next
		End Sub

		' Dialog

		Public Property IsTopDrawerOpen As Boolean
			Get
				Return _isTopDrawerOpen
			End Get
			Set
				If Equals( Value, _isTopDrawerOpen ) Then Return
				_isTopDrawerOpen = value
				NotifyOfPropertyChange( Function() IsTopDrawerOpen )
			End Set
		End Property

		Public Sub ShowRoomDetailDialog()
			ActivateItem( New RoomDetailViewModel() )
		End Sub

		Public Sub ShowAddRoomDialog()
			ActivateItem( New AddRoomViewModel() )
		End Sub
	End Class
End Namespace
