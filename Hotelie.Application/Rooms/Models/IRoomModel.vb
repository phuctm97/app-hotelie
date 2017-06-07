Namespace Rooms.Models
	Public Interface IRoomModel
		ReadOnly Property Id As String

		ReadOnly Property Name As String

		ReadOnly Property Note As String

		ReadOnly Property State As Integer

		ReadOnly Property Category As IRoomCategoryModel
	End Interface
End Namespace