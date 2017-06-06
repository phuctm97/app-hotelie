Namespace Leases.Queries.GetLeasesList
    Public Interface IGetLeasesListQuery
        Function Execute() As IEnumerable(Of LeasesListIemModel)
        Function ExecuteAsync() As Task(Of IEnumerable(Of LeasesListIemModel))
    End Interface
End NameSpace