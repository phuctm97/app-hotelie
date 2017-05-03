Imports System.Data.Entity
Imports Hotelie.Domain.Rooms
Imports Hotelie.Persistence.Rooms

Namespace Common
	Public Class DatabaseContext
		Inherits DbContext

		Public Property Rooms As DbSet(Of Room)

		Public Property RoomCategories As DbSet(Of RoomCategory)

		Public Sub New()
			MyBase.New( "name=DatabaseContext" )

			Database.SetInitializer( New DatabaseInitializer )
		End Sub

		Protected Overrides Sub OnModelCreating( modelBuilder As DbModelBuilder )
			MyBase.OnModelCreating( modelBuilder )

			modelBuilder.Configurations.Add( New RoomCategoryConfiguration )
			modelBuilder.Configurations.Add( New RoomConfiguration )
		End Sub
	End Class
End Namespace

'Public Class MyEntity
'    Public Property Id() As Int32
'    Public Property Name() As String
'End Class
