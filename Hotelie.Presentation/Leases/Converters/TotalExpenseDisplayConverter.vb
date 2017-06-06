Imports System.Globalization

Namespace Leases.Converters
	Public Class TotalExpenseDisplayConverter
		Implements IValueConverter

		Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
			If IsNothing(value) Then Return "Tổng chi phí: 0đ"

			Dim total = CType(value, Decimal)
			Return $"Tổng chi phí: {total:N0}đ"
		End Function

		Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
