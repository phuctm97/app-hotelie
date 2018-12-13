Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Application.Users.Commands
Imports Hotelie.Application.Users.Factories
Imports Hotelie.Application.Users.Queries
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Users

Namespace User
    <TestClass>
    Public Class TestGetUserPermission
        Private _databaseService As IDatabaseService
        Private _userRepository As IUserRepository
        Private _permissionRepository As IPermissionRepository
        Private _getUserPermissions As IGetUserPermissionsQuery
        Private _updateUserPermissions As IUpdateUserPermissionCommand
        Private _createUserFactory As ICreateUserFactory
        Private _unitOfWork As IUnitOfWork
        Private _removeUser As IRemoveUserCommand

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS", $"HotelieDatabase")
            _unitOfWork = New UnitOfWork(_databaseService)
            _userRepository = New UserRepository(_databaseService)
            _permissionRepository = New PermissionRepository(_databaseService)
            _getUserPermissions = New GetUserPermissionsQuery(_userRepository, _permissionRepository)
            _updateUserPermissions = New UpdateUserPermissionCommand(_userRepository, _unitOfWork, _permissionRepository)
            _createUserFactory = New CreateUserFactory(_userRepository,_unitOfWork)
            _removeUser = New RemoveUserCommand(_userRepository,_unitOfWork)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _databaseService.Dispose()
        End Sub

        <TestMethod>
        Public Sub TestGetUserPermission_DebugOnly()

            Dim userPermission = _getUserPermissions.Execute("admin")

            Assert.IsTrue(True)
        End Sub

        <TestMethod>
        Public Sub Estupdate()
            Dim r = _updateUserPermissions.Execute("admin", True, True, True, True, True, True) 
            Assert.IsTrue(True)
        End Sub

        <TestMethod>
        Public Sub TestAdduser()
            Dim r = _createUserFactory.Execute("khach3","khach")
            Assert.IsFalse(False)
        End Sub

        <TestMethod>
        Public Sub TestRemove()
            Dim r = _removeUser.Execute("khach")
            Assert.IsFalse(False)

        End Sub
    End Class
End NameSpace