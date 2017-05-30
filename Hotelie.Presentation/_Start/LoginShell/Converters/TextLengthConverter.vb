Imports System.Globalization

Namespace Start.LoginShell.Converters
	Public Class TextLengthConverter
		Implements IValueConverter

		Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim text = CType(value, String)

			Return text.Length > 0
		End Function

		Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function

	End Class
End Namespace
