Imports System.Globalization
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Rooms

Namespace Leases
    <TestClass>
    Public Class TestLeaseRepository
        Private _databaseService As IDatabaseService
        Private _leaseRepository As LeaseRepository
        Private _roomRepository As RoomRepository
        Private _leasesList As List(Of Lease)
        Private _roomsList As List(Of Room)

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS", $"HotelieDatabase")
            _leaseRepository = new LeaseRepository(_databaseService)
            _roomRepository = New RoomRepository(_databaseService)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _databaseService.Context.Dispose()
        End Sub

        Public Sub LeasesInitialize()
            DisposeLeases()
            _databaseService.Context.CustomerCategories.RemoveRange(_databaseService.Context.CustomerCategories)
            _databaseService.Context.LeaseDetails.RemoveRange(_databaseService.Context.LeaseDetails)

            Dim roomCategory = New RoomCategory() With {.Id = "00001", .Name="Annonymous", .Price=200000}
            Dim customerCategory = New CustomerCategory() With {.Id="CTEST",.Name="Category",.Coefficient=22}

            _roomRepository.AddRoomCategory(roomCategory)
            _databaseService.Context.SaveChanges()

            Dim room1 = New Room() With {.Id="PH001",.Name="101",.Category=roomCategory,.State=0}
            Dim room2 = New Room() With {.Id="PH002",.Name="201",.Category=roomCategory,.State=0}
            Dim room3 = New Room() With {.Id="PH003",.Name="301",.Category=roomCategory,.State=0}

            _roomsList = New List(Of Room)
            _roomsList.Add(room1)
            _roomsList.Add(room2)
            _roomsList.Add(room3)
            _roomRepository.AddRange(_roomsList)
            _databaseService.Context.SaveChanges()

            Dim lease1 = New Lease() With{.Id = "LS00100",
                    .CheckinDate = DateTime.ParseExact("5/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .ExpectedCheckoutDate = DateTime.ParseExact("26/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .Room = room1, .RoomPrice = room1.Category.Price}


            Dim lease2 = New Lease() With {.Id = "LS00200",
                    .CheckinDate = DateTime.ParseExact("7/5/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .ExpectedCheckoutDate = DateTime.ParseExact("8/5/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .Room = room2, .RoomPrice = room2.Category.Price}


            Dim lease3 = New Lease() With {.Id = "LS00300",
                    .CheckinDate = DateTime.ParseExact("15/11/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .ExpectedCheckoutDate = DateTime.ParseExact("16/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .Room = room3, .RoomPrice = room3.Category.Price}


            _leasesList = new List(Of Lease)
            _leasesList.Add(lease1)
            _leasesList.Add(lease2)
            _leasesList.Add(lease3)
            _leaseRepository.AddRange(_leasesList)
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeLeases()
            _leasesList?.Clear()
            _roomsList?.Clear()
            _leaseRepository.RemoveRange(_leaseRepository.GetAll())
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _databaseService.Context.SaveChanges()
        End Sub

        <TestMethod>
        Public Sub TestGetLease_ValidLeaseId_ValidLeaseReturn()
            ' input
            LeasesInitialize()
            ' valid id
            Dim index = 1
            Dim lease = _leasesList(index)

            ' act
            Dim leaseFound = _leaseRepository.GetOne(lease.Id)

            ' assert
            Assert.IsNotNull(leaseFound)
            Assert.IsTrue(leaseFound.Id = lease.Id)
            Assert.AreEqual(lease.CheckinDate, leaseFound.CheckinDate)
            Assert.AreEqual(lease.ExpectedCheckoutDate, leaseFound.ExpectedCheckoutDate)
            Assert.AreEqual(lease.ExtraCoefficient, leaseFound.ExtraCoefficient)
            Assert.AreEqual(lease.RoomPrice, leaseFound.RoomPrice)
            Assert.AreEqual(lease.Room.Id, leaseFound.Room.Id)
            Assert.AreEqual(lease.CustomerCoefficient, leaseFound.CustomerCoefficient)

            ' rollback
            DisposeLeases()
        End Sub

        <TestMethod>
        Public Sub TestGetLeases__CountEquals()
            ' input
            LeasesInitialize()

            ' act
            Dim leaseFound = _leaseRepository.GetAll().ToList

            ' assert
            Assert.AreEqual(_leasesList.Count, leaseFound.Count)

            ' rollback
            DisposeLeases()
        End Sub

        <TestMethod>
        Public Sub TestRemoveLease_ValidLeaseId_LeaseRemoved()
            ' input
            LeasesInitialize()

            ' Valid lease id
            Dim index = 0
            Dim lease = _leaseRepository.GetOne(_leasesList(index).Id)

            ' act
            _leaseRepository.Remove(lease)
            _databaseService.Context.SaveChanges()

            ' assert
            lease = _leaseRepository.GetOne(_leasesList(index).Id)
            Assert.IsNull(lease)

            ' rollback
            DisposeLeases()
        End Sub

        <TestMethod>
        Public Sub TestRemoveAllLease__LeaseCountEqualZero()
            ' input
            LeasesInitialize()

            ' act
            _leaseRepository.RemoveRange(_leaseRepository.GetAll())
            _databaseService.Context.SaveChanges()

            ' assert
            Dim lease = _leaseRepository.GetAll().ToList
            Assert.AreEqual(0, lease.Count)

            ' rollback
            DisposeLeases()
        End Sub

        <TestMethod>
        Public Sub TestAddLease_ValidLease_LeaseCountIncrease()
            ' pre-input
            DisposeLeases()

            ' input
            Dim roomCategory = New RoomCategory() With {.Id = "00001", .Name="Annonymous", .Price=200000}
            _databaseService.Context.RoomCategories.Add(roomCategory)
            _databaseService.Context.SaveChanges()

            Dim room1 = New Room() With {.Id="PH001",.Name="101",.Category=roomCategory,.State=0}
            _databaseService.Context.Rooms.Add(room1)
            _databaseService.Context.SaveChanges()

            Dim leasez = New Lease() With{.Id = "LS00100",
                    .CheckinDate = DateTime.ParseExact("5/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .ExpectedCheckoutDate = DateTime.ParseExact("26/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture),
                    .Room = room1, .RoomPrice = room1.Category.Price}

            ' act
            _leaseRepository.Add(New Lease() With {.Room = room1,
                                    .CheckinDate =leasez.CheckinDate,
                                    .ExpectedCheckoutDate = leasez.ExpectedCheckoutDate,
                                    .Id = leasez.Id,
                                    .RoomPrice = leasez.RoomPrice})

            _databaseService.Context.SaveChanges()

            ' assert
            Dim lease = _leaseRepository.GetAll().ToList
            Assert.AreEqual(1, lease.Count)
            Assert.AreEqual(leasez.Id, lease(0).Id)
            Assert.AreEqual(leasez.Room.Id, lease(0).Room.Id)
            Assert.AreEqual(leasez.ExpectedCheckoutDate, lease(0).ExpectedCheckoutDate)
            Assert.AreEqual(leasez.CheckinDate, lease(0).CheckinDate)

            ' rollback
            DisposeLeases()
        End Sub

        <TestMethod>
        Public Sub TestAddLeases_ValidLeases_LeasesCountExactly()
            ' pre-input
            DisposeLeases()

            ' input
            Dim roomCategory = New RoomCategory() With {.Id = "00001", .Name="Annonymous", .Price=200000}
            _databaseService.Context.RoomCategories.Add(roomCategory)
            _databaseService.Context.SaveChanges()

            Dim room1 = New Room() With {.Id="PH001",.Name="101",.Category=roomCategory,.State=0}
            _databaseService.Context.Rooms.Add(room1)
            _databaseService.Context.SaveChanges()

            ' declare
                ' lease1 info
            Dim id1 = "LS00001"
            Dim checkinDate1 = DateTime.ParseExact("5/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture)
            Dim exCDate1 = DateTime.ParseExact("26/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture)

                ' lease 2 info
            Dim id2 = "LS00002"
            Dim checkinDate2 = DateTime.ParseExact("7/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture)
            Dim exCDate2 = DateTime.ParseExact("9/12/2016", "d/m/yyyy", CultureInfo.InvariantCulture)

            ' act
            Dim lease1 = New Lease() With {.Room = room1,
                    .CheckinDate = checkinDate1,
                    .ExpectedCheckoutDate = exCDate1,
                    .Id = id1,
                    .RoomPrice = 200000}
            Dim lease2 = New Lease() With {.Room = room1,
                    .CheckinDate = checkinDate2,
                    .ExpectedCheckoutDate = exCDate2,
                    .Id = id2,
                    .RoomPrice = 300000}

            Dim leases = New List(Of Lease)
            leases.Add(lease1)
            leases.Add(lease2)

            _leaseRepository.AddRange(leases)
            _databaseService.Context.SaveChanges()

            ' assert
            Dim lease = _leaseRepository.GetAll().ToList
            Assert.AreEqual(2, lease.Count)

            ' rollback
            DisposeLeases()
        End Sub

        <TestMethod>
        Public Sub TestFindLease_ValidLeaseId_LeaseFoundWithData()
            ' pre-input
            LeasesInitialize()

            ' valid lease
            Dim index = 2
            Dim lease = _leasesList(2)

            ' act
            Dim leaseFound = _leaseRepository.Find(Function(o) o.Id = lease.Id).FirstOrDefault()

            ' assert
            Assert.AreEqual(lease.CheckinDate, leaseFound.CheckinDate)
            Assert.AreEqual(lease.Room.Id, leaseFound.Room.Id)
            Assert.AreEqual(lease.RoomPrice, leaseFound.RoomPrice)
            Assert.AreEqual(lease.Room.Category.Id, leaseFound.Room.Category.Id)
            Assert.AreEqual(lease.ExtraCoefficient, leaseFound.ExtraCoefficient)

            ' rollback
            DisposeLeases()
        End Sub
    End Class
End NameSpace