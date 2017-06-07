Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class FixLease
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases")
            DropIndex("dbo.LeaseDetails", New String() { "LeaseId" })
            AlterColumn("dbo.LeaseDetails", "LeaseId", Function(c) c.String(nullable := False, maxLength := 7, fixedLength := true, unicode := false))
            CreateIndex("dbo.LeaseDetails", "LeaseId")
            AddForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases", "Id", cascadeDelete := True)
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases")
            DropIndex("dbo.LeaseDetails", New String() { "LeaseId" })
            AlterColumn("dbo.LeaseDetails", "LeaseId", Function(c) c.String(maxLength := 7, fixedLength := true, unicode := false))
            CreateIndex("dbo.LeaseDetails", "LeaseId")
            AddForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases", "Id")
        End Sub
    End Class
End Namespace
