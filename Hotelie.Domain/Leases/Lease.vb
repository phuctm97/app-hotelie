Imports Hotelie.Domain.Rooms

Namespace Leases
    Public Class Lease
        Public Property Id As String
        Public Property Room As Room
        Public Property CheckinDate As Date
        Public Property ExpectedCheckoutDate As Date
        Public Property RoomPrice As Decimal
        Public Property ExtraCoefficient As Double
        Public Property CustomerCoefficient As Double
        Public Property LeaseDetails As List(Of LeaseDetail)

        Public Function CalculateExpense() As Decimal
            Dim numberOfDays = DateTime.Now().Subtract(CheckinDate).Days()
            Dim unitPrice = RoomPrice*ExtraCoefficient*numberOfDays
            Dim expense = RoomPrice*(1 + CustomerCoefficient)*numberOfDays

            Return unitPrice+expense
        End Function
    End Class
End Namespace
