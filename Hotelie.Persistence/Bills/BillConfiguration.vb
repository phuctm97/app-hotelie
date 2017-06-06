Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Bills

Namespace Bills
    Public Class BillConfiguration
        Inherits EntityTypeConfiguration(Of Bill)

        Public Sub New()
            HasKey(Function(p)p.Id)

            HasMany(Function(p)p.Details).
                WithRequired.Map(Function(o)o.MapKey("BillId"))

            [Property](Function(p)p.Id).
                IsRequired().
                IsUnicode(False).
                HasMaxLength(7).
                IsFixedLength()

            [Property](Function(p)p.CustomerName).
                IsOptional().
                IsUnicode().
                HasMaxLength(50)

            [Property](Function(p)p.CustomerAddress).
                IsOptional().
                IsUnicode().
                HasMaxLength(100)

            [Property](Function(p)p.TotalExpense).
                IsRequired().
                HasColumnType("money")

        End Sub

    End Class
End NameSpace