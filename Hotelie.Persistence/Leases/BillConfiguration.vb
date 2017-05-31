Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Leases

Namespace Leases
    Public Class BillConfiguration
        Inherits EntityTypeConfiguration(Of Bill)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                IsRequired().
                IsUnicode( False ).
                IsFixedLength().
                HasMaxLength(5)

            [Property](Function(p)p.CustomerName).
                IsRequired().
                IsUnicode().
                HasMaxLength(50)

            [Property](Function(p)p.Address).
                IsUnicode().
                IsOptional().
                HasMaxLength(100)

            [Property](Function(p)p.TotalPrice).
                IsRequired().
                HasColumnType("money")
        End Sub
    End Class
End Namespace