Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries.GetLeaseData
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList

Namespace Leases.ViewModels
	Public Class ScreenLeaseDetailViewModel
		Inherits PropertyChangedBase

		' Dependencies
		Private ReadOnly _getLeaseDataQuery As IGetLeaseDataQuery
		Private _id As String
		Private _roomUnitPrice As Decimal
		Private _checkinDate As Date
		Private _room As SimpleRoomsListItemModel
		Private _expectedCheckoutDate As Date

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Public Property RoomUnitPrice As Decimal
			Get
				Return _roomUnitPrice
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _roomUnitPrice ) Then Return
				_roomUnitPrice = value
				NotifyOfPropertyChange( Function() RoomUnitPrice )
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
				NotifyOfPropertyChange( Function() NumberOfUsedDays )
			End Set
		End Property

		Public ReadOnly Property NumberOfUsedDays As Integer
			Get
				Return (Date.Now - CheckinDate).TotalDays
			End Get
		End Property

		Public Property Room As SimpleRoomsListItemModel
			Get
				Return _room
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _room ) Then Return
				_room = value
				NotifyOfPropertyChange( Function() Room )
			End Set
		End Property

		Public Property ExpectedCheckoutDate As Date
			Get
				Return _expectedCheckoutDate
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _expectedCheckoutDate ) Then Return
				_expectedCheckoutDate = value
				NotifyOfPropertyChange( Function() ExpectedCheckoutDate )
			End Set
		End Property

		Public Sub New( getLeaseDataQuery As IGetLeaseDataQuery )
			_getLeaseDataQuery = getLeaseDataQuery
		End Sub

		Public Sub Init()
		End Sub

		Public Async Function InitAsync() As Task
		End Function

		Public Sub SetLease( id As String )
			Dim model = _getLeaseDataQuery.Execute( id )
			If IsNothing( model ) Then Return

			id = id
			RoomUnitPrice = model.RoomPrice
			CheckinDate = model.CheckinDate
			ExpectedCheckoutDate = model.ExpectedCheckoutDate
		End Sub
	End Class
End Namespace
