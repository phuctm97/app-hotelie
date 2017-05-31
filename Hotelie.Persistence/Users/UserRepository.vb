
Imports System.Data.Entity
Imports Hotelie.Domain.Users
Imports Hotelie.Persistence.Common

Namespace Users
    Public Class UserRepository
        Inherits Repository(Of User)

        Public Sub New(context As DbContext)
            MyBase.New(context)
        End Sub
    End Class

End Namespace