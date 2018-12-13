Imports System.Globalization

Namespace Start.MainWindow.Converters
	Public Class WindowStateReverseNameConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim state = CType(value, WindowState)
			If state = WindowState.Maximized Then Return "Thu nhỏ"
			Return "Phóng to"
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
