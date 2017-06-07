Namespace Rooms.Factories
	Public Interface ICreateRoomFactory
		Function Execute(name As String, categoryId As String, note As String ) As String
		Function ExecuteAsync(name As String, categoryId As String, note As String ) As Task(Of String)
	End Interface
End Namespace