Namespace Rooms.Queries.GetRoomCategoryData
	Public Interface IGetRoomCategoryDataQuery
		Function Execute( id As String ) As RoomCategoryModel
		Function ExecuteAsync( id As String ) As Task(Of RoomCategoryModel)
	End Interface
End Namespace