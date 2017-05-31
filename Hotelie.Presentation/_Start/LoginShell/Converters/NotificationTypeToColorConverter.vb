Imports System.Globalization
Imports Hotelie.Presentation.Start.Login.Models

Namespace Start.LoginShell.Converters
	Public Class NotificationTypeToColorConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim type = CType(value, NotificationType)

			Select type
				Case NotificationType.None
					Return Colors.White
				Case NotificationType.Ok
					Return Colors.Green
				Case NotificationType.Information
					Return Colors.DodgerBlue
				Case NotificationType.Error
					Return Colors.Red
				Case NotificationType.Warning
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
