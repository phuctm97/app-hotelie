Imports System.Globalization
Imports Hotelie.Application.Leases.Factories
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.DatabaseServices
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Rooms

Namespace Leases.Factories
    <TestClass>
    Public Class TestGetLeasesQuery
        Private _databaseService As DatabaseService
        Private _leaseRepository As LeaseRepository
        Private _roomRepository As RoomRepository
        Private _roomsList As List(Of Room)
        Private _unitOfWork As IUnitOfWork
        Private _getLeasesQuery As GetLeasesListQuery
        Private _createLeaseFactory As ICreateLeaseFactory

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService(
                $"data source=KHUONG-ASUS\SQLEXPRESS;initial catalog=HotelieDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
            _leaseRepository = new LeaseRepository(_databaseService)
            _roomRepository = New RoomRepository(_databaseService)
            _unitOfWork = New UnitOfWork(_databaseService)
            _getLeasesQuery = New GetLeasesListQuery(_leaseRepository)
            _createLeaseFactory = New CreateLeaseFactory(_leaseRepository,_unitOfWork,_roomRepository)
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

        End Sub

        Public Sub DisposeLeases()
            _roomsList?.Clear()
            _leaseRepository.RemoveRange(_leaseRepository.GetAll())
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _databaseService.Context.SaveChanges()
        End Sub


        <TestMethod>
        Public Sub TestCreateLeaseFactory_ValidNewLeaseInfo_AddedNewLease()
            ' pre-act
            LeasesInitialize()

            ' Valid new lease info
            Dim newId  = "LS001"
            Dim newRoomId = _roomsList(0).Id
            Dim newBeginDate = DateTime.ParseExact("7/5/2016", "d/m/yyyy", CultureInfo.InvariantCulture)
            Dim newEndDate = DateTime.ParseExact("8/5/2016", "d/m/yyyy", CultureInfo.InvariantCulture)

            ' act
            Dim lease = _createLeaseFactory.Execute(newId,newRoomId,newBeginDate,newEndDate)
            
            ' assert
            Assert.IsNotNull(lease)

            Dim newLease = _leaseRepository.GetOne(newId)
            Assert.AreEqual(_roomsList(0).Category.Price,newLease.Price)
            Assert.AreEqual(newRoomId,newLease.Room.Id)
            Assert.AreEqual(newRoomId,newLease.Room.Id)


            ' rollback
            DisposeLeases()
        End Sub
    End Class
End Namespace