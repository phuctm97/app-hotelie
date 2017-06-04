Namespace Leases.Queries
    Public Interface IGetLeasesListQuery
        Function Execute() As IEnumerable(Of LeaseModel)
        Function ExecuteAsync() As Task(Of IEnumerable(Of LeaseModel))
    End Interface
End NameSpace