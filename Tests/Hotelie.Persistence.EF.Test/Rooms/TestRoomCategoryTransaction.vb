Imports Hotelie.Persistence.Common
Imports Hotelie.Domain.Rooms
Namespace Rooms
    <TestClass>
    Public Class TestRoomCategoryTransaction
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
            Dim roomCategory As New RoomCategory() With {.Name="VIP",.Id="VIP01",.Price="200000"}

            'act
            _context.RoomCategories.Add(roomCategory)
            _context.SaveChanges()

            'assert
            Dim rc = _context.RoomCategories.Where(Function(r)r.Id="VIP01").ToList()
            Assert.IsTrue(rc.Count=1)

            _context.RoomCategories.remove(rc(0))
            _context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub GetRoomCategory()
            ' pre-act
            Dim roomCategory As New RoomCategory() With {.Name="Get",.Id="Get01",.Price="200000"}
            _context.RoomCategories.Add(roomCategory)
            _context.SaveChanges()

            ' assert
            Dim rc = _context.RoomCategories.Where(Function(r)r.Id="Get01").ToList()
            Assert.IsTrue(rc(0).Name="Get")


            _context.RoomCategories.remove(rc(0))
            _context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub DeleteRoomCategory()
            ' pre-act
            Dim roomCategory As New RoomCategory() With {.Name="remove",.Id="VIP01",.Price="200000"}
            _context.RoomCategories.Add(roomCategory)
            _context.SaveChanges()
            _context.RoomCategories.Remove(roomCategory)
            _context.SaveChanges()

            ' assert
            Dim rc = _context.RoomCategories.Where(Function(r)r.Id="remove").ToList()
            Assert.IsTrue(rc.Count=0)

        End Sub

        <TestMethod>
        Public Sub TestNumberOfRoomInitialize()

            Dim rc = _context.RoomCategories.Where(Function(r)r.Id="NOR01").ToList()
            Assert.IsTrue(rc.Count>0)

        End Sub
    End Class
End NameSpace