Imports System.Globalization
Imports Hotelie.Presentation.Start.MainWindow.Models
Imports MaterialDesignThemes.Wpf

Namespace Start.MainWindow.Converters
	Public Class StaticNotificationTypeToIconKindConverter
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim type = CType(value, StaticNotificationType)

			Select type
				Case StaticNotificationType.None
					Return PackIconKind.AppleKeyboardCommand
				Case StaticNotificationType.Ok
					Return PackIconKind.Check
				Case StaticNotificationType.Information
					Return PackIconKind.InformationVariant
				Case StaticNotificationType.Error
					Return PackIconKind.Close
				Case StaticNotificationType.Warning
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
