Imports Caliburn.Micro

Namespace Rules.Models
	Public Class CustomerCategoryModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _name As String
		Private _coefficient As Double

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

		Public Property Coefficient As Double
			Get
				Return _coefficient
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _coefficient ) Then Return
				_coefficient = value
				NotifyOfPropertyChange( Function() Coefficient )
			End Set
		End Property

		Sub New()
			_id = String.Empty
			_name = String.Empty
			_coefficient = 0
		End Sub
	End Class
End Namespace