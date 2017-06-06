Namespace Leases.Queries.GetLeasesList
	Public Interface IGetLeasesListQuery
		Function Execute() As IEnumerable(Of LeasesListItemModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of LeasesListItemModel))
	End Interface
End Namespace