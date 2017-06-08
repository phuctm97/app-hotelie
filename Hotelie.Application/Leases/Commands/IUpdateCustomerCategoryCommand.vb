Namespace Leases.Commands
	Public Interface IUpdateCustomerCategoryCommand
		Function Execute( id As String,
		                  name As String,
		                  coefficient As Double ) As String

		Function ExecuteAsync( id As String,
		                       name As String,
		                       coefficient As Double ) As Task(Of String)
	End Interface
End Namespace