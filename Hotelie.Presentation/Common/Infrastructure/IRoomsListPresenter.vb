Imports Hotelie.Application.Rooms.Models

Namespace Infrastructure
	Public Interface IRoomsListPresenter
		Sub OnRoomAdded( model As IRoomModel )

		Sub OnRoomUpdated( model As IRoomModel )

		Sub OnRoomRemoved( id As String )
	End Interface
End Namespace