Namespace Rooms.Queries.GetSimpleRoomsList
	Public Interface IGetSimpleRoomsListQuery
		Function Execute() As IEnumerable(Of SimpleRoomsListItemModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of SimpleRoomsListItemModel))
	End Interface
End Namespace