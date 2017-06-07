Namespace Leases.Commands
	Public Interface IUpdateLeaseDetailCommand
		Function Execute( id As String,
		                  customerName As String,
		                  customerLicenseId As String,
		                  customerAddress As String,
		                  customerCategoryId As String ) As String

		Function ExecuteAsync( id As String,
		                       customerName As String,
		                       customerLicenseId As String,
		                       customerAddress As String,
		                       customerCategoryId As String ) As Task(Of String)
	End Interface
End Namespace