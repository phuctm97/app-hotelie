Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Models
Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Presentation.Bills.ViewModels

Namespace Bills.Models
	Public Class EditableBillDetailModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _lease As ILeaseModel
		Private _numberOfDays As Integer
		Private _extraCharge As Decimal
		Private _totalExpense As Decimal
		Private _room As IRoomModel

		Public Property Id As String
			Get
				Return $"{_id}"
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Public Property Lease As ILeaseModel
			Get
				Return _lease
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _lease ) Then Return
				_lease = value
				NotifyOfPropertyChange( Function() Lease )
				UpdateDataBaseOnLease()
			End Set
		End Property

		Public Property Room As IRoomModel
			Get
				Return _room
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _room ) Then Return
				_room = value
				NotifyOfPropertyChange( Function() Room )
				UpdateDataBaseOnRoom()
			End Set
		End Property

		Public Property NumberOfDays As Integer
			Get
				Return _numberOfDays
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _numberOfDays ) Then Return
				_numberOfDays = value
				NotifyOfPropertyChange( Function() NumberOfDays )
			End Set
		End Property

		Public Property ExtraCharge As Decimal
			Get
				Return _extraCharge
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _extraCharge ) Then Return
				_extraCharge = value
				NotifyOfPropertyChange( Function() ExtraCharge )
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

		Public Sub New()
			Id = String.Empty
			NumberOfDays = 0
			ExtraCharge = 0
			TotalExpense = 0
			Lease = Nothing
			Room = Nothing
		End Sub

		Private Sub UpdateDataBaseOnLease()
			If IsNothing( Lease ) OrElse IsNothing( Lease.Room ) Then Return

			Dim roomToUpdate = ScreenAddBillViewModel.Rooms.FirstOrDefault( Function( r ) r.Id = Lease.Room.Id )
			If IsNothing( roomToUpdate )
				_lease = Nothing
				NotifyOfPropertyChange( Function() Lease )
				Return
			End If

			_room = roomToUpdate
			NotifyOfPropertyChange( Function() Room )

			NumberOfDays = Lease.NumberOfUsedDays
			ExtraCharge = Lease.ExtraCharge
			TotalExpense = Lease.TotalExpense
		End Sub

		Private Sub UpdateDataBaseOnRoom()
			If IsNothing( Room ) Then Return

			Dim leaseToUpdate = ScreenAddBillViewModel.Leases.FirstOrDefault( Function( l ) l.Room.Id = Room.Id )
			If IsNothing( leaseToUpdate )
				_room = Nothing
				NotifyOfPropertyChange( Function() Room )
				Return
			End If

			_lease = leaseToUpdate
			NotifyOfPropertyChange( Function() Lease )

			NumberOfDays = Lease.NumberOfUsedDays
			ExtraCharge = Lease.ExtraCharge
			TotalExpense = Lease.TotalExpense
		End Sub
	End Class
End Namespace