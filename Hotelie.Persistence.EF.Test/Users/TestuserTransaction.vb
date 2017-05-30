Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common

Namespace Users
    <TestClass>
    Public Class TestUserTransaction
        Private _context As DatabaseContext

        <TestInitialize>
        Public Sub TestInitialize()
            _context =
                New DatabaseContext(
                    $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _context.Dispose()
        End Sub

        <TestMethod>
        Public Sub TestAddUser_ValidUser_UserAdded()
            Dim category = _context.UserCategories.First
            Dim user As New User With{.Id="NhanVien", .Password="abc",.Category=category}

            ' act
            _context.Users.Add(user)
            _context.SaveChanges()

            ' assert
            Dim users = _context.Users.Where(Function(u) u.Id = "NhanVien").ToList()

            Assert.IsTrue(users.Count = 1)

            _context.Users.Remove(users(0))
            _context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub GetUser()
            ' pre-act
            Dim category = _context.UserCategories.First
            Dim user As New User With{.Id="NhanVien", .Password="abc",.Category=category}

            _context.Users.Add(user)
            _context.SaveChanges()

            ' assert
            Dim users = _context.Users.Where(Function(u) u.Id.StartsWith("NhanVien")).ToList()
            Assert.IsTrue((users(0).Password = "abc") And (users(0).Category.Id="NV001"))

            _context.Users.Remove(users(0))
            _context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub DeleteUser()
            ' pre-act
            Dim category = _context.UserCategories.First
            Dim user As New User With{.Id="remove", .Password="abc",.Category=category}

            _context.Users.Add(user)
            _context.SaveChanges()

            ' act
            _context.Users.Remove(user)
            _context.SaveChanges()
            ' assert
            Dim users = _context.Users.Where(Function(u) u.Id = "remove").ToList()
            Assert.IsTrue(users.Count = 0)
           
        End Sub
    End Class
End NameSpace