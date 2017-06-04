Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Users

Namespace Authentication
    <TestClass>
    Public Class TestAuthentication
        Private _authentication As Hotelie.Application.Services.Authentication.Authentication
        Private _databaseService As IDatabaseService
        Private _userRepository As UserRepository
        Private _usersList As List(Of User)

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS",$"HotelieDatabase")
            _userRepository = new UserRepository(_databaseService)
            _authentication = new Hotelie.Application.Services.Authentication.Authentication(_userRepository)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _databaseService.Context.Dispose()
        End Sub

        Public Sub UserInitialize()
            Dim userCategory = new UserCategory() With{.Id="10001",.Name="nhanvien"}
            _userRepository.AddUserCategory(userCategory)
            _databaseService.Context.SaveChanges()

            Dim user = New User() With{.Id="nhanvien1",.Password="matkhau",.Category=userCategory}
            Dim admin = New User() With{.Id="admin",.Password="admin",.Category=userCategory}

            _usersList = new List(Of User)
            _usersList.Add(user)
            _usersList.Add(admin)

            _userRepository.AddRange(_usersList)
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeUsers()
            _usersList?.Clear()
            _userRepository.RemoveUserCategories(_userRepository.GetAllUserCategories())
            _userRepository.RemoveRange(_userRepository.GetAll())
            _databaseService.Context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub TestTryLogin_AlreadyLoggedIn_AlreadyLoggedInError()
            ' pre-act
            DisposeUsers()
            _authentication.Logout()

            ' input
            UserInitialize()

            ' act
            Dim userIndex = 0
            Dim userLoginIndex = 1
            _authentication.TryLogin(_usersList(userIndex).Id,_usersList(userIndex).Password)
            Dim errorLog = _authentication.TryLogin(_usersList(userLoginIndex).Id,_usersList(userLoginIndex).Password)

            ' assert
            Assert.IsTrue(_authentication.LoggedIn)
            Assert.IsNotNull(errorLog)
            Assert.IsTrue(_authentication.LoggedAccount.Username = _usersList(userIndex).Id)
            Assert.AreEqual(Hotelie.Application.Services.Authentication.Authentication.AlreadyLoggedInError,errorLog(0))

            ' rollback
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestTryLogin_InvalidUsername_ErrorUsernameIsInvalid()
            ' pre-act
            DisposeUsers()
            _authentication.Logout()

            ' input
            UserInitialize()

            ' act
            Dim invalidAccountUsername = "ThisIsAInvalidUsername"
            Dim errorLog = _authentication.TryLogin(invalidAccountUsername,"whateverthisis")
            
            ' assert
            Assert.IsFalse(_authentication.LoggedIn)
            CollectionAssert.AllItemsAreNotNull(errorLog)
            Assert.AreEqual(1,errorLog.Count())
            Assert.AreEqual(Hotelie.Application.Services.Authentication.Authentication.UsernameInvalidError,errorLog(0))

            ' rollback
            _authentication.Logout()
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestTryLogin_InvalidPassword_ErrorPasswordIsInvalid()
            ' pre-act
            DisposeUsers()
            _authentication.Logout()

            ' input
            UserInitialize()

            ' act
            Dim invalidPassword = "ThisIsAInvalidPassword"
            Dim errorLog = _authentication.TryLogin(_usersList(0).Id,invalidPassword)
            
            ' assert
            Assert.IsFalse(_authentication.LoggedIn)
            CollectionAssert.AllItemsAreNotNull(errorLog)
            Assert.AreEqual(1,errorLog.Count())
            Assert.AreEqual(Hotelie.Application.Services.Authentication.Authentication.PasswordInvalidError,errorLog(0))

            ' rollback
            _authentication.Logout()
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestTryLogin_ValidUsernameAndPassword_NoError()
            ' pre-act
            DisposeUsers()
            _authentication.Logout()

            ' input
            UserInitialize()

            ' act
            Dim validAccountIndex = 0
            Dim validAccountUsername = _usersList(validAccountIndex).Id
            Dim validAccountPassword = _usersList(validAccountIndex).Password
            Dim errorLog = _authentication.TryLogin(validAccountUsername, validAccountPassword)
            
            ' assert
            Assert.IsTrue(_authentication.LoggedIn)
            Assert.AreEqual(0,errorLog.Count)
            Assert.IsTrue(_authentication.LoggedAccount.Username = _usersList(validAccountIndex).Id)

            ' rollback
            _authentication.Logout()
            DisposeUsers()
        End Sub
    End Class
End NameSpace