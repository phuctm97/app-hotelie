Imports Hotelie.Domain.Rooms

Namespace Rooms.Models
	Public Class RoomCategoryModel
		Implements IRoomCategoryModel

		Private ReadOnly _entity As RoomCategory

		Sub New( entity As RoomCategory )
			_entity = entity
		End Sub

		Protected Sub New()
			_entity = Nothing
		End Sub

		Public ReadOnly Property Id As String Implements IRoomCategoryModel.Id
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property Name As String Implements IRoomCategoryModel.Name
			Get
				Return _entity.Name
			End Get
		End Property

		Public ReadOnly Property UnitPrice As Decimal Implements IRoomCategoryModel.UnitPrice
			Get
				Return _entity.Price
			End Get
		End Property
	End Class
End Namespace
