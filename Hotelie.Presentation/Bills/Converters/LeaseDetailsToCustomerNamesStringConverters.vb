Imports System.Globalization
Imports Hotelie.Application.Bills.Models
Imports Hotelie.Application.Leases.Models

Namespace Bills.Converters
	Public Class LeaseDetailsToCustomerNamesStringConverters
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim details = CType(value, ICollection(Of ILeaseDetailModel))
			If IsNothing(details) Then Return String.Empty

			Return String.Join( ", ", details.Select( Function( d ) d.CustomerName ) )
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
