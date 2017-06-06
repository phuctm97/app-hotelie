Imports System.Globalization

Namespace Rooms.Converters
	Public Class RoomStateToStringConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			If String.IsNullOrEmpty(value) Then Return Nothing
			Dim state = CType(value, Integer)

			Select Case state
				Case 0
					Return "Trống"
				Case 1
					Return "Đang thuê"
			End Select

			Return "Tất cả"
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
