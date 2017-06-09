Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Application.Users.UserModels
Imports Hotelie.Domain.Users

Namespace Users.Queries
    Public Class GetAllUsersQuery
        Implements IGetAllUsersQuery

        Private ReadOnly _userRepostory As IUserRepository
        Private ReadOnly _permissionRepository As IPermissionRepository

        Sub New(userRepostory As IUserRepository, permissionRepository As IPermissionRepository)
            _userRepostory = userRepostory
            _permissionRepository = permissionRepository
        End Sub

        Public Function Execute() As List(Of IUserModel) Implements IGetAllUsersQuery.Execute
            Dim users = _userRepostory.GetAll().ToList()
            Dim userModel = New List(Of IUserModel)
            For Each user As User In users
                Dim permissions = _permissionRepository.GetPermission(user)
                userModel.Add(New UserModel(user,permissions))
            Next
            Return userModel
        End Function

        Public Async Function ExecuteAsync() As Task(Of List(Of IUserModel)) Implements IGetAllUsersQuery.ExecuteAsync
            Dim users = Await _userRepostory.GetAll().ToListAsync()
            Dim userModel = New List(Of IUserModel)
            For Each user As User In users
                Dim permissions = Await _permissionRepository.GetPermissionAsync(user)
                userModel.Add(New UserModel(user,permissions))
            Next
            Return userModel
        End Function
    End Class
End NameSpace