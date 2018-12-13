Imports Hotelie.Application.Services.Persistence

Namespace Leases.Queries.GetLeaseData
    Public Class GetLeaseDataQuery
        Implements IGetLeaseDataQuery

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute(id As String) As LeaseModel Implements IGetLeaseDataQuery.Execute
            Dim lease = _leaseRepository.GetOne(id)
            Return New LeaseModel(lease)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of LeaseModel) Implements IGetLeaseDataQuery.ExecuteAsync
            Dim lease = Await _leaseRepository.GetOneAsync(id)
            Return New LeaseModel(lease)
        End Function
    End Class
End NameSpace