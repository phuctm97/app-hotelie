Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbUpdate_FixUsersTable_1
        Inherits DbMigration
    
        Public Overrides Sub Up()
            RenameTable(name := "dbo.UserPermissions", newName := "UserCategoryPermissions")
        End Sub
        
        Public Overrides Sub Down()
            RenameTable(name := "dbo.UserCategoryPermissions", newName := "UserPermissions")
        End Sub
    End Class
End Namespace
