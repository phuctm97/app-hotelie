Imports System.Data.Entity
Imports System.Linq.Expressions
Imports Hotelie.Application.Services.Persistence

Namespace Common
	Public Class Repository(Of TEntity As Class)
		Implements IRepository(Of TEntity)

		Private ReadOnly _databaseService As IDatabaseService

		Sub New( databaseService As DatabaseService )
			_databaseService = databaseService
		End Sub

		Public Sub Add( entity As TEntity ) Implements IRepository(Of TEntity).Add
			_databaseService.Context.Set(Of TEntity).Add( entity )
		End Sub

		Public Sub AddRange( entities As IEnumerable(Of TEntity) ) Implements IRepository(Of TEntity).AddRange
			_databaseService.Context.Set(Of TEntity).AddRange( entities )
		End Sub

		Public Sub Remove( entity As TEntity ) Implements IRepository(Of TEntity).Remove
			_databaseService.Context.Set(Of TEntity).Remove( entity )
		End Sub

		Public Sub RemoveRange( entities As IEnumerable(Of TEntity) ) Implements IRepository(Of TEntity).RemoveRange
			_databaseService.Context.Set(Of TEntity).RemoveRange( entities )
		End Sub

		Public Function Find( predicate As Expression(Of Func(Of TEntity, Boolean)) ) As IQueryable(Of TEntity) _
			Implements IRepository(Of TEntity).Find
			Return _databaseService.Context.Set(Of TEntity).Where( predicate )
		End Function

		Public Function GetAll() As IQueryable(Of TEntity) Implements IRepository(Of TEntity).GetAll
			Return _databaseService.Context.Set(Of TEntity)
		End Function

		Public Overridable Function GetOne( id As Object ) As TEntity Implements IRepository(Of TEntity).GetOne
			Throw _
				New NotImplementedException(
					"Not implemented yet because generic do not know entity key property to compare. It should be overrided in derived class of specific entity type" )
		End Function
	End Class
End Namespace
