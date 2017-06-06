Imports System.Windows.Media
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Domain.Rooms

Namespace Tests.Rooms.Queries.GetRoomCategoriesList
	Public Class GetRoomCategoriesListQuery
		Implements IGetRoomCategoriesListQuery

		Public Function Execute() As IEnumerable(Of RoomCategoriesListItemModel) _
			Implements IGetRoomCategoriesListQuery.Execute
			Dim list = New List(Of RoomCategoriesListItemModel)
			For Each roomCategory As RoomCategory In RoomRepositoryTest.RoomCategories
				list.Add( New RoomCategoriesListItemModel With {.Id=roomCategory.Id,
					        .Name=roomCategory.Name,
					        .DisplayColor=Colors.Black,
					        .UnitPrice=roomCategory.Price} )
			Next
			Return list
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomCategoriesListItemModel)) _
			Implements IGetRoomCategoriesListQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute() )
		End Function
	End Class
End Namespace
