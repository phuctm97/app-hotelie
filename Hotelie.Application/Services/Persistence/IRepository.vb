Imports System.Linq.Expressions

Namespace Services.Persistence
	Public Interface IRepository(Of TEntity)

		Function GetOne( id As Object ) As TEntity

		Function GetAll() As IQueryable(Of TEntity)

		Function Find( predicate As Expression(Of Func(Of TEntity, Boolean)) ) As IQueryable(Of TEntity)

		Sub Add( entity As TEntity )

		Sub AddRange( entities As IEnumerable(Of TEntity) )

		Sub Remove( entity As TEntity )

		Sub RemoveRange( entities As IEnumerable(Of TEntity) )
	End Interface
End Namespace