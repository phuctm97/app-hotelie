Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries
    Public Class GetRoomCategoryQuery
        Implements IGetRoomCategoryQuery

        Private ReadOnly _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(id As String) As IRoomCategoryModel Implements IGetRoomCategoryQuery.Execute
            Dim category = _roomRepository.GetRoomCategory(id)
            Return New RoomCategoryModel(category)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of IRoomCategoryModel) Implements IGetRoomCategoryQuery.ExecuteAsync
            Dim category = Await _roomRepository.GetRoomCategoryAsync(id)
            Return New RoomCategoryModel(category)
        End Function
    End Class
End NameSpace