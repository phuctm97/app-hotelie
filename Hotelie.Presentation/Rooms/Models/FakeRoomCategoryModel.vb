Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Models
	Public Class FakeRoomCategoryModel
		Inherits RoomCategoryModel

		Public Sub New()
		End Sub

		Public Overrides ReadOnly Property Id As String
			Get
				Return String.Empty
			End Get
		End Property

		Public Overrides ReadOnly Property Name As String
			Get
				Return "Tất cả"
			End Get
		End Property

		Public Overrides ReadOnly Property UnitPrice As Decimal
			Get
				Return - 1
			End Get
		End Property
	End Class
End Namespace