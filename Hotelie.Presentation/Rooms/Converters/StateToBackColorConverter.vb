Imports System.Globalization

Namespace Rooms.Converters
	Public Class StateToBackColorConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim state = CType(value, Integer)

			Select Case state
				Case 0
					Return Colors.Green

				Case 1
					Return Colors.Red

				Case 2
					Return Colors.Gold
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
