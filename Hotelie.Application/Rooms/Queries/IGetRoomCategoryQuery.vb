Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetRoomCategoryQuery
		Function Execute() As RoomCategoryModel

		Function ExecuteAsync() As Task(Of RoomCategoryModel)
	End Interface
End Namespace