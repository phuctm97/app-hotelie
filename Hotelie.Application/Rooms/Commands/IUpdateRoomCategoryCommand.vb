Namespace Rooms.Commands
	Public Interface IUpdateRoomCategoryCommand
		Function Execute( id As String,
		                  name As String,
		                  unitPrice As Decimal ) As String

		Function ExecuteAsync( id As String,
		                       name As String,
		                       unitPrice As Decimal ) As Task(Of String)
	End Interface
End Namespace