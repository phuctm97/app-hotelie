Imports Hotelie.Application.Services.Persistence

Namespace Common
	Public Class UnitOfWork
		Implements IUnitOfWork

		Private ReadOnly _databaseService As DatabaseService

		Sub New( databaseService As DatabaseService )
			_databaseService = databaseService
			If _databaseService.Context Is Nothing Then Throw New ArgumentNullException()
		End Sub


		Public Sub Commit() Implements IUnitOfWork.Commit
			_databaseService.Context.SaveChanges()
		End Sub

        Public Async Sub CommitAsync() Implements IUnitOfWork.CommitAsync
            Await _databaseService.Context.SaveChangesAsync()
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
			_databaseService.Context.Dispose()
		End Sub
	End Class
End Namespace