Imports Hotelie.Application.Rooms.Commands.UpdateRoom

Namespace Tests.Rooms.Commands.UpdateRoom
	Public Class UpdateRoomCommand
		Implements IUpdateRoomCommand

		Public Function Execute( id As String,
		                         name As String,
		                         categoryId As String,
		                         note As String,
		                         state As Int32 ) As String Implements IUpdateRoomCommand.Execute
			Dim room = RoomRepositoryTest.Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( room ) Then Return "Không tìm thấy phòng cần cập nhật"

			Dim category = RoomRepositoryTest.RoomCategories.FirstOrDefault( Function( c ) c.Id = categoryId )
			If IsNothing( category ) Then Return "Không tìm thấy loại phòng tương ứng để cập nhật"

			room.Name = name
			room.Category = category
			room.Note = note
			room.State = state

			Return String.Empty
		End Function

		Public Async Function ExecuteAsync( id As String,
		                                    name As String,
		                                    categoryId As String,
		                                    note As String,
		                                    state As Int32 ) As Task(Of String) Implements IUpdateRoomCommand.ExecuteAsync
			Return Await Task.Run( Function() Execute( id, name, categoryId, note, state ) )
		End Function
	End Class
End Namespace
