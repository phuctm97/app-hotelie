Imports System.Data.Entity
Imports Hotelie.Application.Bills.Models
Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Bills

Namespace Bills.Queries
    Public Class GetAllBillsQuery
        Implements IGetAllBillsQuery

        Private ReadOnly _billRepository As IBillRepository

        Sub New(billRepository As IBillRepository)
            _billRepository = billRepository
        End Sub

        Public Function Execute() As IEnumerable(Of IBillModel) Implements IGetAllBillsQuery.Execute
            Dim bills = _billRepository.GetAll().ToList()
            Dim billModels = New List(Of BillModel)
            For Each bill As Bill In bills
                billModels.Add(New BillModel(bill))
            Next
            Return billModels
        End Function

        Public Async Function ExecuteAsync() As Task(Of IEnumerable(Of IBillModel)) Implements IGetAllBillsQuery.ExecuteAsync
            Dim bills = Await _billRepository.GetAll().ToListAsync()
            Dim billModels = New List(Of BillModel)
            For Each bill As Bill In bills
                billModels.Add(New BillModel(bill))
            Next
            Return billModels
        End Function
    End Class
End NameSpace