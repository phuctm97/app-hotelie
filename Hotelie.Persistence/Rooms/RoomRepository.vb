Imports System.Linq.Expressions
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common

Namespace Rooms
    Public Class RoomRepository
        Inherits Repository(Of Room)
        Implements IRoomRepository

        Private ReadOnly _databaseService As DatabaseService

        Public Sub New(databaseService As DatabaseService)
            MyBase.New(databaseService)

            _databaseService = databaseService
        End Sub

        Public Sub AddRoomCategories(entities As IEnumerable(Of RoomCategory)) _
            Implements IRoomRepository.AddRoomCategories
            _databaseService.Context.RoomCategories.AddRange(entities)
        End Sub

        Public Sub AddRoomCategory(entity As RoomCategory) Implements IRoomRepository.AddRoomCategory
            _databaseService.Context.RoomCategories.Add(entity)
        End Sub

        Public Sub RemoveRoomCategories(entities As IEnumerable(Of RoomCategory)) _
            Implements IRoomRepository.RemoveRoomCategories
            _databaseService.Context.RoomCategories.RemoveRange(entities)
        End Sub

        Public Sub RemoveRoomCategory(entity As RoomCategory) Implements IRoomRepository.RemoveRoomCategory
            _databaseService.Context.RoomCategories.Remove(entity)
        End Sub

        Public Function FindRoomCategory(predicate As Expression(Of Func(Of RoomCategory, Boolean))) _
            As IQueryable(Of RoomCategory) Implements IRoomRepository.FindRoomCategory
            Return _databaseService.Context.RoomCategories.Where(predicate)
        End Function

        Public Function GetAllRoomCategories() As IQueryable(Of RoomCategory) _
            Implements IRoomRepository.GetAllRoomCategories
            Return _databaseService.Context.RoomCategories
        End Function

        Public Function GetRoomCategory(id As Object) As RoomCategory _
            Implements IRoomRepository.GetRoomCategory

            Dim idString = CType(id, String)
            If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

            Return _databaseService.Context.RoomCategories.FirstOrDefault(Function(p) String.Equals(p.Id, idString))
        End Function

        Public Overrides Function GetOne(id As Object) As Room
            Dim idString = CType(id, String)
            If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

            Return _databaseService.Context.Rooms.FirstOrDefault(Function(p) String.Equals(p.Id, idString))
        End Function
    End Class
End Namespace
