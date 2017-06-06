Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries.GetSimpleRoomsList
    Public Class GetSimpleRoomListQuery
        Implements IGetSimpleRoomsListQuery
        
        Private _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute() As IEnumerable(Of SimpleRoomsListItemModel) Implements IGetSimpleRoomsListQuery.Execute
            Dim rooms =
                    _roomRepository.GetAll().Select(
                        Function(r) New SimpleRoomsListItemModel With _
                                                       { .Id = r.Id,
                                                       .Name=r.Name,
                                                       .CategoryName = r.Category.Name,
                                                       .UnitPrice = r.Category.Price
                                                       })
          
            Return rooms
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of SimpleRoomsListItemModel)) Implements IGetSimpleRoomsListQuery.ExecuteAsync
            Dim rooms =
                    Await _roomRepository.GetAll().Select(
                        Function(r) New SimpleRoomsListItemModel With _
                                                       { .Id = r.Id,
                                                       .Name=r.Name,
                                                       .CategoryName = r.Category.Name,
                                                       .UnitPrice = r.Category.Price
                                                       }).ToListAsync()
            Return rooms
        End Function
    End Class
End NameSpace