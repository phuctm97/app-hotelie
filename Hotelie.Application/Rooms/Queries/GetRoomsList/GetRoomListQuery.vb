Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries.GetRoomsList
    Public Class GetRoomsListQuery
        Implements IGetRoomsListQuery

        Private ReadOnly _roomRepository As IRoomRepository

        Sub New( roomRepository As IRoomRepository )
            _roomRepository = roomRepository
        End Sub

        Public Function Execute() As IEnumerable(Of RoomModel) Implements IGetRoomsListQuery.Execute
            Dim rooms =
                    _roomRepository.GetAll().Select(
                        Function( r ) New RoomModel With _
                                                       { .Name=r.Name,
                                                       .CategoryName=r.Category.Name,
                                                       .Note=r.Note,
                                                       .Price=r.Category.Price,
                                                       .State=r.State} )

            Return rooms
        End Function
    End Class
End Namespace