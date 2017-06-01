Imports System.Windows.Media
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Tests.Rooms.Queries.GetRoomsList
	Public Class GetRoomsListQuery
		Implements IGetRoomsListQuery

		Dim ReadOnly _tempRooms As List(Of RoomModel)

		Public Sub New()
			_tempRooms = New List(Of RoomModel) From {
				New RoomModel With {.Id="1", .Name="A101", .CategoryName="Phòng thường 1", .CategoryDisplayColor=Colors.Black, .Price=100000, .State=1},
				New RoomModel With {.Id="2", .Name="A102", .CategoryName="Phòng thường 2", .CategoryDisplayColor=Colors.Black, .Price=150000, .State=0},
				New RoomModel With {.Id="3", .Name="A103", .CategoryName="Phòng Vip 1", .CategoryDisplayColor=Colors.Black, .Price=300000, .State=0},
				New RoomModel With {.Id="4", .Name="A104", .CategoryName="Phòng Vip 2", .CategoryDisplayColor=Colors.Black, .Price=450000, .State=1},
				New RoomModel With {.Id="5", .Name="A201", .CategoryName="Phòng thường 1", .CategoryDisplayColor=Colors.Black, .Price=100000, .State=0},
				New RoomModel With {.Id="6", .Name="A202", .CategoryName="Phòng thường 2", .CategoryDisplayColor=Colors.Black, .Price=150000, .State=0},
				New RoomModel With {.Id="7", .Name="A203", .CategoryName="Phòng Vip 1", .CategoryDisplayColor=Colors.Black, .Price=300000, .State=1},
				New RoomModel With {.Id="8", .Name="A204", .CategoryName="Phòng Vip 2", .CategoryDisplayColor=Colors.Black, .Price=450000, .State=0}}
		End Sub

		Public Function Execute() As IEnumerable(Of RoomModel) Implements IGetRoomsListQuery.Execute
			Return _tempRooms
		End Function
	End Class
End Namespace
