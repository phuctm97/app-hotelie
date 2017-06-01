Imports System.Windows.Media
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList

Namespace Tests.Rooms.Queries.GetRoomCategoriesList
	Public Class GetRoomCategoriesList
		Implements IGetRoomCategoriesList

		Dim ReadOnly _categories As List(Of RoomCategoryModel)

		Public Sub New()
			_categories = New List(Of RoomCategoryModel) From {
				New RoomCategoryModel With {.Id="1", .Name="Phòng thường 1", .DisplayColor=Colors.Black},
				New RoomCategoryModel With {.Id="2", .Name="Phòng thường 2", .DisplayColor=Colors.Black},
				New RoomCategoryModel With {.Id="3", .Name="Phòng Vip 1", .DisplayColor=Colors.Black},
				New RoomCategoryModel With {.Id="4", .Name="Phòng Vip 2", .DisplayColor=Colors.Black}}
		End Sub

		Public Function Execute() As IEnumerable(Of RoomCategoryModel) Implements IGetRoomCategoriesList.Execute
			Return _categories
		End Function
	End Class
End Namespace
