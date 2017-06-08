Imports Hotelie.Application.Rooms.Models

Namespace Rooms.Models
	Public Class FakeRoomCategoryModel
		Implements IRoomCategoryModel

		Public Sub New()
		End Sub

		Public ReadOnly Property Id As String Implements IRoomCategoryModel.Id
			Get
				Return String.Empty
			End Get
		End Property

		Public ReadOnly Property Name As String Implements IRoomCategoryModel.Name
			Get
				Return "Tất cả"
			End Get
		End Property

		Public ReadOnly Property UnitPrice As Decimal Implements IRoomCategoryModel.UnitPrice
			Get
				Return - 1
			End Get
		End Property
	End Class
End Namespace