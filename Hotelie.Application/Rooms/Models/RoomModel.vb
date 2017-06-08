Imports Hotelie.Domain.Rooms

Namespace Rooms.Models
	Public Class RoomModel
		Implements IRoomModel

		Private ReadOnly _entity As Room

		Sub New( entity As Room )
			_entity = entity
			Category = New RoomCategoryModel( _entity.Category )
		End Sub

		Protected Sub New()
			_entity = Nothing
		End Sub

		Public ReadOnly Property Id As String Implements IRoomModel.Id
			Get
				Return _entity.Id
			End Get
		End Property

		Public ReadOnly Property Name As String Implements IRoomModel.Name
			Get
				Return _entity.Name
			End Get
		End Property

		Public ReadOnly Property Note As String Implements IRoomModel.Note
			Get
				Return _entity.Note
			End Get
		End Property

		Public ReadOnly Property State As Integer Implements IRoomModel.State
			Get
				Return _entity.State
			End Get
		End Property

		Public ReadOnly Property Category As IRoomCategoryModel Implements IRoomModel.Category
	End Class
End Namespace
