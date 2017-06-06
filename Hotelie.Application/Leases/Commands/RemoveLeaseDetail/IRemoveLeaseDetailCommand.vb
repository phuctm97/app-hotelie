Namespace Leases.Commands.RemoveLeaseDetail
	Public Interface IRemoveLeaseDetailCommand
		Function Execute( id As String ) As String

		Function ExecuteAsync( id As String ) As Task(Of String)
	End Interface
End Namespace