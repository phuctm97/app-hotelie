Imports System.Globalization

Namespace Leases.Converters
	Public Class NumberOfUsedDaysDisplayConverters
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			If IsNothing( value ) Then Return "Mới vào phòng"
			Dim beginDate = CType(value, Date)

			Dim duration As Integer = (Today - beginDate).TotalDays
			If duration = 0 Then Return "Mới vào phòng"
			Return $"Đã ở {duration} ngày"
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace