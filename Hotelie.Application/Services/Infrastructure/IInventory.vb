Namespace Services.Infrastructure
	Public Interface IInventory
		Sub Track( childInventory As Object,
		           code As Integer )

		Sub Reload()

		Function ReloadAsync() As Task

		Sub OnRoomAdded( id As String )

		Function OnRoomAddedAsync( id As String ) As Task

		Sub OnRoomRemoved( id As String )

		Function OnRoomRemovedAsync( id As String ) As Task

		Sub OnRoomUpdated( id As String )

		Function OnRoomUpdatedAsync( id As String ) As Task

		Sub OnLeaseAdded( id As String )

		Function OnLeaseAddedAsync( id As String ) As Task

		Sub OnLeaseRemoved( id As String )

		Function OnLeaseRemovedAsync( id As String ) As Task

		Sub OnLeaseUpdated( id As String )

		Function OnLeaseUpdatedAsync( id As String ) As Task

		Sub OnBillAdded( id As String )

		Function OnBillAddedAsync( id As String ) As Task

		Sub OnBillRemoved( id As String )

		Function OnBillRemovedAsync( id As String ) As Task
	End Interface
End Namespace