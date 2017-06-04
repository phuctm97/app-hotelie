
Imports System.Runtime.CompilerServices
Imports Caliburn.Micro
Imports Hotelie.Application.Services.Infrastructure

Namespace Infrastructure
	Public Enum ChildInventoryType
		RoomCollectionPresenter
	End Enum

	Public Class Inventory
		Implements IInventory

		Private ReadOnly _roomCollectionPresenters As List(Of IRoomCollectionPresenter)

		Public Sub New()
			_roomCollectionPresenters = New List(Of IRoomCollectionPresenter)()
		End Sub

		Public Sub OnRoomAdded( id As String,
		                        name As String,
		                        categoryId As String,
		                        note As String ) Implements IInventory.OnRoomAdded
			For Each roomCollectionPresenter As IRoomCollectionPresenter In _roomCollectionPresenters
				roomCollectionPresenter.OnRoomAdded( id, name, categoryId, note )
			Next
		End Sub

		Public Sub OnRoomRemoved( id As String ) Implements IInventory.OnRoomRemoved
			For Each roomCollectionPresenter As IRoomCollectionPresenter In _roomCollectionPresenters
				roomCollectionPresenter.OnRoomRemoved( id )
			Next
		End Sub

		Public Sub OnRoomUpdated( id As String,
		                          name As String,
		                          categoryId As String,
		                          note As String,
		                          state As Int32 ) Implements IInventory.OnRoomUpdated
			For Each roomCollectionPresenter As IRoomCollectionPresenter In _roomCollectionPresenters
				roomCollectionPresenter.OnRoomUpdated( id, name, categoryId, note, state )
			Next
		End Sub

		Public Sub Track( childInventory As Object,
		                  code As Integer ) Implements IInventory.Track
			Select Case code
				Case ChildInventoryType.RoomCollectionPresenter
					_roomCollectionPresenters.Add( childInventory )
			End Select
		End Sub
	End Class

	Module InventoryExtensions
		< Extension >
		Public Sub RegisterInventory( roomCollectionPresenter As IRoomCollectionPresenter )
			IoC.Get(Of IInventory).Track( roomCollectionPresenter, ChildInventoryType.RoomCollectionPresenter )
		End Sub
	End Module
End Namespace
