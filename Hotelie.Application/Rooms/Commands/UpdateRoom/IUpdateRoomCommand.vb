Namespace Rooms.Commands.UpdateRoom
	Public Interface IUpdateRoomCommand
		Function Execute( id As String,
		                  name As String,
		                  categoryId As String,
		                  note As String,
		                  state As Integer ) As String

		Function ExecuteAsync( id As String,
		                       name As String,
		                       categoryId As String,
		                       note As String,
		                       state As Integer ) As Task(Of String)
	End Interface
End Namespace
