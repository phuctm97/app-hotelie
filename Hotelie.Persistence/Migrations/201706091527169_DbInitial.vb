Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbInitial
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.BillDetails",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 10, fixedLength := true, unicode := false),
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
            
            CreateTable(
                "dbo.Bills",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 7, fixedLength := true, unicode := false),
                        .CustomerName = c.String(maxLength := 50),
                        .CustomerAddress = c.String(maxLength := 100),
                        .TotalExpense = c.Decimal(nullable := False, storeType := "money"),
                        .UserId = c.String(maxLength := 20, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Users", Function(t) t.UserId) _
                .Index(Function(t) t.UserId)
            
            CreateTable(
                "dbo.Users",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 20, unicode := false),
                        .Password = c.String(nullable := False, maxLength := 50, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Leases",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 7, fixedLength := true, unicode := false),
                        .CheckinDate = c.DateTime(nullable := False),
                        .ExpectedCheckoutDate = c.DateTime(),
                        .RoomPrice = c.Decimal(storeType := "money"),
                        .ExtraCoefficient = c.Double(),
                        .CustomerCoefficient = c.Double(),
                        .Paid = c.Byte(nullable := False),
                        .RoomId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Rooms", Function(t) t.RoomId, cascadeDelete := True) _
                .Index(Function(t) t.RoomId)
            
            CreateTable(
                "dbo.LeaseDetails",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 10, fixedLength := true, unicode := false),
                        .CustomerName = c.String(nullable := False, maxLength := 50),
                        .LicenseId = c.String(maxLength := 20),
                        .Address = c.String(maxLength := 100),
                        .CustomerCategoryId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .LeaseId = c.String(nullable := False, maxLength := 7, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.CustomerCategories", Function(t) t.CustomerCategoryId, cascadeDelete := True) _
                .ForeignKey("dbo.Leases", Function(t) t.LeaseId, cascadeDelete := True) _
                .Index(Function(t) t.CustomerCategoryId) _
                .Index(Function(t) t.LeaseId)
            
            CreateTable(
                "dbo.CustomerCategories",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .Name = c.String(nullable := False, maxLength := 30),
                        .Coefficient = c.Double(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Rooms",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .Name = c.String(nullable := False, maxLength := 50),
                        .State = c.Byte(nullable := False),
                        .Note = c.String(),
                        .CategoryId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.RoomCategories", Function(t) t.CategoryId, cascadeDelete := True) _
                .Index(Function(t) t.CategoryId)
            
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
                "dbo.Parameters",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .MaximumCustomer = c.Int(nullable := False),
                        .ExtraCoefficient = c.Double(nullable := False),
                        .CustomerCoefficient = c.Double(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Permissions",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .Name = c.String(nullable := False, maxLength := 40)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.UserPermissions",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .PermissionId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .UserId = c.String(nullable := False, maxLength := 20, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Permissions", Function(t) t.PermissionId, cascadeDelete := True) _
                .ForeignKey("dbo.Users", Function(t) t.UserId, cascadeDelete := True) _
                .Index(Function(t) t.PermissionId) _
                .Index(Function(t) t.UserId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.UserPermissions", "UserId", "dbo.Users")
            DropForeignKey("dbo.UserPermissions", "PermissionId", "dbo.Permissions")
            DropForeignKey("dbo.BillDetails", "LeaseId", "dbo.Leases")
            DropForeignKey("dbo.Leases", "RoomId", "dbo.Rooms")
            DropForeignKey("dbo.Rooms", "CategoryId", "dbo.RoomCategories")
            DropForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases")
            DropForeignKey("dbo.LeaseDetails", "CustomerCategoryId", "dbo.CustomerCategories")
            DropForeignKey("dbo.BillDetails", "BillId", "dbo.Bills")
            DropForeignKey("dbo.Bills", "UserId", "dbo.Users")
            DropIndex("dbo.UserPermissions", New String() { "UserId" })
            DropIndex("dbo.UserPermissions", New String() { "PermissionId" })
            DropIndex("dbo.Rooms", New String() { "CategoryId" })
            DropIndex("dbo.LeaseDetails", New String() { "LeaseId" })
            DropIndex("dbo.LeaseDetails", New String() { "CustomerCategoryId" })
            DropIndex("dbo.Leases", New String() { "RoomId" })
            DropIndex("dbo.Bills", New String() { "UserId" })
            DropIndex("dbo.BillDetails", New String() { "LeaseId" })
            DropIndex("dbo.BillDetails", New String() { "BillId" })
            DropTable("dbo.UserPermissions")
            DropTable("dbo.Permissions")
            DropTable("dbo.Parameters")
            DropTable("dbo.RoomCategories")
            DropTable("dbo.Rooms")
            DropTable("dbo.CustomerCategories")
            DropTable("dbo.LeaseDetails")
            DropTable("dbo.Leases")
            DropTable("dbo.Users")
            DropTable("dbo.Bills")
            DropTable("dbo.BillDetails")
        End Sub
    End Class
End Namespace
