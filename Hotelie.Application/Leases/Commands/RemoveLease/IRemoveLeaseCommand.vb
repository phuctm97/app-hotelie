Namespace Leases.Commands.RemoveLease
	Public Interface IRemoveLeaseCommand
		Function Execute( id As String ) As String

		Function ExecuteAsync( id As String ) As Task(Of String)
	End Interface
End Namespace