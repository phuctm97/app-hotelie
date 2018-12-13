Imports System.Globalization

Namespace Reports.Converters
	Public Class RatioToPercentConverter
		Implements IValueConverter

		Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim d = CType(Value, Double)

			Return $"{Math.Round(d*100, 1)}%"
		End Function

		Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace