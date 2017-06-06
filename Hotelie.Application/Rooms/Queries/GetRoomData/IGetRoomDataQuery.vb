Namespace Rooms.Queries.GetRoomData
	Public Interface IGetRoomDataQuery
		Function Execute( id As String ) As RoomModel

		Function ExecuteAsync( id As String ) As Task(Of RoomModel)
	End Interface
End Namespace