Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries
    Public Class GetRooomQuery
        Implements IGetRoomQuery

        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(id As String) As IRoomModel Implements IGetRoomQuery.Execute
            Dim room = _roomRepository.GetOne(id)
            Return New RoomModel(room)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of IRoomModel) Implements IGetRoomQuery.ExecuteAsync
            Dim room = Await _roomRepository.GetOneAsync(id)
            Return New RoomModel(room)
        End Function
    End Class
End NameSpace