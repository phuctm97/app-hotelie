Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries.GetRoomCategoryData
    Public Class GetRoomCategoryDataQuery
        Implements IGetRoomCategoryDataQuery

        Private _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute(id As String) As RoomCategoryModel Implements IGetRoomCategoryDataQuery.Execute
            Dim roomCategory = _roomRepository.GetRoomCategory(id)
            Return New RoomCategoryModel(roomCategory)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of RoomCategoryModel) Implements IGetRoomCategoryDataQuery.ExecuteAsync
            Dim roomCategory = Await _roomRepository.GetRoomCategoryAsync(id)
            Return New RoomCategoryModel(roomCategory)
        End Function
    End Class
End NameSpace