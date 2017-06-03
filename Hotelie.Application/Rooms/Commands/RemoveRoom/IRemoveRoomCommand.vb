Namespace Rooms.Commands.RemoveRoom
	Public Interface IRemoveRoomCommand
		Sub Execute( id As String )
		Sub ExecuteAsync( id As String )
	End Interface
End Namespace