Imports System.Runtime.CompilerServices
Imports Caliburn.Micro

Namespace Infrastructure
	Public Interface IInventory
		Sub Track( roomCollectionPresenter As IRoomCollectionPresenter )

		Sub OnRoomAdded( id As String,
		                 name As String,
		                 categoryId As String,
		                 note As String )

		Sub OnRoomUpdated( id As String,
		                   name As String,
		                   categoryId As String,
		                   note As String,
		                   state As Integer )
	End Interface

	Module InventoryExtensions
		< Extension >
		Public Sub RegisterInventory( roomCollectionPresenter As IRoomCollectionPresenter )
			IoC.Get(Of IInventory).Track( roomCollectionPresenter )
		End Sub
	End Module
End Namespace