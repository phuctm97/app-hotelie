Namespace Leases.Commands.UpdateLease
	Public Interface IUpdateLeaseCommand
		Function Execute( id As String,
		                  roomId As String,
		                  expectedCheckoutDate As Date ) As String

		Function ExecuteAsync( id As String,
		                       roomId As String,
		                       expectedCheckoutDate As Date ) As Task(Of String)
	End Interface
End Namespace