
Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Bills
Imports Hotelie.Domain.Leases

Namespace Bills.Factories
    Public Class CreateBillFactory
        Implements ICreateBillFactory

        Private ReadOnly _billRepository As IBillRepository
        Private ReadOnly _unitOfWork As IUnitOfWork
        Private ReadOnly _userRepository As IUserRepository
        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(billRepository As IBillRepository, unitOfWork As IUnitOfWork, userRepository As IUserRepository, leaseRepository As ILeaseRepository)
            _billRepository = billRepository
            _unitOfWork = unitOfWork
            _userRepository = userRepository
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute(payerName As String, payerAddress As String, leases As List(Of String),
                                totalExpense As Decimal, userId As String) As String _
            Implements ICreateBillFactory.Execute
            Try


                ' auto create new Id
                Dim defaultId = 0
                Dim newId As String = Nothing
                Dim bills = _billRepository.GetAll().ToList()

                If (bills.Count = 0) Then
                    newId = 1.ToString("0000000")
                Else
                    Do While newId = Nothing
                        defaultId += 1
                        Dim newIdCheck = defaultId.ToString("0000000")
                        Dim q = True
                        For Each unit As Bill In bills
                            If (unit.Id = newIdCheck) Then
                                q = False
                                Exit For
                            End If
                        Next
                        If q Then newId = newIdCheck
                    Loop
                End If

                Dim newBill = New Bill() With _
                        {.Id = newId,
                        .CustomerAddress = payerAddress,
                        .CustomerName = payerName,
                        .TotalExpense = totalExpense}

                ' get user who make this bill
                Try
                    Dim user = _userRepository.GetOne(userId)
                    newBill.User = user
                Catch
                End Try

                newBill.Details = New List(Of BillDetail)

                ' get new bill detail list of  id
                Dim defaultDetailId = 1
                Dim qu = True
                Dim billDetailsList = _billRepository.GetBillDetails()
                Dim idList = New List(Of String)
                For i = 0 To leases.Count - 1
                    Dim newDetailId = String.Empty
                    If billDetailsList.Count = 0 And qu Then
                        idList.Add(1.ToString("0000000000"))
                        qu = False
                    Else
                        Do While newDetailId = Nothing
                            defaultDetailId += 1
                            Dim newIdCheck = defaultDetailId.ToString("0000000000")
                            Dim q = True
                            For Each unit As BillDetail In billDetailsList
                                If (unit.Id = newIdCheck) Then
                                    q = False
                                    Exit For
                                End If
                            Next
                            If q Then newDetailId = newIdCheck
                        Loop
                        idList.Add(newDetailId)
                    End If
                Next

                Dim j = - 1
                For Each leaseString As String In leases
                    j += 1
                    Dim lease = _leaseRepository.GetOne(leaseString)
                    Dim billDetail = New BillDetail() _
                            With {.Id = idList(j),.CheckinDate=lease.CheckinDate,.Lease = lease}
                    Dim numberOfDays = DateTime.Now().Subtract(lease.CheckinDate).TotalDays()
                    Dim extraPrice = lease.RoomPrice*lease.ExtraCoefficient*numberOfDays
                    Dim expense = lease.RoomPrice*(1 + lease.CustomerCoefficient)*numberOfDays
                    billDetail.NumberOfDays = numberOfDays
                    billDetail.ExtraCharge = extraPrice
                    billDetail.TotalExpense = extraPrice + expense
                    newBill.Details.Add(billDetail)
                    lease.Paid = 1
                Next

                _billRepository.Add(newBill)
                _unitOfWork.Commit()
                Return String.Empty
            Catch
                Return "Không thể thêm hóa đơn thanh toán"
            End Try
        End Function

        Public Async Function ExecuteAsync(payerName As String, payerAddress As String, leases As List(Of String),
                                           totalExpense As Decimal, userId As String) As Task(Of String) _
            Implements ICreateBillFactory.ExecuteAsync
            Try
                ' auto create new Id
                Dim defaultId = 0
                Dim newId As String = Nothing
                Dim bills = Await _billRepository.GetAll().ToListAsync()

                If (bills.Count = 0) Then
                    newId = 1.ToString("0000000")
                Else
                    Do While newId = Nothing
                        defaultId += 1
                        Dim newIdCheck = defaultId.ToString("0000000")
                        Dim q = True
                        For Each unit As Bill In bills
                            If (unit.Id = newIdCheck) Then
                                q = False
                                Exit For
                            End If
                        Next
                        If q Then newId = newIdCheck
                    Loop
                End If

                Dim newBill = New Bill() With _
                        {.Id = newId,
                        .CustomerAddress = payerAddress,
                        .CustomerName = payerName,
                        .TotalExpense = totalExpense}

                newBill.Details = New List(Of BillDetail)

                ' get user who make this bill
                Try
                    Dim user = Await _userRepository.GetOneAsync(userId)
                    newBill.User = user
                Catch
                End Try

                ' get new bill detail list of  id
                Dim defaultDetailId = 1
                Dim qu = True
                Dim billDetailsList = Await _billRepository.GetBillDetailsAsync()
                Dim idList = New List(Of String)
                For i = 0 To leases.Count - 1
                    Dim newDetailId = String.Empty
                    If billDetailsList.Count = 0 And qu Then
                        idList.Add(1.ToString("0000000000"))
                        qu = False
                    Else
                        Do While newDetailId = Nothing
                            defaultDetailId += 1
                            Dim newIdCheck = defaultDetailId.ToString("0000000000")
                            Dim q = True
                            For Each unit As BillDetail In billDetailsList
                                If (unit.Id = newIdCheck) Then
                                    q = False
                                    Exit For
                                End If
                            Next
                            If q Then newDetailId = newIdCheck
                        Loop
                        idList.Add(newDetailId)
                    End If
                Next

                Dim j = - 1
                For Each leaseString As String In leases
                    j += 1
                    Dim lease = Await _leaseRepository.GetOneAsync(leaseString)
                    Dim billDetail = New BillDetail() _
                            With {.Id = idList(j),.CheckinDate=lease.CheckinDate,.Lease = lease}
                    Dim numberOfDays = DateTime.Now().Subtract(lease.CheckinDate).TotalDays()
                    Dim extraPrice = lease.RoomPrice*lease.ExtraCoefficient*numberOfDays
                    Dim expense = lease.RoomPrice*(1 + lease.CustomerCoefficient)*numberOfDays
                    billDetail.NumberOfDays = numberOfDays
                    billDetail.ExtraCharge = extraPrice
                    billDetail.TotalExpense = extraPrice + expense
                    newBill.Details.Add(billDetail)
                    lease.Paid = 1
                Next

                _billRepository.Add(newBill)
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch
                Return "Không thể thêm hóa đơn thanh toán"
            End Try
        End Function
    End Class
End NameSpace