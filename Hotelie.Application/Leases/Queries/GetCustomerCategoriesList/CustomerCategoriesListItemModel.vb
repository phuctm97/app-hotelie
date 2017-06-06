Imports Caliburn.Micro

Namespace Leases.Queries.GetCustomerCategoriesList
	Public Class CustomerCategoriesListItemModel
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

		Public Sub New()
			Id = String.Empty

			Name = String.Empty

			Coefficient = 0
		End Sub
	End Class
End Namespace