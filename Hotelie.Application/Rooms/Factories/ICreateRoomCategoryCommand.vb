Namespace Rooms.Factories
	Public Interface ICreateRoomCategoryCommand
		Function Execute( name As String,
		                  unitPrice As Decimal ) As String

		Function ExecuteAsync( name As String,
		                       unitPrice As Decimal ) As Task(Of String)
	End Interface
End Namespace