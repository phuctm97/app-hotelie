Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Bills
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Parameters
Imports Hotelie.Domain.Rooms
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Bills
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Parameters
Imports Hotelie.Persistence.Rooms
Imports Hotelie.Persistence.Users

Namespace Common
    Public Class DatabaseContext
        Inherits DbContext
        Implements IDatabaseContext

        Public Property Rooms As DbSet(Of Room) Implements IDatabaseContext.Rooms
        Public Property RoomCategories As DbSet(Of RoomCategory) Implements IDatabaseContext.RoomCategories
        Public Property Leases As DbSet(Of Lease) Implements IDatabaseContext.Leases
        Public Property LeaseDetails As DbSet(Of LeaseDetail) Implements IDatabaseContext.LeaseDetails
        Public Property CustomerCategories As DbSet(Of CustomerCategory) Implements IDatabaseContext.CustomerCategories
        Public Property Parameters As DbSet(Of Parameter) Implements IDatabaseContext.Parameters
        Public Property Users As DbSet(of User) Implements IDatabaseContext.Users
        Public Property Bills As DbSet(of Bill) Implements IDatabaseContext.Bills
        Public Property BillDetails As DbSet(Of BillDetail) Implements IDatabaseContext.BillDetails


        Public Overrides Function [Set] (Of TEntity As Class)() As DbSet(Of TEntity) Implements IDatabaseContext.[Set]
            Return MyBase.[Set] (Of TEntity)()
        End Function

        Public Overrides Function SaveChanges() As Integer Implements IDatabaseContext.SaveChanges
            Return MyBase.SaveChanges()
        End Function

        Public Overrides Function SaveChangesAsync() As Task(Of Integer) _
            Implements IDatabaseContext.SaveChangesAsync
            Return MyBase.SaveChangesAsync()
        End Function

        Public Property Permissions As DbSet(of Permission) Implements IDatabaseContext.Permissions
        Public Property UserCategories As DbSet(of UserCategory) Implements IDatabaseContext.UserCategories

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
            modelBuilder.Configurations.Add(New UserConfiguration)
            modelBuilder.Configurations.Add(New PermissionConfiguration)
            modelBuilder.Configurations.Add(New UserCategoryConfiguration)
            modelBuilder.Configurations.Add(New LeaseConfiguration)
            modelBuilder.Configurations.Add(New LeaseDetailConfiguration)
            modelBuilder.Configurations.Add(New CustomerCategoryConfiguration)
            modelBuilder.Configurations.Add(New BillConfiguration)
            modelBuilder.Configurations.Add(New BillDetailConfiguration)
        End Sub
    End Class
End Namespace