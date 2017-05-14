Imports System.Data.Entity
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Moq

< TestClass >
Public Class RepositoryTest
	Private _setMock As Mock(Of DbSet(Of Room))
	Private _contextMock As Mock(Of DatabaseContext)
	Private _rooms As List(Of Room)

	Private _repository As Repository(Of Room)

	< TestInitialize >
	Public Sub TestInitialize()
		_rooms = New List(Of Room) From {
			New Room With {.Id="RMT01", .Name="Room Test 1"},
			New Room With {.Id="RMT02", .Name="Room Test 2"},
			New Room With {.Id="RMT03", .Name="Room Test 3"},
			New Room With {.Id="RMT04", .Name="Room Test 4"},
			New Room With {.Id="RMT05", .Name="Room Test 5"}}

		_setMock = New Mock(Of DbSet(Of Room))
		_setMock.As(Of IQueryable(Of Room)).Setup( Function( m ) m.Provider ).Returns( _rooms.AsQueryable().Provider )
		_setMock.As(Of IQueryable(Of Room)).Setup( Function( m ) m.Expression ).Returns( _rooms.AsQueryable().Expression )
		_setMock.As(Of IQueryable(Of Room)).Setup( Function( m ) m.ElementType ).Returns( _rooms.AsQueryable().ElementType )
		_setMock.As(Of IQueryable(Of Room)).Setup( Function( m ) m.GetEnumerator() ).Returns(
			_rooms.AsQueryable().GetEnumerator() )

		_contextMock = New Mock(Of DatabaseContext)
		_contextMock.Setup( Function( c ) c.Set(Of Room) ).Returns( _setMock.Object )

		_repository = New Repository(Of Room)( _contextMock.Object )
	End Sub

	< TestCleanup >
	Public Sub TestCleanup()
		_repository = Nothing

		_contextMock.Reset()
		_contextMock = Nothing

		_setMock.Reset()
		_setMock = Nothing

		_rooms.Clear()
		_rooms = Nothing
	End Sub

	< TestMethod >
	Public Sub Add_WithValidItem_ShouldContextSetAddCalledOnce()
		_repository.Add( New Room With {.Id = "RMT10", .Name="Test Room 10"} )

		_setMock.Verify( Function( m ) m.Add( It.IsAny(Of Room) ), Times.Once )
	End Sub

	< TestMethod >
	Public Sub GetOne_WithValidId_ShouldReturnCorrectItem()
		Dim room = _repository.GetAll().FirstOrDefault()

		Assert.IsNotNull( room )
		Assert.AreEqual( _rooms( 0 ).Id, room.Id )
		Assert.AreEqual( _rooms( 0 ).Name, room.Name )
	End Sub
End Class