Imports Hotelie.Domain.Leases

Namespace Bills
    Public Class BillDetail
        Public Property Id As String

        Public Property Lease As Lease

        Public Property Bill As Bill

        Public Property CheckinDate As Date

        Public Property NumberOfDays As Integer

        Public Property ExtraCharge As Decimal

        Public Property TotalExpense As Decimal
    End Class
End NameSpace