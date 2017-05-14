Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Moq

Namespace Rooms.Queries.GetRoomsList
	< TestClass >
	Public Class GetRoomsListTest
		Private _rooms As List(Of Room)
		Private _repoMock As Mock(Of IRoomRepository)
		Private _query As IGetRoomsListQuery

		< TestInitialize >
		Public Sub TestInitialize()
			' generate random repo
			Dim rand = New Random

			Dim roomCategories = New List(Of RoomCategory)
			For i = 0 To 2
				roomCategories.Add( New RoomCategory With {.Id=i.ToString( "CAT00" ), .Name=$"Category {i}"} )
			Next

			_rooms = New List(Of Room)
			For i = 0 To 10
				_rooms.Add(
					New Room _
					          With {.Id=i.ToString( "PH000" ), .Name=$"Room {i}", .Category=roomCategories( rand.Next( 0, 3 ) )} )
			Next

			' fake the repo
			_repoMock = New Mock(Of IRoomRepository)
			_repoMock.Setup( Function( r ) r.GetAll() ).Returns( _rooms.AsQueryable() )

			' create query
			_query = New GetRoomsListQuery( _repoMock.Object )
		End Sub

		< TestCleanup >
		Public Sub TestCleanup()
			_rooms.Clear()
			_rooms = Nothing

			_repoMock.Reset()
			_repoMock = Nothing

			_query = Nothing
		End Sub

		< TestMethod >
		Public Sub Execute_ShouldReturnCorrectItems()
			' act
			Dim results = _query.Execute().ToList()

			' assert
			Assert.AreEqual( _rooms.Count, results.Count )

			For i = 0 To results.Count() - 1
				Assert.AreEqual( _rooms( i ).Id, results( i ).Id )
				Assert.AreEqual( _rooms( i ).Name, results( i ).Name )
				Assert.AreEqual( _rooms( i ).Category.Name, results( i ).CategoryName )
				Assert.AreEqual( _rooms( i ).Note, results( i ).Note )
			Next
		End Sub
	End Class
End Namespace