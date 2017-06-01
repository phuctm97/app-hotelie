Imports System.Globalization
Imports Caliburn.Micro
Imports Hotelie.Presentation.Common

Namespace Rooms.Converters
	Public Class CategoryToBackColorConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim id = CType(value, String)
			Return IoC.Get(Of Colorizer)().GetColor( "room-category", id ).BackColor
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
