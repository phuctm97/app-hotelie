Namespace Rooms.Queries.GetRoomCategoriesList
	Public Interface IGetRoomCategoriesListQuery
		Function Execute() As IEnumerable(Of RoomCategoriesListItemModel)
		Function ExecuteAsync() As Task(Of IEnumerable(Of RoomCategoriesListItemModel))
	End Interface
End Namespace