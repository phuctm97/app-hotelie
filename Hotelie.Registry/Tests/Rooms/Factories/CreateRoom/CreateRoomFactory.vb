Imports Hotelie.Application.Rooms.Factories.CreateRoom

Namespace Tests.Rooms.Factories.CreateRoom
	Public Class CreateRoomFactory
		Implements ICreateRoomFactory

		Public Function Execute( name As String,
		                         categoryId As String,
		                         note As String ) As RoomModel Implements ICreateRoomFactory.Execute
			Dim newId = 1
			While (True)
				Dim newIdStr = newId.ToString()
				Dim rc = RoomsTest.Rooms.FirstOrDefault( Function( r ) r.Id = newIdStr )

				If IsNothing( rc ) Then Exit While
				newId += 1
			End While

			Dim category = RoomsTest.RoomCategories.FirstOrDefault( Function( c ) c.Id = categoryId )
			If IsNothing( category ) Then Throw New EntryPointNotFoundException()

			Dim newRoom = New RoomModel With {.Id=newId, .Name=name, .Note=note, .State=0,
				    .CategoryId=categoryId,.CategoryName=category.Name,.CategoryDisplayColor=category.DisplayColor,
				    .Price=category.Price,.IsVisible=False}
			Return newRoom
		End Function

		Public Async Function ExecuteAsync( name As String,
		                                    categoryId As String,
		                                    note As String ) As Task(Of RoomModel) Implements ICreateRoomFactory.ExecuteAsync
			Return Await Task.Run( Function() Execute( name, categoryId, note ) )
		End Function
	End Class
End Namespace
