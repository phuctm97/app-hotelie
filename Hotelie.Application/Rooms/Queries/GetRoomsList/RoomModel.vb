Imports System.Windows.Media
Imports Caliburn.Micro

Namespace Rooms.Queries.GetRoomsList
	Public Class RoomModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _name As String
		Private _categoryName As String
		Private _categoryDisplayColor As Color
		Private _state As Integer
		Private _price As Decimal
		Private _note As String
		Private _isVisible As Boolean

		Property Id As String
			Get
				Return _id
			End Get
			Set
				If String.Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Property Name As String
			Get
				Return _name
			End Get
			Set
				If String.Equals( Value, _name ) Then Return
				_name = value
				NotifyOfPropertyChange( Function() Name )
			End Set
		End Property

		Property CategoryName As String
			Get
				Return _categoryName
			End Get
			Set
				If String.Equals( Value, _categoryName ) Then Return
				_categoryName = value
				NotifyOfPropertyChange( Function() CategoryName )
			End Set
		End Property

		Property CategoryDisplayColor As Color
			Get
				Return _categoryDisplayColor
			End Get
			Set
				If Equals( Value, _categoryDisplayColor ) Then Return
				_categoryDisplayColor = value
				NotifyOfPropertyChange( Function() CategoryDisplayColor )
			End Set
		End Property

		Property State As Integer
			Get
				Return _state
			End Get
			Set
				If Equals( Value, _state ) Then Return
				_state = value
				NotifyOfPropertyChange( Function() State )
			End Set
		End Property

		Property Price As Decimal
			Get
				Return _price
			End Get
			Set
				If Equals( Value, _price ) Then Return
				_price = value
				NotifyOfPropertyChange( Function() Price )
			End Set
		End Property

		Property Note As String
			Get
				Return _note
			End Get
			Set
				If String.Equals( Value, _note ) Then Return
				_note = value
				NotifyOfPropertyChange( Function() Note )
			End Set
		End Property

		Property IsVisible As Boolean
			Get
				Return _isVisible
			End Get
			Set
				If Equals( Value, _isVisible ) Then Return
				_isVisible = value
				NotifyOfPropertyChange( Function() IsVisible )
			End Set
		End Property

		Public Sub New()
			CategoryDisplayColor = Colors.Black
			IsVisible = False
		End Sub
	End Class
End Namespace
