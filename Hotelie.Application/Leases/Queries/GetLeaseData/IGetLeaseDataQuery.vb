Namespace Leases.Queries.GetLeaseData
	Public Interface IGetLeaseDataQuery
		Function Execute( id As String ) As LeaseModel

		Function ExecuteAsync( id As String ) As Task(Of LeaseModel)
	End Interface
End Namespace