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
		End Sub

		Public Sub SetDatabaseConnection( dataSource As String,
		                                  catalog As String ) Implements IDatabaseService.SetDatabaseConnection
		End Sub

		Public Function CheckDatabaseConnection( dataSource As String,
		                                         catalog As String ) As Boolean _
			Implements IDatabaseService.CheckDatabaseConnection
			Return True
		End Function

		Public Async Function CheckDatabaseConnectionAsync( dataSource As String,
		                                                    catalog As String ) As Task(Of Boolean) _
			Implements IDatabaseService.CheckDatabaseConnectionAsync
			Return True
		End Function
	End Class
End Namespace
