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
            _databaseService = New DatabaseService(
                $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _leaseRepository = new LeaseRepository(_databaseService)
            _roomRepository = New RoomRepository(_databaseService)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _databaseService.Context.Dispose()
        End Sub

        Public Sub LeasesInitialize()
            DisposeLeases()

            Dim roomCategory = New RoomCategory() With {.Id = "00001", .Name="Annonymous", .Price=200000}

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
            Assert.IsTrue(leaseFound.Id = lease.Id And leaseFound.BeginDate = lease.BeginDate)


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
            LeasesInitialize()
            _leaseRepository.RemoveRange(_leaseRepository.GetAll())
            _databaseService.Context.SaveChanges()

            ' act
            _leaseRepository.Add(New Lease() With {.Room = _roomsList(0), 
                                    .BeginDate = _leasesList(0).BeginDate,
                                    .EndDate = _leasesList(0).EndDate,
                                    .Id = _leasesList(0).Id,
                                    .Price = _leasesList(0).Price})
            _databaseService.Context.SaveChanges()

            ' assert
            Dim lease = _leaseRepository.GetAll().ToList
            Assert.AreEqual(1, lease.Count)

            ' rollback
            DisposeLeases()
        End Sub

        <TestMethod>
        Public Sub TestAddLeases_ValidLeases_LeasesCountExactly()
            ' pre-input
            LeasesInitialize()
            _leaseRepository.RemoveRange(_leaseRepository.GetAll())
            _databaseService.Context.SaveChanges()

            ' act
            Dim lease1 = New Lease() With {.Room = _roomsList(0), 
                    .BeginDate = _leasesList(0).BeginDate,
                    .EndDate = _leasesList(0).EndDate,
                    .Id = _leasesList(0).Id,
                    .Price = _leasesList(0).Price}
            Dim lease2 = New Lease() With {.Room = _roomsList(1), 
                    .BeginDate = _leasesList(1).BeginDate,
                    .EndDate = _leasesList(1).EndDate,
                    .Id = _leasesList(1).Id,
                    .Price = _leasesList(1).Price}
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
            Dim leaseFound = _leaseRepository.Find(Function(o)o.Id = lease.Id).FirstOrDefault()

            ' assert
            Assert.AreEqual(lease.BeginDate, leaseFound.BeginDate)
            Assert.AreEqual(lease.Room.Id, leaseFound.Room.Id)

            ' rollback
            DisposeLeases()
        End Sub

        <TestMethod>
        Public Sub TestGetLeaseCustomers_ValidLeaseId_AllLeaseCustomers()
            ' pre-input
            LeasesInitialize()

            ' valid lease
            Dim index = 1
            Dim lease = _leasesList(index)

            ' input
            Dim customerCategory = New CustomerCategory() With {.Id="CUS01",.Name="Noi Dia"}
            _databaseService.Context.CustomerCategories.Add(customerCategory)
            _databaseService.Context.SaveChanges()

            Dim leaseCustomer1 = New LeaseDetail() _
                    With {.Lease = lease,.Address="Address 1",.CustomerCategory=customerCategory,
                    .Id="LD001",.CustomerName="Nguyen Van Thi",.LicenseId="123123"}
            _databaseService.Context.LeaseDetails.Add(leaseCustomer1)

            Dim leaseCustomer2 = New LeaseDetail() _
                    With {.Lease = lease,.Address="Address 2",.CustomerCategory=customerCategory,
                    .Id="LD002",.CustomerName="Nguyen Van No",.LicenseId="444555"}
            _databaseService.Context.LeaseDetails.Add(leaseCustomer2)

            _databaseService.Context.SaveChanges()

            ' act
            Dim leaseCustomers = _leaseRepository.GetCustomers(lease.Id)

            ' assert
            Assert.AreEqual(2,leaseCustomers.Count)

            CollectionAssert.AllItemsAreNotNull(leaseCustomers)
            For Each leaseCustomer As LeaseDetail In leaseCustomers
                Dim q = (Equals(leaseCustomer1.Id,leaseCustomer.Id) And (Equals(leaseCustomer1.CustomerCategory.Id,leaseCustomer.CustomerCategory.Id))) _
                        Or (Equals(leaseCustomer2.Id,leaseCustomer.Id) And (Equals(leaseCustomer2.CustomerCategory.Id,leaseCustomer.CustomerCategory.Id)))
                Assert.IsTrue(q)
            Next

            ' rollback
            _databaseService.Context.LeaseDetails.Remove(leaseCustomer1)
            _databaseService.Context.LeaseDetails.Remove(leaseCustomer2)
            _databaseService.Context.CustomerCategories.Remove(customerCategory)
            _databaseService.Context.SaveChanges()
            DisposeLeases()
        End Sub
    End Class
End NameSpace