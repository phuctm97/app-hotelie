Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Factories
    Public Class CreateLeaseDetailFactory
        Implements ICreateLeaseDetailFactory

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(leaseRepository As ILeaseRepository, unitOfWork As IUnitOfWork)
            _leaseRepository = leaseRepository
            _unitOfWork = unitOfWork
        End Sub

        Public Function Execute(leaseId As String, customerName As String, customerLicenseId As String,
                                customerAddress As String, customerCategoryId As String) As String _
            Implements ICreateLeaseDetailFactory.Execute
            Try
                ' auto-create new Id
                Dim leaseDetails = _leaseRepository.GetLeaseDetails().ToList()
                Dim defaultId = 0
                Dim newId = String.Empty

                If leaseDetails.Count = 0 Then
                    newId = 1.ToString("0000000000")
                Else
                    Do While newId = Nothing
                        defaultId += 1
                        Dim newIdCheck = defaultId.ToString("0000000000")
                        Dim q = True
                        For Each unit As LeaseDetail In leaseDetails
                            If (unit.Id = newIdCheck) Then
                                q = False
                                Exit For
                            End If
                        Next
                        If q Then newId = newIdCheck
                    Loop
                End If

                Dim customerCategory =
                        _leaseRepository.GetCustomerCategories().FirstOrDefault(Function (p) p.Id = customerCategoryId)

                Dim newDetail = New LeaseDetail() With {
                        .Id = newId, .Address = customerAddress, .CustomerName=customerName, _
                        .CustomerCategory = customerCategory, .LicenseId = customerLicenseId}

                Dim lease = _leaseRepository.GetOne(leaseId)
                lease.LeaseDetails.Add(newDetail)

                _unitOfWork.Commit()
                Return newId
            Catch
                Return String.Empty
            End Try
        End Function

        Public Async Function ExecuteAsync(leaseId As String, customerName As String, customerLicenseId As String,
                                           customerAddress As String, customerCategoryId As String) As Task(Of String) _
            Implements ICreateLeaseDetailFactory.ExecuteAsync
            Try
                ' auto-create new Id
                Dim leaseDetails = Await _leaseRepository.GetLeaseDetailsAsync()
                Dim defaultId = 0
                Dim newId = String.Empty

                If leaseDetails.Count = 0 Then
                    newId = 1.ToString("0000000000")
                Else
                    Do While newId = Nothing
                        defaultId += 1
                        Dim newIdCheck = defaultId.ToString("0000000000")
                        Dim q = True
                        For Each unit As LeaseDetail In leaseDetails
                            If (unit.Id = newIdCheck) Then
                                q = False
                                Exit For
                            End If
                        Next
                        If q Then newId = newIdCheck
                    Loop
                End If

                Dim customerCategories =  Await _leaseRepository.GetCustomerCategoriesAsync()
                Dim customerCategory =
                        customerCategories.FirstOrDefault(Function (p) p.Id = customerCategoryId)

                Dim newDetail = New LeaseDetail() With {
                        .Id = newId, .Address = customerAddress, .CustomerName=customerName, _
                        .CustomerCategory = customerCategory, .LicenseId = customerLicenseId}

                Dim lease = Await _leaseRepository.GetOneAsync(leaseId)
                lease.LeaseDetails.Add(newDetail)

                _unitOfWork.Commit()
                Return newId
            Catch
                Return String.Empty
            End Try
        End Function
    End Class
End NameSpace