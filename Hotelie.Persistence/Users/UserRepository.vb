Imports Hotelie.Application.Services.Persistence
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common

Namespace Users
    Public Class UserRepository
        Inherits Repository(Of User)
        Implements IUserRepository

        Private ReadOnly _context As DatabaseContext

        Public Sub New(context As DatabaseContext)
            MyBase.New(context)
            _context = context
        End Sub
    End Class
End Namespace