Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbUpdate_CustomerCategory_AddCoefficient
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.CustomerCategories", "Coefficient", Function(c) c.Double(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.CustomerCategories", "Coefficient", Function(c) c.Byte(nullable := False))
        End Sub
    End Class
End Namespace
