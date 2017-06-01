Imports System.Windows.Media

Namespace Rooms.Queries.GetRoomsList
	Public Class RoomModel
		Property Id As String

		Property Name As String

		Property CategoryName As String

		Property CategoryDisplayColor As Color

		Property State As Integer

		Property Price As Decimal

		Property Note As String

		Property IsVisible As Boolean

		Public Sub New()
			CategoryDisplayColor = Colors.Black
			IsVisible = False
		End Sub
	End Class
End Namespace
