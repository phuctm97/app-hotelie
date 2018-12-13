Imports System.Globalization

Namespace Rooms.Converters
	Public Class RoomStateToActionNameConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim state = CType(Value, Integer)

			Select Case state
				Case 0
					Return "THUÊ"
				Case 1
					Return "THANH TOÁN"
				Case Else
					Return "K. XÁC ĐỊNH"
			End Select
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace