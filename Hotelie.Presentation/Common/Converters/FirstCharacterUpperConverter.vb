Imports System.Globalization

Namespace Common.Converters
	Public Class FirstCharacterUpperConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim str = CType(value, String)
			If str.Length = 0 Then Return DependencyProperty.UnsetValue

			Return str.ToUpper()( 0 )
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace