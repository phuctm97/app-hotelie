Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Models
	Public Class EditableRoomModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _name As String
		Private _category As RoomCategoryModel
		Private _note As String
		Private _state As Integer

		Public Property Id As String
			Get
				Return _id
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _id ) Then Return
				_id = Value
				NotifyOfPropertyChange( Function() Id )
			End Set
		End Property

		Public Property Name As String
			Get
				Return _name
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _name ) Then Return
				_name = value
				NotifyOfPropertyChange( Function() Name )
			End Set
		End Property

		Public Property Category As RoomCategoryModel
			Get
				Return _category
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _category ) Then Return
				_category = value
				NotifyOfPropertyChange( Function() Category )
			End Set
		End Property

		Public Property Note As String
			Get
				Return _note
			End Get
			Set
				If IsNothing( Value ) OrElse String.Equals( Value, _note ) Then Return
				_note = value
				NotifyOfPropertyChange( Function() Note )
			End Set
		End Property

		Public Property State As Integer
			Get
				Return _state
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _state ) Then Return
				_state = value
				NotifyOfPropertyChange( Function() State )
			End Set
		End Property

		Public Sub New()
			Id = String.Empty
			Name = String.Empty
			Category = Nothing
			Note = String.Empty
			State = 0
		End Sub
	End Class
End Namespace