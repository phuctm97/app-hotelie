Imports System.Globalization
Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries

Namespace Leases.Converters
	Public Class NumberOfCustomersDisplayConverters
		Implements IValueConverter

		Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
			If IsNothing(value) Then Return "0 người"

			Dim collection = CType(value, IObservableCollection(Of LeaseCustomerModel))
			If IsNothing(collection) Then Return "0 người"

			Return $"{collection.Count} người"
		End Function

		Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace