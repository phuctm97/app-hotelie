Imports System.Globalization

Namespace Rooms.Converters
	Public Class RoomStateToBackColorConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim state = CType(value, Integer)

			Select Case state
				Case 0
					Return Color.FromRgb( 0, 230, 118 )

				Case 1
					Return Color.FromRgb( 255, 0, 87 )
			End Select

			Return Colors.White
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
