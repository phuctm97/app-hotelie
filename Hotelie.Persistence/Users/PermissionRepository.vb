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

        Public Function GetPermission(user As User) As List(Of UserPermission) Implements IPermissionRepository.GetPermission
            Return _databaseService.Context.UserPermissions.Where(Function(p)p.User.Id= user.id).ToList()
        End Function

        Public Async Function GetPermissionAsync(user As User) As Task(Of List(Of UserPermission)) Implements IPermissionRepository.GetPermissionAsync
            Return Await _databaseService.Context.UserPermissions.Where(Function(p)p.User.Id= user.id).ToListAsync()
        End Function
    End Class
End NameSpace