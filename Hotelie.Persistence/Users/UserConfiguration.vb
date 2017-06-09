Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Users

Namespace Users
    Public Class UserConfiguration
        Inherits EntityTypeConfiguration(Of User)

        Public Sub New()
            HasKey(Function(p)p.Id)

            [Property](Function(p)p.Id).
                IsRequired().
                IsUnicode(False).
                HasMaxLength(20)

            [Property](Function(p)p.Password).
                IsRequired().
                IsUnicode(False).
                HasMaxLength(50)

            HasMany(Function(p) p.Permissions).WithMany().Map(Sub(m)
                m.MapLeftKey("UserId")
                m.MapRightKey("PermissionId")
                m.ToTable("UserPermissions")
                                                                 End Sub)
        End Sub
    End Class
End Namespace
