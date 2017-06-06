Imports Hotelie.Application.Rooms.Queries.GetRoomCategoryData

Namespace Tests.Rooms.Queries.GetRoomCategoryData
	Public Class GetRoomCategoryDataQuery
		Implements IGetRoomCategoryDataQuery

		Public Function Execute( id As String ) As RoomCategoryModel Implements IGetRoomCategoryDataQuery.Execute
			Dim entity = RoomRepositoryTest.RoomCategories.FirstOrDefault( Function( r ) r.Id = id )
			Return New RoomCategoryModel( entity )
		End Function

		Public Async Function ExecuteAsync( id As String ) As Task(Of RoomCategoryModel) _
			Implements IGetRoomCategoryDataQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute( id ) )
		End Function
	End Class
End Namespace