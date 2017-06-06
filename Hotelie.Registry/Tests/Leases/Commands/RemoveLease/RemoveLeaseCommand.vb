Imports Hotelie.Application.Leases.Commands.RemoveLease

Namespace Tests.Leases.Commands.RemoveLease
	Public Class RemoveLeaseCommand
		Implements IRemoveLeaseCommand

		Public Function Execute( id As String ) As String Implements IRemoveLeaseCommand.Execute
			Dim leaseToRemove = LeaseRepositoryTest.Leases.FirstOrDefault( Function( l ) l.Id = id )
			If IsNothing( leaseToRemove ) Then Return "Không tìm thấy phiếu thuê phòng cần xóa"

			LeaseRepositoryTest.Leases.Remove( leaseToRemove )
			Return String.Empty
		End Function

		Public Async Function ExecuteAsync( id As String ) As Task(Of String) Implements IRemoveLeaseCommand.ExecuteAsync
			Return Await Task.Run( Function() Execute( id ) )
		End Function
	End Class
End Namespace