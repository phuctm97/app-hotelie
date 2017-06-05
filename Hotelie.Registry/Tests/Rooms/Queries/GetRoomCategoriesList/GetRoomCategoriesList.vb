Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList

Namespace Tests.Rooms.Queries.GetRoomCategoriesList
	Public Class GetRoomCategoriesListQuery
		Implements IGetRoomCategoriesListQuery

		Public Function Execute() As IEnumerable(Of RoomCategoryModel) Implements IGetRoomCategoriesListQuery.Execute
			Return RoomsTest.RoomCategories
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomCategoryModel)) _
			Implements IGetRoomCategoriesListQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute() )
		End Function
	End Class
End Namespace
