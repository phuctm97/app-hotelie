Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Models

Namespace Leases.Models
	Public Class EditableLeaseModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _room As IRoomModel
		Private _checkinDate As Date
		Private _expectedCheckoutDate As Date
		Private _roomUnitPrice As Decimal
		Private _extraCoefficent As Double
		Private _customerCoefficient As Double

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Property Room As IRoomModel
			Get
				Return _room
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _room ) Then Return
				_room = value
				NotifyOfPropertyChange( Function() Room )
			End Set
		End Property

		Property CheckinDate As Date
			Get
				Return _checkinDate
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _checkinDate ) Then Return
				_checkinDate = value
				NotifyOfPropertyChange( Function() CheckinDate )
				NotifyOfPropertyChange( Function() NumberOfUsedDays )
			End Set
		End Property

		ReadOnly Property NumberOfUsedDays As Integer
			Get
				Return Today.Subtract( CheckinDate ).TotalDays
			End Get
		End Property

		Property ExpectedCheckoutDate As Date
			Get
				Return _expectedCheckoutDate
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _expectedCheckoutDate ) Then Return
				_expectedCheckoutDate = value
				NotifyOfPropertyChange( Function() ExpectedCheckoutDate )
			End Set
		End Property

		Property RoomUnitPrice As Decimal
			Get
				Return _roomUnitPrice
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _roomUnitPrice ) Then Return
				_roomUnitPrice = value
				NotifyOfPropertyChange( Function() RoomUnitPrice )
			End Set
		End Property

		Property ExtraCoefficient As Double
			Get
				Return _extraCoefficent
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _extraCoefficent ) Then Return
				_extraCoefficent = value
				NotifyOfPropertyChange( Function() ExtraCoefficient )
			End Set
		End Property

		Property CustomerCoefficient As Double
			Get
				Return _customerCoefficient
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _customerCoefficient ) Then Return
				_customerCoefficient = value
				NotifyOfPropertyChange( Function() CustomerCoefficient )
			End Set
		End Property

		ReadOnly Property Details As IObservableCollection(Of EditableLeaseDetailModel)

		Public Sub New()
			_id = String.Empty
			_room = Nothing
			_checkinDate = Today
			_expectedCheckoutDate = Today
			_roomUnitPrice = 0
			_extraCoefficent = 0
			_customerCoefficient = 0
			Details = New BindableCollection(Of EditableLeaseDetailModel)()
		End Sub
	End Class
End Namespace