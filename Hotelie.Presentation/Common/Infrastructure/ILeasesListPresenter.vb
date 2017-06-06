Imports Hotelie.Application.Leases.Queries.GetLeaseData

Namespace Common.Infrastructure
	Public Interface ILeasesListPresenter
		Sub OnLeaseAdded( model As LeaseModel )

		Sub OnLeaseUpdated( model As LeaseModel )

		Sub OnLeaseRemoved( id As String )
	End Interface
End Namespace