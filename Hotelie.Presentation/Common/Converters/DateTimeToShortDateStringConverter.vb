Imports System.Globalization

Namespace Common.Converters
	Public Class DateTimeToShortDateStringConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim d = CType(value, DateTime)

			If d.Date = Date.Now.Date Then Return "Hôm nay"
			If CType((d - Date.Now).TotalDays, Integer) = 1 Then Return "Ngày mai"
			If CType((d - Date.Now).TotalDays, Integer) = 2 Then Return "Ngày mốt"
			Return d.ToShortDateString()
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
