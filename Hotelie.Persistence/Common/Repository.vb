Imports System.Linq.Expressions
Imports Hotelie.Application.Services.Persistence

Namespace Common
    Public Class Repository (Of TEntity As Class)
        Implements IRepository(Of TEntity)

        Private ReadOnly _databaseService As IDatabaseService

        Sub New(databaseService As DatabaseService)
            _databaseService = databaseService
        End Sub

        Public Sub Add(entity As TEntity) Implements IRepository(Of TEntity).Add
            Try
                _databaseService.Context.Set (Of TEntity).Add(entity)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Sub

        Public Sub AddRange(entities As IEnumerable(Of TEntity)) Implements IRepository(Of TEntity).AddRange
            Try
                _databaseService.Context.Set (Of TEntity).AddRange(entities)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Sub

        Public Sub Remove(entity As TEntity) Implements IRepository(Of TEntity).Remove
            Try
                _databaseService.Context.Set (Of TEntity).Remove(entity)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Sub

        Public Sub RemoveRange(entities As IEnumerable(Of TEntity)) Implements IRepository(Of TEntity).RemoveRange
            Try
                _databaseService.Context.Set (Of TEntity).RemoveRange(entities)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Sub

        Public Function Find(predicate As Expression(Of Func(Of TEntity, Boolean))) As IQueryable(Of TEntity) _
            Implements IRepository(Of TEntity).Find
            Try
                Return _databaseService.Context.Set (Of TEntity).Where(predicate)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Function

        Public Function GetAll() As IQueryable(Of TEntity) Implements IRepository(Of TEntity).GetAll
            Try
                Return _databaseService.Context.Set (Of TEntity)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Function

        Public Overridable Function GetOne(id As Object) As TEntity Implements IRepository(Of TEntity).GetOne
            Throw _
                New NotImplementedException(
                    "Not implemented yet because generic do not know entity key property to compare. It should be overrided in derived class of specific entity type")
        End Function
    End Class
End Namespace
