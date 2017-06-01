Imports System.Globalization

Namespace Rooms.Converters
	Public Class StateToStateStringConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim state = CType(value, Integer)

			Select Case state
				Case 0
					Return "Trống"
				Case 1
					Return "Đã thuê"
				Case 2
					Return "Chưa dọn"
			End Select

			Return "K. xác định"
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
