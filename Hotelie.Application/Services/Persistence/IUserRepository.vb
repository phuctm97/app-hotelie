Imports System.Linq.Expressions
Imports Hotelie.Domain.Users

Namespace Services.Persistence
    Public Interface IUserRepository
        Inherits IRepository(Of User)

    End Interface
End NameSpace