Imports System.Globalization

Namespace Rooms.Converters
	Public Class RoomStateToVisibiltyConverter
		Implements IValueConverter

		Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim state = CType(value, Integer)
			If state = 0 Then Return Visibility.Visible
			Return Visibility.Collapsed
		End Function

		Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
