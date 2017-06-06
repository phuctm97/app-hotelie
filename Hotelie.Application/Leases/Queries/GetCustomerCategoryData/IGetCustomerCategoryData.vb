Namespace Leases.Queries.GetCustomerCategoryData
    Public Interface IGetCustomerCategoryData
        Function Execute(id As String) As CustomerCategoryModel
        Function ExecuteAsync(id As String) As Task(Of CustomerCategoryModel)
    End Interface
End NameSpace