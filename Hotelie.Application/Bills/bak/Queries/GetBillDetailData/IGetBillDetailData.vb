Namespace Bills.Queries.GetBillDetailData
    Public Interface IGetBillDetailData
        Function Execute(id As String) As BillDetailModel
        Function ExecuteAsync(id As String) As Task(Of BillDetailModel)
    End Interface
End NameSpace