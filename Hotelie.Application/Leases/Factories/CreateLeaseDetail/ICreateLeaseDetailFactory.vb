Namespace Leases.Factories.CreateLeaseDetail
	Public Interface ICreateLeaseDetailFactory
		Function Execute( leaseId As String,
		                  customerName As String,
		                  customerLicenseId As String,
		                  customerAddress As String,
		                  customerCategoryId As String ) As String

		Function ExecuteAsync( leaseId As String,
		                       customerName As String,
		                       customerLicenseId As String,
		                       customerAddress As String,
		                       customerCategoryId As String ) As Task(Of String)
	End Interface
End Namespace