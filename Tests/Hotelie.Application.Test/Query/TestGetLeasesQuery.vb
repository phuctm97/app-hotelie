Imports System.Globalization
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Rooms

Namespace Query
    <TestClass>
    Public Class TestGetLeasesQuery
        Private _context As DatabaseContext
        Private _leaseRepository As LeaseRepository
        Private _roomRepository As RoomRepository
        Private _leasesList As List(Of Lease)
        Private _roomsList As List(Of Room)
        Private _getLeasesQuery As GetLeasesListQuery

        <TestInitialize>
        Public Sub TestInitialize()
            _context = New DatabaseContext(
                $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _leaseRepository = new LeaseRepository(_context)
            _roomRepository = New RoomRepository(_context)
            _getLeasesQuery = New GetLeasesListQuery(_leaseRepository)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _context.Dispose()
        End Sub

        Public Sub LeasesInitialize()
            DisposeLeases()

            Dim roomCategory = New RoomCategory() With {.Id = "00001", .Name="Annonymous", .Price=200000}

            _roomRepository.AddRoomCategory(roomCategory)
            _context.SaveChanges()

            Dim room1 = New Room() With {.Id="PH001",.Name="101",.Category=roomCategory,.State=0}
            Dim room2 = New Room() With {.Id="PH002",.Name="201",.Category=roomCategory,.State=0}
            Dim room3 = New Room() With {.Id="PH003",.Name="301",.Category=roomCategory,.State=0}

            _roomsList = New List(Of Room)
            _roomsList.Add(room1)
            _roomsList.Add(room2)
            _roomsList.Add(room3)
            _roomRepository.AddRange(_roomsList)
            _context.SaveChanges()

            Dim lease1 = New Lease() With{.Id = "LS001", 
                    .BeginDate=DateTime.ParseExact("5/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture), 
                    .EndDate=DateTime.ParseExact("26/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .Room=room1,.Price=room1.Category.Price}

            Dim lease2 = New Lease() With{.Id = "LS002", 
                    .BeginDate=DateTime.ParseExact("7/5/2016", "d/m/yyyy", CultureInfo.InvariantCulture), 
                    .EndDate=DateTime.ParseExact("8/5/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .Room=room2,.Price=room2.Category.Price}

            Dim lease3 = New Lease() With{.Id = "LS003", 
                    .BeginDate=DateTime.ParseExact("15/11/2016", "d/m/yyyy", CultureInfo.InvariantCulture), 
                    .EndDate=DateTime.ParseExact("16/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .Room=room3,.Price=room3.Category.Price}

            _leasesList = new List(Of Lease)
            _leasesList.Add(lease1)
            _leasesList.Add(lease2)
            _leasesList.Add(lease3)
            _leaseRepository.AddRange(_leasesList)
            _context.SaveChanges()
        End Sub

        Public Sub DisposeLeases()
            _leasesList?.Clear()
            _roomsList?.Clear()
            _leaseRepository.RemoveRange(_leaseRepository.GetAll())
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub TestGetLeasesQuery__LeasesReturn()
            ' pre-input
            LeasesInitialize()

            ' act
            Dim leases = _getLeasesQuery.Execute()

            ' assert 
            Assert.IsNotNull(leases)
            For Each lease As Lease In _leasesList
                Dim q = False
                For Each leaseModel As LeaseModel In leases
                    If (leaseModel.Id = lease.Id And leaseModel.RoomId = lease.Room.Id And leaseModel?.BillId = lease.Bill?.Id) Then
                        q = True
                        Exit For
                    End If
                Next
                Assert.IsTrue(q)
            Next

            ' rollback
            DisposeLeases
        End Sub
    End Class
End NameSpace