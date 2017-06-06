Imports Hotelie.Application.Services.Persistence

Namespace Leases.Queries.GetLeaseDetailData
    Public Class GetLeaseDetailData
        Implements IGetLeaseDetailData
        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute(id As String) As LeaseDetailModel Implements IGetLeaseDetailData.Execute
            Dim detail = _leaseRepository.GetLeaseDetails().FirstOrDefault(Function (p) p.Id = id)
            Return New LeaseDetailModel(detail)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of LeaseDetailModel) Implements IGetLeaseDetailData.ExecuteAsync
            Dim details = Await _leaseRepository.GetLeaseDetailsAsync()
            Dim detail  = details.FirstOrDefault(Function (p) p.Id = id)
            Return New LeaseDetailModel(detail)
        End Function
    End Class
End NameSpace