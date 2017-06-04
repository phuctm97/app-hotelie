Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Users

Namespace Users
    <TestClass>
    Public Class TestUserRepository
        Private _databaseService As IDatabaseService
        Private _userRepository As UserRepository
        Private _categoriesList As List(Of UserCategory)
        Private _usersList As List(Of User)

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService= New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS",$"HotelieDatabase")
            _userRepository = new UserRepository(_databaseService)
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
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeCategories()
            _categoriesList?.Clear()
            _userRepository.RemoveUserCategories(_userRepository.GetAllUserCategories())
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub InitializeUsers()
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
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeUsers()
            DisposeCategories()
            _usersList?.Clear()
            _userRepository.RemoveRange(_userRepository.GetAll())
            _databaseService.Context.SaveChanges()
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _userRepository.RemoveUserCategories(_userRepository.GetAllUserCategories())
            _databaseService.Context.Dispose()
        End Sub

        <TestMethod>
        Public Sub TestRemoveAllCategories_RemoveAllCategories_CountEqualsZero()

            ' act
            _userRepository.RemoveUserCategories(_userRepository.GetAllUserCategories())
            _databaseService.Context.SaveChanges()

            ' assert
            Dim categories = _userRepository.GetAllUserCategories().ToList()
            Assert.AreEqual(0, categories.Count())
        End Sub

        <TestMethod>
        Public Sub TestAddCategory_ValidCategory_CountIncrease()
            ' pre-input
            DisposeUsers()

            ' input
            Dim userCategoriesCount = _userRepository.GetAllUserCategories().Count()
            Dim name = "nhanviena"
            Dim id = "10001"

            ' act
            _userRepository.AddUserCategory(New UserCategory() With {.Id=id,.Name=name})
            _databaseService.Context.SaveChanges()

            ' assert
            Assert.AreEqual(userCategoriesCount + 1, _userRepository.GetAllUserCategories().Count())
            CollectionAssert.AllItemsAreNotNull(_userRepository.GetAllUserCategories().ToList())

            ' rollback
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestGetAllUserCategories_ValidCategories_CountExtractly()
            ' pre-act
            DisposeCategories()
            _databaseService.Context.SaveChanges()

            ' input
            Dim id1 = "10001"
            Dim name1 = "nhanviena"
            Dim id2 = "20001"
            Dim name2 = "quanlyb"

            ' pre-act
            _userRepository.AddUserCategory(New UserCategory() With {.Id=id1,.Name=name1})
            _userRepository.AddUserCategory(New UserCategory() With {.Id=id2,.Name=name2})
            _databaseService.Context.SaveChanges()

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
            _databaseService.Context.SaveChanges()

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
            _databaseService.Context.SaveChanges()

            Dim categoryCheck = _userRepository.FindUserCategory(Function(p)(p.Id=category.Id And p.Name = category.Name)).FirstOrDefault()

            Assert.IsNull(categoryCheck)

            ' rollback
            DisposeCategories()
        End Sub



        <TestMethod>
        Public Sub TestAddUser_ValidUser_UserAdded()
            ' pre-act
            DisposeUsers() 
            InitializeCategories()

            ' input
            Dim id = "nhanviena"
            Dim password  = "admin"
            Dim category = _categoriesList(0)
            
            ' act
            _userRepository.Add(New User() With{.Id=id, .Password=password, .Category=category})
            _databaseService.Context.SaveChanges()
            Dim user = _userRepository.Find(Function(p)(p.Id = id And p.Password = password)).FirstOrDefault()

            ' assert
            Assert.IsTrue(user.Id=id And user.Password= password)
            
            'rollback
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestAddUsers_ValidUsersList_UsersAdded()
            ' input
            DisposeUsers()
            InitializeUsers()

            ' assert
            Dim users = _userRepository.GetAll().ToList()
            Dim i=0
            For Each user As User In users
                Assert.IsTrue(user.id = _usersList(i).Id And user.Password = _usersList(i).Password)
                i = i + 1
            Next
            
            ' rollback
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestGetUser_ValidIdIndex_ReturnRightUser()
            ' pre-input
            DisposeUsers()

            ' input
            InitializeUsers()

            ' act
            Dim index = 1
            Dim user = _userRepository.GetOne(_usersList(index).Id)
            
            ' assert
            Assert.IsNotNull(user)
            Assert.AreEqual(_usersList(index).Id, user.Id)
            Assert.AreEqual(_usersList(index).Password, user.Password)

            ' rollback
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestGetUsers__ReturnAllUsers()
            ' pre-input
            DisposeUsers()

            ' input
            InitializeUsers()

            ' act
            Dim users = _userRepository.GetAll().ToList()
            CollectionAssert.AllItemsAreNotNull(users)
            Dim i = 0
            For Each user As User In users
                Assert.AreEqual(_usersList(i).Id,user.Id)
                Assert.AreEqual(_usersList(i).Password,user.Password)
                i = i + 1
            Next

            ' rollback
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestRemoveUser_ValidUsersListItemIndex_UserDeleted()
            ' pre-input
            DisposeUsers()
            
            ' input
            InitializeUsers()

            ' act
            Dim index = 1
            Dim user = _usersList(index)
            Dim userToDelete = _userRepository.Find(Function(p)(p.Id=user.Id And p.Password = user.Password)).FirstOrDefault()
            _userRepository.Remove(userToDelete)
            _databaseService.Context.SaveChanges()
            userToDelete = _userRepository.Find(Function(p)(p.Id=user.Id And p.Password = user.Password)).FirstOrDefault()

            ' assert
            Assert.IsNull(userToDelete)

            ' rollback
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestRemoveAllUsers__UsersIsNull()
            ' pre-input
            DisposeUsers()

            ' input
            InitializeUsers()

            ' act
            _userRepository.RemoveRange(_userRepository.GetAll())
            _databaseService.Context.SaveChanges()

            Dim users = _userRepository.GetAll().ToList()

            Assert.AreEqual(0,users.Count)

            ' rollback
            DisposeUsers()
        End Sub

        <TestMethod>
        Public Sub TestFindUser_ValidUsersListItemIndex_ReturnRightUser()
            ' pre-act
            DisposeUsers()

            ' input
            InitializeUsers()

            'act
            Dim index = 2
            Dim userToFind = _usersList(index)
            Dim user = _userRepository.Find(Function(p)(p.Id=userToFind.Id And p.Password=userToFind.Password)).FirstOrDefault()

            ' assert
            Assert.AreEqual(_usersList(index).Id,user.Id)
            Assert.AreEqual(_usersList(index).Password,user.Password)

            ' rollback
            DisposeUsers()
        End Sub
    End Class
End NameSpace