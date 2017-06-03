Imports Caliburn.Micro

Namespace Start.MainWindow.Models
	Public Class StaticDialogModel
		Inherits PropertyChangedBase

		Private _content As Object
		Private _isVisible As Boolean

		Public Property Content As Object
			Get
				Return _content
			End Get
			Set
				If Equals( Value, _content ) Then Return
				_content = value
				NotifyOfPropertyChange( Function() Content )
			End Set
		End Property

		Public Property IsVisible As Boolean
			Get
				Return _isVisible
			End Get
			Set
				If Equals( Value, _isVisible ) Then Return
				_isVisible = value
				NotifyOfPropertyChange( Function() IsVisible )
			End Set
		End Property
	End Class
End Namespace
