Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbUpdate_User_NowHasNoCategory
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.UserCategoryPermissions", "UserCategoryId", "dbo.UserCategories")
            DropForeignKey("dbo.UserCategoryPermissions", "PermissionId", "dbo.Permissions")
            DropForeignKey("dbo.Users", "CategoryId", "dbo.UserCategories")
            DropIndex("dbo.Users", New String() { "CategoryId" })
            DropIndex("dbo.UserCategoryPermissions", New String() { "UserCategoryId" })
            DropIndex("dbo.UserCategoryPermissions", New String() { "PermissionId" })
            CreateTable(
                "dbo.UserPermissions",
                Function(c) New With
                    {
                        .UserCategoryId = c.String(nullable := False, maxLength := 20, unicode := false),
                        .PermissionId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) New With { t.UserCategoryId, t.PermissionId }) _
                .ForeignKey("dbo.Users", Function(t) t.UserCategoryId, cascadeDelete := True) _
                .ForeignKey("dbo.Permissions", Function(t) t.PermissionId, cascadeDelete := True) _
                .Index(Function(t) t.UserCategoryId) _
                .Index(Function(t) t.PermissionId)
            
            DropColumn("dbo.Users", "CategoryId")
            DropTable("dbo.UserCategories")
            DropTable("dbo.UserCategoryPermissions")
        End Sub
        
        Public Overrides Sub Down()
            CreateTable(
                "dbo.UserCategoryPermissions",
                Function(c) New With
                    {
                        .UserCategoryId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .PermissionId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) New With { t.UserCategoryId, t.PermissionId })
            
            CreateTable(
                "dbo.UserCategories",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .Name = c.String(nullable := False, maxLength := 50)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            AddColumn("dbo.Users", "CategoryId", Function(c) c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false))
            DropForeignKey("dbo.UserPermissions", "PermissionId", "dbo.Permissions")
            DropForeignKey("dbo.UserPermissions", "UserCategoryId", "dbo.Users")
            DropIndex("dbo.UserPermissions", New String() { "PermissionId" })
            DropIndex("dbo.UserPermissions", New String() { "UserCategoryId" })
            DropTable("dbo.UserPermissions")
            CreateIndex("dbo.UserCategoryPermissions", "PermissionId")
            CreateIndex("dbo.UserCategoryPermissions", "UserCategoryId")
            CreateIndex("dbo.Users", "CategoryId")
            AddForeignKey("dbo.Users", "CategoryId", "dbo.UserCategories", "Id", cascadeDelete := True)
            AddForeignKey("dbo.UserCategoryPermissions", "PermissionId", "dbo.Permissions", "Id", cascadeDelete := True)
            AddForeignKey("dbo.UserCategoryPermissions", "UserCategoryId", "dbo.UserCategories", "Id", cascadeDelete := True)
        End Sub
    End Class
End Namespace
