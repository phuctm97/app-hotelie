Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbUpdate_Lease_PropertyPaid
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Leases", "Paid", Function(c) c.Byte(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Leases", "Paid")
        End Sub
    End Class
End Namespace
