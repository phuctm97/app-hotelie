Imports System.Data.Entity
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Parameters
Imports Hotelie.Domain.Rooms
Imports Hotelie.Domain.Users

Namespace Common
    Public Class DatabaseInitializer
        Inherits CreateDatabaseIfNotExists(Of DatabaseContext)

        Protected Overrides Sub Seed(context As DatabaseContext)
            MyBase.Seed(context)
            SeedParameters(context)
            SeedPermissions(context)
            SeedUsers(context)
            SeedRoomCategories(context)
            SeedRooms(context)
            SeedCustomerCategories(context)
        End Sub

        Private Sub SeedParameters(context As DatabaseContext)
            context.Parameters.Add(
                New Parameter() _
                                      With {.Id = "1",
                                      .MaximumCustomer = 3,
                                      .CustomerCoefficient = 1.5,
                                      .ExtraCoefficient = 1.5})

            context.SaveChanges()
        End Sub

        Private Sub SeedPermissions(context As DatabaseContext)
            context.Permissions.Add(New Permission() With {.Id = "PM001", .Name = "Manage Rooms"})
            context.Permissions.Add(New Permission() With {.Id = "PM002", .Name = "Add Leases"})
            context.Permissions.Add(New Permission() With {.Id = "PM003", .Name = "Edit Leases"})
            context.Permissions.Add(New Permission() With {.Id = "PM004", .Name = "Remove Leases"})
            context.Permissions.Add(New Permission() With {.Id = "PM005", .Name = "Manage Users"})
            context.Permissions.Add(New Permission() With {.Id = "PM006", .Name = "Manage Rules"})

            context.SaveChanges()
        End Sub

        Private Sub SeedUsers(context As DatabaseContext)
            Dim admin = New User() With {.Id = "admin", .Password = "admin"}
            Dim manageUsers = context.Permissions.First(Function(p) p.Id = "PM005")
            context.Users.Add(admin)
            context.UserPermissions.Add(New UserPermission() With {
                                           .Id = "00001", .User = admin, .Permission = manageUsers})

            context.SaveChanges()
        End Sub

        Private Sub SeedRoomCategories(context As DatabaseContext)
            context.RoomCategories.Add(
                New RoomCategory _
                                          With {.Id = "NOR01", .Name = "Normal 1 for Single", .Price = 150000D})
            context.RoomCategories.Add(
                New RoomCategory _
                                          With {.Id = "NOR02", .Name = "Normal 2 for Single", .Price = 200000D})
            context.RoomCategories.Add(
                New RoomCategory _
                                          With {.Id = "NOR03", .Name = "Normal 1 for Couple", .Price = 250000D})
            context.RoomCategories.Add(
                New RoomCategory _
                                          With {.Id = "NOR04", .Name = "Normal 2 for Couple", .Price = 300000D})
            context.RoomCategories.Add(
                New RoomCategory _
                                          With {.Id = "VIP01", .Name = "V.I.P 1 for Single", .Price = 400000D})
            context.RoomCategories.Add(
                New RoomCategory _
                                          With {.Id = "VIP02", .Name = "V.I.P 1 for Couple", .Price = 700000D})

            context.SaveChanges()
        End Sub

        Private Sub SeedRooms(context As DatabaseContext)
            Dim categories = context.RoomCategories.ToList()

            context.Rooms.Add(New Room With {.Id = "RM101", .Name = "Floor 1 Room 1", .Category = categories(0)})
            context.Rooms.Add(New Room With {.Id = "RM102", .Name = "Floor 1 Room 2", .Category = categories(1)})
            context.Rooms.Add(New Room With {.Id = "RM103", .Name = "Floor 1 Room 3", .Category = categories(2)})
            context.Rooms.Add(New Room With {.Id = "RM104", .Name = "Floor 1 Room 4", .Category = categories(3)})
            context.Rooms.Add(New Room With {.Id = "RM105", .Name = "Floor 1 Room 5", .Category = categories(4)})
            context.Rooms.Add(New Room With {.Id = "RM106", .Name = "Floor 1 Room 6", .Category = categories(5)})
            context.Rooms.Add(New Room With {.Id = "RM201", .Name = "Floor 2 Room 1", .Category = categories(0)})
            context.Rooms.Add(New Room With {.Id = "RM202", .Name = "Floor 2 Room 2", .Category = categories(1)})
            context.Rooms.Add(New Room With {.Id = "RM203", .Name = "Floor 2 Room 3", .Category = categories(2)})
            context.Rooms.Add(New Room With {.Id = "RM204", .Name = "Floor 2 Room 4", .Category = categories(3)})
            context.Rooms.Add(New Room With {.Id = "RM205", .Name = "Floor 2 Room 5", .Category = categories(4)})
            context.Rooms.Add(New Room With {.Id = "RM206", .Name = "Floor 2 Room 6", .Category = categories(5)})

            context.SaveChanges()
        End Sub

        Private Sub SeedCustomerCategories(context As DatabaseContext)
            context.CustomerCategories.Add(New CustomerCategory() _
                                              With {.Id = "CC001", .Name = "Citizen", .Coefficient = 1.0})
            context.CustomerCategories.Add(New CustomerCategory() _
                                              With {.Id = "CC002", .Name = "Foreigner", .Coefficient = 1.5})

            context.SaveChanges()
        End Sub
    End Class
End Namespace