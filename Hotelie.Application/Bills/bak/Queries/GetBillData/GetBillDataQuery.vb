Imports Hotelie.Application.Services.Persistence

Namespace Bills.Queries.GetBillData
    Public Class GetBillDataQuery
        Implements IGetBillDataQuery

        Private ReadOnly _billRepository As IBillRepository

        Sub New(billRepository As IBillRepository)
            _billRepository = billRepository
        End Sub

        Public Function Execute(id As String) As BillModel Implements IGetBillDataQuery.Execute
            Dim bill = _billRepository.GetOne(id)
            Return New BillModel(bill)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of BillModel) Implements IGetBillDataQuery.ExecuteAsync
            Dim bill = Await _billRepository.GetOneAsync(id)
            Return New BillModel(bill)
        End Function
    End Class
End NameSpace