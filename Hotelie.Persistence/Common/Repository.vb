Imports System.Data.Entity
Imports System.Linq.Expressions
Imports Hotelie.Application.Services.Persistence

Namespace Common
	Public Class Repository(Of TEntity As Class)
		Implements IRepository(Of TEntity)

		Private ReadOnly _context As DbContext

		Sub New( context As DbContext )
			_context = context
		End Sub

		Public Sub Add( entity As TEntity ) Implements IRepository(Of TEntity).Add
			_context.Set(Of TEntity).Add( entity )
		End Sub

		Public Sub AddRange( entities As IEnumerable(Of TEntity) ) Implements IRepository(Of TEntity).AddRange
			_context.Set(Of TEntity).AddRange( entities )
		End Sub

		Public Sub Remove( entity As TEntity ) Implements IRepository(Of TEntity).Remove
			_context.Set(Of TEntity).Remove( entity )
		End Sub

		Public Sub RemoveRange( entities As IEnumerable(Of TEntity) ) Implements IRepository(Of TEntity).RemoveRange
			_context.Set(Of TEntity).RemoveRange( entities )
		End Sub

		Public Function Find( predicate As Expression(Of Func(Of TEntity, Boolean)) ) As IEnumerable(Of TEntity) _
			Implements IRepository(Of TEntity).Find
			Return _context.Set(Of TEntity).Where( predicate ).ToList()
		End Function

		Public Function GetAll() As IEnumerable(Of TEntity) Implements IRepository(Of TEntity).GetAll
			Return _context.Set(Of TEntity).ToList()
		End Function

		Public Function GetOne( id As Object ) As TEntity Implements IRepository(Of TEntity).GetOne
			Return _context.Set(Of TEntity).Find( id )
		End Function
	End Class
End Namespace
