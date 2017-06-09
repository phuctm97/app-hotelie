Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users

Namespace Users
    Public Class GetUserPermissions
        Implements IGetUserPermissions

        Private ReadOnly _userRepository As IUserRepository

        Sub New(userRepository As IUserRepository)
            _userRepository = userRepository
        End Sub

        Public Function Execute(id As String) As IUserModel Implements IGetUserPermissions.Execute
            Dim user = _userRepository.GetOne(id)
            Return New UserModel(user)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of IUserModel) Implements IGetUserPermissions.ExecuteAsync
            Dim user = Await _userRepository.GetOneAsync(id)
            Return New UserModel(user)
        End Function
    End Class
End NameSpace