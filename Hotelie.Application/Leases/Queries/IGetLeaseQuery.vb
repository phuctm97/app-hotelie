Imports Hotelie.Application.Leases.Models

Namespace Leases.Queries
	Public Interface IGetLeaseQuery
		Function Execute( id As String ) As ILeaseModel

		Function ExecuteAsync( id As String ) As Task(Of ILeaseModel)
	End Interface
End Namespace