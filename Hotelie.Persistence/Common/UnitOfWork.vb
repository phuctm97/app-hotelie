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
		End Sub


		Public Sub Commit() Implements IUnitOfWork.Commit
			_context.SaveChanges()
		End Sub

		Public Sub Dispose() Implements IDisposable.Dispose
			_context.Dispose()
		End Sub
	End Class
End Namespace