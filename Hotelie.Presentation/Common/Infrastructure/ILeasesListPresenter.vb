Imports Hotelie.Application.Leases.Models

Namespace Common.Infrastructure
	Public Interface ILeasesListPresenter
		Sub OnLeaseAdded( model As ILeaseModel )

		Sub OnLeaseUpdated( model As ILeaseModel )

		Sub OnLeaseRemoved( id As String )
	End Interface
End Namespace