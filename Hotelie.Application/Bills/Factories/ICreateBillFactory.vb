Imports Hotelie.Domain.Leases

Namespace Bills.Factories
    Public Interface ICreateBillFactory
        Function Execute(payerName As String, payerAddress As String, leases As List(Of String), totalExpense As Decimal, userId As String) As String
        Function ExecuteAsync(payerName As String, payerAddress As String, leases As List(Of String), totalExpense As Decimal, userId As String) As Task(Of String)
    End Interface
End NameSpace