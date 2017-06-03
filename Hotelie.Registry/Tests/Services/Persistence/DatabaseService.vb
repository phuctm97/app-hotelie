﻿Imports Hotelie.Application.Services.Persistence

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

		Public Sub SetDatabaseConnection(connectionString As String) Implements IDatabaseService.SetDatabaseConnection
		End Sub

		Public Function CheckDatabaseConnection(connectionString As String) As Boolean Implements IDatabaseService.CheckDatabaseConnection
			If String.Equals(connectionString, "success")
				Return True
			End If

			Return False
		End Function

	End Class
End Namespace
