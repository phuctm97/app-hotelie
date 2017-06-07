Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetRoomCategoryQuery
		Function Execute() As IRoomCategoryModel

		Function ExecuteAsync() As Task(Of IRoomCategoryModel)
	End Interface
End Namespace