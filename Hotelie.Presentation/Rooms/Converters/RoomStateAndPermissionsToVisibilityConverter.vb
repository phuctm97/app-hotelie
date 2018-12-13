Imports System.Globalization

Namespace Rooms.Converters
	Public Class RoomStateAndPermissionsToVisibilityConverter
		Implements IMultiValueConverter

		Public Function Convert( values() As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IMultiValueConverter.Convert
			If TypeOf values( 0 ) IsNot Integer Then Return Visibility.Collapsed
			Dim state = CType(values( 0 ), Integer)

			If TypeOf values( 1 ) IsNot Boolean Then Return Visibility.Collapsed
			Dim canAddLease = CType(values( 1 ), Boolean)

			If state = 0 And Not canAddLease
				Return Visibility.Collapsed
			End If

			Return Visibility.Visible
		End Function

		Public Function ConvertBack( value As Object,
		                             targetTypes() As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object() Implements IMultiValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
