Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Application.Users
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Users

Namespace User
    <TestClass>
    Public Class TestGetUserPermission
        Private  _databaseService As IDatabaseService
        Private  _userRepository As IUserRepository
        Private _getUserPermissions As IGetUserPermissions

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS", $"HotelieDatabase")
            _userRepository = New UserRepository(_databaseService)
            _getUserPermissions = New GetUserPermissions(_userRepository)
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
    End Class
End NameSpace