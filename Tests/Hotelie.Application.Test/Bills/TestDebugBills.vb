Imports Hotelie.Application.Bills.Commands.RemoveBill
Imports Hotelie.Application.Bills.Factories
Imports Hotelie.Application.Bills.Queries.GetBillsList
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases
Imports Hotelie.Persistence.Bills
Imports Hotelie.Persistence.Common
Imports Hotelie.Persistence.Leases
Imports Hotelie.Persistence.Users

Namespace Bills
    <TestClass>
    Public Class TestDebugBills
        Private _databaseService As IDatabaseService
        Private _billRepository As IBillRepository
        Private _getBillsListQuery As IGetBillsListQuery
        Private _createBillFactory As ICreateBillFactory
        Private _leaseRepository As ILeaseRepository
        Private _unitOfWork As IUnitOfWork
        Private _removeBill As IRemoveBillCommand
        Private _userRepository As IUserRepository

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS", $"HotelieDatabase")
            _billRepository = New BillRepository(_databaseService)
            _getBillsListQuery = New GetBillsListQuery(_billRepository)
            _unitOfWork = New UnitOfWork(_databaseService)
            _userRepository = New UserRepository(_databaseService)
            _createBillFactory = New CreateBillFactory(_billRepository,_unitOfWork,_userRepository)
            _leaseRepository = New LeaseRepository(_databaseService)
            _removeBill = New RemoveBillCommand(_unitOfWork,_billRepository)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup
            _databaseService.Dispose()
        End Sub

        <TestMethod>
        Public Sub TestGetBills_Factory()
            Dim r = _getBillsListQuery.Execute()

            CollectionAssert.AllItemsAreNotNull(r)

        End Sub

        <TestMethod>
        Public Sub TestCreateBills_Factory()
            Dim lease = _leaseRepository.GetOne("LS00004")
            Dim lease2 = _leaseRepository.GetOne("L000001")
            Dim leaseList = New List(Of Lease)
            leaseList.Add(lease)
            leaseList.Add(lease2)
            Dim r = _createBillFactory.Execute("PayerName","PayerAddress",leaseList,3000000,"admin")

            Assert.IsNotNull(r)

        End Sub

        <TestMethod>
        Public Sub TestRemove()
            _removeBill.Execute("0000001")
            Dim r = _billRepository.GetOne("0000001")
            Assert.IsNull(r)
        End Sub
    End Class
End NameSpace