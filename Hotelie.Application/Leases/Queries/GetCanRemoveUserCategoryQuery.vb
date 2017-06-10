Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Queries
    Public Class GetCanRemoveUserCategoryQuery
        Implements IGetCanRemoveUserCategoryQuery

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute(id As String) As Boolean Implements IGetCanRemoveUserCategoryQuery.Execute
            Dim category = _leaseRepository.GetCustomerCategories().FirstOrDefault(Function(p)p.Id = id)
            Dim leases = _leaseRepository.GetAll().Where(Function(p)p.Paid = 0)
            For Each lease As Lease In leases
                For Each leaseDetail As LeaseDetail In lease.LeaseDetails
                    If leaseDetail.CustomerCategory.Id = category.Id Then Return False
                Next
            Next
            Return True
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of Boolean) _
            Implements IGetCanRemoveUserCategoryQuery.ExecuteAsync
            Dim categories = Await _leaseRepository.GetCustomerCategoriesAsync()
            Dim category = categories.FirstOrDefault(Function(p)p.Id = id)
            Dim leases = _leaseRepository.GetAll().Where(Function(p)p.Paid = 0)
            For Each lease As Lease In leases
                For Each leaseDetail As LeaseDetail In lease.LeaseDetails
                    If leaseDetail.CustomerCategory.Id = category.Id Then Return False
                Next
            Next
            Return True
        End Function
    End Class
End NameSpace