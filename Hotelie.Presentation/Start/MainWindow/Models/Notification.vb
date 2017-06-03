Imports Caliburn.Micro

Namespace Start.MainWindow.Models
	Public Enum NotificationType
		None
		Ok
		Information
		Warning
		[Error]
	End Enum

	Public Class Notification
		Inherits PropertyChangedBase

		Private _text As String
		Private _type As NotificationType

		Public Property Text As String
			Get
				Return _text
			End Get
			Set
				If String.Equals( Value, _text ) Then Return

				_text = value
				NotifyOfPropertyChange( Function() Text )
			End Set
		End Property

		Public Property Type As NotificationType
			Get
				Return _type
			End Get
			Set
				If Equals( Value, _type ) Then Return

				_type = value
				NotifyOfPropertyChange( Function() Type )
			End Set
		End Property
	End Class
End Namespace
