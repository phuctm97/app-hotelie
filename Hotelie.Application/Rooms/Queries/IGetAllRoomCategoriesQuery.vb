Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetAllRoomCategoriesQuery
		Function Execute() As IEnumerable(Of IRoomCategoryModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of IRoomCategoryModel))
	End Interface
End Namespace