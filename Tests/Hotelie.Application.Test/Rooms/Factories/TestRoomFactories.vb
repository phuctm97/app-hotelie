Imports Hotelie.Application.Rooms.Factories.CreateRoom
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Rooms

Namespace Rooms.Factories
    <TestClass>
    Public Class TestRoomFactories
        Private _databaseService As IDatabaseService
        Private _roomRepository As IRoomRepository
        Private _unitOfWork As IUnitOfWork
        Private _roomCategoriesList As List(Of RoomCategory)
        Private _createRoomFactory As CreateRoomFactory

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection(
                $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _roomRepository = New RoomRepository(_databaseService)
            _unitOfWork = New UnitOfWork(_databaseService)
            _createRoomFactory = New CreateRoomFactory(_roomRepository, _unitOfWork)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup
            _databaseService.Context.Dispose()
        End Sub

        Public Sub RoomCategoriesInitialize()
            DisposeRoomCategories()
            _roomCategoriesList = New List(Of RoomCategory)
            Dim roomCategory1 = New RoomCategory() With{.Id="00001",.Name="NOR01",.Price=100000}
            Dim roomCategory2 = New RoomCategory() With{.Id="00002",.Name="VIP01",.Price=200000}
            _roomCategoriesList.Add(roomCategory1)
            _roomCategoriesList.Add(roomCategory2)
            _roomRepository.AddRoomCategories(_roomCategoriesList)
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeRoomCategories()
            _roomCategoriesList?.Clear()
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _databaseService.Context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub TestRoomFactory_NewValidRoomInfo_RoomAdded()
            ' pre-act
            RoomCategoriesInitialize()

            ' input
            Dim newId = "PH001"
            Dim newName = "Phòng mới"
            Dim newCategoryId  = _roomCategoriesList(1).Id
            Dim newNote = ""
            
            Dim room1 = _createRoomFactory.Execute(newName,newCategoryId,newNote)
            Dim room2 = _createRoomFactory.Execute(newName,newCategoryId,newNote)
            Dim room3 = _createRoomFactory.Execute(newName,newCategoryId,newNote)
            Dim room4 = _createRoomFactory.Execute(newName,newCategoryId,newNote)
            
            ' Assert
            Assert.IsNotNull(room1)
            Assert.IsNotNull(room2)
            Assert.IsNotNull(room3)
            Assert.IsNotNull(room4)
            
            Dim newRoom = _roomRepository.GetOne("PH002")
            Assert.AreEqual(newName,newRoom.Name)
            Assert.AreEqual(newCategoryId, newRoom.Category.ID)

            ' rollback
            DisposeRoomCategories
        End Sub
    End Class
End NameSpace