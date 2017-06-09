Imports Hotelie.Application.Leases.Models

Namespace Common.Infrastructure
	Public Interface ILeasePresenter
		Sub Reload()

		Function ReloadAsync() As Task

		Sub OnLeaseUpdated( model As ILeaseModel )
	End Interface
End Namespace