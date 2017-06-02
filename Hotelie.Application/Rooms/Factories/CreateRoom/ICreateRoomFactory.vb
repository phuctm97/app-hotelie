Namespace Rooms.Factories.CreateRoom
	Public Interface ICreateRoomFactory
		Function Execute( id As String, name As String, categoryId As String, note As String ) As RoomModel
	End Interface
End Namespace