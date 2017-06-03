Imports System.Globalization

Namespace Common.Converters
	Public Class DecimalToCurrencyStringConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			If String.IsNullOrEmpty( value ) Then Return Nothing

			Dim money = CType(value, Decimal)

			If money < 0 Then Return "Không giới hạn"
			Return $"{money:N0} đ"
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
