﻿Imports Hotelie.Domain.Leases

Namespace Services.Persistence
    Public Interface ILeaseRepository
        Inherits IRepository(Of Lease)
        Function GetLeaseDetails() As List(Of LeaseDetail)
        Function GetLeaseDetailsAsync() As Task(Of List(Of LeaseDetail))
    End Interface
End NameSpace