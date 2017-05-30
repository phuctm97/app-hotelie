Imports Caliburn.Micro

Namespace Rooms.ViewModels
	Public Class RoomDetailViewModel
		Inherits PropertyChangedBase

		Private _id As String
		Private _isOpen As Boolean
		Private _isAvailable As Boolean
		'Private _room As RoomModel

		'Public Property Id As String
		'    Get
		'        Return _id
		'    End Get
		'    Set
		'        If String.Equals(_id, Value) Then Return
		'        _id = "ROOM " + Value + " DETAIL"
		'        NotifyOfPropertyChange(Function() Id)
		'    End Set
		'End Property

		Public Property IsOpen As Boolean
			Get
				Return _isOpen
			End Get
			Set
				If Equals( _isOpen, Value ) Then Return
				_isOpen = Value
				NotifyOfPropertyChange( Function() IsOpen )
			End Set
		End Property

		Public Property IsAvailable As Boolean
			Get
				Return _isAvailable
			End Get
			Set
				If Equals( _isAvailable, Value ) then Return
				_isAvailable = Value
				NotifyOfPropertyChange( Function() IsAvailable )
			End Set
		End Property

		'Public Property Room As RoomModel
		'    Get
		'        Return _room
		'    End Get
		'    Set
		'        If Equals(_room, Value) then Return
		'        _room = value
		'        NotifyOfPropertyChange(Function() Room)
		'    End Set
		'End Property

		'Public Sub LoadRoomsInfo()
		'    'Select * from Rooms where Id = Me.Id
		'    'if room state = free set availble to True
		'    Room = new RoomModel() With {.Category="VIP",   .ID=Me.Id,  .State=-1,    .Name="101"}
		'End Sub

		Public Sub New()
			IsOpen = False
			'LoadRoomsInfo()
		End Sub
	End Class
End Namespace