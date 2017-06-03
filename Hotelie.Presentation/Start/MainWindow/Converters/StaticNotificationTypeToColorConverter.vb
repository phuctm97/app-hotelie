Imports System.Globalization
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Start.MainWindow.Converters
	Public Class StaticNotificationTypeToColorConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim type = CType(value, StaticNotificationType)

			Select type
				Case StaticNotificationType.None
					Return Colors.White
				Case StaticNotificationType.Ok
					Return Colors.Green
				Case StaticNotificationType.Information
					Return Colors.DodgerBlue
				Case StaticNotificationType.Error
					Return Colors.Red
				Case StaticNotificationType.Warning
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
