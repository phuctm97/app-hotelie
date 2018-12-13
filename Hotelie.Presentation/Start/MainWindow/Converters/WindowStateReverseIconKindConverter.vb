Imports System.Globalization
Imports MaterialDesignThemes.Wpf

Namespace Start.MainWindow.Converters
	Public Class WindowStateReverseIconKindConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim state = CType(value, WindowState)
			If state = WindowState.Maximized Then Return PackIconKind.ArrowCompressAll
			Return PackIconKind.ArrowExpandAll
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace