Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Users

Namespace Users
    Public Class UserCategoryConfiguration
        Inherits EntityTypeConfiguration(Of UserCategory)

        Public Sub New()
            HasKey(Function(p)p.Id)
            HasMany(Function(p) p.Permissions).WithMany().Map(Sub(m)
                m.MapLeftKey("UserCategoryId")
                m.MapRightKey("PermissionId")
                m.ToTable("UserCategoryPermissions")
                                                                 End Sub)

            [Property](Function(p)p.Id).
                IsRequired().
                IsUnicode(False).
                IsFixedLength().
                HasMaxLength(5)

            [Property](Function(p)p.Name).
                IsRequired().
                IsUnicode().
                HasMaxLength(50)
        End Sub
    End Class
End Namespace
