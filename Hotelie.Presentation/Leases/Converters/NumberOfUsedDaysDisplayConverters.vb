Imports System.Globalization

Namespace Leases.Converters
	Public Class NumberOfUsedDaysDisplayConverters
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			If IsNothing( value ) Then Return "Mới vào phòng."
			Dim n = CType(value, Integer)

			If n = 0 Then Return "Mới vào phòng."
			Return $"Đã ở {n} ngày."
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace