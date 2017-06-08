Imports Hotelie.Application.Leases.Models
Imports Hotelie.Domain.Bills

Namespace Bills.Models
	Public Class BillDetailModel
		Implements IBillDetailModel

		Private ReadOnly _entity As BillDetail

		Sub New( entity As BillDetail )
			_entity = entity

			Lease = New LeaseModel( _entity.Lease )
		End Sub

		Public ReadOnly Property Id As String Implements IBillDetailModel.Id
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property Lease As ILeaseModel Implements IBillDetailModel.Lease

		Public ReadOnly Property CheckinDate As Date Implements IBillDetailModel.CheckinDate
			Get
				Return _entity.CheckinDate
			End Get
		End Property

		Public ReadOnly Property NumberOfDays As Integer Implements IBillDetailModel.NumberOfDays
			Get
				Return _entity.NumberOfDays
			End Get
		End Property

		Public ReadOnly Property CheckoutDate As Date Implements IBillDetailModel.CheckoutDate
			Get
				Return CheckinDate.Add( TimeSpan.FromDays( NumberOfDays ) )
			End Get
		End Property

		Public ReadOnly Property ExtraCharge As Decimal Implements IBillDetailModel.ExtraCharge
			Get
				Return _entity.ExtraCharge
			End Get
		End Property

		Public ReadOnly Property TotalExpense As Decimal Implements IBillDetailModel.TotalExpense
			Get
				Return _entity.TotalExpense
			End Get
		End Property
	End Class
End Namespace