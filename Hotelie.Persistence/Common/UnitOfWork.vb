Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Persistence.Rooms
Imports Hotelie.Persistence.Users

Namespace Common
	Public Class UnitOfWork
		Implements IUnitOfWork

		Private ReadOnly _context As DatabaseContext

		Sub New( context As DatabaseContext )
			_context = context
			If _context Is Nothing Then Throw New ArgumentNullException()

			RoomRepository = New RoomRepository( _context )
			UserRepository = New UserRepository( _context )
		End Sub

		Public ReadOnly Property RoomRepository As IRoomRepository Implements IUnitOfWork.RoomRepository
		Public ReadOnly Property UserRepository As IUserRepository Implements IUnitOfWork.UserRepository

		Public Sub Commit() Implements IUnitOfWork.Commit
			_context.SaveChanges()
		End Sub

		Public Sub Rollback() Implements IUnitOfWork.Rollback
		End Sub

		Public Sub Dispose() Implements IDisposable.Dispose
			_context.Dispose()
		End Sub
	End Class
End Namespace