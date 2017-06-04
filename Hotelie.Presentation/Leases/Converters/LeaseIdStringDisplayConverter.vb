Imports System.Globalization

Namespace Leases.Converters
	Public Class LeaseIdStringDisplayConverter
		Implements IValueConverter

		Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
			If String.IsNullOrEmpty(value) Then Return String.Empty

			Dim id = CType(value, String)
			Return $"#{id}"
		End Function

		Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
