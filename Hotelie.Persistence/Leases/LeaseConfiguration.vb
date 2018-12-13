Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Leases

Namespace Leases
    Public Class LeaseConfiguration
        Inherits EntityTypeConfiguration(Of Lease)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                IsRequired().
                HasMaxLength(7).
                IsUnicode(False).
                IsFixedLength()

            HasRequired(Function(p)p.Room).
                WithMany.Map(Function(m)m.MapKey("RoomId"))

            [Property](Function(p)p.CheckinDate).
                IsRequired()

            [Property](Function(p)p.ExpectedCheckoutDate).
                IsOptional()

            [Property](Function(p)p.RoomPrice).
                IsOptional().
                HasColumnType("money")

            [Property](Function(p)p.ExtraCoefficient).
                IsOptional()

            [Property](Function(p)p.CustomerCoefficient).
                IsOptional()

            HasMany(Function(p)p.LeaseDetails).
                WithRequired.Map(Function(l)l.MapKey("LeaseId"))

            [Property](Function(p)p.Paid).
                IsRequired()

        End Sub
    End Class
End NameSpace