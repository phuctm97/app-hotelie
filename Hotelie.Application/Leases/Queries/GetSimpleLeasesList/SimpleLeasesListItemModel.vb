Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries.GetLeasesList

Namespace Leases.Queries.GetSimpleLeasesList
	Public Class SimpleLeasesListItemModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _roomId As String
		Private _checkinDate As Date
		Private _totalExpense As Decimal
		Private _extraCharge As Decimal
		Private _unitPrice As Decimal

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
				NotifyOfPropertyChange( Function() IdEx )
			End Set
		End Property

		Public ReadOnly Property IdEx As String
		Get
				Return $"#{Id}"
		End Get
		End Property

		Public Property RoomId As String
			Get
				Return _roomId
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _roomId ) Then Return
				_roomId = value
				NotifyOfPropertyChange( Function() RoomId )
			End Set
		End Property

		Public Property CheckinDate As Date
			Get
				Return _checkinDate
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _checkinDate ) Then Return
				_checkinDate = value
				NotifyOfPropertyChange( Function() CheckinDate )
			End Set
		End Property

		Public Property ExtraCharge As Decimal
			Get
				Return _extraCharge
			End Get
			Set
				If IsNothing(Value) OrElse Equals(Value, _extraCharge) Then Return
				_extraCharge = value
				NotifyOfPropertyChange(Function() ExtraCharge)
			End Set
		End Property

		Public Property TotalExpense As Decimal
			Get
				Return _totalExpense
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _totalExpense ) Then Return
				_totalExpense = value
				NotifyOfPropertyChange( Function() TotalExpense )
			End Set
		End Property

		Public Property UnitPrice As Decimal
			Get
				Return _unitPrice
			End Get
			Set
				If IsNothing(Value) OrElse Equals(Value, _unitPrice) Then Return
				_unitPrice = value
				NotifyOfPropertyChange(Function() UnitPrice)
			End Set
		End Property

		Public Sub New()
			Id = String.Empty
			RoomId = String.Empty
			CheckinDate = Date.Now
			ExtraCharge = 0
			TotalExpense = 0
			UnitPrice = 0
		End Sub
	End Class
End Namespace