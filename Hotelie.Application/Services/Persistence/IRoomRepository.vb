Imports System.Linq.Expressions
Imports Hotelie.Domain.Rooms

Namespace Services.Persistence
	Public Interface IRoomRepository
		Inherits IRepository(Of Room)

	    Function GetRoomCategory( id As Object ) As RoomCategory

	    Function GetRoomCategoryAsync( id As Object ) As RoomCategory

	    Function GetAllRoomCategories() As IQueryable(Of RoomCategory)

	    Function FindRoomCategory( predicate As Expression(Of Func(Of RoomCategory, Boolean)) ) As IQueryable(Of RoomCategory)

	    Sub AddRoomCategory( entity As RoomCategory )

	    Sub AddRoomCategories( entities As IEnumerable(Of RoomCategory) )

	    Sub RemoveRoomCategory( entity As RoomCategory )

	    Sub RemoveRoomCategories( entities As IEnumerable(Of RoomCategory) )

	End Interface
End Namespace