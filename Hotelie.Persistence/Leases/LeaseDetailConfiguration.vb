Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Leases
Namespace Leases
    Public Class LeaseDetailConfiguration
        Inherits EntityTypeConfiguration(Of LeaseDetail)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                HasMaxLength(10).
                IsFixedLength().
                IsRequired.
                IsUnicode(False)

            [Property](Function(p)p.CustomerName).
                IsRequired().
                IsUnicode().
                HasMaxLength(50)

            [Property](Function(p)p.LicenseId).
                IsOptional().
                HasMaxLength(20)

            [Property](Function(p)p.Address).
                IsOptional().
                HasMaxLength(100)

            HasRequired(Function(p)p.CustomerCategory).
                WithMany.Map(Function(l)l.MapKey("CustomerCategoryId"))

        End Sub
    End Class
End NameSpace