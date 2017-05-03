Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Rooms

Namespace Rooms
	Public Class RoomCategoryConfiguration
		Inherits EntityTypeConfiguration(Of RoomCategory)

		Public Sub New()

			HasKey( Function( p ) p.Id )

			[Property]( Function( p ) p.Id ).
				IsRequired().
				IsUnicode( False ).
				IsFixedLength().
				HasMaxLength( 5 )

			[Property]( Function( p ) p.Name ).
				IsRequired().
				IsUnicode().
				HasMaxLength( 50 )

			[Property]( Function( p ) p.Price ).
				IsRequired().
				HasColumnType( "money" )
		End Sub
	End Class
End Namespace