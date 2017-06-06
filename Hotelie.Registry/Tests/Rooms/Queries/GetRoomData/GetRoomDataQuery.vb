Imports Hotelie.Application.Rooms.Queries.GetRoomData

Namespace Tests.Rooms.Queries.GetRoomData
	Public Class GetRoomDataQuery
		Implements IGetRoomDataQuery

		Public Function Execute( id As String ) As RoomModel Implements IGetRoomDataQuery.Execute
			Dim entity = RoomRepositoryTest.Rooms.FirstOrDefault( Function( r ) r.Id = id )
			Return New RoomModel( entity )
		End Function

		Public Async Function ExecuteAsync( id As String ) As Task(Of RoomModel) Implements IGetRoomDataQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute( id ) )
		End Function
	End Class
End Namespace