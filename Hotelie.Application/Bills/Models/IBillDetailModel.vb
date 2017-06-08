Imports Hotelie.Application.Leases.Models

Namespace Bills.Models
	Public Interface IBillDetailModel
		ReadOnly Property Id As String

		ReadOnly Property Lease As ILeaseModel

		ReadOnly Property CheckinDate As Date

		ReadOnly Property NumberOfDays As Integer

		ReadOnly Property CheckoutDate As Date

		ReadOnly Property ExtraCharge As Decimal

		ReadOnly Property TotalExpense As Decimal
	End Interface
End Namespace