Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries
    Public Class GetCanRemoveRoomCategory
        Implements IGetCanRemoveRoomCategoryQuery

        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(id As String) As Boolean Implements IGetCanRemoveRoomCategoryQuery.Execute
            Dim category = _roomRepository.GetRoomCategory(id)
            Dim rooms = _roomRepository.Find(Function(p)p.Category.Id = category.Id).ToList()
            If IsNothing(rooms) Then Return True
            Return False
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of Boolean) Implements IGetCanRemoveRoomCategoryQuery.ExecuteAsync
            Dim category = Await _roomRepository.GetRoomCategoryAsync(id)
            Dim rooms = Await _roomRepository.Find(Function(p)p.Category.Id = category.Id).ToListAsync()
            If IsNothing(rooms) Then Return True
            Return False
        End Function
    End Class
End NameSpace