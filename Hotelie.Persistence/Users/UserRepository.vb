Imports System.Linq.Expressions
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common

Namespace Users
    Public Class UserRepository
        Inherits Repository(Of User)
        Implements IUserRepository

        Private ReadOnly _databaseService As DatabaseService

        Public Sub New(databaseService As DatabaseService)
            MyBase.New(databaseService)
            _databaseService = databaseService
        End Sub

        Public Sub AddUserCategories(entities As IEnumerable(Of UserCategory)) _
            Implements IUserRepository.AddUserCategories
            Try
                _databaseService.Context.UserCategories.AddRange(entities)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Sub

        Public Sub AddUserCategory(entity As UserCategory) Implements IUserRepository.AddUserCategory
            Try
                _databaseService.Context.UserCategories.Add(entity)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Sub

        Public Sub RemoveUserCategories(entities As IEnumerable(Of UserCategory)) _
            Implements IUserRepository.RemoveUserCategories
            Try
                _databaseService.Context.UserCategories.RemoveRange(entities)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Sub

        Public Sub RemoveUserCategory(entity As UserCategory) Implements IUserRepository.RemoveUserCategory
            Try
                _databaseService.Context.UserCategories.Remove(entity)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Sub

        Public Function FindUserCategory(predicate As Expression(Of Func(Of UserCategory, Boolean))) _
            As IQueryable(Of UserCategory) Implements IUserRepository.FindUserCategory
            Try
                Return _databaseService.Context.UserCategories.Where(predicate)
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Function

        Public Function GetAllUserCategories() As IQueryable(Of UserCategory) _
            Implements IUserRepository.GetAllUserCategories
            Try
                Return _databaseService.Context.UserCategories
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Function

        Public Function GetUserCategory(id As Object) As UserCategory _
            Implements IUserRepository.GetUserCategory
            Try
                Dim idString = CType(id, String)
                If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

                Return _databaseService.Context.UserCategories.FirstOrDefault(Function(p) String.Equals(p.Id, idString))
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Function

        Public Overrides Function GetOne(id As Object) As User
            Try
                Dim idString = CType(id, String)
                If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

                Return _databaseService.Context.Users.FirstOrDefault(Function(p) String.Equals(p.Id, idString))
            Catch
                Throw New DatabaseConnectionFailedException
            End Try
        End Function
    End Class
End Namespace