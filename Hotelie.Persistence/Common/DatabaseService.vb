Imports System.Data.Entity
Imports Hotelie.Application.Services.Persistence

Namespace Common
    Public Class DatabaseService
        Implements IDatabaseService

        Private _context As IDatabaseContext

        Public ReadOnly Property Context As IDatabaseContext Implements IDatabaseService.Context
            Get
                Return _context
            End Get
        End Property

        Public Function SetDatabaseConnection(serverName As String, databaseName As String) As Boolean _
            Implements IDatabaseService.SetDatabaseConnection
            _context?.Dispose()
            Dim connectionString =
                    $"data source={serverName};initial catalog={databaseName _
                    };integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"

            _context = New DatabaseContext(connectionString)
            Try
                Dim leases = _context.Leases.ToList()
                Dim leaseDetails = _context.LeaseDetails.ToList()
                Dim bills = _context.Bills.ToList()
                Dim billDetails = _context.BillDetails.ToList()
                Dim parameters = _context.Parameters.ToList()
                Dim permissions = _context.Permissions.ToList()
                Dim roomCategories = _context.RoomCategories.ToList()
                Dim userCategories = _context.UserCategories.ToList()
                Dim users = _context.Users.ToList()
                Dim customerCategories = _context.CustomerCategories.ToList()
                Dim rooms = _context.Rooms.ToList()
            Catch
                Return False
            End Try
            Return True
        End Function

        Public Function CheckDatabaseConnection(serverName As String, databaseName As String) As Integer _
            Implements IDatabaseService.CheckDatabaseConnection
            Dim checker = 0

            Dim connectionString =
                    $"data source={serverName};initial catalog={databaseName _
                    };integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"

            Dim dbContext = New DatabaseContext(connectionString)


            Try
                dbContext.Database.Connection.Open()
                dbContext.Database.Connection.Close()
                checker = 2
            Catch
                dbContext.Dispose()
                Return checker
            End Try

            Dim serverAdmin
            Try
                serverAdmin = dbContext.Users.FirstOrDefault(Function(p)p.Id = "admin")
                If Not(IsNothing(serverAdmin)) Then
                    dbContext.Dispose()
                    Return 1
                End If
            Catch
            End Try

            dbContext.Dispose()
            Return checker
        End Function

        Public Async Function CheckDatabaseConnectionAsync(serverName As String, databaseName As String) _
            As Task(Of Integer) Implements IDatabaseService.CheckDatabaseConnectionAsync

            Dim checker = 0

            Dim connectionString =
                    $"data source={serverName};initial catalog={databaseName _
                    };integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"

            Dim dbContext = New DatabaseContext(connectionString)

            Try
                Await dbContext.Database.Connection.OpenAsync()
                dbContext.Database.Connection.Close()
                checker = 1
            Catch
                dbContext.Dispose()
                Return checker
            End Try

            Dim serverAdmin
            Try
                serverAdmin = Await dbContext.Users.FirstOrDefaultAsync(Function(p)p.Id = "admin")
                If Not(IsNothing(serverAdmin)) Then
                    dbContext.Dispose()
                    Return 2
                End If
            Catch
            End Try

            dbContext.Dispose()
            Return checker
        End Function

        Public Sub New()
            _context = New DatabaseContext()
        End Sub

        Public Sub Dispose() Implements IDatabaseService.Dispose
            _context.Dispose()
        End Sub
    End Class
End NameSpace