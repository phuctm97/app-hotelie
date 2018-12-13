Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetRoomCategoryQuery
		Function Execute(id As String) As IRoomCategoryModel

		Function ExecuteAsync(id As String) As Task(Of IRoomCategoryModel)
	End Interface
End Namespace