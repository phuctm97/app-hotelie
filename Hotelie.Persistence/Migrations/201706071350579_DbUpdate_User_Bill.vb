Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbUpdate_User_Bill
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Bills", "UserId", Function(c) c.String(maxLength := 20, unicode := false))
            CreateIndex("dbo.Bills", "UserId")
            AddForeignKey("dbo.Bills", "UserId", "dbo.Users", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Bills", "UserId", "dbo.Users")
            DropIndex("dbo.Bills", New String() { "UserId" })
            DropColumn("dbo.Bills", "UserId")
        End Sub
    End Class
End Namespace
