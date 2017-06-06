Namespace Services.Infrastructure
	Public Interface IInventory
		Sub Track( childInventory As Object,
		           code As Integer )

		Sub OnRoomAdded( id As String )

		Function OnRoomAddedAsync( id As String ) As Task

		Sub OnRoomRemoved( id As String )

		Function OnRoomRemovedAsync( id As String ) As Task

		Sub OnRoomUpdated( id As String )

		Function OnRoomUpdatedAsync( id As String ) As Task
	End Interface
End Namespace