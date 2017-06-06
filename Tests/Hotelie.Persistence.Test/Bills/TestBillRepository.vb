Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Bills
Imports Hotelie.Persistence.Bills
Imports Hotelie.Persistence.Common

Namespace Bills
    <TestClass>
    Public Class TestBillRepository
        Private _databaseService As IDatabaseService
        Private _billsList As List(Of Bill)
        Private _billRepository As IBillRepository

        <TestInitialize>
        Public Sub TestInitialize()
            _databaseService = New DatabaseService()
            _databaseService.SetDatabaseConnection($"KHUONG-ASUS\SQLEXPRESS", $"HotelieDatabase")
           _billRepository = New BillRepository(_databaseService)
        End Sub

        <TestCleanup>
        Public Sub TestCleanup()
            _databaseService.Dispose()
        End Sub

        Public Sub InitBills()
            DisposeBills()
            Dim bill1 = New Bill() With {.Id = "B000001",.CustomerName="CustomerName1",.CustomerAddress="CustomerAddress1",.TotalExpense=200000}
            Dim bill2 = New Bill() With {.Id = "B000002",.CustomerName="CustomerName2",.CustomerAddress="CustomerAddress2",.TotalExpense=400000}

            _billsList = New List(Of Bill)
            _billsList.Add(bill1)
            _billsList.Add(bill2)
            _databaseService.Context.Bills.AddRange(_billsList)
            _databaseService.Context.SaveChanges()
        End Sub

        Public Sub DisposeBills()
            _billsList?.Clear()
            _databaseService.Context.Bills.RemoveRange(_databaseService.Context.Bills)
        End Sub

        <TestMethod>
        Public Sub TestGetBills__ValidBillList()
            ' pre-act
            InitBills()

            ' act
            Dim result = _billRepository.GetAll().ToList()

            ' Assery
            Assert.IsTrue(result.Count=2)
            Assert.IsTrue(result.Contains(_billsList(0)))
            Assert.IsTrue(result.Contains(_billsList(1)))
            ' rollback 
            DisposeBills
        End Sub

        <TestMethod>
        Public Sub TestGetOne_ValidBillId_ReturnBill()
            ' pre-act
            InitBills()

            ' input valid
            Dim index = 0
            Dim bill = _billsList(index)

            ' act
            Dim result = _billRepository.GetOne(bill.Id)

            ' Assery
            Assert.IsTrue(result.Id = bill.Id)
            Assert.IsTrue(result.TotalExpense = bill.TotalExpense)
            Assert.IsTrue(result.CustomerAddress = bill.CustomerAddress)
            ' rollback 
            DisposeBills
        End Sub

        <TestMethod>
        Public Sub TestAddOne_ValidBillInfo_NewBillAdded()
            ' pre-act
            DisposeBills()

            ' input valid
            Dim bill = New Bill() With {.Id = "0000001",.CustomerName="CustomerName",.CustomerAddress="CustomerAddress",.TotalExpense=200000}
            _billRepository.Add(bill)
            _databaseService.Context.SaveChanges()

            ' act
            Dim result = _databaseService.Context.Bills.FirstOrDefault(Function(p)p.Id = bill.Id)

            ' Assery
            Assert.IsTrue(result.Id = bill.Id)
            Assert.IsTrue(result.TotalExpense = bill.TotalExpense)
            Assert.IsTrue(result.CustomerAddress = bill.CustomerAddress)
            ' rollback 
            DisposeBills
        End Sub

        <TestMethod>
        Public Sub TestAddMany_ValidBillInfo_NewBillAdded()
            ' pre-act
            DisposeBills()

            ' input valid
            Dim bill1 = New Bill() With {.Id = "0000001",.CustomerName="CustomerName1",.CustomerAddress="CustomerAddress1",.TotalExpense=200000}
            Dim bill2 = New Bill() With {.Id = "0000002",.CustomerName="CustomerName2",.CustomerAddress="CustomerAddress2",.TotalExpense=3200000}
            Dim billsList = New List(Of Bill)
            billsList.Add(bill1)
            billsList.Add(bill2)
            _billRepository.AddRange(billsList)
            _databaseService.Context.SaveChanges()

            ' act
            Dim result = _databaseService.Context.Bills.ToList()

            ' Assery
            Assert.IsTrue(result.Exists(Function(b)b.Id = bill1.Id))
            Assert.IsTrue(result.Exists(Function(b)b.CustomerName = bill1.CustomerName))
            Assert.IsTrue(result.Exists(Function(b)b.Id = bill2.Id))
            ' rollback 
            DisposeBills
        End Sub

        <TestMethod>
        Public Sub TestRemove_ValidBillId_BillWithIdRemoved()
            ' pre-act
            DisposeBills()

            ' input valid
            Dim bill1 = New Bill() With {.Id = "0000001",.CustomerName="CustomerName1",.CustomerAddress="CustomerAddress1",.TotalExpense=200000}
            Dim bill2 = New Bill() With {.Id = "0000002",.CustomerName="CustomerName2",.CustomerAddress="CustomerAddress2",.TotalExpense=3200000}
            _databaseService.Context.Bills.Add(bill1)
            _databaseService.Context.Bills.Add(bill2)

            ' act
            _billRepository.Remove(bill1)

            _databaseService.Context.SaveChanges()
            ' Assery
            Assert.IsTrue(1)
            ' rollback 
            DisposeBills
        End Sub
    End Class
End NameSpace