Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbMigration_Initial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.RoomCategories",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .Name = c.String(nullable := False, maxLength := 50),
                        .Price = c.Decimal(nullable := False, storeType := "money")
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Rooms",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .Name = c.String(nullable := False, maxLength := 50),
                        .Note = c.String(),
                        .CategoryId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.RoomCategories", Function(t) t.CategoryId, cascadeDelete := True) _
                .Index(Function(t) t.CategoryId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Rooms", "CategoryId", "dbo.RoomCategories")
            DropIndex("dbo.Rooms", New String() { "CategoryId" })
            DropTable("dbo.Rooms")
            DropTable("dbo.RoomCategories")
        End Sub
    End Class
End Namespace
