Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetRoomQuery
		Function Execute( id As String ) As RoomModel

		Function ExecuteAsync( id As String ) As Task(Of RoomModel)
	End Interface
End Namespace