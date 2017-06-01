Namespace Rooms
	Public Enum RoomState
		Free = 0
		Occupied = 1
		Dirty = 2
	End Enum

	Public Class Room
		Public Property Id As String

		Public Property Name As String

		Public Property Category As RoomCategory

        Public Property State As Byte

		Public Property Note As String
	End Class
End Namespace
