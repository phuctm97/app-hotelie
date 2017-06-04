Imports System.Windows.Media
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Namespace Tests.Rooms.Queries.GetRoomsList
	Public Class GetRoomsListQuery
		Implements IGetRoomsListQuery

		Dim ReadOnly _tempRooms As List(Of RoomModel)

		Public Sub New()
			_tempRooms = New List(Of RoomModel) From {
				New RoomModel With {.Id="1", .Name="A101", .CategoryId="1", .CategoryName="Phòng thường 1", .CategoryDisplayColor=Colors.Black, .Price=100000, .State=1},
				New RoomModel With {.Id="2", .Name="A102", .CategoryId="2", .CategoryName="Phòng thường 2", .CategoryDisplayColor=Colors.Black, .Price=150000, .State=0},
				New RoomModel With {.Id="3", .Name="A103", .CategoryId="3", .CategoryName="Phòng Vip 1", .CategoryDisplayColor=Colors.Black, .Price=300000, .State=0},
				New RoomModel With {.Id="4", .Name="A104", .CategoryId="4", .CategoryName="Phòng Vip 2", .CategoryDisplayColor=Colors.Black, .Price=450000, .State=1},
				New RoomModel With {.Id="5", .Name="A201", .CategoryId="1", .CategoryName="Phòng thường 1", .CategoryDisplayColor=Colors.Black, .Price=100000, .State=0},
				New RoomModel With {.Id="6", .Name="A202", .CategoryId="2", .CategoryName="Phòng thường 2", .CategoryDisplayColor=Colors.Black, .Price=150000, .State=0},
				New RoomModel With {.Id="7", .Name="A203", .CategoryId="3", .CategoryName="Phòng Vip 1", .CategoryDisplayColor=Colors.Black, .Price=300000, .State=1},
				New RoomModel With {.Id="8", .Name="A204", .CategoryId="4", .CategoryName="Phòng Vip 2", .CategoryDisplayColor=Colors.Black, .Price=450000, .State=0}}
		End Sub

		Public Function Execute() As IEnumerable(Of RoomModel) Implements IGetRoomsListQuery.Execute
			Return _tempRooms
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomModel)) Implements IGetRoomsListQuery.ExecuteAsync
			Return Await Task.Run(Function() Execute())
		End Function
	End Class
End Namespace
