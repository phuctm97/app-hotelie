Imports Caliburn.Micro

Namespace Reports.Models
	Public Class FigureModel
		Inherits PropertyChangedBase

		Private _name As String
		Private _ratio As Double
		Private _figure As Integer

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

		Public Property Figure As Integer
			Get
				Return _figure
			End Get
			Set
				If Equals( Value, _figure ) Then Return
				_figure = value
				NotifyOfPropertyChange( Function() Figure )
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
			Ratio = 0
			Figure = 0
		End Sub
	End Class
End Namespace