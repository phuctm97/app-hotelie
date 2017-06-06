Namespace Bills
    Public Class Bill
        Public Property Id As String
        Public Property CustomerName As String
        Public Property CustomerAddress As String
        Public Property Details As List(Of BillDetail)
        Public Property TotalExpense As Decimal
    End Class
End NameSpace