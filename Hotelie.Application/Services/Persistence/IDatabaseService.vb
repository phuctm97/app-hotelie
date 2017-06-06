Namespace Services.Persistence
    Public Interface IDatabaseService
        ReadOnly Property Context As IDatabaseContext
        Function CheckDatabaseConnection(serverName As String, databaseName As String) As Integer
        Function CheckDatabaseConnectionAsync(serverName As String, databaseName As String) As Task(Of Integer)
        Sub SetDatabaseConnection(serverName As String, databaseName As String)
        Sub Dispose()
    End Interface
End NameSpace