Imports Hotelie.Application.Services.Persistence

Namespace Leases.Queries.GetCustomerCategoryData
    Public Class GetCustomerCategoryData
        Implements IGetCustomerCategoryData

        Private ReadOnly _leaseRepository As ILeaseRepository

        Sub New(leaseRepository As ILeaseRepository)
            _leaseRepository = leaseRepository
        End Sub

        Public Function Execute(id As String) As CustomerCategoryModel Implements IGetCustomerCategoryData.Execute
            Dim categories  = _leaseRepository.GetCustomerCategories()
            Dim category = categories.FirstOrDefault(Function (p) p.Id = id)
            Return New CustomerCategoryModel(category)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of CustomerCategoryModel) Implements IGetCustomerCategoryData.ExecuteAsync
            Dim categories  = Await _leaseRepository.GetCustomerCategoriesAsync()
            Dim category = categories.FirstOrDefault(Function(p)p.Id = id)
            Return New CustomerCategoryModel(category)
        End Function
    End Class
End NameSpace