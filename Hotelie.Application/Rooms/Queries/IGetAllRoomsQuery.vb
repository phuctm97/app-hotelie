Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetAllRoomsQuery
		Function Execute() As IEnumerable(Of RoomModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of RoomModel))
	End Interface
End Namespace