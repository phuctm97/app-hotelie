Imports Hotelie.Application.Leases.Models
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Leases

Namespace Leases.Queries
    Public Class GetAllCustomerCategoriesQuery
        Implements IGetAllCustomerCategoriesQuery

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute() As IEnumerable(Of ICustomerCategoryModel) Implements IGetAllCustomerCategoriesQuery.Execute
            Dim categories = _leaseRepository.GetCustomerCategories()
            Dim categoryModels = New List(Of CustomerCategoryModel)
            For Each customerCategory As CustomerCategory In categories
                categoryModels.Add(New CustomerCategoryModel(customerCategory))
            Next
            Return categoryModels
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of ICustomerCategoryModel)) Implements IGetAllCustomerCategoriesQuery.ExecuteAsync
            Dim categories = Await _leaseRepository.GetCustomerCategoriesAsync()
            Dim categoryModels = New List(Of CustomerCategoryModel)
            For Each customerCategory As CustomerCategory In categories
                categoryModels.Add(New CustomerCategoryModel(customerCategory))
            Next
            Return categoryModels
        End Function
    End Class
End NameSpace