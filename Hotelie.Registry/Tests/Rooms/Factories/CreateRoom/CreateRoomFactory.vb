Imports Hotelie.Application.Rooms.Factories.CreateRoom

Namespace Tests.Rooms.Factories.CreateRoom
	Public Class CreateRoomFactory
		Implements ICreateRoomFactory

		Public Function Execute( name As String,
		                         categoryId As String,
		                         note As String ) As String Implements ICreateRoomFactory.Execute
			Dim newId = 1
			While (True)
				Dim newIdStr = newId.ToString()
				Dim rc = RoomRepositoryTest.Rooms.FirstOrDefault( Function( r ) r.Id = newIdStr )

				If IsNothing( rc ) Then Exit While
				newId += 1
			End While

			Dim category = RoomRepositoryTest.RoomCategories.FirstOrDefault( Function( c ) c.Id = categoryId )
			If IsNothing( category ) Then Return String.Empty

			RoomRepositoryTest.Rooms.Add( New Domain.Rooms.Room With {.Id = newId,
				                            .Name = name,
				                            .Category = category,
				                            .Note=note,
				                            .State=0} )
			Return newId
		End Function

		Public Async Function ExecuteAsync( name As String,
		                                    categoryId As String,
		                                    note As String ) As Task(Of String) Implements ICreateRoomFactory.ExecuteAsync
			Return Await Task.Run( Function() Execute( name, categoryId, note ) )
		End Function
	End Class
End Namespace
