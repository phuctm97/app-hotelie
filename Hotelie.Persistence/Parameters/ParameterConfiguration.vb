Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Parameters

Namespace Parameters
    Public Class ParameterConfiguration
        Inherits EntityTypeConfiguration(Of Parameter)

        Public Sub New ()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                IsRequired().
                IsUnicode( False ).
                IsFixedLength().
                HasMaxLength(5)

            [Property](Function(p)p.MaximumCustomer).
                IsRequired()

            [Property](Function(p)p.ExtraCoefficient).
                IsRequired()

            [Property](Function(p)p.CustomerCoefficient).
                IsRequired()

        End Sub

    End Class
End Namespace
