Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries.GetSimpleLeasesList
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Presentation.Bills.ViewModels

Namespace Bills.Models
	Public Class EditableBillDetailModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _room As SimpleRoomsListItemModel
		Private _lease As SimpleLeasesListItemModel

		Private ReadOnly _rooms As IObservableCollection(Of SimpleRoomsListItemModel)
		Private ReadOnly _leases As IObservableCollection(Of SimpleLeasesListItemModel)

		Public Sub New()
			_rooms = ScreenAddBillViewModel.Rooms
			_leases = ScreenAddBillViewModel.Leases
			_id = String.Empty
			_room = Nothing
			_lease = Nothing
		End Sub

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

		Public Property Room As SimpleRoomsListItemModel
			Get
				Return _room
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _room ) Then Return
				_room = value
				NotifyOfPropertyChange( Function() Room )
				UpdateBaseOnRoom()
			End Set
		End Property

		Public Property Lease As SimpleLeasesListItemModel
			Get
				Return _lease
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _lease ) Then Return
				_lease = value
				UpdateBaseOnLease()
				NotifyOfPropertyChange( Function() Lease )
				NotifyOfPropertyChange( Function() ExtraCharge )
				NotifyOfPropertyChange( Function() TotalExpense )
				NotifyOfPropertyChange( Function() NumberOfUsedDays )
				NotifyOfPropertyChange( Function() UnitPrice )
			End Set
		End Property

		Public ReadOnly Property NumberOfUsedDays As Integer
			Get
				If IsNothing( _lease ) Then Return 0
				Return Today.Subtract( _lease.CheckinDate ).TotalDays
			End Get
		End Property

		Public ReadOnly Property ExtraCharge As Decimal
			Get
				If _lease Is Nothing Then Return 0
				Return _lease.ExtraCharge
			End Get
		End Property

		Public ReadOnly Property TotalExpense As Decimal
			Get
				If _lease Is Nothing Then Return 0
				Return _lease.TotalExpense
			End Get
		End Property

		Public ReadOnly Property UnitPrice As Decimal
			Get
				If _lease Is Nothing Then Return 0
				Return _lease.UnitPrice
			End Get
		End Property

		Private Sub UpdateBaseOnLease()
			If _lease Is Nothing Then Return
			If _room IsNot Nothing AndAlso _lease.RoomId = _room.Id Then Return

			_room = _rooms.FirstOrDefault( Function( r ) r.Id = _lease.RoomId )
			NotifyOfPropertyChange( Function() Room )
		End Sub

		Private Sub UpdateBaseOnRoom()
			If _room Is Nothing Then Return
			If _lease IsNot Nothing AndAlso _room.Id = _lease.Id Then Return

			_lease = _leases.FirstOrDefault( Function( l ) l.RoomId = _room.Id )
			NotifyOfPropertyChange( Function() Lease )
			NotifyOfPropertyChange( Function() Lease )
			NotifyOfPropertyChange( Function() ExtraCharge )
			NotifyOfPropertyChange( Function() TotalExpense )
			NotifyOfPropertyChange( Function() NumberOfUsedDays )
			NotifyOfPropertyChange( Function() UnitPrice )
		End Sub
	End Class
End Namespace