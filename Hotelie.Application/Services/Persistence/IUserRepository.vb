Imports System.Linq.Expressions
Imports Hotelie.Domain.Users

Namespace Services.Persistence
    Public Interface IUserRepository
        Inherits IRepository(Of User)

        Function GetUserCategory( id As Object ) As UserCategory
        Function GetUserCategoryAsync( id As Object ) As UserCategory

        Function GetAllUserCategories() As IQueryable(Of UserCategory)

        Function FindUserCategory( predicate As Expression(Of Func(Of UserCategory, Boolean)) ) As IQueryable(Of UserCategory)

        Sub AddUserCategory( entity As UserCategory )

        Sub AddUserCategories( entities As IEnumerable(Of UserCategory) )

        Sub RemoveUserCategory( entity As UserCategory )
       
        Sub RemoveUserCategories( entities As IEnumerable(Of UserCategory) )

    End Interface
End NameSpace