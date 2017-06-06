Imports Hotelie.Application.Rooms.Queries.GetRoomData

Namespace Infrastructure
	Public Interface IRoomsListPresenter
		Sub OnRoomAdded( model As RoomModel )

		Sub OnRoomUpdated( model As RoomModel )

		Sub OnRoomRemoved( id As String )
	End Interface
End Namespace