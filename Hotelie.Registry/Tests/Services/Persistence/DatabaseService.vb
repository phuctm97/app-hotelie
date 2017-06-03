Imports Hotelie.Application.Services.Persistence

Namespace Tests.Services.Persistence
	Public Class DatabaseService
		Implements IDatabaseService

		Public ReadOnly Property Context As IDatabaseContext Implements IDatabaseService.Context
			Get
				Throw New NotImplementedException()
			End Get
		End Property

		Public Sub Dispose() Implements IDatabaseService.Dispose
			Throw New NotImplementedException()
		End Sub

		Public Sub SetDatabaseConnection(connectionString As String) Implements IDatabaseService.SetDatabaseConnection
			Throw New NotImplementedException()
		End Sub

		Public Function CheckDatabaseConnection(connectionString As String) As Boolean Implements IDatabaseService.CheckDatabaseConnection
			Throw New NotImplementedException()
		End Function

	End Class
End Namespace
