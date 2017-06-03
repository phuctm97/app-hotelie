Imports Hotelie.Persistence.Common

Namespace DatabaseServices
    Public Class DatabaseService
        Implements IDatabaseService

        Private _context As DatabaseContext

        Public Property Context As DatabaseContext
            Get 
                Return _context
            End Get
            Private Set
                _context = Value
            End Set
        End Property

        Public Sub SetDatabaseConnection(connectionString As String) Implements IDatabaseService.SetDatabaseConnection
            Context?.Dispose()
            Context = New DatabaseContext(connectionString)
        End Sub

        Public Function CheckDatabaseConnection(connectionString As String) As Boolean Implements IDatabaseService.CheckDatabaseConnection
            Dim dbContext = New DatabaseContext(connectionString)
            Try
                dbContext.Database.Connection.Open()
                dbContext.Database.Connection.Close()
            Catch
                Return False
            End Try
            Return True
        End Function

        Public Sub New()
        End Sub

        Public Sub New(connectionString As String)
            Context = New DatabaseContext(connectionString)
        End Sub

        Public Sub Dispose() Implements IDatabaseService.Dispose
            Context.Dispose()
        End Sub
    End Class
End NameSpace