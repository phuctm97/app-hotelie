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
            InitBills()

            ' act
            _billRepository.Remove(_billsList(0))
            _databaseService.Context.SaveChanges()

            ' assert
            Dim result = _databaseService.Context.Bills.ToList()

            Assert.IsFalse((result.Exists(Function(p)p.Id = _billsList(0).Id)) Or _
                           (result.Where(Function(p)p.CustomerAddress= _billsList(0).CustomerAddress).Count>0) Or _
                           (result.Where(Function(p)p.TotalExpense = _billsList(0).TotalExpense).Count>0))


            ' rollback 
            DisposeBills
        End Sub

        <TestMethod>
        Public Sub TestRemoveAll__BillCountEqualsZero()
            ' pre-act
            InitBills()

            ' act
            _billRepository.RemoveRange(_databaseService.Context.Bills)
            _databaseService.Context.SaveChanges()

            ' Assert
            Dim result = _databaseService.Context.Bills.ToList()
            Assert.IsTrue(result.Count = 0)
            Assert.IsFalse(result.Any(Function (p) p.Id = _billsList(0).Id))
            Assert.IsFalse(result.Any(Function (p) p.CustomerAddress = _billsList(0).CustomerAddress))

            ' rollback 
            DisposeBills
        End Sub

        <TestMethod>
        Public Sub TestFind_ValidBillId_BillReturn()
            ' pre-act
            InitBills()

            ' input
            Dim index = 1
            Dim bill = _billsList(index)

            ' act
            Dim billFound = _billRepository.Find(Function(p)p.Id = bill.Id).FirstOrDefault

            ' Assert
            Dim result = _databaseService.Context.Bills.ToList()
            
            Assert.IsNotNull(billFound)
            Assert.AreEqual(bill.CustomerAddress, billFound.CustomerAddress)
            Assert.AreEqual(bill.id, billFound.id)
            Assert.AreEqual(bill.CustomerName, billFound.CustomerName)
            ' rollback 
            DisposeBills
        End Sub


    End Class
End NameSpace