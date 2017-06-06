Namespace Leases.Queries.GetCustomerCategoriesList
	Public Interface IGetCustomerCategoriesListQuery
		Function Execute() As IEnumerable(Of CustomerCategoriesListItemModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of CustomerCategoriesListItemModel))
	End Interface
End Namespace