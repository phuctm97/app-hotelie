Namespace Rooms.Commands.RemoveRoom
	Public Interface IRemoveRoomCommand
		Sub Execute( id As String )
		Function ExecuteAsync( id As String ) As Task(Of Integer)
	End Interface
End Namespace