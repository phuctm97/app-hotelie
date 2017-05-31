Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class DbInitialize_FirstComplete
        Inherits DbMigration
    
        Public Overrides Sub Up()
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
            
            CreateTable(
                "dbo.CustomerCategories",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .Name = c.String(nullable := False, maxLength := 50)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.LeaseDetails",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .CustomerName = c.String(nullable := False, maxLength := 50),
                        .LicenseId = c.String(nullable := False, maxLength := 20),
                        .Address = c.String(maxLength := 100),
                        .CustomerCategoryId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .LeaseId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.CustomerCategories", Function(t) t.CustomerCategoryId, cascadeDelete := True) _
                .ForeignKey("dbo.Leases", Function(t) t.LeaseId, cascadeDelete := True) _
                .Index(Function(t) t.CustomerCategoryId) _
                .Index(Function(t) t.LeaseId)
            
            CreateTable(
                "dbo.Leases",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .BeginDate = c.DateTime(nullable := False),
                        .EndDate = c.DateTime(nullable := False),
                        .Price = c.Decimal(storeType := "money"),
                        .NumberOfDate = c.Int(),
                        .ExtraCoefficient = c.Double(nullable := False),
                        .CustomerCoefficient = c.Int(nullable := False),
                        .ExtraCharge = c.Decimal(storeType := "money"),
                        .TotalPrice = c.Decimal(storeType := "money"),
                        .BillId = c.String(maxLength := 5, fixedLength := true, unicode := false),
                        .RoomId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Bills", Function(t) t.BillId) _
                .ForeignKey("dbo.Rooms", Function(t) t.RoomId, cascadeDelete := True) _
                .Index(Function(t) t.BillId) _
                .Index(Function(t) t.RoomId)
            
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
                "dbo.UserCategories",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .Name = c.String(nullable := False, maxLength := 50)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Users",
                Function(c) New With
                    {
                        .Id = c.String(nullable := False, maxLength := 20, unicode := false),
                        .Password = c.String(nullable := False, maxLength := 50, unicode := false),
                        .CategoryId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.UserCategories", Function(t) t.CategoryId, cascadeDelete := True) _
                .Index(Function(t) t.CategoryId)
            
            CreateTable(
                "dbo.UserPermissions",
                Function(c) New With
                    {
                        .UserCategoryId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false),
                        .PermissionId = c.String(nullable := False, maxLength := 5, fixedLength := true, unicode := false)
                    }) _
                .PrimaryKey(Function(t) New With { t.UserCategoryId, t.PermissionId }) _
                .ForeignKey("dbo.UserCategories", Function(t) t.UserCategoryId, cascadeDelete := True) _
                .ForeignKey("dbo.Permissions", Function(t) t.PermissionId, cascadeDelete := True) _
                .Index(Function(t) t.UserCategoryId) _
                .Index(Function(t) t.PermissionId)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Users", "CategoryId", "dbo.UserCategories")
            DropForeignKey("dbo.UserPermissions", "PermissionId", "dbo.Permissions")
            DropForeignKey("dbo.UserPermissions", "UserCategoryId", "dbo.UserCategories")
            DropForeignKey("dbo.LeaseDetails", "LeaseId", "dbo.Leases")
            DropForeignKey("dbo.Leases", "RoomId", "dbo.Rooms")
            DropForeignKey("dbo.Rooms", "CategoryId", "dbo.RoomCategories")
            DropForeignKey("dbo.Leases", "BillId", "dbo.Bills")
            DropForeignKey("dbo.LeaseDetails", "CustomerCategoryId", "dbo.CustomerCategories")
            DropIndex("dbo.UserPermissions", New String() { "PermissionId" })
            DropIndex("dbo.UserPermissions", New String() { "UserCategoryId" })
            DropIndex("dbo.Users", New String() { "CategoryId" })
            DropIndex("dbo.Rooms", New String() { "CategoryId" })
            DropIndex("dbo.Leases", New String() { "RoomId" })
            DropIndex("dbo.Leases", New String() { "BillId" })
            DropIndex("dbo.LeaseDetails", New String() { "LeaseId" })
            DropIndex("dbo.LeaseDetails", New String() { "CustomerCategoryId" })
            DropTable("dbo.UserPermissions")
            DropTable("dbo.Users")
            DropTable("dbo.UserCategories")
            DropTable("dbo.Permissions")
            DropTable("dbo.Parameters")
            DropTable("dbo.RoomCategories")
            DropTable("dbo.Rooms")
            DropTable("dbo.Leases")
            DropTable("dbo.LeaseDetails")
            DropTable("dbo.CustomerCategories")
            DropTable("dbo.Bills")
        End Sub
    End Class
End Namespace
