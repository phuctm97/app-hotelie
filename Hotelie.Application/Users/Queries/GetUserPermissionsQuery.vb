Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Application.Users.UserModels
Imports Hotelie.Domain.Users

Namespace Users.Queries
    Public Class GetUserPermissionsQuery
        Implements IGetUserPermissionsQuery

        Private ReadOnly _userRepository As IUserRepository
        Private ReadOnly _permissionRepository As IPermissionRepository

        Sub New(userRepository As IUserRepository, permissionRepository As IPermissionRepository)
            _userRepository = userRepository
            _permissionRepository = permissionRepository
        End Sub

        Public Function Execute(id As String) As IUserModel Implements IGetUserPermissionsQuery.Execute
            Dim user = _userRepository.GetOne(id)
            Dim permissions = _permissionRepository.GetPermission(user)
            Return New UserModel(user,permissions)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of IUserModel) Implements IGetUserPermissionsQuery.ExecuteAsync
            Dim user = Await _userRepository.GetOneAsync(id)
            Dim permissions = Await _permissionRepository.GetPermissionAsync(user)
            Return New UserModel(user,permissions)
        End Function
    End Class
End NameSpace