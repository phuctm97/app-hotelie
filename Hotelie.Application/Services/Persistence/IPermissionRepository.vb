Imports Hotelie.Domain.Users

Namespace Services.Persistence
    Public Interface IPermissionRepository
        Function GetPermission(user As User) As List(Of UserPermission)
        Function GetPermissionAsync(user As User) As Task(Of List(Of UserPermission))
    End Interface
End NameSpace