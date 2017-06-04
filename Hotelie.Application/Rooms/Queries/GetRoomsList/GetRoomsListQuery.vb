Imports System.Data.Entity
Imports System.Windows.Media
Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries.GetRoomsList
    Public Class GetRoomsListQuery
        Implements IGetRoomsListQuery

        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute() As IEnumerable(Of RoomModel) Implements IGetRoomsListQuery.Execute
            Dim rooms =
                    _roomRepository.GetAll().Select(
                        Function(r) New RoomModel With _
                                                       { .Id = r.Id,
                                                       .State = r.State,
                                                       .Note = r.Note,
                                                       .Name = r.Name,
                                                       .CategoryId=r.Category.Id,
                                                       .CategoryName=r.Category.Name,
                                                       .Price = r.Category.Price
                                                       })
            ' Exception: System.NotSupportedException Unable to create a constant value of type 'System.Windows.Media.Color'
            ' TODO: Add Category display color column to database -> add ".CategoryDisplayColor = r.CategoryDisplayColor"
            For Each room As RoomModel In rooms
                room.CategoryDisplayColor = Colors.Black
            Next
            Return rooms
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of RoomModel)) Implements IGetRoomsListQuery.ExecuteAsync
            Dim rooms = Await _
                    _roomRepository.GetAll().Select(
                        Function(r) New RoomModel With _
                                                       { .Id = r.Id,
                                                       .State = r.State,
                                                       .Note = r.Note,
                                                       .Name = r.Name,
                                                       .CategoryId=r.Category.Id,
                                                       .CategoryName=r.Category.Name,
                                                       .Price = r.Category.Price
                                                       }).ToListAsync
            ' Exception: System.NotSupportedException Unable to create a constant value of type 'System.Windows.Media.Color'
            ' TODO: Add Category display color column to database -> add ".CategoryDisplayColor = r.CategoryDisplayColor"
            For Each room As RoomModel In rooms
                room.CategoryDisplayColor = Colors.Black
            Next
            Return rooms
        End Function
    End Class
End Namespace