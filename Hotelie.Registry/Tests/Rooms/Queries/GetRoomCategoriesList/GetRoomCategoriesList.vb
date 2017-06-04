Imports System.Windows.Media
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList

Namespace Tests.Rooms.Queries.GetRoomCategoriesList
	Public Class GetRoomCategoriesListQuery
		Implements IGetRoomCategoriesListQuery

		Dim ReadOnly _categories As List(Of RoomCategoryModel)

		Public Sub New()
			_categories = New List(Of RoomCategoryModel) From {
				New RoomCategoryModel With {.Id="1", .Name="Phòng thường 1", .Price=100000, .DisplayColor=Colors.Black},
				New RoomCategoryModel With {.Id="2", .Name="Phòng thường 2", .Price=150000, .DisplayColor=Colors.Black},
				New RoomCategoryModel With {.Id="3", .Name="Phòng Vip 1", .Price=300000, .DisplayColor=Colors.Black},
				New RoomCategoryModel With {.Id="4", .Name="Phòng Vip 2", .Price=450000, .DisplayColor=Colors.Black}}
		End Sub

		Public Function Execute() As IEnumerable(Of RoomCategoryModel) Implements IGetRoomCategoriesListQuery.Execute
			Return _categories
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomCategoryModel)) _
			Implements IGetRoomCategoriesListQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute() )
		End Function
	End Class
End Namespace
