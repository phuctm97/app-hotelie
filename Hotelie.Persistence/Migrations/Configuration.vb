Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq
Imports Hotelie.Domain.Rooms

Namespace Migrations

    Friend NotInheritable Class Configuration 
        Inherits DbMigrationsConfiguration(Of Common.DatabaseContext)

        Public Sub New()
            AutomaticMigrationsEnabled = True
        End Sub

    End Class

End Namespace
