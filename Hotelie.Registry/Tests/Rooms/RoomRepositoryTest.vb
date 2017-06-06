
Imports Hotelie.Domain.Rooms

Public Module RoomRepositoryTest
	Public Property Rooms As List(Of Room)

	Public Property RoomCategories As List(Of RoomCategory)

	Sub New()
		RoomCategories = New List(Of RoomCategory) From {
			New RoomCategory With {.Id="1", .Name="Phòng thường 1", .Price=100000},
			New RoomCategory With {.Id="2", .Name="Phòng thường 2", .Price=150000},
			New RoomCategory With {.Id="3", .Name="Phòng Vip 1", .Price=300000},
			New RoomCategory With {.Id="4", .Name="Phòng Vip 2", .Price=450000}}

		Rooms = New List(Of Room) From {
			New Room _
				With {.Id="1", .Name="A101", .Category=RoomCategories( 0 ), .State=1},
			New Room _
				With {.Id="2", .Name="A102", .Category=RoomCategories( 1 ), .State=0},
			New Room _
				With {.Id="3", .Name="A103", .Category=RoomCategories( 2 ), .State=0},
			New Room _
				With {.Id="4", .Name="A104", .Category=RoomCategories( 3 ), .State=1},
			New Room _
				With {.Id="5", .Name="A201", .Category=RoomCategories( 0 ), .State=0},
			New Room _
				With {.Id="6", .Name="A202", .Category=RoomCategories( 1 ), .State=0},
			New Room _
				With {.Id="7", .Name="A203", .Category=RoomCategories( 2 ), .State=1},
			New Room _
				With {.Id="8", .Name="A204", .Category=RoomCategories( 3 ), .State=0}}
	End Sub
End Module
