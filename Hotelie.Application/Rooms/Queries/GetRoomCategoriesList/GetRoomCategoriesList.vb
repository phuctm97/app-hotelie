Imports System.Data.Entity
Imports System.Windows.Media
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms

Namespace Rooms.Queries.GetRoomCategoriesList
    Public Class GetRoomCategoriesListQuery
        Implements IGetRoomCategoriesListQuery

        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute() As IEnumerable(Of RoomCategoryModel) Implements IGetRoomCategoriesListQuery.Execute
            Dim rooms = _roomRepository.GetAllRoomCategories().Select(Function(r) New RoomCategoryModel With _
                                                        { .Id = r.Id,
                                                        .Name = r.Name,
                                                        .Price = r.Price})
            ' Exception: System.NotSupportedException Unable to create a constant value of type 'System.Windows.Media.Color'
            ' TODO: Add Category display color column to database -> add ".DisplayColor = r.Color"
            For Each roomCategory As RoomCategoryModel In rooms
                roomCategory.DisplayColor = Colors.Black
            Next
            Return rooms
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomCategoryModel)) Implements IGetRoomCategoriesListQuery.ExecuteAsync
            Dim rooms = Await _roomRepository.GetAllRoomCategories().Select(Function(r) New RoomCategoryModel With _
                                                                         { .Id = r.Id,
                                                                         .Name = r.Id,
                                                                         .Price = r.Price}).ToListAsync()
            ' Exception: System.NotSupportedException Unable to create a constant value of type 'System.Windows.Media.Color'
            ' TODO: Add Category display color column to database -> add ".DisplayColor = r.Color"
            For Each roomCategory As RoomCategoryModel In rooms
                roomCategory.DisplayColor = Colors.Black
            Next
            Return rooms
        End Function
    End Class
End Namespace
