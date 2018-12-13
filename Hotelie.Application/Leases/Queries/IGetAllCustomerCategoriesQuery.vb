Imports Hotelie.Application.Leases.Models

Namespace Leases.Queries
	Public Interface IGetAllCustomerCategoriesQuery
		Function Execute() As IEnumerable(Of ICustomerCategoryModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of ICustomerCategoryModel))
	End Interface
End Namespace