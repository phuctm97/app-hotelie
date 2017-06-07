Imports Hotelie.Domain.Rooms

Namespace Rooms.Models
	Public Class RoomModel
		Private ReadOnly _entity As Room

		Sub New( entity As Room )
			_entity = entity
			Category = New RoomCategoryModel( _entity.Category )
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

		Public ReadOnly Property Note As String
			Get
				Return _entity.Note
			End Get
		End Property

		Public ReadOnly Property State As Integer
			Get
				Return _entity.State
			End Get
		End Property

		Public ReadOnly Property Category As RoomCategoryModel
	End Class
End Namespace
