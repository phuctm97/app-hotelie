Imports System.Windows.Media
Imports Caliburn.Micro

Namespace Rooms.Queries.GetRoomCategoriesList
	Public Class RoomCategoriesListItemModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _name As String
		Private _unitPrice As Decimal
		Private _displayColor As Color

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing(Value) OrElse String.Equals( Value, _id ) Then Return
				_id = value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Public Property Name As String
			Get
				Return _name
			End Get
			Set
				If IsNothing(Value) OrElse String.Equals( Value, _name ) Then Return
				_name = value
				NotifyOfPropertyChange( Function() Name )
			End Set
		End Property

		Public Property DisplayColor As Color
			Get
				Return _displayColor
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _displayColor ) Then Return
				_displayColor = value
				NotifyOfPropertyChange( Function() DisplayColor )
			End Set
		End Property

		Public Property UnitPrice as Decimal
			Get
				Return _unitPrice
			End Get
			Set
				If IsNothing(Value) OrElse Equals( Value, _unitPrice ) Then Return
				_unitPrice = value
				NotifyOfPropertyChange( Function() UnitPrice )
			End Set
		End Property

		Public Sub New()
			Id = String.Empty
			Name = String.Empty
			UnitPrice = 0
			DisplayColor = Colors.Black
		End Sub
	End Class
End Namespace