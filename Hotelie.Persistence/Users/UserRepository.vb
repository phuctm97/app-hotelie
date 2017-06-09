Imports System.Data.Entity
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


        Public Overrides Function GetOne(id As Object) As User
            Dim idString = CType(id, String)
            If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

            Return _databaseService.Context.Users.FirstOrDefault(Function(p) String.Equals(p.Id, idString))
        End Function

        Public Overrides Async Function GetOneAsync(id As Object) As Task(Of User)
            Dim idString = CType(id, String)
            If idString Is Nothing Then Throw New InvalidCastException("Id must be string")

            Dim user  = Await _databaseService.Context.Users.FirstOrDefaultAsync(Function(p) String.Equals(p.Id, idString))

            Return user
        End Function
        
    End Class
End Namespace