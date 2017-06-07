Namespace Bills.Queries.GetBillData
    Public Interface IGetBillDataQuery
        Function Execute(id As String) As BillModel

        Function ExecuteAsync(id As String) As Task(Of BillModel)
    End Interface
End NameSpace