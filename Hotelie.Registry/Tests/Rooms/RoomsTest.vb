
Imports System.Windows.Media
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList
Imports Hotelie.Application.Rooms.Queries.GetRoomsList

Public Module RoomsTest
	Public Property Rooms As List(Of RoomModel)

	Public Property RoomCategories As List(Of RoomCategoryModel)

	Sub New()
		RoomCategories = New List(Of RoomCategoryModel) From {
			New RoomCategoryModel With {.Id="1", .Name="Phòng thường 1", .Price=100000, .DisplayColor=Colors.Black},
			New RoomCategoryModel With {.Id="2", .Name="Phòng thường 2", .Price=150000, .DisplayColor=Colors.Black},
			New RoomCategoryModel With {.Id="3", .Name="Phòng Vip 1", .Price=300000, .DisplayColor=Colors.Black},
			New RoomCategoryModel With {.Id="4", .Name="Phòng Vip 2", .Price=450000, .DisplayColor=Colors.Black}}

		Rooms = New List(Of RoomModel) From {
			New RoomModel _
				With {.Id="1", .Name="A101", .CategoryId="1", .CategoryName="Phòng thường 1", .CategoryDisplayColor=Colors.Black,
					.Price=100000, .State=1},
			New RoomModel _
				With {.Id="2", .Name="A102", .CategoryId="2", .CategoryName="Phòng thường 2", .CategoryDisplayColor=Colors.Black,
					.Price=150000, .State=0},
			New RoomModel _
				With {.Id="3", .Name="A103", .CategoryId="3", .CategoryName="Phòng Vip 1", .CategoryDisplayColor=Colors.Black,
					.Price=300000, .State=0},
			New RoomModel _
				With {.Id="4", .Name="A104", .CategoryId="4", .CategoryName="Phòng Vip 2", .CategoryDisplayColor=Colors.Black,
					.Price=450000, .State=1},
			New RoomModel _
				With {.Id="5", .Name="A201", .CategoryId="1", .CategoryName="Phòng thường 1", .CategoryDisplayColor=Colors.Black,
					.Price=100000, .State=0},
			New RoomModel _
				With {.Id="6", .Name="A202", .CategoryId="2", .CategoryName="Phòng thường 2", .CategoryDisplayColor=Colors.Black,
					.Price=150000, .State=0},
			New RoomModel _
				With {.Id="7", .Name="A203", .CategoryId="3", .CategoryName="Phòng Vip 1", .CategoryDisplayColor=Colors.Black,
					.Price=300000, .State=1},
			New RoomModel _
				With {.Id="8", .Name="A204", .CategoryId="4", .CategoryName="Phòng Vip 2", .CategoryDisplayColor=Colors.Black,
					.Price=450000, .State=0}}
	End Sub
End Module
