Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Rooms

Namespace RoomsQuery
    <TestClass>
    Public Class TestGetRoomList
        Private _context As DatabaseContext
        Private _roomRepository As IRoomRepository
        <TestInitialize>
        Public Sub TestInitialize()
            _context = New DatabaseContext(
                $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _roomRepository= new RoomRepository(_context)
        End Sub
        <TestCleanup>
        Public Sub TestCleanup()
            _context.Dispose()
        End Sub
        <TestMethod>
        Public Sub GetRoom_GetAllRooms_ExactlyCount()
            ' act
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            Dim rc = New RoomCategory()With{}

            ' assert

        End Sub
    End Class
End NameSpace