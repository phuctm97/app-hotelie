Imports Hotelie.Application.Services.Persistence

Namespace Leases.Commands.UpdateLeaseDetail
    Public Class UpdateLeaseDetailCommand
        Implements IUpdateLeaseDetailCommand

        Private ReadOnly _leaseRepository As ILeaseRepository
        Private ReadOnly _unitOfWork As IUnitOfWork

        Sub New(unitOfWork As IUnitOfWork, leaseRepository As ILeaseRepository)
            _unitOfWork = unitOfWork
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute(id As String, customerName As String, customerLicenseId As String, customerAddress As String, customerCategoryId As String) As String Implements IUpdateLeaseDetailCommand.Execute
            Try
                Dim leaseDetail = _leaseRepository.GetLeaseDetail(id)
                Dim customerCategory = _leaseRepository.GetCustomerCategories().FirstOrDefault(Function(p)p.Id = customerCategoryId)
                leaseDetail.CustomerCategory = customerCategory
                leaseDetail.CustomerName = customerName
                leaseDetail.Address = customerAddress
                leaseDetail.LicenseId = customerLicenseId
                _unitOfWork.Commit()
                Return String.Empty
            Catch 
                Return "Không thể chỉnh sửa chi tiết phiếu thuê phòng"
            End Try
        End Function

        Public Async Function ExecuteAsync(id As String, customerName As String, customerLicenseId As String, customerAddress As String, customerCategoryId As String) As Task(Of String) Implements IUpdateLeaseDetailCommand.ExecuteAsync
            Try
                Dim leaseDetail = Await _leaseRepository.GetLeaseDetailAsync(id)
                Dim customerCategories =Await  _leaseRepository.GetCustomerCategoriesAsync()
                Dim customerCategory = customerCategories.FirstOrDefault(Function(p)p.Id=customerCategoryId)

                leaseDetail.CustomerCategory = customerCategory
                leaseDetail.CustomerName = customerName
                leaseDetail.Address = customerAddress
                leaseDetail.LicenseId = customerLicenseId
                Await _unitOfWork.CommitAsync()
                Return String.Empty
            Catch 
                Return "Không thể chỉnh sửa chi tiết phiếu thuê phòng"
            End Try
        End Function
    End Class
End NameSpace