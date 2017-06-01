Imports System.Globalization
Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomCategoriesList

Namespace Rooms.Converters
	Public Class RoomCategoriesPlusAllConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim collection = CType(value, IObservableCollection(Of RoomCategoryModel))

			Dim last = collection.LastOrDefault()
			If last Is Nothing Or last.Id <> "##all##"
				collection.Add( New RoomCategoryModel With {.Id="##all##", .Name="Tất cả", .DisplayColor=Colors.Black} )
			End If

			Return collection
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
