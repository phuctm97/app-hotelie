Imports Hotelie.Domain.Leases

Namespace Services.Persistence
    Public Interface ILeaseRepository
        Inherits IRepository(Of Lease)
        Function GetLeaseDetails() As List(Of LeaseDetail)
        Function GetLeaseDetailsAsync() As Task(Of List(Of LeaseDetail))
        Function GetLeaseDetail(id As String) As LeaseDetail
        Function GetLeaseDetailAsync(id As String) As Task(Of LeaseDetail)
        Sub RemoveLeaseDetail(leaseDetail As LeaseDetail)
        Function GetCustomerCategories() As List(Of CustomerCategory)
        Function GetCustomerCategoriesAsync() As Task(Of List(Of CustomerCategory))
    End Interface
End NameSpace