Imports Hotelie.Domain.Users

Namespace Services.Persistence
    Public Interface IPermissionRepository
        Function GetPermission(user As User) As List(Of Permission)
        Function GetPermissionAsync(user As User) As Task(Of List(Of Permission))
    End Interface
End NameSpace