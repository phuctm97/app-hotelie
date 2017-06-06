Imports System.Windows.Media
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Domain.Rooms

Namespace Tests.Rooms.Queries.GetRoomsList
	Public Class GetRoomsListQuery
		Implements IGetRoomsListQuery

		Public Function Execute() As IEnumerable(Of RoomsListItemModel) Implements IGetRoomsListQuery.Execute
			Dim list = New List(Of RoomsListItemModel)
			For Each room As Room In RoomRepositoryTest.Rooms
				list.Add( New RoomsListItemModel With {.Id=room.Id,
					        .Name=room.Name,
					        .CategoryId=room.Category.Id,
					        .CategoryName=room.Category.Name,
					        .CategoryDisplayColor=Colors.Black,
					        .UnitPrice=room.Category.Price,
					        .State=room.State,
					        .IsFiltersMatched=False} )
			Next
			Return list
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomsListItemModel)) _
			Implements IGetRoomsListQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute() )
		End Function
	End Class
End Namespace
