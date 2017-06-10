Namespace Leases.Queries
    Public Interface IGetCanRemoveUserCategoryQuery
        Function Execute(id As String) As Boolean
        Function ExecuteAsync(id As String) As Task(Of Boolean)
    End Interface
End NameSpace