Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Leases

Namespace Leases
    Public Class CustomerCategoryConfiguration
        Inherits EntityTypeConfiguration(Of CustomerCategory)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                HasMaxLength(5).
                IsUnicode(False).
                IsRequired().
                IsFixedLength()

            [Property](Function(p)p.Name).
                IsRequired().
                HasMaxLength(30).
                IsUnicode()

            [Property](Function(p)p.Coefficient).
                IsRequired()

        End Sub

    End Class
End NameSpace