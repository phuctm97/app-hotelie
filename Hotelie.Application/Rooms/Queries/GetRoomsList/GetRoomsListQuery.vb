Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries.GetRoomsList
    Public Class GetRoomsListQuery
        Implements IGetRoomsListQuery

        Private _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute() As IEnumerable(Of RoomsListItemModel) Implements IGetRoomsListQuery.Execute
            Dim rooms =
                    _roomRepository.GetAll().Select(
                        Function(r) New RoomsListItemModel With _
                                                       { .Id = r.Id,
                                                       .Name=r.Name,
                                                       .CategoryName = r.Category.Name,
                                                       .UnitPrice = r.Category.Price,
                                                       .CategoryId = r.Category.Id,
                                                       .State = r.State
                                                       }).ToList()
          
            Return rooms
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomsListItemModel)) Implements IGetRoomsListQuery.ExecuteAsync
            Dim rooms =
                    Await _roomRepository.GetAll().Select(
                        Function(r) New RoomsListItemModel With _
                                                       { .Id = r.Id,
                                                       .Name=r.Name,
                                                       .CategoryName = r.Category.Name,
                                                       .UnitPrice = r.Category.Price,
                                                       .CategoryId = r.Category.Id,
                                                       .State = r.State
                                                       }).ToListAsync()
          
            Return rooms
        End Function
    End Class
End NameSpace