Namespace Leases.Queries.GetSimpleLeasesList
	Public Interface IGetSimpleLeasesListQuery
		Function Execute() As IEnumerable(Of SimpleLeasesListItemModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of SimpleLeasesListItemModel))
	End Interface
End Namespace