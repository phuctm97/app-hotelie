Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Leases

Namespace Leases
    Public Class LeaseConfiguration
        Inherits EntityTypeConfiguration(Of Lease)

        Public Sub New()
            HasKey(Function(p) p.Id)

            [Property](Function(p) p.Id).
                IsRequired().
                IsUnicode(False).
                IsFixedLength().
                HasMaxLength(5)

            HasRequired(Function(p) p.Room).
                WithMany().Map(Function(m) m.MapKey("RoomId"))

            [Property](Function(p) p.BeginDate).
                IsRequired()

            [Property](Function(p) p.EndDate).
                IsRequired()

            [Property](Function(p) p.NumberOfDate).
                IsOptional()

            [Property](Function(p) p.Price).
                HasColumnType("money").
                IsOptional()

            [Property](Function(p) p.ExtraCoefficient).
                IsRequired()

            [Property](Function(p) p.CustomerCoefficient).
                IsRequired()

            [Property](Function(p) p.ExtraCharge).
                HasColumnType("money").
                IsOptional()

            [Property](Function(p) p.TotalPrice).
                HasColumnType("money").
                IsOptional()

            HasOptional(Function(p)p.Bill).
                WithMany().Map(Function(m)m.MapKey("BillId"))
        End Sub
    End Class
End Namespace