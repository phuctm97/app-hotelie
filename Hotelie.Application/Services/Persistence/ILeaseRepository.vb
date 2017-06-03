Imports Hotelie.Domain.Leases

Namespace Services.Persistence
    Public Interface ILeaseRepository
        Inherits IRepository(Of Lease)
        Function GetCustomers(id As String) As List(Of LeaseDetail)
    End Interface
End NameSpace