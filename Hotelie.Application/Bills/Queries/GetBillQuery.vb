Imports Hotelie.Application.Bills.Models
Imports Hotelie.Application.Services.Persistence

Namespace Bills.Queries
    Public Class GetBillQuery
        Implements IGetBillQuery

        Private ReadOnly _billRepository As IBillRepository

        Sub New(billRepository As IBillRepository)
            _billRepository = billRepository
        End Sub

        Public Function Execute(id As String) As IBillModel Implements IGetBillQuery.Execute
            Dim bill = _billRepository.GetOne(id)
            Return New BillModel(bill)
        End Function

        Public Async Function ExecuteAsync(id As String) As Task(Of IBillModel) Implements IGetBillQuery.ExecuteAsync
            Dim bill = Await _billRepository.GetOneAsync(id)
            Return New BillModel(bill)
        End Function
    End Class
End NameSpace