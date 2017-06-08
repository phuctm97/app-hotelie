Imports Caliburn.Micro

Namespace Reports.Models
	Public Class RevenueModel
		Inherits PropertyChangedBase

		Private _name As String
		Private _revenue As Decimal
		Private _ratio As Double

		Public Property Name As String
			Get
				Return _name
			End Get
			Set
				If Equals( Value, _name ) Then Return
				_name = value
				NotifyOfPropertyChange( Function() Name )
			End Set
		End Property

		Public Property Revenue As Decimal
			Get
				Return _revenue
			End Get
			Set
				If Equals( Value, _revenue ) Then Return
				_revenue = value
				NotifyOfPropertyChange( Function() Revenue )
			End Set
		End Property

		Public Property Ratio As Double
			Get
				Return _ratio
			End Get
			Set
				If Equals( Value, _ratio ) Then Return
				_ratio = value
				NotifyOfPropertyChange( Function() Ratio )
			End Set
		End Property

		Public Sub New()
			Name = String.Empty
			Revenue = 0
			Ratio = 0
		End Sub
	End Class
End Namespace
