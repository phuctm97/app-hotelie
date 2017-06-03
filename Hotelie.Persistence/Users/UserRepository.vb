Imports System.Linq.Expressions
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.DatabaseServices

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
            _databaseService.Context.UserCategories.AddRange(entities)
        End Sub

        Public Sub AddUserCategory(entity As UserCategory) Implements IUserRepository.AddUserCategory
            _databaseService.Context.UserCategories.Add(entity)
        End Sub

        Public Sub RemoveUserCategories(entities As IEnumerable(Of UserCategory)) _
            Implements IUserRepository.RemoveUserCategories
            _databaseService.Context.UserCategories.RemoveRange(entities)
        End Sub

        Public Sub RemoveUserCategory(entity As UserCategory) Implements IUserRepository.RemoveUserCategory
            _databaseService.Context.UserCategories.Remove(entity)
        End Sub

        Public Function FindUserCategory(predicate As Expression(Of Func(Of UserCategory, Boolean))) _
            As IQueryable(Of UserCategory) Implements IUserRepository.FindUserCategory
            Return _databaseService.Context.UserCategories.Where(predicate)
        End Function

        Public Function GetAllUserCategories() As IQueryable(Of UserCategory) _
            Implements IUserRepository.GetAllUserCategories
            Return _databaseService.Context.UserCategories
        End Function

        Public Function GetUserCategory(id As Object) As UserCategory _
            Implements IUserRepository.GetUserCategory

            Dim idString = CType(id, String)
            If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

            Return _databaseService.Context.UserCategories.FirstOrDefault(Function(p) String.Equals(p.Id, idString))
        End Function

        Public Overrides Function GetOne(id As Object) As User
            Dim idString = CType(id, String)
            If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

            Return _databaseService.Context.Users.FirstOrDefault(Function(p) String.Equals(p.Id, idString))
        End Function
    End Class
End Namespace