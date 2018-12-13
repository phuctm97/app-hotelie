Imports Hotelie.Domain.Users

Namespace Services.Persistence
    Public Interface IPermissionRepository
        Inherits IRepository(Of UserPermission)
        Function GetPermissionType(id As String) As Permission
        Function GetPermissionTypeAsync(id As String) As Task(Of Permission)
        Function GetPermission(user As User) As List(Of UserPermission)
        Function GetPermissionAsync(user As User) As Task(Of List(Of UserPermission))
        Sub RemovePermissionsOfUser(user As User)
        Sub AddPermission(userPermission As UserPermission)
    End Interface
End NameSpace