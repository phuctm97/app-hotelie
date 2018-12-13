Namespace Leases.Models
	Public Interface ILeaseDetailModel
		ReadOnly Property Id As String

		ReadOnly Property CustomerName As String

		ReadOnly Property CustomerCategory As ICustomerCategoryModel

		ReadOnly Property CustomerLicenseId As String

		ReadOnly Property CustomerAddress As String
	End Interface
End Namespace