Imports System.Linq.Expressions
Imports Hotelie.Domain.Users

Namespace Services.Persistence
    Public Interface IUserRepository
        Inherits IRepository(Of User)

        Function GetRoomCategory( id As Object ) As UserCategory

        Function GetAllRoomCategories() As IQueryable(Of UserCategory)

        Function FindRoomCategory( predicate As Expression(Of Func(Of UserCategory, Boolean)) ) As IQueryable(Of UserCategory)

        Sub AddRoomCategory( entity As UserCategory )

        Sub AddRoomCategories( entities As IEnumerable(Of UserCategory) )

        Sub RemoveRoomCategory( entity As UserCategory )

        Sub RemoveRoomCategories( entities As IEnumerable(Of UserCategory) )

    End Interface
End NameSpace