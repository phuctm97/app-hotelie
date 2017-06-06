Imports Hotelie.Application.Leases.Queries.GetCustomerCategoriesList
Imports Hotelie.Domain.Leases

Namespace Tests.Leases.Queries.GetCustomerCategoriesList
	Public Class GetCustomerCategoriesListQuery
		Implements IGetCustomerCategoriesListQuery

		Public Function Execute() As IEnumerable(Of CustomerCategoriesListItemModel) _
			Implements IGetCustomerCategoriesListQuery.Execute
			Dim list = New List(Of CustomerCategoriesListItemModel)

			For Each customerCategory As CustomerCategory In LeaseRepositoryTest.CustomerCategories
				list.Add( New CustomerCategoriesListItemModel With {.Id=customerCategory.Id,
					        .Name=customerCategory.Name,
					        .Coefficient=customerCategory.Coefficient} )
			Next

			Return list
		End Function

		Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of CustomerCategoriesListItemModel)) _
			Implements IGetCustomerCategoriesListQuery.ExecuteAsync
			Return Await Task.Run( Function() Execute() )
		End Function
	End Class
End Namespace