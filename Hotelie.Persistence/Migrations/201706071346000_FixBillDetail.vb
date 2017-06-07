Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class FixBillDetail
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropPrimaryKey("dbo.BillDetails")
            AlterColumn("dbo.BillDetails", "Id", Function(c) c.String(nullable := False, maxLength := 10, fixedLength := true, unicode := false))
            AddPrimaryKey("dbo.BillDetails", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropPrimaryKey("dbo.BillDetails")
            AlterColumn("dbo.BillDetails", "Id", Function(c) c.String(nullable := False, maxLength := 128))
            AddPrimaryKey("dbo.BillDetails", "Id")
        End Sub
    End Class
End Namespace
