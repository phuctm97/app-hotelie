Imports System.Globalization
Imports Hotelie.Application.Leases.Commands.RemoveLease
Imports Hotelie.Application.Leases.Commands.UpdateLease
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Rooms

Namespace Leases.Commands
    <TestClass>
    Public Class TestLeaseCommand
        Private _databaseService As IDatabaseService
        Private _leaseRepository As LeaseRepository
        Private _roomRepository As RoomRepository
        Private _leasesList As List(Of Lease)
        Private _roomsList As List(Of Room)
        Private _removeLeaseCommand As IRemoveLeaseCommand
        Private _updateLeaseCommand As IUpdateLeaseCommand
        Private _unitOfWork As UnitOfWork


        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS",$"HotelieDatabase")
            _leaseRepository = new LeaseRepository(_databaseService)
            _roomRepository = New RoomRepository(_databaseService)
            _unitOfWork = New UnitOfWork(_databaseService)
            _removeLeaseCommand = New RemoveLeaseCommand(_leaseRepository, _unitOfWork)
            _updateLeaseCommand = New UpdateLeaseCommand(_leaseRepository, _unitOfWork, _roomRepository)
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
        Public Sub TestRemoveLeaseCommand_ValidLeaseId_LeaseRemoved()
            ' pre
            LeasesInitialize()

            ' valid lease id
            Dim index = 1
            Dim leaseId = _leasesList(index).Id

            ' act
            _removeLeaseCommand.Execute(leaseId)

            ' Assert
            Dim leaseFound = _leaseRepository.GetOne(leaseId)
            Assert.IsNull(leaseFound)

            ' rollback
            DisposeLeases()
        End Sub

        <TestMethod>
        Public Sub TestUpdateLeaseCommand_ValidNewLeaseInfo_LeaseUpdate()
            ' pre
            LeasesInitialize()

            ' valid lease id
            Dim index = 1
            Dim leaseId = _leasesList(index).Id

            ' new lease info
            Dim leaseIndex = 2
            Dim newRoomId = _leasesList(index).Room.Id
            Dim newBeginDate = DateTime.ParseExact("24/6/2015","d/m/yyyy", CultureInfo.InvariantCulture)
            Dim newEndDate = DateTime.ParseExact("25/6/2015","d/m/yyyy", CultureInfo.InvariantCulture)

            ' act
            _updateLeaseCommand.Execute(leaseId,newRoomId,newBeginDate,newEndDate)

            ' Assert
            Dim leaseFound = _leaseRepository.GetOne(leaseId)
            Assert.AreEqual(leaseId,leaseFound.Id)
            Assert.AreEqual(newBeginDate,leaseFound.BeginDate)
            Assert.AreEqual(newEndDate,leaseFound.EndDate)
            Assert.AreEqual(newRoomId,leaseFound.Room.Id)

            ' rollback
            DisposeLeases()
        End Sub
    End Class
End NameSpace