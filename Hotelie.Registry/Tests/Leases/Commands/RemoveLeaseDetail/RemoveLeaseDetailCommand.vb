Imports Hotelie.Application.Leases.Commands.RemoveLeaseDetail
Imports Hotelie.Domain.Leases

Namespace Tests.Leases.Commands.RemoveLeaseDetail
	Public Class RemoveLeaseDetailCommand
		Implements IRemoveLeaseDetailCommand

		Public Function Execute( id As String ) As String Implements IRemoveLeaseDetailCommand.Execute
			Dim leaseToRemove As Lease = Nothing
			Dim leaseDetailToRemove As LeaseDetail = Nothing

			For Each lease As Lease In LeaseRepositoryTest.Leases
				leaseToRemove = lease
				leaseDetailToRemove = Nothing
				For Each detail As LeaseDetail In lease.LeaseDetails
					If String.Equals( id, detail.Id )
						leaseDetailToRemove = detail
						Exit For
					End If
				Next
				If leaseDetailToRemove IsNot Nothing Then Exit For
			Next

			If leaseToRemove IsNot Nothing And leaseDetailToRemove IsNot Nothing
				leaseToRemove.LeaseDetails.Remove( leaseDetailToRemove )
				Return String.Empty
			End If

			Return "Không tìm thấy phiếu thuê và chi tiết tương ứng cần xóa"
		End Function

		Public Async Function ExecuteAsync( id As String ) As Task(Of String) _
			Implements IRemoveLeaseDetailCommand.ExecuteAsync
			Return Await Task.Run( Function() Execute( id ) )
		End Function
	End Class
End Namespace