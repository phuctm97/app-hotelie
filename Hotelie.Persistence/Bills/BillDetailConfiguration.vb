Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Bills

Namespace Bills
    Public Class BillDetailConfiguration
        Inherits EntityTypeConfiguration(Of BillDetail)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                HasMaxLength(10).
                IsUnicode(False).
                IsFixedLength().
                IsRequired()

            HasRequired(Function(p)p.Lease).
                WithOptional.Map(Function(m)m.MapKey("LeaseId"))

            HasRequired(Function(p)p.Bill).WithMany(Function(o)o.Details).Map(Function(m)m.MapKey("BillId"))

            [Property](Function(p)p.CheckinDate).
                IsOptional()

            [Property](Function(p)p.NumberOfDays).
                IsOptional()

            [Property](Function(p)p.ExtraCharge).
                IsOptional().
                HasColumnType("money")

            [Property](Function(p)p.TotalExpense).
                HasColumnType("money").
                IsRequired()
        End Sub
    End Class
End NameSpace