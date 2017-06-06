Imports System.Globalization
Imports MaterialDesignThemes.Wpf

Namespace Rooms.Converters
	Public Class RoomStateToIconKindConverter
		Implements IValueConverter

		Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim state = CType(value, Integer)

			Select Case state
				Case 0
					Return PackIconKind.EmoticonHappy
				Case 1
					Return PackIconKind.EmoticonSad
			End Select

			Return DependencyProperty.UnsetValue
		End Function

		Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
