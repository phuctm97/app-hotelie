Imports Hotelie.Application.Leases.Commands.UpdateLease

Namespace Tests.Leases.Commands.UpdateLease
	Public Class UpdateLeaseCommand
		Implements IUpdateLeaseCommand

		Public Function Execute( id As String,
		                         roomId As String,
		                         expectedCheckoutDate As DateTime ) As String Implements IUpdateLeaseCommand.Execute
			Dim leaseToUpdate = LeaseRepositoryTest.Leases.FirstOrDefault( Function( l )l.Id = id )
			If IsNothing( leaseToUpdate ) Then Return "Không tìm thấy phiếu thuê phòng cần cập nhật"

			If leaseToUpdate.Room.Id <> roomId
				Dim newRoom = RoomRepositoryTest.Rooms.FirstOrDefault( Function( r )r.Id = roomId )
				If IsNothing( newRoom ) Then Return "Không tìm thấy phòng tương ứng để cập nhật"

				leaseToUpdate.Room = newRoom
			End If

			leaseToUpdate.ExpectedCheckoutDate = expectedCheckoutDate
			Return String.Empty
		End Function

		Public Async Function ExecuteAsync( id As String,
		                                    roomId As String,
		                                    expectedCheckoutDate As DateTime ) As Task(Of String) _
			Implements IUpdateLeaseCommand.ExecuteAsync
			Return Await Task.Run( Function() Execute( id, roomId, expectedCheckoutDate ) )
		End Function
	End Class
End Namespace