Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Models
	Public Class FilterableRoomModel
		Inherits PropertyChangedBase

		Private _model As RoomModel
		Private _isFilterMatch As Boolean

		Public Property Model As IRoomModel
			Get
				Return _model
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _model ) Then Return
				_model = value
				NotifyOfPropertyChange( Function() Model )
			End Set
		End Property

		Public Property IsFiltersMatch As Boolean
			Get
				Return _isFilterMatch
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _isFilterMatch ) Then Return
				_isFilterMatch = value
				NotifyOfPropertyChange( Function() IsFiltersMatch )
			End Set
		End Property
	End Class
End Namespace
