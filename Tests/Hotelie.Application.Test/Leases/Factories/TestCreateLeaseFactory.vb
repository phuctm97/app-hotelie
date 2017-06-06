Imports System.Globalization
Imports Hotelie.Application.Leases.Factories
Imports Hotelie.Application.Leases.Factories.CreateLease
Imports Hotelie.Application.Leases.Factories.CreateLeaseDetail
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Leases.Queries.GetLeasesList
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Parameters
Imports Hotelie.Persistence.Rooms

Namespace Leases.Factories
    <TestClass>
    Public Class TestGetLeasesQuery
        Private _databaseService As IDatabaseService
        Private _leaseRepository As LeaseRepository
        Private _roomRepository As RoomRepository
        Private _roomsList As List(Of Room)
        Private _unitOfWork As IUnitOfWork
        Private _createLeaseFactory As ICreateLeaseFactory
        Private _parameterRepository As IParameterRepository
        Private _createLeaseDetailFactory As CreateLeaseDetailFactory

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS", $"HotelieDatabase")
            _leaseRepository = new LeaseRepository(_databaseService)
            _roomRepository = New RoomRepository(_databaseService)
            _unitOfWork = New UnitOfWork(_databaseService)
            _parameterRepository = New ParameterRepository(_databaseService)
            _createLeaseFactory = New CreateLeaseFactory(_leaseRepository,_roomRepository,_parameterRepository,_unitOfWork)
            _createLeaseDetailFactory = New CreateLeaseDetailFactory(_leaseRepository,_unitOfWork)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _databaseService.Context.Dispose()
        End Sub

        Public Sub LeasesInitialize()
            DisposeLeases()

            Dim roomCategory = New RoomCategory() With {.Id = "00001", .Name = "Annonymous", .Price = 200000}
            _databaseService.Context.Parameters.Add(New Domain.Parameters.Parameter() With {.Id="00001",.MaximumCustomer=4,.CustomerCoefficient=0.5,.ExtraCoefficient=0.25})
            _roomRepository.AddRoomCategory(roomCategory)
            _databaseService.Context.SaveChanges()

            Dim room1 = New Room() With {.Id = "PH001", .Name = "101", .Category = roomCategory, .State = 0}
            Dim room2 = New Room() With {.Id = "PH002", .Name = "201", .Category = roomCategory, .State = 0}
            Dim room3 = New Room() With {.Id = "PH003", .Name = "301", .Category = roomCategory, .State = 0}

            _roomsList = New List(Of Room)
            _roomsList.Add(room1)
            _roomsList.Add(room2)
            _roomsList.Add(room3)
            _roomRepository.AddRange(_roomsList)
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeLeases()
            _roomsList?.Clear()
            _databaseService.Context.Parameters.RemoveRange(_databaseService.Context.Parameters)
            _databaseService.Context.CustomerCategories.RemoveRange(_databaseService.Context.CustomerCategories)
            _leaseRepository.RemoveRange(_leaseRepository.GetAll())
            _roomRepository.RemoveRange(_roomRepository.GetAll())
            _roomRepository.RemoveRoomCategories(_roomRepository.GetAllRoomCategories())
            _databaseService.Context.SaveChanges()
        End Sub


        <TestMethod>
        Public Sub TestCreateLeaseFactory_ValidNewLeaseInfo_AddedNewLease()
            ' pre-act
            LeasesInitialize()

            ' input
            Dim cusCate= New CustomerCategory() With {.Id = "CUS01", .Name = "Khach", .Coefficient =1}
            _databaseService.Context.CustomerCategories.Add(cusCate)
            _databaseService.Context.SaveChanges() 

            Dim leaseDetail1 = New CreateLeaseDetailModel() With{.CustomerName="Cus 1", .CustomerAddress="adr 1",.CustomerCategoryId=cusCate.Id,.CustomerLicenseId="lsc id"}
            Dim leaseDetail2 = New CreateLeaseDetailModel() With{.CustomerName="Cus 2", .CustomerAddress="adr 21",.CustomerCategoryId=cusCate.Id,.CustomerLicenseId="lsc id"}
            Dim leaseDetail3 = New CreateLeaseDetailModel() With{.CustomerName="Cus 31", .CustomerAddress="adr 421",.CustomerCategoryId=cusCate.Id,.CustomerLicenseId="lsc id"}
            Dim details = New List(Of CreateLeaseDetailModel)
            details.Add(leaseDetail1)
            details.Add(leaseDetail2)
            details.Add(leaseDetail3)

            ' act
            Dim lease1 = _createLeaseFactory.Execute(_roomsList(0).Id,DateTime.Now(),DateTime.Now(),details)
            
            Dim leaseDetail4 = New CreateLeaseDetailModel() With{.CustomerName="Cus 4 1", .CustomerAddress="adr 31",.CustomerCategoryId=cusCate.Id,.CustomerLicenseId="lsc id"}
            Dim k = _createLeaseDetailFactory.Execute(lease1,"NewCustomerFromFactory","12345678","Addressssssss","CUS01")

            ' assert
            Assert.IsNotNull(lease1)

            ' rollback
            DisposeLeases()
        End Sub
    End Class
End Namespace