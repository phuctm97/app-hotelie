Imports Hotelie.Application.Leases.Factories.CreateLeaseDetail
Imports Hotelie.Domain.Leases

Namespace Tests.Leases.Factories.CreateLeaseDetail
	Public Class CreateLeaseDetailFactory
		Implements ICreateLeaseDetailFactory

		Public Function Execute( leaseId As String,
		                         customerName As String,
		                         customerLicenseId As String,
		                         customerAddress As String,
		                         customerCategoryId As String ) As String Implements ICreateLeaseDetailFactory.Execute
			Dim newId = 0
			While(True)
				Dim newIdStr = newId.ToString()
				Dim existed = False

				For Each lease As Lease In LeaseRepositoryTest.Leases
					If lease.LeaseDetails.Any( Function( d ) d.Id = newIdStr )
						existed = True
						Exit For
					End If
				Next

				If existed
					newId += 1
				Else
					Exit While
				End If
			End While

			Dim idStr = newId.ToString()
			Dim leaseToAdd = LeaseRepositoryTest.Leases.FirstOrDefault( Function( l ) l.Id = leaseId )
			If IsNothing( leaseToAdd ) Then Return "Không tìm thấy phiếu thuê phòng tương ứng"

			Dim customerCategory = LeaseRepositoryTest.CustomerCategories.FirstOrDefault( Function( c ) c.Id = customerCategoryId )
			If IsNothing( customerCategory ) Then Return "Không tìm được loại khách tương ứng"

			leaseToAdd.LeaseDetails.Add( New LeaseDetail With {.Id=idStr,
				                           .CustomerName=customerName,
				                           .LicenseId=customerLicenseId,
				                           .Address=customerAddress,
				                           .CustomerCategory=customerCategory} )
			Return String.Empty
		End Function

		Public Async Function ExecuteAsync( leaseId As String,
		                                    customerName As String,
		                                    customerLicenseId As String,
		                                    customerAddress As String,
		                                    customerCategoryId As String ) As Task(Of String) _
			Implements ICreateLeaseDetailFactory.ExecuteAsync
			Return Await Task.Run( Function() Execute( leaseId,
			                                           customerName,
			                                           customerLicenseId,
			                                           customerAddress,
			                                           customerCategoryId ) )
		End Function
	End Class
End Namespace