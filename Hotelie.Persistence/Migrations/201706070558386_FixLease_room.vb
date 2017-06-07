Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class FixLease_room
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.Leases", "RoomId", "dbo.Rooms")
            AddForeignKey("dbo.Leases", "RoomId", "dbo.Rooms", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Leases", "RoomId", "dbo.Rooms")
            AddForeignKey("dbo.Leases", "RoomId", "dbo.Rooms", "Id", cascadeDelete := True)
        End Sub
    End Class
End Namespace
