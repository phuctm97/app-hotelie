Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries.GetRoomCategoriesList
    Public Class GetRoomCategoriesList
        Implements IGetRoomCategoriesListQuery

        Private _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute() As IEnumerable(Of RoomCategoriesListItemModel) Implements IGetRoomCategoriesListQuery.Execute
            Dim categories = _roomRepository.GetAllRoomCategories().Select(Function(r) New RoomCategoriesListItemModel With _
                                                                         { .Id = r.Id,
                                                                         .Name = r.Name,
                                                                         .UnitPrice = r.Price}).ToList()
            
            Return categories
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomCategoriesListItemModel)) Implements IGetRoomCategoriesListQuery.ExecuteAsync
            Dim categories = Await _roomRepository.GetAllRoomCategories().Select(Function(r) New RoomCategoriesListItemModel With _
                                                                              { .Id = r.Id,
                                                                              .Name = r.Name,
                                                                              .UnitPrice = r.Price}).TolistAsync()
            
            Return categories
        End Function
    End Class
End NameSpace