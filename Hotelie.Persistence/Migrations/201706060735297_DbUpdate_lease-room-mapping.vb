Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbUpdate_leaseroommapping
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.Leases", "RoomId", "dbo.Rooms")
            AddForeignKey("dbo.Leases", "RoomId", "dbo.Rooms", "Id", cascadeDelete := True)
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Leases", "RoomId", "dbo.Rooms")
            AddForeignKey("dbo.Leases", "RoomId", "dbo.Rooms", "Id")
        End Sub
    End Class
End Namespace
