Imports Hotelie.Application.Leases.Models
Imports Hotelie.Application.Services.Persistence

Namespace Leases.Queries
    Public Class GetLeaseQuery
        Implements IGetLeaseQuery
            
        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute(id As String) As ILeaseModel Implements IGetLeaseQuery.Execute
            Dim lease = _leaseRepository.GetOne(id)
            Return New LeaseModel(lease)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of ILeaseModel) Implements IGetLeaseQuery.ExecuteAsync
            Dim lease = Await _leaseRepository.GetOneAsync(id)
            Return New LeaseModel(lease)
        End Function
    End Class
End NameSpace