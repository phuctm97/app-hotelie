Imports Hotelie.Domain.Users

Namespace Bills
    Public Class Bill
        Public Property Id As String
        Public Property CustomerName As String
        Public Property CustomerAddress As String
        Public Property Details As List(Of BillDetail)
        Public Property TotalExpense As Decimal
        Public Property User As User
    End Class
End NameSpace