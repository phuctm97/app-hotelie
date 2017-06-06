Imports Hotelie.Application.Rooms.Commands.RemoveRoom

Namespace Tests.Rooms.Commands.RemoveRoom
	Public Class RemoveRoomCommand
		Implements IRemoveRoomCommand

		Public Function Execute( id As String ) As String Implements IRemoveRoomCommand.Execute
			Dim room = RoomRepositoryTest.Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( room ) Then Return "Không tìm thấy phòng cần xóa"

			RoomRepositoryTest.Rooms.Remove( room )
			Return String.Empty
		End Function

		Public Async Function ExecuteAsync( id As String ) As Task(Of String) Implements IRemoveRoomCommand.ExecuteAsync
			Return Await Task.Run( Function() Execute( id ) )
		End Function
	End Class
End Namespace