Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Presentation.Rooms.ViewModels

Namespace Rooms.Design
	Public Class RoomsWorkspaceDesignModel
		Inherits RoomsWorkspaceViewModel

		Public Sub New()
			MyBase.New( Nothing )

			Rooms.AddRange( { _
				                New RoomModel _
				              With {.Id="1", .Name="A101", .CategoryName="Phòng thường 1", .CategoryDisplayColor=Colors.Black,
				              .Price=100000, .State=1},
				                New RoomModel _
				              With {.Id="2", .Name="A102", .CategoryName="Phòng thường 2", .CategoryDisplayColor=Colors.Black,
				              .Price=150000, .State=0},
				                New RoomModel _
				              With {.Id="3", .Name="A103", .CategoryName="Phòng Vip 1", .CategoryDisplayColor=Colors.Black,
				              .Price=300000, .State=0},
				                New RoomModel _
				              With {.Id="4", .Name="A104", .CategoryName="Phòng Vip 2", .CategoryDisplayColor=Colors.Black,
				              .Price=450000, .State=1},
				                New RoomModel _
				              With {.Id="5", .Name="A201", .CategoryName="Phòng thường 1", .CategoryDisplayColor=Colors.Black,
				              .Price=100000, .State=0},
				                New RoomModel _
				              With {.Id="6", .Name="A202", .CategoryName="Phòng thường 2", .CategoryDisplayColor=Colors.Black,
				              .Price=150000, .State=0},
				                New RoomModel _
				              With {.Id="7", .Name="A203", .CategoryName="Phòng Vip 1", .CategoryDisplayColor=Colors.Black,
				              .Price=300000, .State=1},
				                New RoomModel _
				              With {.Id="8", .Name="A204", .CategoryName="Phòng Vip 2", .CategoryDisplayColor=Colors.Black,
				              .Price=450000, .State=0}} )
		End Sub
	End Class
End Namespace
