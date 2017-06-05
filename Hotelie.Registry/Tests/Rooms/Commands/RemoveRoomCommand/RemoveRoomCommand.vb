Imports Hotelie.Application.Rooms.Commands.RemoveRoom

Namespace Tests.Rooms.Commands
	Public Class RemoveRoomCommand
		Implements IRemoveRoomCommand

		Public Sub Execute( id As String ) Implements IRemoveRoomCommand.Execute
			Dim room = RoomsTest.Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( room ) Then Throw New EntryPointNotFoundException()

			RoomsTest.Rooms.Remove( room )
		End Sub

		Public Async Function ExecuteAsync( id As String ) As Task(Of Int32) Implements IRemoveRoomCommand.ExecuteAsync
			Await Task.Run( Sub() Execute( id ) )
			Return 0
		End Function
	End Class
End Namespace