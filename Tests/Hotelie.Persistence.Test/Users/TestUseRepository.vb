Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Users

Namespace Users
    <TestClass>
    Public Class TestUseRepository
        Private _context As DatabaseContext
        Private _roomRepository As UserRepository
        Private _categoriesList As List(Of User)

        <TestInitialize>
        Public Sub TestInitialize()
            _context = New DatabaseContext(
                $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _roomRepository = new UserRepository(_context)
        End Sub

        Public Sub InitializeCategories()
            Dim cat1 = New User() With{.Id = "00001", .Password = "1231" }
            Dim cat2 = New User() With{.Id = "00002", .Password = "1232" }
            Dim cat3 = New User() With{.Id = "00003", .Password = "1233" ,.Price=400000}
            Dim cat4 = New User() With{.Id = "00004", .Password = "1234" ,.Price=500000}
            _categoriesList = new List(Of User)
            _categoriesList.Add(cat1)
            _categoriesList.Add(cat2)
            _categoriesList.Add(cat3)
            _categoriesList.Add(cat4)
            _roomRepository.AddRange(_categoriesList)
            _context.SaveChanges()
        End Sub

        Public Sub DisposeCategories()
            _categoriesList?.Clear()
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _context.SaveChanges()
        End Sub
        
    End Class
End NameSpace