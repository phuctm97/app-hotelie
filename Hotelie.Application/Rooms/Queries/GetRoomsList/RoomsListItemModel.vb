Imports System.Windows.Media
Imports Caliburn.Micro

Namespace Rooms.Queries.GetRoomsList
	Public Class RoomsListItemModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _name As String
		Private _categoryId As String
		Private _categoryName As String
		Private _categoryDisplayColor As Color
		Private _state As Integer
		Private _unitPrice As Decimal
		Private _isFiltersMatched As Boolean

		Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Property Name As String
			Get
				Return _name
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _name ) Then Return
				_name = value
				NotifyOfPropertyChange( Function() Name )
			End Set
		End Property

		Property CategoryId As String
			Get
				Return _categoryId
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _categoryId ) Then Return
				_categoryId = value
				NotifyOfPropertyChange( Function() CategoryId )
			End Set
		End Property

		Property CategoryName As String
			Get
				Return _categoryName
			End Get
			Set
				If IsNothing(Value) OrElse String.Equals( Value, _categoryName ) Then Return
				_categoryName = value
				NotifyOfPropertyChange( Function() CategoryName )
			End Set
		End Property

		Property CategoryDisplayColor As Color
			Get
				Return _categoryDisplayColor
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _categoryDisplayColor ) Then Return
				_categoryDisplayColor = value
				NotifyOfPropertyChange( Function() CategoryDisplayColor )
			End Set
		End Property

		Property State As Integer
			Get
				Return _state
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _state ) Then Return
				_state = value
				NotifyOfPropertyChange( Function() State )
			End Set
		End Property

		Property UnitPrice As Decimal
			Get
				Return _unitPrice
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _unitPrice ) Then Return
				_unitPrice = value
				NotifyOfPropertyChange( Function() UnitPrice )
			End Set
		End Property

		Property IsFiltersMatched As Boolean
			Get
				Return _isFiltersMatched
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _isFiltersMatched ) Then Return
				_isFiltersMatched = value
				NotifyOfPropertyChange( Function() IsFiltersMatched )
			End Set
		End Property

		Public Sub New()
			Id = String.Empty
			Name = String.Empty
			CategoryId = String.Empty
			CategoryName = String.Empty
			CategoryDisplayColor = Colors.Black
			State = 0
			UnitPrice = 0
			IsFiltersMatched = False
		End Sub
	End Class
End Namespace
