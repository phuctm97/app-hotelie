Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common

Namespace Users
    Public Class PermissionRepository
        Inherits Repository(Of UserPermission)
        Implements IPermissionRepository
        
        Private ReadOnly _databaseService As IDatabaseService

        Sub New(databaseService As IDatabaseService)
            MyBase.New(databaseService)
            _databaseService = databaseService
        End Sub

        Public Sub AddPermission(userPermission As UserPermission) Implements IPermissionRepository.AddPermission
            _databaseService.Context.UserPermissions.Add(userPermission)
        End Sub

        Public Sub RemovePermissionsOfUser(user As User) Implements IPermissionRepository.RemovePermissionsOfUser
            Dim userPermissions = _databaseService.Context.UserPermissions.Where(Function(p)p.User.Id = user.Id).ToList()
            _databaseService.Context.UserPermissions.RemoveRange(userPermissions)
        End Sub

        Public Function GetPermission(user As User) As List(Of UserPermission) Implements IPermissionRepository.GetPermission
            Return _databaseService.Context.UserPermissions.Where(Function(p)p.User.Id= user.id).ToList()
        End Function

        Public Async Function GetPermissionAsync(user As User) As Task(Of List(Of UserPermission)) Implements IPermissionRepository.GetPermissionAsync
            Return Await _databaseService.Context.UserPermissions.Where(Function(p)p.User.Id= user.id).ToListAsync()
        End Function

        Public Function GetPermissionType(id As String) As Permission Implements IPermissionRepository.GetPermissionType
            Return _databaseService.Context.Permissions.FirstOrDefault(Function(p)p.Id = id)
        End Function

        Public Async Function GetPermissionTypeAsync(id As String) As Task(Of Permission) Implements IPermissionRepository.GetPermissionTypeAsync
            Return Await _databaseService.Context.Permissions.FirstOrDefaultAsync(Function(p)p.Id = id)
        End Function
    End Class
End NameSpace