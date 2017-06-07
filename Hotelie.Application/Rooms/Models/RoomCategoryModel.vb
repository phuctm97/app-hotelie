Imports Hotelie.Domain.Rooms

Namespace Rooms.Models
	Public Class RoomCategoryModel
		Private ReadOnly _entity As RoomCategory

		Sub New( entity As RoomCategory )
			_entity = entity
		End Sub

		Protected Sub New()
			_entity = Nothing
		End Sub

		Public Overridable ReadOnly Property Id As String
			Get
				Return _entity.Id
			End Get
		End Property

		Public Overridable ReadOnly Property Name As String
			Get
				Return _entity.Name
			End Get
		End Property

		Public Overridable ReadOnly Property UnitPrice As Decimal
			Get
				Return _entity.Price
			End Get
		End Property
	End Class
End Namespace
