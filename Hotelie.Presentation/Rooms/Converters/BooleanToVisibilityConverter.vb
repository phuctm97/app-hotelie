Imports System.Globalization

Namespace Rooms.Converters
	Public Class BooleanToVisibilityConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			If Equals( value, True ) Then Return Visibility.Visible
			Return Visibility.Collapsed
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			If Equals( value, Visibility.Visible ) Then Return True
			Return False
		End Function
	End Class
End Namespace
