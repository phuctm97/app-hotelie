Namespace Rooms.Factories.CreateRoom
	Public Interface ICreateRoomFactory
		Function Execute(name As String, categoryId As String, note As String ) As RoomModel
		Function ExecuteAsync(name As String, categoryId As String, note As String ) As RoomModel
	End Interface
End Namespace