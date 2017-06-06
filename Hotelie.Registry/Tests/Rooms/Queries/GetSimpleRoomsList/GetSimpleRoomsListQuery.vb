Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Domain.Rooms

Namespace Tests.Rooms.Queries.GetSimpleRoomsList
	Public Class GetSimpleRoomsListQuery
		Implements IGetSimpleRoomsListQuery

		Public Function Execute() As IEnumerable(Of SimpleRoomsListItemModel) Implements IGetSimpleRoomsListQuery.Execute
			Dim list = New List(Of SimpleRoomsListItemModel)
			For Each room As Room In RoomRepositoryTest.Rooms
				list.Add( New SimpleRoomsListItemModel With {
					        .Id=room.Id, 
					        .Name=room.Name,
					        .CategoryName=room.Category.Name,
					        .UnitPrice=room.Category.Price} )
			Next

			Return list
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of SimpleRoomsListItemModel)) _
			Implements IGetSimpleRoomsListQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute() )
		End Function
	End Class
End Namespace
