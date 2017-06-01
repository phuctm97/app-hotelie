
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Rooms

Namespace Rooms
    <TestClass>
    Public Class TestRoomRepository
        Private _context As DatabaseContext
        Private _roomRepository As RoomRepository

        <TestInitialize>
        Public Sub TestInitialize()
            _context = New DatabaseContext(
                $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _roomRepository = new RoomRepository(_context)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _context.Dispose()
        End Sub

        <TestMethod>
        Public Sub TestRemoveAllCategories_RemoveAllCategories_CountEqualsZero()

            ' act
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _context.SaveChanges()

            ' assert
            Dim categories = _roomRepository.GetAllRoomCategories().ToList()
            Assert.AreEqual(0,categories.Count())
            
        End Sub

        <TestMethod>
        Public Sub TestAddCategory_ValidCategory_CountIncrease()

            ' input
            Dim roomCategoriesCount = _roomRepository.GetAllRoomCategories().Count()
            Dim name = "Normal"
            Dim price = 200000
            Dim id = "CTG01"

            ' act
            _roomRepository.AddRoomCategory(New RoomCategory() With {.Id=id,.Name=name,.Price=price})
            _context.SaveChanges()

            ' assert
            Assert.AreEqual(roomCategoriesCount + 1, _roomRepository.GetAllRoomCategories().Count())
            CollectionAssert.AllItemsAreNotNull(_roomRepository.GetAllRoomCategories().ToList())

            ' rollback
            Dim roomCategory = _roomRepository.GetRoomCategory(id)
            _roomRepository.RemoveRoomCategory(roomCategory)
            _context.SaveChanges()
        End Sub
    End Class
End NameSpace