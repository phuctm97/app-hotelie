Imports System.Data.Entity
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Parameters
Imports Hotelie.Domain.Rooms
Imports Hotelie.Domain.Users

Namespace Services.Persistence
    Public Interface IDatabaseContext
        Inherits IDisposable
        Property Bills As DbSet(Of Bill)
        Property CustomerCategories As DbSet(Of CustomerCategory)
        Property LeaseDetails As DbSet(Of LeaseDetail)
        Property Leases As DbSet(Of Lease)
        Property Parameters As DbSet(Of Parameter)
        Property Permissions As DbSet(Of Permission)
        Property RoomCategories As DbSet(Of RoomCategory)
        Property Rooms As DbSet(Of Room)
        Property UserCategories As DbSet(Of UserCategory)
        Property Users As DbSet(Of User)
        Function [Set](Of TEntity As Class) As DbSet(Of TEntity)
        Function SaveChanges() As Integer
    End Interface
End Namespace
