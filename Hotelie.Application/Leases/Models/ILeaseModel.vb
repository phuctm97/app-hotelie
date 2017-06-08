Namespace Leases.Models
	Public Interface ILeaseModel
		ReadOnly Property IdEx As String

		ReadOnly Property Id As String

		ReadOnly Property Room As Rooms.Models.IRoomModel

		ReadOnly Property CheckinDate As Date

		ReadOnly Property NumberOfUsedDays As Integer

		ReadOnly Property ExpectedCheckoutDate As Date

		ReadOnly Property RoomUnitPrice As Decimal

		ReadOnly Property ExtraCoefficient As Double

		ReadOnly Property CustomerCoefficient As Double

		ReadOnly Property ExtraCharge As Decimal

		ReadOnly Property TotalExpense As Decimal

		ReadOnly Property IsPaid As Boolean

		ReadOnly Property Details As List(Of ILeaseDetailModel)
	End Interface
End Namespace