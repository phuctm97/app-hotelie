Imports Hotelie.Application.Leases.Models

Namespace Common.Infrastructure
	Public Interface ILeasePresenter
		Sub OnLeaseUpdated(model As ILeaseModel)
	End Interface
End Namespace