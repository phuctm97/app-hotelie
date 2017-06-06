Imports Caliburn.Micro

Namespace Leases.Queries.GetLeasesList
	Public Class LeasesListIemModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _roomName As String
		Private _roomCategoryName As String
		Private _checkinDate As Date
		Private _expectedCheckoutDate As Date
		Private _unitPrice As Decimal
		Private _totalExpense As Decimal
		Private _details As IObservableCollection(Of LeasesListItemDetailModel)

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

		Public Property RoomName As String
			Get
				Return _roomName
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _roomName ) Then Return
				_roomName = value
				NotifyOfPropertyChange( Function() RoomName )
			End Set
		End Property

		Public Property RoomCategoryName As String
			Get
				Return _roomCategoryName
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _roomCategoryName ) Then Return
				_roomCategoryName = value
				NotifyOfPropertyChange( Function() RoomCategoryName )
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

		Public Property ExpectedCheckoutDate As Date
			Get
				Return _expectedCheckoutDate
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _expectedCheckoutDate ) Then Return
				_expectedCheckoutDate = value
				NotifyOfPropertyChange( Function() ExpectedCheckoutDate )
			End Set
		End Property

		Public Property UnitPrice As Decimal
			Get
				Return _unitPrice
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _unitPrice ) Then Return
				_unitPrice = Value
				NotifyOfPropertyChange( Function() UnitPrice )
			End Set
		End Property

		Public Property TotalExpense As Decimal
			Get
				Return _totalExpense
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _totalExpense ) Then Return
				_totalExpense = Value
				NotifyOfPropertyChange( Function() TotalExpense )
			End Set
		End Property

		Public Property Details As IObservableCollection(Of LeasesListItemDetailModel)
			Get
				Return _details
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _details ) Then Return
				_details = Value
				NotifyOfPropertyChange( Function() Details )
			End Set
		End Property

		Public Sub New()
			Id = String.Empty
			RoomName = String.Empty
			RoomCategoryName = String.Empty
			CheckinDate = New Date( 2017, 1, 1 )
			ExpectedCheckoutDate = New Date( 2017, 1, 1 )
			UnitPrice = 0
			TotalExpense = 0
			Details = New BindableCollection(Of LeasesListItemDetailModel)()
		End Sub
	End Class
End NameSpace