Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Users

Namespace Users
    <TestClass>
    Public Class TestUserRepository
        Private _context As DatabaseContext
        Private _userRepository As UserRepository
        Private _categoriesList As List(Of UserCategory)
        Private _usersList As List(Of User)

        <TestInitialize>
        Public Sub TestInitialize()
            _context = New DatabaseContext(
                $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _userRepository = new UserRepository(_context)
        End Sub

        Public Sub InitializeCategories()
            Dim cat1 = New UserCategory() With{.Id = "00001", .Name = "NhanVien"}
            Dim cat2 = New UserCategory() With{.Id = "00002", .Name = "GiamDoc"}
            Dim cat3 = New UserCategory() With{.Id = "00003", .Name = "QuanLy"}
            Dim cat4 = New UserCategory() With{.Id = "00004", .Name = "Khach"}
            _categoriesList = new List(Of UserCategory)
            _categoriesList.Add(cat1)
            _categoriesList.Add(cat2)
            _categoriesList.Add(cat3)
            _categoriesList.Add(cat4)
            _userRepository.AddUserCategories(_categoriesList)
            _context.SaveChanges()
        End Sub

        Public Sub DisposeCategories()
            _categoriesList?.Clear()
            _userRepository.RemoveUserCategories(_userRepository.GetAllUserCategories())
            _context.SaveChanges()
        End Sub

        Public Sub InitializeRooms()
            DisposeCategories()
            InitializeCategories()
            _usersList = new List(Of User)
            Dim user1 = new User() With{.Id="admin1",.Password="admin01",.Category=_categoriesList(0)}
            Dim user2 = new User() With{.Id="admin2",.Password="admin02",.Category=_categoriesList(1)}
            Dim user3 = new User() With{.Id="admin3",.Password="admin03",.Category=_categoriesList(2)}
            Dim user4 = new User() With{.Id="admin4",.Password="admin04",.Category=_categoriesList(3)}
            Dim user5 = new User() With{.Id="admin5",.Password="admin05",.Category=_categoriesList(1)}
            _usersList.Add(user1)
            _usersList.Add(user2)
            _usersList.Add(user3)
            _usersList.Add(user4)
            _usersList.Add(user5)
            _userRepository.AddRange(_usersList)
            _context.SaveChanges()
        End Sub

        Public Sub DisposeRooms()
            DisposeCategories()
            _usersList?.Clear()
            _userRepository.RemoveRange(_userRepository.GetAll())
            _context.SaveChanges()
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _userRepository.RemoveUserCategories(_userRepository.GetAllUserCategories())
            _context.Dispose()
        End Sub

        <TestMethod>
        Public Sub TestRemoveAllCategories_RemoveAllCategories_CountEqualsZero()

            ' act
            _userRepository.RemoveUserCategories(_userRepository.GetAllUserCategories())
            _context.SaveChanges()

            ' assert
            Dim categories = _userRepository.GetAllUserCategories().ToList()
            Assert.AreEqual(0, categories.Count())
        End Sub

        <TestMethod>
        Public Sub TestAddCategory_ValidCategory_CountIncrease()

            ' input
            Dim roomCategoriesCount = _userRepository.GetAllUserCategories().Count()
            Dim name = "Normal"
            Dim price = 200000
            Dim id = "CTG01"

            ' act
            _userRepository.AddUserCategory(New UserCategory() With {.Id=id,.Name=name})
            _context.SaveChanges()

            ' assert
            Assert.AreEqual(roomCategoriesCount + 1, _userRepository.GetAllUserCategories().Count())
            CollectionAssert.AllItemsAreNotNull(_userRepository.GetAllUserCategories().ToList())

            ' rollback
            Dim roomCategory = _userRepository.GetUserCategory(id)
            _userRepository.RemoveUserCategory(roomCategory)
            _context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub TestGetAllUserCategories_ValidCategories_CountExtractly()
            ' pre-act
            DisposeCategories()
            _context.SaveChanges()

            ' input
            Dim name1 = "Normal"
            Dim price1 = 300000
            Dim id1 = "CTG01"
            Dim name2 = "Old"
            Dim price2 = 200000
            Dim id2 = "CTG02"

            ' pre-act
            _userRepository.AddUserCategory(New UserCategory() With {.Id=id1,.Name=name1})
            _userRepository.AddUserCategory(New UserCategory() With {.Id=id2,.Name=name2})
            _context.SaveChanges()

            ' act
            Dim categories = _userRepository.GetAllUserCategories().ToList()

            ' assert
            CollectionAssert.AllItemsAreNotNull(categories)
            Assert.AreEqual(categories(0).Id, id1)
            Assert.AreEqual(categories(1).Id, id2)
            Assert.IsTrue(categories.Count = 2)

            ' rollback
            _userRepository.RemoveUserCategories(_userRepository.GetAllUserCategories())
        End Sub

        <TestMethod>
        Public Sub FindCategory_ValidCategory_ValidResult()
            ' input
            DisposeCategories()
            InitializeCategories()

            ' act
            Dim exampleCategory = _categoriesList(1)
            Dim category = _userRepository.FindUserCategory(Function(p)(p.Id = exampleCategory.Id)).FirstOrDefault()

            ' Assert
            Assert.IsTrue((category.Id = exampleCategory.Id) And (category.Name = exampleCategory.Name))

            ' rollback
            DisposeCategories()
        End Sub

        <TestMethod>
        Public Sub TestAddCategories_ValidCategories_ValidData()
            ' pre-act
            DisposeCategories()

            ' input
            Dim id = "10001"
            Dim name = "CatName"
            Dim id2 = "20001"
            Dim name2 = "CatName2"
            ' pre-act
            Dim categories = New List(Of UserCategory)
            categories.Add(New UserCategory() With{.Id=id,.Name=name})
            categories.Add(New UserCategory() With{.Id=id2,.Name=name2})

            ' act
            _userRepository.AddUserCategories(categories)
            _context.SaveChanges()

            Dim categoriesFromDatabase = _userRepository.GetAllUserCategories().ToList()

            ' assert
            Assert.AreEqual(2,categoriesFromDatabase.Count)
            CollectionAssert.AllItemsAreNotNull(categoriesFromDatabase)
            Assert.IsTrue(categoriesFromDatabase(0).Id="10001" And categoriesFromDatabase(0).Name="CatName")
            Assert.IsTrue(categoriesFromDatabase(1).Id="20001" And categoriesFromDatabase(1).Name="CatName2")

            ' rollback
            DisposeCategories()
        End Sub

        <TestMethod>
        Public Sub TestGetCategory_ValidCategoryId_ReturnExactlyCategoryData()
            ' pre-act
            DisposeCategories()

            ' input
            InitializeCategories()

            ' act
            Dim category = _userRepository.GetUserCategory(_categoriesList(2).Id)

            ' assert
            Assert.IsTrue(_categoriesList(2).Id= category.Id And _categoriesList(2).Name=category.Name)

            ' rollback
            DisposeCategories()
        End Sub

        <TestMethod>
        Public Sub TestRemoveCategory_ValidCategory_CategoryDeleted()
            ' input
            DisposeCategories()
            InitializeCategories()

            ' act
            Dim category  = _categoriesList(0)
            Dim categoryToDelete = _userRepository.FindUserCategory(Function(p)(p.Id=category.Id And p.Name = category.Name)).FirstOrDefault()

            ' assert
            _userRepository.RemoveUserCategory(categoryToDelete)
            _context.SaveChanges()

            Dim categoryCheck = _userRepository.FindUserCategory(Function(p)(p.Id=category.Id And p.Name = category.Name)).FirstOrDefault()

            Assert.IsNull(categoryCheck)

            ' rollback
            DisposeCategories()
        End Sub



        <TestMethod>
        Public Sub TestAddRoom_ValidRoom_RoomAdded()
            ' pre-act
            DisposeRooms() 
            InitializeCategories()

            ' input
            Dim id = "r0001"
            Dim password  = "101"
            Dim category = _categoriesList(0)
            
            ' act
            _userRepository.Add(New User() With{.Id=id, .Password=password, .Category=category})
            _context.SaveChanges()
            Dim room = _userRepository.Find(Function(p)(p.Id = id And p.Password = password)).FirstOrDefault()

            ' assert
            Assert.IsTrue(room.Id=id And room.Password= password)
            
            'rollback
            DisposeRooms()
        End Sub

        <TestMethod>
        Public Sub TestAddRooms_ValidRoomsList_RoomsAdded()
            ' input
            DisposeRooms()
            InitializeRooms()

            ' assert
            Dim rooms = _userRepository.GetAll().ToList()
            Dim i=0
            For Each room As User In rooms
                Assert.IsTrue(room.id = _usersList(i).Id And room.Password = _usersList(i).Password)
                i = i + 1
            Next
            
            ' rollback
            DisposeRooms()
        End Sub

        <TestMethod>
        Public Sub TestGetRoom_ValidIdIndex_ReturnRightRoom()
            ' pre-input
            DisposeRooms()

            ' input
            InitializeRooms()

            ' act
            Dim index = 1
            Dim room = _userRepository.GetOne(_usersList(index).Id)
            
            ' assert
            Assert.IsNotNull(room)
            Assert.AreEqual(_usersList(index).Id, room.Id)
            Assert.AreEqual(_usersList(index).Password, room.Password)

            ' rollback
            DisposeRooms()
        End Sub

        <TestMethod>
        Public Sub TestGetRooms__ReturnAllRooms()
            ' pre-input
            DisposeRooms()

            ' input
            InitializeRooms()

            ' act
            Dim rooms = _userRepository.GetAll().ToList()
            CollectionAssert.AllItemsAreNotNull(rooms)
            Dim i = 0
            For Each room As User In rooms
                Assert.AreEqual(_usersList(i).Id,room.Id)
                Assert.AreEqual(_usersList(i).Password,room.Password)
                i = i + 1
            Next

            ' rollback
            DisposeRooms()
        End Sub

        <TestMethod>
        Public Sub TestRemoveRoom_ValidRoomsListItemIndex_RoomDeleted()
            ' pre-input
            DisposeRooms()
            
            ' input
            InitializeRooms()

            ' act
            Dim index = 1
            Dim room = _usersList(index)
            Dim roomToDelete = _userRepository.Find(Function(p)(p.Id=room.Id And p.Password = room.Password)).FirstOrDefault()
            _userRepository.Remove(roomToDelete)
            _context.SaveChanges()
            roomToDelete = _userRepository.Find(Function(p)(p.Id=room.Id And p.Password = room.Password)).FirstOrDefault()

            ' assert
            Assert.IsNull(roomToDelete)

            ' rollback
            DisposeRooms()
        End Sub

        <TestMethod>
        Public Sub TestRemoveAllRooms__RoomsIsNull()
            ' pre-input
            DisposeRooms()

            ' input
            InitializeRooms()

            ' act
            _userRepository.RemoveRange(_userRepository.GetAll())
            _context.SaveChanges()

            Dim rooms = _userRepository.GetAll().ToList()

            Assert.AreEqual(0,rooms.Count)

            ' rollback
            DisposeRooms()
        End Sub

        <TestMethod>
        Public Sub TestFindRoom_ValidRoomsListItemIndex_ReturnRightRoom()
            ' pre-act
            DisposeRooms()

            ' input
            InitializeRooms()

            'act
            Dim index = 2
            Dim roomToFind = _usersList(index)
            Dim room = _userRepository.Find(Function(p)(p.Id=roomToFind.Id And p.Password=roomToFind.Password)).FirstOrDefault()

            ' assert
            Assert.AreEqual(_usersList(index).Id,room.Id)
            Assert.AreEqual(_usersList(index).Password,room.Password)

            ' rollback
            DisposeRooms()
        End Sub
    End Class
End NameSpace