Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Persistence.Rooms

Namespace Common
	Public Class UnitOfWork
		Implements IUnitOfWork

		Private ReadOnly _context As DatabaseContext

		Sub New( context As DatabaseContext )
			_context = context
			If _context Is Nothing Then Throw New ArgumentNullException()

			RoomRepository = New RoomRepository( _context )
		End Sub

		Public ReadOnly Property RoomRepository As IRoomRepository Implements IUnitOfWork.RoomRepository

		Public Function Commit() As Int32 Implements IUnitOfWork.Commit
			Throw New NotImplementedException()
		End Function

		Public Function Rollback() As Int32 Implements IUnitOfWork.Rollback
			Throw New NotImplementedException()
		End Function

		Public Sub Dispose() Implements IDisposable.Dispose
			_context.Dispose()
		End Sub
	End Class
End Namespace