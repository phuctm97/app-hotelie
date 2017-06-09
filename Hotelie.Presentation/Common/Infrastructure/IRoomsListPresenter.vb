Imports Hotelie.Application.Rooms.Models

Namespace Common.Infrastructure
	Public Interface IRoomsListPresenter
		Sub Reload()

		Function ReloadAsync() As Task

		Sub OnRoomAdded( model As IRoomModel )

		Sub OnRoomUpdated( model As IRoomModel )

		Sub OnRoomRemoved( id As String )
	End Interface
End Namespace