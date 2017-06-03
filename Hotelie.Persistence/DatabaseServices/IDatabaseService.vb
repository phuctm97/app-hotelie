Namespace DatabaseServices
    Public Interface IDatabaseService
        Function CheckDatabaseConnection(connectionString As String) As Boolean
        Sub SetDatabaseConnection(connectionString As String)
        Sub Dispose()
    End Interface
End NameSpace