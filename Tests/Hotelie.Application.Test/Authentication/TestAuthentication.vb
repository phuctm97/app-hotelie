Imports Hotelie.Application.Services.Authentication
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.DatabaseServices
Imports Hotelie.Persistence.Users

Namespace Authentication
    <TestClass>
    Public Class TestAuthentication
        Private _authentication As Hotelie.Application.Services.Authentication.Authentication
        Private _databaseService As DatabaseService
        Private _userRepository As UserRepository
        Private _usersList As List(Of User)

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService =
                New DatabaseService(
                    $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
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
            _authentication.TryLogin(New Account() With{.Username=_usersList(userIndex).Id,.Password=_usersList(userIndex).Password})
            Dim errorLog = _authentication.TryLogin(New Account() With{.Username=_usersList(userLoginIndex).Id,.Password=_usersList(userLoginIndex).Password})

            ' assert
            Assert.IsTrue(_authentication.LoggedIn)
            Assert.IsNotNull(errorLog)
            Assert.IsTrue(_authentication.LoggedAccount.Username = _usersList(userIndex).Id And _authentication.LoggedAccount.Password=_usersList(userIndex).Password)
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
            Dim invalidAccountUsername = New Account() With {.Username = "ThisIsAInvalidUsername",.Password="admin"}
            Dim errorLog = _authentication.TryLogin(invalidAccountUsername)
            
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
            Dim invalidAccountPassword = New Account() With {.Username = _usersList(0).Id,.Password="ThisIsAInvalidPassword"}
            Dim errorLog = _authentication.TryLogin(invalidAccountPassword)
            
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
            Dim validAccount = New Account() With {.Username = _usersList(validAccountIndex).Id,.Password=_usersList(validAccountIndex).Password}
            Dim errorLog = _authentication.TryLogin(validAccount)
            
            ' assert
            Assert.IsTrue(_authentication.LoggedIn)
            Assert.AreEqual(0,errorLog.Count)
            Assert.IsTrue(_authentication.LoggedAccount.Username = _usersList(validAccountIndex).Id And _authentication.LoggedAccount.Password=_usersList(validAccountIndex).Password)

            ' rollback
            _authentication.Logout()
            DisposeUsers()
        End Sub
    End Class
End NameSpace