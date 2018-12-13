Imports Caliburn.Micro

Namespace Rules.Models
	Public Class EditableRoomCategoryModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _name As String
		Private _unitPrice As Decimal

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing(Value) OrElse Equals(Value, _id) Then Return
				_id = value
				NotifyOfPropertyChange(Function() Id)
			End Set
		End Property

		Public Property Name As String
			Get
				Return _name
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _name ) Then Return
				_name = value
				NotifyOfPropertyChange( Function() Name )
			End Set
		End Property

		Public Property UnitPrice As Decimal
			Get
				Return _unitPrice
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _unitPrice ) Then Return
				_unitPrice = value
				NotifyOfPropertyChange( Function() UnitPrice )
			End Set
		End Property

		Sub New()
			_id = String.Empty
			_name = String.Empty
			_unitPrice = 0
		End Sub
	End Class
End Namespace