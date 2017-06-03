Imports Hotelie.Domain.Leases

Namespace Services.Persistence
    Public Interface ILeaseRepository
        Inherits IRepository(Of Lease)
        Function GetCustomers() As List(Of LeaseDetail)
    End Interface
End NameSpace