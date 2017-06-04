Namespace Rooms.Queries.GetRoomsList
	Public Interface IGetRoomsListQuery
		Function Execute() As IEnumerable(Of RoomModel)
		Function ExecuteAsync() As Task(Of IEnumerable(Of RoomModel))
	End Interface
End Namespace