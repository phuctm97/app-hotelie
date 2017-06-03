Imports Hotelie.Application.Services.Persistence

Namespace Common
    Public Class DatabaseService
        Implements IDatabaseService

        Private _context As DatabaseContext

        Public ReadOnly Property Context As IDatabaseContext Implements IDatabaseService.Context
            Get 
                Return _context
            End Get
        End Property

        Public Sub SetDatabaseConnection(connectionString As String) Implements IDatabaseService.SetDatabaseConnection
            _context?.Dispose()
            _context = New DatabaseContext(connectionString)
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

        Public Async Function CheckDatabaseConnectionAsync(connectionString As String) As Task(Of Boolean) Implements IDatabaseService.CheckDatabaseConnectionAsync
            Dim dbContext = New DatabaseContext(connectionString)
            Try
                Await dbContext.Database.Connection.OpenAsync()
                dbContext.Database.Connection.Close()
            Catch
                Return False
            End Try
            Return True
        End Function

        Public Sub New()
        End Sub

        Public Sub New(connectionString As String)
            _context = New DatabaseContext(connectionString)
        End Sub

        Public Sub Dispose() Implements IDatabaseService.Dispose
            _context.Dispose()
        End Sub

        
    End Class
End NameSpace