Imports Hotelie.Domain.Bills

Namespace Services.Persistence
    Public Interface IBillRepository
        Inherits IRepository(Of Bill)
        Function GetBillDetails() As List(Of BillDetail)
        Function GetBillDetailsAsync() As Task(Of List(Of BillDetail))
    End Interface
End NameSpace