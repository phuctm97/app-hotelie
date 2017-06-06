Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbUpdate_Rebuild_Lease
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.Leases", "BillId", "dbo.Bills")
            DropForeignKey("dbo.Leases", "RoomId", "dbo.Rooms")
            DropForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases")
            DropIndex("dbo.LeaseDetails", New String() { "LeaseId" })
            DropIndex("dbo.Leases", New String() { "BillId" })
            DropPrimaryKey("dbo.LeaseDetails")
            DropPrimaryKey("dbo.Leases")
            AddColumn("dbo.Leases", "CheckinDate", Function(c) c.DateTime(nullable := False))
            AddColumn("dbo.Leases", "ExpectedCheckoutDate", Function(c) c.DateTime())
            AddColumn("dbo.Leases", "RoomPrice", Function(c) c.Decimal(storeType := "money"))
            AlterColumn("dbo.CustomerCategories", "Name", Function(c) c.String(nullable := False, maxLength := 30))
            AlterColumn("dbo.LeaseDetails", "Id", Function(c) c.String(nullable := False, maxLength := 10, fixedLength := true, unicode := false))
            AlterColumn("dbo.LeaseDetails", "LicenseId", Function(c) c.String(maxLength := 20))
            AlterColumn("dbo.LeaseDetails", "LeaseId", Function(c) c.String(maxLength := 7, fixedLength := true, unicode := false))
            AlterColumn("dbo.Leases", "Id", Function(c) c.String(nullable := False, maxLength := 7, fixedLength := true, unicode := false))
            AlterColumn("dbo.Leases", "ExtraCoefficient", Function(c) c.Double())
            AlterColumn("dbo.Leases", "CustomerCoefficient", Function(c) c.Double())
            AddPrimaryKey("dbo.LeaseDetails", "Id")
            AddPrimaryKey("dbo.Leases", "Id")
            CreateIndex("dbo.LeaseDetails", "LeaseId")
            AddForeignKey("dbo.Leases", "RoomId", "dbo.Rooms", "Id")
            AddForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases", "Id")
            DropColumn("dbo.Leases", "BeginDate")
            DropColumn("dbo.Leases", "EndDate")
            DropColumn("dbo.Leases", "Price")
            DropColumn("dbo.Leases", "NumberOfDate")
            DropColumn("dbo.Leases", "ExtraCharge")
            DropColumn("dbo.Leases", "TotalPrice")
            DropColumn("dbo.Leases", "BillId")
            DropTable("dbo.Bills")
        End Sub
        
        Public Overrides Sub Down()
            CreateTable(
                "dbo.Bills",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .CustomerName = c.String(nullable := False, maxLength := 50),
                        .Address = c.String(maxLength := 100),
                        .TotalPrice = c.Decimal(nullable := False, storeType := "money")
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            AddColumn("dbo.Leases", "BillId", Function(c) c.String(maxLength := 5, fixedLength := true, unicode := false))
            AddColumn("dbo.Leases", "TotalPrice", Function(c) c.Decimal(storeType := "money"))
            AddColumn("dbo.Leases", "ExtraCharge", Function(c) c.Decimal(storeType := "money"))
            AddColumn("dbo.Leases", "NumberOfDate", Function(c) c.Int())
            AddColumn("dbo.Leases", "Price", Function(c) c.Decimal(storeType := "money"))
            AddColumn("dbo.Leases", "EndDate", Function(c) c.DateTime(nullable := False))
            AddColumn("dbo.Leases", "BeginDate", Function(c) c.DateTime(nullable := False))
            DropForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases")
            DropForeignKey("dbo.Leases", "RoomId", "dbo.Rooms")
            DropIndex("dbo.LeaseDetails", New String() { "LeaseId" })
            DropPrimaryKey("dbo.Leases")
            DropPrimaryKey("dbo.LeaseDetails")
            AlterColumn("dbo.Leases", "CustomerCoefficient", Function(c) c.Int(nullable := False))
            AlterColumn("dbo.Leases", "ExtraCoefficient", Function(c) c.Double(nullable := False))
            AlterColumn("dbo.Leases", "Id", Function(c) c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false))
            AlterColumn("dbo.LeaseDetails", "LeaseId", Function(c) c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false))
            AlterColumn("dbo.LeaseDetails", "LicenseId", Function(c) c.String(nullable := False, maxLength := 20))
            AlterColumn("dbo.LeaseDetails", "Id", Function(c) c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false))
            AlterColumn("dbo.CustomerCategories", "Name", Function(c) c.String(nullable := False, maxLength := 50))
            DropColumn("dbo.Leases", "RoomPrice")
            DropColumn("dbo.Leases", "ExpectedCheckoutDate")
            DropColumn("dbo.Leases", "CheckinDate")
            AddPrimaryKey("dbo.Leases", "Id")
            AddPrimaryKey("dbo.LeaseDetails", "Id")
            CreateIndex("dbo.Leases", "BillId")
            CreateIndex("dbo.LeaseDetails", "LeaseId")
            AddForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases", "Id", cascadeDelete := True)
            AddForeignKey("dbo.Leases", "RoomId", "dbo.Rooms", "Id", cascadeDelete := True)
            AddForeignKey("dbo.Leases", "BillId", "dbo.Bills", "Id")
        End Sub
    End Class
End Namespace
