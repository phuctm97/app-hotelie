Namespace Bills.Queries.GetBillsList
    Public Interface IGetBillsListQuery
        Function Execute() As IEnumerable(Of BillsListItemModel)
        Function ExecuteAsync() As Task(Of IEnumerable(Of BillsListItemModel))
    End Interface
End NameSpace