Imports System.Data.Entity
Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms

Namespace Rooms.Queries
    Public Class GetAllRoomsQuery
        Implements IGetAllRoomsQuery

        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute() As IEnumerable(Of IRoomModel) Implements IGetAllRoomsQuery.Execute
            Dim rooms = _roomRepository.GetAll().ToList()
            Dim roomModels = New List(Of RoomModel)
            For Each room As Room In rooms
                roomModels.Add(New RoomModel(room))
            Next
            Return roomModels
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of IRoomModel)) Implements IGetAllRoomsQuery.ExecuteAsync
            Dim rooms = Await _roomRepository.GetAll().ToListAsync()
            Dim roomModels = New List(Of RoomModel)
            For Each room As Room In rooms
                roomModels.Add(New RoomModel(room))
            Next
            Return roomModels
        End Function
    End Class
End NameSpace