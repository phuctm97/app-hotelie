Imports Hotelie.Application.Services.Persistence

Namespace Leases.Queries.GetCustomerCategoriesList
    Public Class GetCustomerCategoriesListQuery
        Implements IGetCustomerCategoriesListQuery

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute() As IEnumerable(Of CustomerCategoriesListItemModel) Implements IGetCustomerCategoriesListQuery.Execute
            Dim categories = _leaseRepository.GetCustomerCategories().Select(Function(r) New CustomerCategoriesListItemModel() With _
                                                                             {.Id = r.Id,
                                                                             .Name = r.Name,
                                                                             .Coefficient  = r.Coefficient}).ToList()
            Return categories
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of CustomerCategoriesListItemModel)) Implements IGetCustomerCategoriesListQuery.ExecuteAsync
            Dim categoriesList = Await _leaseRepository.GetCustomerCategoriesAsync()
            Dim categoriesModel = categoriesList.Select(Function(r) New CustomerCategoriesListItemModel() With _
                       {.Id = r.Id,
                       .Name = r.Name,
                       .Coefficient  = r.Coefficient}).ToList()

            Return categoriesModel
        End Function
    End Class
End NameSpace