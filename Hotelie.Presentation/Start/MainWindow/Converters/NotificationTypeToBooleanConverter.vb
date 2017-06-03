Imports System.Globalization
Imports Hotelie.Presentation.Start.Login.Models

Namespace Start.MainWindow.Converters
	Public Class NotificationTypeToBooleanConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			If Equals( value, NotificationType.None ) Then Return False
			Return True
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			If Equals( value, False ) Then Return NotificationType.None
			Return NotificationType.Information
		End Function
	End Class
End Namespace
