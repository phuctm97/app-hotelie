Namespace Leases.Factories
	Public Interface ICreateCustomerCategoryFactory
		Function Execute(name As String, coefficient As Double) As String

		Function ExecuteAsync(name As String, coefficient As Double) As Task(Of String)

	End Interface
End Namespace