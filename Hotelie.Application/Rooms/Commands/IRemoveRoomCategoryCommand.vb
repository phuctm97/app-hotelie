Namespace Rooms.Commands
	Public Interface IRemoveRoomCategoryCommand
		Function Execute( id As String ) As String

		Function ExecuteAsync( id As String ) As Task(Of String)
	End Interface
End Namespace