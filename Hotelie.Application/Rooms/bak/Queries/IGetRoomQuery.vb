Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetRoomQuery
		Function Execute( id As String ) As IRoomModel

		Function ExecuteAsync( id As String ) As Task(Of IRoomModel)
	End Interface
End Namespace