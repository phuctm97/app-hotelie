Imports System.Globalization
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Start.MainWindow.Converters
	Public Class StaticNotificationTypeToBooleanConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			If Equals( value, StaticNotificationType.None ) Then Return False
			Return True
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			If Equals( value, False ) Then Return StaticNotificationType.None
			Return StaticNotificationType.Information
		End Function
	End Class
End Namespace
