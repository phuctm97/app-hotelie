Imports Hotelie.Domain.Users

Namespace Users
    Public Interface IGetUserPermissions
        Function Execute(id As String) As IUserModel
        Function ExecuteAsync(id As String) As Task(Of IUserModel)
    End Interface
End NameSpace