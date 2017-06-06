Imports Hotelie.Domain.Leases

Namespace Services.Persistence
    Public Interface ILeaseRepository
        Inherits IRepository(Of Lease)
        Function GetDetails As List(Of LeaseDetail)
        Function GetDetailsAsync As Task(Of List(Of LeaseDetail))
    End Interface
End NameSpace