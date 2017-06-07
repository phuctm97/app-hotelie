Imports System.Globalization
Imports Hotelie.Application.Bills.Models

Namespace Bills.Converters
	Public Class LeaseDetailsToCustomerNamesStringConverters
		Implements IValueConverter

		Public Function Convert( value As Object,
		                         targetType As Type,
		                         parameter As Object,
		                         culture As CultureInfo ) As Object Implements IValueConverter.Convert
			Dim detail = CType(value, BillDetailModel)
			If IsNothing(detail) Then Return String.Empty

			Return String.Join( ", ", detail.Lease.Details.Select( Function( d ) d.CustomerName ) )
		End Function

		Public Function ConvertBack( value As Object,
		                             targetType As Type,
		                             parameter As Object,
		                             culture As CultureInfo ) As Object Implements IValueConverter.ConvertBack
			Throw New NotImplementedException()
		End Function
	End Class
End Namespace
