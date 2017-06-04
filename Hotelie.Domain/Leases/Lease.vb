Imports Hotelie.Domain.Rooms

Namespace Leases
    Public Class Lease
        Public Property Id As String
        Public Property Room As Room
        Public Property BeginDate As Date
        Public Property EndDate As Date
        Public Property Price As Decimal
        Public Property NumberOfDate As Integer
        Public Property ExtraCoefficient As Double
        Public Property CustomerCoefficient As Integer
        Public Property ExtraCharge As Decimal
        Public Property TotalPrice As Decimal
        Public Property Bill As Bill
        Public Property Customers As List(Of LeaseDetail)
    End Class
End Namespace
