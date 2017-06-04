
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Rooms

Namespace Rooms
    <TestClass>
    Public Class TestRoomRepository
        Private _databaseService As IDatabaseService
        Private _roomRepository As RoomRepository
        Private _categoriesList As List(Of RoomCategory)
        Private _roomsList As List(Of Room)

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS",$"HotelieDatabase")
            _roomRepository = new RoomRepository(_databaseService)
        End Sub

        Public Sub InitializeCategories()
            Dim cat1 = New RoomCategory() With{.Id = "00001", .Name = "VIP01" ,.Price=200000}
            Dim cat2 = New RoomCategory() With{.Id = "00002", .Name = "VIP02" ,.Price=300000}
            Dim cat3 = New RoomCategory() With{.Id = "00003", .Name = "VIP03" ,.Price=400000}
            Dim cat4 = New RoomCategory() With{.Id = "00004", .Name = "VIP04" ,.Price=500000}
            _categoriesList = new List(Of RoomCategory)
            _categoriesList.Add(cat1)
            _categoriesList.Add(cat2)
            _categoriesList.Add(cat3)
            _categoriesList.Add(cat4)
            _roomRepository.AddRoomCategories(_categoriesList)
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeCategories()
            _categoriesList?.Clear()
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub InitializeRooms()
            DisposeCategories()
            InitializeCategories()
            _roomsList = new List(Of Room)
            Dim room1 = new Room() With{.Id="r0001",.Name="101",.Category=_categoriesList(0),.State=0}
            Dim room2 = new Room() With{.Id="r0002",.Name="102",.Category=_categoriesList(1),.State=0}
            Dim room3 = new Room() With{.Id="r0003",.Name="103",.Category=_categoriesList(3),.State=0}
            Dim room4 = new Room() With{.Id="r0004",.Name="104",.Category=_categoriesList(2),.State=0}
            Dim room5 = new Room() With{.Id="r0005",.Name="105",.Category=_categoriesList(1),.State=0}
            _roomsList.Add(room1)
            _roomsList.Add(room2)
            _roomsList.Add(room3)
            _roomsList.Add(room4)
            _roomsList.Add(room5)
            _roomRepository.AddRange(_roomsList)
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeRooms()
            DisposeCategories()
            _roomsList?.Clear()
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            _databaseService.Context.SaveChanges()
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _databaseService.Context.Dispose()
        End Sub

        <TestMethod>
        Public Sub TestRemoveAllCategories_RemoveAllCategories_CountEqualsZero()

            ' act
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _databaseService.Context.SaveChanges()

            ' assert
            Dim categories = _roomRepository.GetAllRoomCategories().ToList()
            Assert.AreEqual(0, categories.Count())
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
            _databaseService.Context.SaveChanges()

            ' assert
            Assert.AreEqual(roomCategoriesCount + 1, _roomRepository.GetAllRoomCategories().Count())
            CollectionAssert.AllItemsAreNotNull(_roomRepository.GetAllRoomCategories().ToList())

            ' rollback
            Dim roomCategory = _roomRepository.GetRoomCategory(id)
            _roomRepository.RemoveRoomCategory(roomCategory)
            _databaseService.Context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub TestGetAllRoomCategories_ValidCategories_CountExtractly()
            ' pre-act
            DisposeCategories()
            _databaseService.Context.SaveChanges()

            ' input
            Dim name1 = "Normal"
            Dim price1 = 300000
            Dim id1 = "CTG01"
            Dim name2 = "Old"
            Dim price2 = 200000
            Dim id2 = "CTG02"

            ' pre-act
            _roomRepository.AddRoomCategory(New RoomCategory() With {.Id=id1,.Name=name1,.Price=price1})
            _roomRepository.AddRoomCategory(New RoomCategory() With {.Id=id2,.Name=name2,.Price=price2})
            _databaseService.Context.SaveChanges()

            ' act
            Dim categories = _roomRepository.GetAllRoomCategories().ToList()

            ' assert
            CollectionAssert.AllItemsAreNotNull(categories)
            Assert.AreEqual(categories(0).Id, id1)
            Assert.AreEqual(categories(1).Id, id2)
            Assert.IsTrue(categories.Count = 2)

            ' rollback
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
        End Sub

        <TestMethod>
        Public Sub FindCategory_ValidCategory_ValidResult()
            ' input
            DisposeCategories()
            InitializeCategories()

            ' act
            Dim exampleCategory = _categoriesList(1)
            Dim category = _roomRepository.FindRoomCategory(Function(p)(p.Id = exampleCategory.Id)).FirstOrDefault()

            ' Assert
            Assert.IsTrue((category.Id = exampleCategory.Id) And (category.Name = exampleCategory.Name) And (category.Price = exampleCategory.Price))

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
            Dim price = 100
            Dim id2 = "20001"
            Dim name2 = "CatName2"
            Dim price2 = 200
            ' pre-act
            Dim categories = New List(Of RoomCategory)
            categories.Add(New RoomCategory() With{.Id=id,.Name=name,.Price=price})
            categories.Add(New RoomCategory() With{.Id=id2,.Name=name2,.Price=price2})

            ' act
            _roomRepository.AddRoomCategories(categories)
            _databaseService.Context.SaveChanges()

            Dim categoriesFromDatabase = _roomRepository.GetAllRoomCategories().ToList()

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
            Dim category = _roomRepository.GetRoomCategory(_categoriesList(2).Id)

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
            Dim categoryToDelete = _roomRepository.FindRoomCategory(Function(p)(p.Id=category.Id And p.Name = category.Name)).FirstOrDefault()

            ' assert
            _roomRepository.RemoveRoomCategory(categoryToDelete)
            _databaseService.Context.SaveChanges()

            Dim categoryCheck = _roomRepository.FindRoomCategory(Function(p)(p.Id=category.Id And p.Name = category.Name)).FirstOrDefault()

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
            Dim name  = "101"
            Dim category = _categoriesList(0)
            
            ' act
            _roomRepository.Add(New Room() With{.Id=id, .Name=name, .Category=category})
            _databaseService.Context.SaveChanges()
            Dim room = _roomRepository.Find(Function(p)(p.Id = id And p.Name = name And p.Category.Price = category.Price)).FirstOrDefault()

            ' assert
            Assert.IsTrue(room.Id=id And room.Name= name And room.Category.Price = category.Price)
            
            'rollback
            DisposeRooms()
        End Sub

        <TestMethod>
        Public Sub TestAddRooms_ValidRoomsList_RoomsAdded()
            ' input
            DisposeRooms()
            InitializeRooms()

            ' assert
            Dim rooms = _roomRepository.GetAll().ToList()
            Dim i=0
            For Each room As Room In rooms
                Assert.IsTrue(room.id = _roomsList(i).Id And room.Name = _roomsList(i).Name)
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
            Dim room = _roomRepository.GetOne(_roomsList(index).Id)
            
            ' assert
            Assert.IsNotNull(room)
            Assert.AreEqual(_roomsList(index).Id, room.Id)
            Assert.AreEqual(_roomsList(index).Name, room.Name)

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
            Dim rooms = _roomRepository.GetAll().ToList()
            CollectionAssert.AllItemsAreNotNull(rooms)
            Dim i = 0
            For Each room As Room In rooms
                Assert.AreEqual(_roomsList(i).Id,room.Id)
                Assert.AreEqual(_roomsList(i).Name,room.Name)
                Assert.AreEqual(_roomsList(i).State,room.State)
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
            Dim room = _roomsList(index)
            Dim roomToDelete = _roomRepository.Find(Function(p)(p.Id=room.Id And p.Name = room.Name)).FirstOrDefault()
            _roomRepository.Remove(roomToDelete)
            _databaseService.Context.SaveChanges()
            roomToDelete = _roomRepository.Find(Function(p)(p.Id=room.Id And p.Name = room.Name)).FirstOrDefault()

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
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            _databaseService.Context.SaveChanges()

            Dim rooms = _roomRepository.GetAll().ToList()

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
            Dim roomToFind = _roomsList(index)
            Dim room = _roomRepository.Find(Function(p)(p.Id=roomToFind.Id And p.Name=roomToFind.Name)).FirstOrDefault()

            ' assert
            Assert.AreEqual(_roomsList(index).Id,room.Id)
            Assert.AreEqual(_roomsList(index).Name,room.Name)
            Assert.AreEqual(_roomsList(index).State,room.State)

            ' rollback
            DisposeRooms()
        End Sub
    End Class
End NameSpace