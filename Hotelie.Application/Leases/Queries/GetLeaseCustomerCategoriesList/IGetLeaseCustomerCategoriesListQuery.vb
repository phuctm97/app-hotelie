Namespace Leases.Queries.GetLeaseCustomerCategoriesList
	Public Interface IGetLeaseCustomerCategoriesListQuery
		Function Execute() As IEnumerable(Of LeaseCustomerCategoryListItemModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of LeaseCustomerCategoryListItemModel))
	End Interface
End Namespace