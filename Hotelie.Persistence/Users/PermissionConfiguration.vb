Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Users

Namespace Users
    Public Class PermissionConfiguration
        Inherits EntityTypeConfiguration(Of Permission)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                IsRequired().
                IsUnicode(False).
                IsFixedLength().
                HasMaxLength(5)

            [Property](Function(p)p.Name).
                IsRequired().
                HasMaxLength(40)
        End Sub
    End Class
End Namespace
