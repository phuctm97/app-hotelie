Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Queries
	Public Interface IGetAllRoomCategoriesQuery
		Function Execute() As IEnumerable(Of RoomCategoryModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of RoomCategoryModel))
	End Interface
End Namespace