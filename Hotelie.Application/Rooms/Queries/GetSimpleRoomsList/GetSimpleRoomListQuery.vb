Imports Hotelie.Application.Services.Persistence

Namespace Rooms.Queries.GetSimpleRoomsList
    Public Class GetSimpleRoomListQuery
        Implements IGetSimpleRoomsListQuery
        
        Private _roomRepository As IRoomRepository

        Sub New(roomRepository As IRoomRepository)
            _roomRepository = roomRepository
        End Sub

        Public Function Execute() As IEnumerable(Of SimpleRoomsListItemModel) Implements IGetSimpleRoomsListQuery.Execute
            Return Nothing
        End Function

        Public Function ExecuteAsync() As Task(Of IEnumerable(Of SimpleRoomsListItemModel)) Implements IGetSimpleRoomsListQuery.ExecuteAsync
            Throw New NotImplementedException()
        End Function
    End Class
End NameSpace