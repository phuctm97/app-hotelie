Imports Hotelie.Application.Rooms.Commands.RemoveRoom
Imports Hotelie.Application.Rooms.Commands.UpdateRoom
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Rooms

Namespace Rooms.Commands.RemoveRoom
    <TestClass>
    Public Class TestRemoveRoomCommand
        Private _context As DatabaseContext
        Private _unitOfWork As IUnitOfWork
        Private _roomRepository As IRoomRepository
        Private _removeRoomCommand As IRemoveRoomCommand
        Private _updateRoomCommand As IUpdateRoomCommand
        Private _roomsList As List(Of Room)
        Private _roomCategoriesList As List(Of RoomCategory)

        <TestInitialize>
        Public Sub TestInitialize()
            _context =
                New DatabaseContext(
                    $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _roomRepository = New RoomRepository(_context)
            _unitOfWork = New UnitOfWork(_context)
            _removeRoomCommand = new RemoveRoomCommand(_roomRepository, _unitOfWork)
            _updateRoomCommand = new UpdateRoomCommand(_roomRepository, _unitOfWork)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup
            _context.Dispose()
        End Sub

        Public Sub RoomsInitialize()
            _roomCategoriesList = New List(Of RoomCategory)
            Dim roomCategory1 = New RoomCategory() With{.Id="00001",.Name="NOR01",.Price=100000}
            Dim roomCategory2 = New RoomCategory() With{.Id="00002",.Name="VIP01",.Price=200000}
            _roomCategoriesList.Add(roomCategory1)
            _roomCategoriesList.Add(roomCategory2)
            _roomRepository.AddRoomCategories(_roomCategoriesList)
            _context.SaveChanges()

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
            _context.SaveChanges()
        End Sub

        Public Sub DisposeRooms()
            _roomsList?.Clear()
            _roomCategoriesList?.Clear()
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub TestRemoveRoomCommand_ValidRoomId_RoomRemoved()
            ' pre-input
            DisposeRooms()

            ' input
            RoomsInitialize()

            ' act
            Dim roomId = _roomsList(1).Id
            _removeRoomCommand.Execute(roomId)

            Dim room = _roomRepository.GetOne(roomId)

            ' assert 
            Assert.IsNull(room)

            ' rollback
            DisposeRooms()
        End Sub

        <TestMethod>
        Public Sub TestUpdateRoomCommand_ValidRoomData_RoomUpdate()
            ' pre-input
            DisposeRooms()

            ' input
            RoomsInitialize()

            ' New room info
            Dim name = "Phòng mới"
            Dim categoryId = _roomCategoriesList(1).Id
            Dim note = "abcxyz"

            ' act
            Dim index = 0
            _updateRoomCommand.Execute(_roomsList(index).Id,name,categoryId,note,0)

            ' assert
            Dim room = _roomRepository.GetOne(_roomsList(index).Id)
            Assert.IsNotNull(room)
            Assert.AreEqual(name, room.Name)
            Assert.AreEqual(categoryId, room.Category.Id)
            Assert.AreEqual(note, room.Note)

            ' rollback 
            DisposeRooms()
        End Sub
    End Class
End NameSpace