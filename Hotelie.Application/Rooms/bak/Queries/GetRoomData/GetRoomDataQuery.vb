Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries.GetRoomData
    Public Class GetRoomDataQuery
        Implements IGetRoomDataQuery

        Private _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(id As String) As RoomModel Implements IGetRoomDataQuery.Execute
            Dim room = _roomRepository.GetOne(id)
            Return New RoomModel(room)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of RoomModel) Implements IGetRoomDataQuery.ExecuteAsync
            Dim room = Await _roomRepository.GetOneAsync(id)
            Return New RoomModel(room)
        End Function
    End Class
End NameSpace