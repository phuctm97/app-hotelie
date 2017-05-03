Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Common

Namespace Rooms
	Public Class RoomRepository
		Inherits Repository(Of Room)
		Implements IRoomRepository

		Private ReadOnly _context As DatabaseContext

		Public Sub New( context As DatabaseContext )
			MyBase.New( context )

			_context = context
		End Sub
	End Class
End Namespace
