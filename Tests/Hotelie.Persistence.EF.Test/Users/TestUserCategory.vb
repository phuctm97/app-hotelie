Imports Hotelie.Domain.Rooms
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common

Namespace Users
    ' <TestClass>
    Public Class TestUserCategory
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
        Public Sub AddRoomCategory()
            Dim userCategory As New UserCategory() With {.Name="VIP",.Id="VIP01"}

            'act
            _context.UserCategories.Add(userCategory)
            _context.SaveChanges()

            'assert
            Dim rc = _context.UserCategories.Where(Function(r)r.Id="VIP01").ToList()
            Assert.IsTrue(rc.Count=1)

            _context.UserCategories.remove(rc(0))
            _context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub GetRoomCategory()
            ' pre-act
            Dim userCategory As New UserCategory() With {.Name="Get",.Id="Get01"}
            _context.UserCategories.Add(userCategory)
            _context.SaveChanges()

            ' assert
            Dim rc = _context.UserCategories.Where(Function(r)r.Id="Get01").ToList()
            Assert.IsTrue(rc(0).Name="Get")


            _context.UserCategories.remove(rc(0))
            _context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub DeleteRoomCategory()
            ' pre-act
            Dim userCategory As New UserCategory() With {.Name="remove",.Id="VIP01"}
            _context.UserCategories.Add(userCategory)
            _context.SaveChanges()
            _context.UserCategories.Remove(userCategory)
            _context.SaveChanges()

            ' assert
            Dim rc = _context.UserCategories.Where(Function(r)r.Id="remove").ToList()
            Assert.IsTrue(rc.Count=0)

        End Sub
    End Class
End NameSpace