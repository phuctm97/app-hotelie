Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Presentation.Rooms.ViewModels

Namespace Rooms.Design
	Public Class RoomsWorkspaceDesignModel
		Inherits RoomsWorkspaceViewModel

		Public Sub New()
			MyBase.New( Nothing, Nothing )

			Rooms.AddRange( {New RoomModel _
				              With {.Id="1", .Name="A101", .CategoryId="1", .CategoryName="Phòng thường 1",
				              .CategoryDisplayColor=Colors.Black, .Price=100000, .State=1},
			                 New RoomModel _
				              With {.Id="2", .Name="A102", .CategoryId="2", .CategoryName="Phòng thường 2",
				              .CategoryDisplayColor=Colors.Black, .Price=150000, .State=0},
			                 New RoomModel _
				              With {.Id="3", .Name="A103", .CategoryId="3", .CategoryName="Phòng Vip 1",
				              .CategoryDisplayColor=Colors.Black, .Price=300000, .State=0},
			                 New RoomModel _
				              With {.Id="4", .Name="A104", .CategoryId="4", .CategoryName="Phòng Vip 2",
				              .CategoryDisplayColor=Colors.Black, .Price=450000, .State=1},
			                 New RoomModel _
				              With {.Id="5", .Name="A201", .CategoryId="1", .CategoryName="Phòng thường 1",
				              .CategoryDisplayColor=Colors.Black, .Price=100000, .State=0},
			                 New RoomModel _
				              With {.Id="6", .Name="A202", .CategoryId="2", .CategoryName="Phòng thường 2",
				              .CategoryDisplayColor=Colors.Black, .Price=150000, .State=0},
			                 New RoomModel _
				              With {.Id="7", .Name="A203", .CategoryId="3", .CategoryName="Phòng Vip 1",
				              .CategoryDisplayColor=Colors.Black, .Price=300000, .State=1},
			                 New RoomModel _
				              With {.Id="8", .Name="A204", .CategoryId="4", .CategoryName="Phòng Vip 2",
				              .CategoryDisplayColor=Colors.Black, .Price=450000, .State=0}} )

			RoomCategories.AddRange( {New RoomCategoryModel With {.Id="1", .Name="Phòng thường 1", .DisplayColor=Colors.Black},
			                          New RoomCategoryModel With {.Id="2", .Name="Phòng thường 2", .DisplayColor=Colors.Black},
			                          New RoomCategoryModel With {.Id="3", .Name="Phòng Vip 1", .DisplayColor=Colors.Black},
			                          New RoomCategoryModel With {.Id="4", .Name="Phòng Vip 2", .DisplayColor=Colors.Black}} )
		End Sub
	End Class
End Namespace
