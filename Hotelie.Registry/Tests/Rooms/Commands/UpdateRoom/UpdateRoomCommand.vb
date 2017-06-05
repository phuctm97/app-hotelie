Imports Hotelie.Application.Rooms.Commands.UpdateRoom

Namespace Tests.Rooms.Commands.UpdateRoom
	Public Class UpdateRoomCommand
		Implements IUpdateRoomCommand

		Public Sub Execute( id As String,
		                    name As String,
		                    categoryId As String,
		                    note As String,
		                    state As Int32 ) Implements IUpdateRoomCommand.Execute
			Dim room = RoomsTest.Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( room ) Then Throw New EntryPointNotFoundException()

			room.Name = name

			Dim category = RoomsTest.RoomCategories.FirstOrDefault( Function( c ) c.Id = categoryId )
			If IsNothing( category ) Then Throw New EntryPointNotFoundException()
			room.CategoryId = categoryId
			room.CategoryName = category.Name
			room.CategoryDisplayColor = category.DisplayColor
			room.Price = category.Price

			room.Note = note
			room.State = state
		End Sub

		Public Async Function ExecuteAsync( id As String,
		                                    name As String,
		                                    categoryId As String,
		                                    note As String,
		                                    state As Int32 ) As Task(Of Int32) Implements IUpdateRoomCommand.ExecuteAsync
			Await Task.Run( Sub() Execute( id, name, categoryId, note, state ) )
			Return 0
		End Function
	End Class
End Namespace
