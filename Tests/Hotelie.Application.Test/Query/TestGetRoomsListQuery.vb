Imports Hotelie.Application.Rooms.Queries.GetRoomsList
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Rooms

Namespace Query
    <TestClass>
    Public Class TestGetRoomsListQuery
        Private _databaseService As IDatabaseService
        Private _roomRepository As RoomRepository
        Private _roomsList As List(Of Room)
        Private _roomCategoriesList As List(Of RoomCategory)
        Private _getRoomListQuery As GetRoomsListQuery

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection(
                $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _roomRepository = New RoomRepository(_databaseService)
            _getRoomListQuery = New GetRoomsListQuery(_roomRepository)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _databaseService.Context.Dispose()
        End Sub

        Public Sub RoomsInitialize()
            _roomCategoriesList = New List(Of RoomCategory)
            Dim roomCategory1 = New RoomCategory() With{.Id="00001",.Name="NOR01",.Price=100000}
            Dim roomCategory2 = New RoomCategory() With{.Id="00002",.Name="VIP01",.Price=200000}
            _roomCategoriesList.Add(roomCategory1)
            _roomCategoriesList.Add(roomCategory2)
            _roomRepository.AddRoomCategories(_roomCategoriesList)
            _databaseService.Context.SaveChanges()

            _roomsList = New List(Of Room)
            Dim room1 = new Room() _
                    With {.Id="PH001",.Name="Phòng VIP 1",.Category=roomCategory1,.State=0,.Note="Note of PH001"}
            Dim room2 = new Room() _
                    With {.Id="PH002",.Name="Phòng Thường 1",.Category=roomCategory2,.State=1,.Note="Note of PH002"}
            Dim room3 = new Room() _
                    With {.Id="PH003",.Name="Phòng VIP 1",.Category=roomCategory1,.State=1,.Note="Note of PH003"}
            _roomsList.Add(room1)
            _roomsList.Add(room2)
            _roomsList.Add(room3)
            _roomRepository.AddRange(_roomsList)
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeRooms()
            _roomsList?.Clear()
            _roomCategoriesList?.Clear()
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _databaseService.Context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub TestGetAllRoom__ReturnAllRoom()
            ' pre-act
            DisposeRooms()

            ' input
            RoomsInitialize()

            ' act
            Dim rooms = _getRoomListQuery.Execute().ToList()

            ' assert
            CollectionAssert.AllItemsAreNotNull(rooms)
            Assert.AreEqual(_roomsList.Count, rooms.Count)
            For Each room As Room In _roomsList
                Dim q = False
                For Each rm As RoomModel In rooms
                    If room.Id= rm.Id Then 
                        q = True
                        Exit For
                    End If
                Next
                Assert.IsTrue(q)
            Next

            ' rollback
            DisposeRooms()
        End Sub
    End Class
End Namespace