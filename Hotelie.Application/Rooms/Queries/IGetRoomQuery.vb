Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetRoomQuery
		Function Execute() As RoomModel

		Function ExecuteAsync() As Task(Of RoomModel)
	End Interface
End Namespace