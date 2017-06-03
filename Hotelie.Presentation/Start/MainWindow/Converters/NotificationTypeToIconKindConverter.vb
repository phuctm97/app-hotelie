Imports System.Globalization
Imports Hotelie.Presentation.Start.Login.Models
Imports MaterialDesignThemes.Wpf

Namespace Start.MainWindow.Converters
	Public Class NotificationTypeToIconKindConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim type = CType(value, NotificationType)

			Select type
				Case NotificationType.None
					Return PackIconKind.AppleKeyboardCommand
				Case NotificationType.Ok
					Return PackIconKind.Check
				Case NotificationType.Information
					Return PackIconKind.InformationVariant
				Case NotificationType.Error
					Return PackIconKind.Close
				Case NotificationType.Warning
					Return PackIconKind.Alert
			End Select

			Return PackIconKind.AppleKeyboardCommand
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
