Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Models
Imports Hotelie.Application.Rooms.Models

Namespace Leases.Models
	Public Class UpdatableLeaseModel
		Inherits PropertyChangedBase
		Implements ILeaseModel

		Private ReadOnly _baseModel As ILeaseModel

		Private _backingRoomModel As IRoomModel
		Private _isFiltersMatch As Boolean

		Sub New( baseModel As ILeaseModel )
			_baseModel = baseModel
			_backingRoomModel = _baseModel.Room
		End Sub

		Public Property IsFiltersMatch As Boolean
			Get
				Return _isFiltersMatch
			End Get
			Set
				If Equals( Value, _isFiltersMatch ) Then Return
				_isFiltersMatch = value
				NotifyOfPropertyChange( Function() IsFiltersMatch )
			End Set
		End Property

		Public ReadOnly Property CheckinDate As DateTime Implements ILeaseModel.CheckinDate
			Get
				Return _baseModel.CheckinDate
			End Get
		End Property

		Public ReadOnly Property CustomerCoefficient As Double Implements ILeaseModel.CustomerCoefficient
			Get
				Return _baseModel.CustomerCoefficient
			End Get
		End Property

		Public ReadOnly Property Details As List(Of ILeaseDetailModel) Implements ILeaseModel.Details
			Get
				Return _baseModel.Details
			End Get
		End Property

		Public ReadOnly Property ExpectedCheckoutDate As DateTime Implements ILeaseModel.ExpectedCheckoutDate
			Get
				Return _baseModel.ExpectedCheckoutDate
			End Get
		End Property

		Public ReadOnly Property ExtraCoefficient As Double Implements ILeaseModel.ExtraCoefficient
			Get
				Return _baseModel.ExtraCoefficient
			End Get
		End Property

		Public ReadOnly Property Id As String Implements ILeaseModel.Id
			Get
				Return _baseModel.Id
			End Get
		End Property

		Public Property Room As IRoomModel Implements ILeaseModel.Room
			Get
				Return _backingRoomModel
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _backingRoomModel ) Then Return
				_backingRoomModel = Value
				NotifyOfPropertyChange( Function() Room )
			End Set
		End Property

		Public ReadOnly Property RoomUnitPrice As Decimal Implements ILeaseModel.RoomUnitPrice
			Get
				Return _baseModel.RoomUnitPrice
			End Get
		End Property

		Public ReadOnly Property NumberOfUsedDays As Int32 Implements ILeaseModel.NumberOfUsedDays
			Get
				Return _baseModel.NumberOfUsedDays
			End Get
		End Property

		Public ReadOnly Property IdEx As String Implements ILeaseModel.IdEx
			Get
				Return $"#{Id}"
			End Get
		End Property

		Public ReadOnly Property ExtraCharge As Decimal Implements ILeaseModel.ExtraCharge
			Get
				Return _baseModel.ExtraCharge
			End Get
		End Property

		Public ReadOnly Property TotalExpense As Decimal Implements ILeaseModel.TotalExpense
			Get
				Return _baseModel.TotalExpense
			End Get
		End Property

		Public ReadOnly Property IsPaid As Boolean Implements ILeaseModel.IsPaid
			Get
				Return _baseModel.IsPaid
			End Get
		End Property
	End Class
End Namespace
