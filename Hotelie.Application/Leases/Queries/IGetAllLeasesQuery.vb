Imports Hotelie.Application.Leases.Models

Namespace Leases.Queries
	Public Interface IGetAllLeasesQuery
		Function Execute() As IEnumerable(Of ILeaseModel)

		Function ExecuteAsync() As Task(Of IEnumerable(Of ILeaseModel))
	End Interface
End Namespace