Imports Hotelie.Domain.Rooms

Namespace Rooms.Queries.GetRoomCategoryData
	Public Class RoomCategoryModel
		Private ReadOnly _entity As RoomCategory

		Sub New( entity As RoomCategory )
			_entity = entity
		End Sub

		Public ReadOnly Property Id As String
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property Name As String
			Get
				Return _entity.Name
			End Get
		End Property

		Public ReadOnly Property UnitPrice As Decimal
			Get
				Return _entity.Price
			End Get
		End Property
	End Class
End Namespace
