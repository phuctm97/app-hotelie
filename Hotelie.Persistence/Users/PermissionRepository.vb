Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users

Namespace Users
    Public Class PermissionRepository
        Implements IPermissionRepository
        
        Private ReadOnly _databaseService As IDatabaseService

        Sub New(databaseService As IDatabaseService)
            _databaseService = databaseService
        End Sub

        Public Function GetPermission(user As User) As List(Of Permission) Implements IPermissionRepository.GetPermission
            Return Nothing
        End Function

        Public Function GetPermissionAsync(user As User) As Task(Of List(Of Permission)) Implements IPermissionRepository.GetPermissionAsync
            Throw New Exception
        End Function
    End Class
End NameSpace