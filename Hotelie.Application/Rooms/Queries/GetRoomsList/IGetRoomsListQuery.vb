Namespace Rooms.Queries.GetRoomsList
	Public Interface IGetRoomsListQuery
		Function Execute() As IEnumerable(Of RoomsListItemModel)
	End Interface
End Namespace