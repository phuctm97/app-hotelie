Imports System.Data.Entity
Imports Hotelie.Domain.Rooms

Namespace Common
	Public Class DatabaseInitializer
		Inherits CreateDatabaseIfNotExists(Of DatabaseContext)

		Protected Overrides Sub Seed( context As DatabaseContext )
			MyBase.Seed( context )
			' SeedRoomCategories( context )

			' SeedRooms( context )
		End Sub

		Private Sub SeedRooms( context As DatabaseContext )
			Dim categories = context.RoomCategories.ToList()

			context.Rooms.Add( New Room With {.Id="RM101", .Name="Floor 1 Room 1", .Category=categories( 0 )} )
			context.Rooms.Add( New Room With {.Id="RM102", .Name="Floor 1 Room 2", .Category=categories( 1 )} )
			context.Rooms.Add( New Room With {.Id="RM103", .Name="Floor 1 Room 3", .Category=categories( 2 )} )
			context.Rooms.Add( New Room With {.Id="RM104", .Name="Floor 1 Room 4", .Category=categories( 3 )} )
			context.Rooms.Add( New Room With {.Id="RM105", .Name="Floor 1 Room 5", .Category=categories( 4 )} )
			context.Rooms.Add( New Room With {.Id="RM106", .Name="Floor 1 Room 6", .Category=categories( 5 )} )
			context.Rooms.Add( New Room With {.Id="RM201", .Name="Floor 2 Room 1", .Category=categories( 0 )} )
			context.Rooms.Add( New Room With {.Id="RM202", .Name="Floor 2 Room 2", .Category=categories( 1 )} )
			context.Rooms.Add( New Room With {.Id="RM203", .Name="Floor 2 Room 3", .Category=categories( 2 )} )
			context.Rooms.Add( New Room With {.Id="RM204", .Name="Floor 2 Room 4", .Category=categories( 3 )} )
			context.Rooms.Add( New Room With {.Id="RM205", .Name="Floor 2 Room 5", .Category=categories( 4 )} )
			context.Rooms.Add( New Room With {.Id="RM206", .Name="Floor 2 Room 6", .Category=categories( 5 )} )

			context.SaveChanges()
		End Sub

		Private Sub SeedRoomCategories( context As DatabaseContext )

			context.RoomCategories.Add( New RoomCategory With {.Id="NOR01", .Name="Normal 1 for Single", .Price=150000D } )
			context.RoomCategories.Add( New RoomCategory With {.Id="NOR02", .Name="Normal 2 for Single", .Price=200000D } )
			context.RoomCategories.Add( New RoomCategory With {.Id="NOR03", .Name="Normal 1 for Couple", .Price=250000D } )
			context.RoomCategories.Add( New RoomCategory With {.Id="NOR04", .Name="Normal 2 for Couple", .Price=300000D } )
			context.RoomCategories.Add( New RoomCategory With {.Id="VIP01", .Name="V.I.P 1 for Single", .Price=400000D } )
			context.RoomCategories.Add( New RoomCategory With {.Id="VIP02", .Name="V.I.P 1 for Couple", .Price=700000D } )

			context.SaveChanges()
		End Sub
	End Class
End Namespace