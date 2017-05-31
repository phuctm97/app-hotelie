Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq
Imports Hotelie.Domain.Rooms

Namespace Migrations

    Friend NotInheritable Class Configuration 
        Inherits DbMigrationsConfiguration(Of Common.DatabaseContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
        End Sub

        Protected Overrides Sub Seed(context As Common.DatabaseContext)
            '  This method will be called after migrating to the latest version.

            '  You can use the DbSet(Of T).AddOrUpdate() helper extension method 
            '  to avoid creating duplicate seed data. E.g.
            '
            '    context.People.AddOrUpdate(
            '       Function(c) c.FullName,
            '       New Customer() With {.FullName = "Andrew Peters"},
            '       New Customer() With {.FullName = "Brice Lambson"},
            '       New Customer() With {.FullName = "Rowan Miller"})

            'Dim rc1 = New RoomCategory() With{.Id="NOR01",.Name="Normal type 1",.Price="200000"}
            'Dim rc2 = New RoomCategory() With{.Id="VIP01",.Name="Vip type 1",.Price="400000"}
            
            'context.RoomCategories.AddOrUpdate(
            'Function(rc){rc.Id,rc.Name,rc.Price},
            'New RoomCategory() With{.Id="NOR01",.Name="Normal type 1",.Price="200000"}
            ')


            'context.Rooms.AddOrUpdate(
            '    Function(r) {r.Id,r.Name,r.Category,r.Note,r.State},
            '    New Room() With{.Id="PH101",.Name="101",.Category=rc1,.State=0},
            '    New Room() With{.Id="PH102",.Name="102",.Category=rc2,.State=1,.Note="Khong chan"},
            '    New Room() With{.Id="PH203",.Name="103",.Category=rc1,.State=1,.Note="Khong giuong"},
            '    New Room() With{.Id="PH204",.Name="104",.Category=rc2,.State=1},
            '    New Room() With{.Id="PH205",.Name="105",.Category=rc1,.State=1},
            '    New Room() With{.Id="PH206",.Name="201",.Category=rc2,.State=1,.Note="Khong goi"},
            '    New Room() With{.Id="PH207",.Name="202",.Category=rc1,.State=0},
            '    New Room() With{.Id="PH208",.Name="203",.Category=rc2,.State=0},
            '    New Room() With{.Id="PH209",.Name="204",.Category=rc1,.State=0,.Note="Khong den"},
            '    New Room() With{.Id="PH211",.Name="205",.Category=rc2,.State=0,.Note="Khong oxi"}
            ')
        End Sub

    End Class

End Namespace
