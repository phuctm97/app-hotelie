Imports Hotelie.Application.Leases.Commands.UpdateLeaseDetail
Imports Hotelie.Domain.Leases

Namespace Tests.Leases.Commands.UpdateLeaseDetail
	Public Class UpdateLeaseDetailCommand
		Implements IUpdateLeaseDetailCommand

		Public Function Execute( id As String,
		                         customerName As String,
		                         customerLicenseId As String,
		                         customerAddress As String,
		                         customerCategoryId As String ) As String Implements IUpdateLeaseDetailCommand.Execute
			Dim customerCategory = LeaseRepositoryTest.CustomerCategories.FirstOrDefault( Function( c ) c.Id = id )
			If customerCategory Is Nothing Then Return "Không tìm thấy loại khách hàng tương ứng"

			For Each lease As Lease In LeaseRepositoryTest.Leases
				For Each detail As LeaseDetail In lease.LeaseDetails
					If Not String.Equals( id, detail.Id ) Then Continue For

					detail.CustomerName = customerName
					detail.LicenseId = customerLicenseId
					detail.Address = customerAddress
					detail.CustomerCategory = customerCategory
				Next
			Next

			Return String.Empty
		End Function

		Public Async Function ExecuteAsync( id As String,
		                                    customerName As String,
		                                    customerLicenseId As String,
		                                    customerAddress As String,
		                                    customerCategoryId As String ) As Task(Of String) _
			Implements IUpdateLeaseDetailCommand.ExecuteAsync
			Return _
				Await Task.Run( Function() Execute( id, customerName, customerLicenseId, customerAddress, customerCategoryId ) )
		End Function
	End Class
End Namespace