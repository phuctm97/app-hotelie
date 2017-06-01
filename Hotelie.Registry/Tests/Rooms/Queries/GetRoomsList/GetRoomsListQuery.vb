Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Tests.Rooms.Queries.GetRoomsList
	Public Class GetRoomsListQuery
		Implements IGetRoomsListQuery

		Dim ReadOnly _tempRooms As List(Of RoomModel)

		Public Sub New()
			_tempRooms = New List(Of RoomModel) From {
				New RoomModel With {.Name="A101", .CategoryName="Phòng thường", .Price=100000, .State=0},
				New RoomModel With {.Name="A102", .CategoryName="Phòng thường", .Price=150000, .State=0}}
		End Sub

		Public Function Execute() As IEnumerable(Of RoomModel) Implements IGetRoomsListQuery.Execute
			Return _tempRooms
		End Function
	End Class
End Namespace
