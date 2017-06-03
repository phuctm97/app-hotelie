Namespace Services.Persistence
    Public Interface IDatabaseService
        ReadOnly Property Context As IDatabaseContext
        Function CheckDatabaseConnection(connectionString As String) As Boolean
        Function CheckDatabaseConnectionAsync(connectionString As String) As Task(Of Boolean)
        Sub SetDatabaseConnection(connectionString As String)
        Sub Dispose()
    End Interface
End NameSpace