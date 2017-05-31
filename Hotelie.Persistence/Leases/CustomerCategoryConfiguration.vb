Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Leases

Namespace Leases
    Public Class CustomerCategoryConfiguration
        Inherits EntityTypeConfiguration(Of CustomerCategory)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                IsRequired().
                IsUnicode( False ).
                IsFixedLength().
                HasMaxLength(5)

            [Property](Function(p)p.Name).
                IsRequired().
                HasMaxLength(50)
        End Sub
    End Class
End Namespace
