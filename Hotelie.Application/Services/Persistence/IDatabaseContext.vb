Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports Hotelie.Domain.Bills
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Parameters
Imports Hotelie.Domain.Rooms
Imports Hotelie.Domain.Users

Namespace Services.Persistence
    Public Interface IDatabaseContext
        Inherits IDisposable
        
        Property CustomerCategories As DbSet(Of CustomerCategory)
        Property LeaseDetails As DbSet(Of LeaseDetail)
        Property Leases As DbSet(Of Lease)
        Property Parameters As DbSet(Of Parameter)
        Property Permissions As DbSet(Of Permission)
        Property RoomCategories As DbSet(Of RoomCategory)
        Property Rooms As DbSet(Of Room)
        Property Users As DbSet(Of User)
        Property Bills As DbSet(Of Bill)
        Property BillDetails As DbSet(Of BillDetail)
        Function [Set](Of TEntity As Class) As DbSet(Of TEntity)
        Function SaveChanges() As Integer
        Function SaveChangesAsync() As Task(Of Integer)
    End Interface
End Namespace
