

Namespace Rooms.Views
	Public Class RoomDetailView
		Private _previousRoomNameLength As Integer

		Public Sub New()

			' This call is required by the designer.
			InitializeComponent()

			' Add any initialization after the InitializeComponent() call.
			InitEffectParameters()
		End Sub

		Private Sub InitEffectParameters()
			_previousRoomNameLength = 0
		End Sub
	End Class
End Namespace
