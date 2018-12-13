Imports System.Globalization

Namespace Common.Converters
	Public Class DecimalToStringConverter
		Implements IValueConverter

		Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim x = CType(value, Decimal)
			If IsNothing(x) Then Return "0"

			Return x.ToString()
		End Function

		Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Dim s = CType(value, String)
			If String.IsNullOrWhiteSpace(s) Then Return 0

			Dim x As Decimal = 0
			Decimal.TryParse(s, x) 

			Return x
		End Function
	End Class
End Namespace
