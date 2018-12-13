Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetAllRoomsQuery
		Function Execute() As IEnumerable(Of IRoomModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of IRoomModel))
	End Interface
End Namespace