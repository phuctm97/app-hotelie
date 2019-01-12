Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Factories
    Public Class CreateLeaseFactory
        Implements ICreateLeaseFactory

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _roomRepository As IRoomRepository
        Private ReadOnly _paramaterRepository As IParameterRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(leaseRepository As ILeaseRepository, roomRepository As IRoomRepository,
                paramaterRepository As IParameterRepository, unitOfWork As IUnitOfWork)
            _leaseRepository = leaseRepository
            _roomRepository = roomRepository
            _paramaterRepository = paramaterRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(roomId As String, checkinDate As Date, expectedCheckoutDate As Date,
                                details As IEnumerable(Of CreateLeaseDetailModel)) As String _
            Implements ICreateLeaseFactory.Execute
            Try
                ' auto-create new lease id
                Dim leases = _leaseRepository.GetAll().ToList()
                Dim defaultId = 0
                Dim newId = String.Empty

                If leases.Count = 0 Then
                    newId = "L" + 1.ToString("000000")
                Else
                    Do While newId = Nothing
                        defaultId += 1
                        Dim newIdCheck = "L" + defaultId.ToString("000000")
                        Dim q = True
                        For Each unit As Lease In leases
                            If (unit.Id = newIdCheck) Then
                                q = False
                                Exit For
                            End If
                        Next
                        If q Then newId = newIdCheck
                    Loop
                End If

                ' update rules
                Dim room = _roomRepository.GetOne(roomId)
                Dim rules = _paramaterRepository.GetRules()
                Dim newLease = New Lease() _
                        With {.Id = newId, .CheckinDate = checkinDate, .ExpectedCheckoutDate =expectedCheckoutDate}
                newLease.CustomerCoefficient = 0
                newLease.ExtraCoefficient = rules.ExtraCoefficient
                newLease.RoomPrice = room.Category.Price

                ' add lease details of lease
                newLease.LeaseDetails = New List(Of LeaseDetail)
                newLease.Room = room
                room.State = 1
                newLease.Paid = 0


                ' get new lease detail list id

                Dim defaultDetailId = 1
                Dim qu = True
                Dim leaseDetailsList = _leaseRepository.GetLeaseDetails()
                Dim idList = New List(Of String)
                For i = 0 To details.Count - 1
                    Dim newDetailId = String.Empty
                    If leaseDetailsList.Count = 0 And qu Then
                        idList.Add(1.ToString("0000000000"))
                        qu = False
                    Else
                        Do While newDetailId = Nothing
                            defaultDetailId += 1
                            Dim newIdCheck = defaultDetailId.ToString("0000000000")
                            Dim q = True
                            For Each unit As LeaseDetail In leaseDetailsList
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

                ' create new lease
                Dim z = - 1
                For Each detailModel As CreateLeaseDetailModel In details
                    z + = 1
                    ' get the customer type
                    Dim customerCategory =
                            _leaseRepository.GetCustomerCategories().FirstOrDefault(
                                Function(p)p.Id = detailModel.CustomerCategoryId)

                    ' add lease detail to lease
                    newLease.LeaseDetails.Add(
                        New LeaseDetail() _
                                                 With {.Id = idList(z), .CustomerCategory = customerCategory, _ 
                                                 .Address=detailModel.CustomerAddress,
                                                 .CustomerName = detailModel.CustomerName,
                                                 .LicenseId = detailModel.CustomerLicenseId})
                Next

                ' init lease coefficient
                Dim coeff = 0
                For Each leaseDetail As LeaseDetail In newLease.LeaseDetails
                    If coeff<leaseDetail.CustomerCategory.Coefficient Then coeff = leaseDetail.CustomerCategory.Coefficient
                Next
                newLease.CustomerCoefficient = coeff

                ' add new lease to database
                _leaseRepository.Add(newLease)
                _unitOfWork.Commit()
                Return newId
            Catch
                Return String.Empty
            End Try
        End Function

        Public Async Function ExecuteAsync(roomId As String, checkinDate As Date, expectedCheckoutDate As Date,
                                           details As IEnumerable(Of CreateLeaseDetailModel)) As Task(Of String) _
            Implements ICreateLeaseFactory.ExecuteAsync
            Try
                ' auto-create new lease id
                Dim leases = Await _leaseRepository.GetAll().ToListAsync()
                Dim defaultId = 0
                Dim newId = String.Empty

                If leases.Count = 0 Then
                    newId = "L" + 1.ToString("000000")
                Else
                    Do While newId = Nothing
                        defaultId += 1
                        Dim newIdCheck = "L" + defaultId.ToString("000000")
                        Dim q = True
                        For Each unit As Lease In leases
                            If (unit.Id = newIdCheck) Then
                                q = False
                                Exit For
                            End If
                        Next
                        If q Then newId = newIdCheck
                    Loop
                End If

                ' update rules
                Dim room = Await _roomRepository.GetOneAsync(roomId)
                Dim rules = Await _paramaterRepository.GetRulesAsync()
                Dim newLease = New Lease() _
                        With {.Id = newId, .CheckinDate = checkinDate, .ExpectedCheckoutDate =expectedCheckoutDate}
                newLease.CustomerCoefficient = 0
                newLease.ExtraCoefficient = rules.ExtraCoefficient
                newLease.RoomPrice = room.Category.Price

                ' add lease details of lease
                newLease.LeaseDetails = New List(Of LeaseDetail)
                newLease.Room = room
                room.State =1
                newLease.Paid = 0

                ' get new lease detail list id

                Dim defaultDetailId = 1
                Dim qu = True
                Dim leaseDetailsList = Await _leaseRepository.GetLeaseDetailsAsync()
                Dim idList = New List(Of String)
                For i = 0 To details.Count - 1
                    Dim newDetailId = String.Empty
                    If leaseDetailsList.Count = 0 And qu Then
                        idList.Add(1.ToString("0000000000"))
                        qu = False
                    Else
                        Do While newDetailId = Nothing
                            defaultDetailId += 1
                            Dim newIdCheck = defaultDetailId.ToString("0000000000")
                            Dim q = True
                            For Each unit As LeaseDetail In leaseDetailsList
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

                ' create new lease
                Dim z = - 1
                For Each detailModel As CreateLeaseDetailModel In details
                    z + = 1
                    ' get the customer type
                    Dim customerCategories = Await _leaseRepository.GetCustomerCategoriesAsync()
                    Dim customerCategory =
                            customerCategories.FirstOrDefault(
                                Function(p)p.Id = detailModel.CustomerCategoryId)

                    ' add lease detail to lease
                    newLease.LeaseDetails.Add(
                        New LeaseDetail() _
                                                 With {.Id = idList(z), .CustomerCategory = customerCategory, _ 
                                                 .Address=detailModel.CustomerAddress,
                                                 .CustomerName = detailModel.CustomerName,
                                                 .LicenseId = detailModel.CustomerLicenseId})
                Next

                ' init lease coefficient
                Dim coeff = 0
                For Each leaseDetail As LeaseDetail In newLease.LeaseDetails
                    If coeff<leaseDetail.CustomerCategory.Coefficient Then coeff = leaseDetail.CustomerCategory.Coefficient
                Next
                newLease.CustomerCoefficient = coeff

                ' add new lease to database
                _leaseRepository.Add(newLease)
                Await _unitOfWork.CommitAsync()
                Return newId
            Catch
                Return String.Empty
            End Try
        End Function
    End Class
End NameSpace