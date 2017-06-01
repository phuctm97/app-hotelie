Imports System.Linq.Expressions
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common

Namespace Rooms
    Public Class RoomRepository
        Inherits Repository(Of Room)
        Implements IRoomRepository

        Private ReadOnly _context As DatabaseContext

        Public Sub New(context As DatabaseContext)
            MyBase.New(context)

            _context = context
        End Sub

        Public Sub AddRoomCategories(entities As IEnumerable(Of RoomCategory)) _
            Implements IRoomRepository.AddRoomCategories
            _context.RoomCategories.AddRange(entities)
        End Sub

        Public Sub AddRoomCategory(entity As RoomCategory) Implements IRoomRepository.AddRoomCategory
            _context.RoomCategories.Add(entity)
        End Sub

        Public Sub RemoveRoomCategories(entities As IEnumerable(Of RoomCategory)) _
            Implements IRoomRepository.RemoveRoomCategories
            _context.RoomCategories.RemoveRange(entities)
        End Sub

        Public Sub RemoveRoomCategory(entity As RoomCategory) Implements IRoomRepository.RemoveRoomCategory
            _context.RoomCategories.Remove(entity)
        End Sub

        Public Function FindRoomCategory(predicate As Expression(Of Func(Of RoomCategory, Boolean))) _
            As IQueryable(Of RoomCategory) Implements IRoomRepository.FindRoomCategory
            Return _context.RoomCategories.Where(predicate)
        End Function

        Public Function GetAllRoomCategories() As IQueryable(Of RoomCategory) _
            Implements IRoomRepository.GetAllRoomCategories
            Return _context.RoomCategories
        End Function

        Public Overridable Function GetRoomCategory(id As Object) As RoomCategory _
            Implements IRoomRepository.GetRoomCategory

            Dim idString = CType(id, String)
            If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

            Return _context.RoomCategories.FirstOrDefault(Function (p) String.Equals(p.Id, idString))
        End Function
    End Class
End Namespace
