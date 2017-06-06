Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbUpdate_addBill
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.Bills",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 7, fixedLength := true, unicode := false),
                        .CustomerName = c.String(maxLength := 50),
                        .CustomerAddress = c.String(maxLength := 100),
                        .TotalExpense = c.Decimal(nullable := False, storeType := "money")
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.BillDetails",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 128),
                        .CheckinDate = c.DateTime(),
                        .NumberOfDays = c.Int(),
                        .ExtraCharge = c.Decimal(storeType := "money"),
                        .TotalExpense = c.Decimal(nullable := False, storeType := "money"),
                        .BillId = c.String(nullable := False, maxLength := 7, fixedLength := true, unicode := false),
                        .LeaseId = c.String(nullable := False, maxLength := 7, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Bills", Function(t) t.BillId, cascadeDelete := True) _
                .ForeignKey("dbo.Leases", Function(t) t.LeaseId) _
                .Index(Function(t) t.BillId) _
                .Index(Function(t) t.LeaseId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.BillDetails", "LeaseId", "dbo.Leases")
            DropForeignKey("dbo.BillDetails", "BillId", "dbo.Bills")
            DropIndex("dbo.BillDetails", New String() { "LeaseId" })
            DropIndex("dbo.BillDetails", New String() { "BillId" })
            DropTable("dbo.BillDetails")
            DropTable("dbo.Bills")
        End Sub
    End Class
End Namespace
