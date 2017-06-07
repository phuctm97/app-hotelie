Imports Hotelie.Application.Leases.Queries.GetLeaseData
Imports Hotelie.Domain.Bills

Namespace Bills.Models
	Public Class BillDetailModel
		Private ReadOnly _entity As BillDetail

		Sub New( entity As BillDetail )
			_entity = entity

			Lease = New LeaseModel( _entity.Lease )
		End Sub

		Public ReadOnly Property Id As String
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property Lease As LeaseModel

		Public ReadOnly Property CheckinDate As Date
			Get
				Return _entity.CheckinDate
			End Get
		End Property

		Public ReadOnly Property NumberOfDays As Integer
			Get
				Return _entity.NumberOfDays
			End Get
		End Property

		Public ReadOnly Property CheckoutDate As Date
			Get
				Return CheckinDate.Add( TimeSpan.FromDays( NumberOfDays ) )
			End Get
		End Property

		Public ReadOnly Property ExtraCharge As Decimal
			Get
				Return _entity.ExtraCharge
			End Get
		End Property

		Public ReadOnly Property TotalExpense As Decimal
			Get
				Return _entity.TotalExpense
			End Get
		End Property
	End Class
End Namespace