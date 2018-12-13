Imports Hotelie.Domain.Leases

Namespace Leases.Queries.GetLeaseDetailData
    Public Interface IGetLeaseDetailData
        Function Execute(id As String) As LeaseDetailModel
        Function ExecuteAsync(id As String) As Task(Of LeaseDetailModel)
    End Interface
End NameSpace