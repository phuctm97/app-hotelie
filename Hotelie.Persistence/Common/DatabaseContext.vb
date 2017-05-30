Imports System.Data.Entity
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Parameters
Imports Hotelie.Domain.Rooms
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Parameters
Imports Hotelie.Persistence.Rooms
Imports Hotelie.Persistence.Users

Namespace Common
    Public Class DatabaseContext
        Inherits DbContext

        Public Property Rooms As DbSet(Of Room)
        Public Property RoomCategories As DbSet(Of RoomCategory)
        Public Property Leases As DbSet(Of Lease)
        Public Property LeaseDetails As DbSet(Of LeaseDetail)
        Public Property CustomerCategories As DbSet(Of CustomerCategory)
        Public Property Bills As DbSet(Of Bill)
        Public Property Parameters As DbSet(Of Parameter)
        Public Property Users As DbSet(of User)
        Public Property Permissions As DbSet(of Permission)
        Public Property UserCategories As DbSet(of UserCategory)

        Public Sub New()
            MyBase.New("name=DatabaseContext")

            Database.SetInitializer(New DatabaseInitializer)
        End Sub

        Public Sub New(connectionString As String)
            
            MyBase.New(connectionString)

            Database.SetInitializer(New DatabaseInitializer)
        
        End Sub

        Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
            MyBase.OnModelCreating(modelBuilder)

            modelBuilder.Configurations.Add(New RoomCategoryConfiguration)
            modelBuilder.Configurations.Add(New RoomConfiguration)
            modelBuilder.Configurations.Add(New ParameterConfiguration)
            modelBuilder.Configurations.Add(New LeaseConfiguration)
            modelBuilder.Configurations.Add(New LeaseDetailConfiguration)
            modelBuilder.Configurations.Add(New BillConfiguration)
            modelBuilder.Configurations.Add(New CustomerCategoryConfiguration)
            modelBuilder.Configurations.Add(New UserConfiguration)
            modelBuilder.Configurations.Add(New PermissionConfiguration)
            modelBuilder.Configurations.Add(New UserCategoryConfiguration)
        End Sub
    End Class
End Namespace