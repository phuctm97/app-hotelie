Namespace DatabaseServices
    Public Class DatabaseService
        Implements IDatabaseService

        Private _context As DatabaseContext

        Public Sub SetDatabaseConnection(connectionString As String) Implements IDatabaseService.SetDatabaseConnection
            Throw New NotImplementedException()
        End Sub

        Public Function CheckDatabaseConnection(connectionString As String) As Boolean Implements IDatabaseService.CheckDatabaseConnection
        End Function
    End Class
End NameSpace