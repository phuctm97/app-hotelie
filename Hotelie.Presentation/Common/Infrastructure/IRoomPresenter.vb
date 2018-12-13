Imports Hotelie.Application.Rooms.Models

Namespace Common.Infrastructure
	Public Interface IRoomPresenter
		Sub Reload()

		Function ReloadAsync() As Task

		Sub OnRoomUpdated( model As IRoomModel )
	End Interface
End Namespace