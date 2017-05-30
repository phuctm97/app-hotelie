Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Leases

Namespace Leases
    Public Class LeaseDetailConfiguration
        Inherits EntityTypeConfiguration(Of LeaseDetail)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                IsRequired().
                IsUnicode( False ).
                IsFixedLength().
                HasMaxLength(5)

            HasRequired(Function(p)p.Lease).
                WithMany().Map(Function(m)m.MapKey("LeaseId"))

            [Property](Function(p)p.CustomerName).
                IsUnicode().
                IsRequired().
                HasMaxLength(50)

            HasRequired(Function(p)p.CustomerCategory).
                WithMany().Map(Function(m)m.MapKey("CustomerCategoryId"))

            [Property](Function(p)p.LicenseId).
                IsRequired().
                HasMaxLength(20)

            [Property](Function(p)p.Address).
                IsOptional().
                HasMaxLength(100)
        End Sub
    End Class
End Namespace
