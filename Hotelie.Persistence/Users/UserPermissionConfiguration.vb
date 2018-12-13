Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Users

Namespace Users
    Public Class UserPermissionConfiguration
        Inherits EntityTypeConfiguration(Of UserPermission)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                IsRequired().
                IsUnicode(False).
                IsFixedLength().
                HasMaxLength(5)

            HasRequired(Function(p)p.User).
                WithMany().Map(Function(m)m.MapKey("UserId"))

            HasRequired(Function(p)p.Permission).
                WithMany().Map(Function(m)m.MapKey("PermissionId"))
        End Sub
    End Class
End NameSpace