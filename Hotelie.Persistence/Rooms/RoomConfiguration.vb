Imports System.Data.Entity.ModelConfiguration
Imports Hotelie.Domain.Rooms

Namespace Rooms
	Public Class RoomConfiguration
		Inherits EntityTypeConfiguration(Of Room)

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

			[Property]( Function( p ) p.Note ).
				IsOptional().
				IsUnicode()

			HasRequired( Function( p ) p.Category ).
				WithMany().Map( Function( m ) m.MapKey( "CategoryId" ) )
		End Sub
	End Class
End Namespace