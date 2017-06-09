Imports Hotelie.Application.Users.UserModels

Namespace Users.Queries
    Public Interface IGetAllUsersQuery
        Function Execute() As List(Of IUserModel)
        Function ExecuteAsync() As Task(Of List(Of IUserModel))
    End Interface
End NameSpace