
Imports System.Runtime.CompilerServices
Imports Caliburn.Micro
Imports Hotelie.Application.Rooms.Queries.GetRoomData
Imports Hotelie.Application.Services.Infrastructure

Namespace Infrastructure
	Public Enum ChildInventoryType
		RoomsListPresenter
		RoomPresenter
	End Enum

	Public Class Inventory
		Implements IInventory

		' Dependencies
		Private ReadOnly _getRoomDataQuery As IGetRoomDataQuery

		Private ReadOnly _roomsListPresenters As List(Of IRoomsListPresenter)
		Private ReadOnly _roomPresenters As List(Of IRoomPresenter)

		Public Sub New( getRoomDataQuery As IGetRoomDataQuery )
			_getRoomDataQuery = getRoomDataQuery
			_roomsListPresenters = New List(Of IRoomsListPresenter)()
			_roomPresenters = New List(Of IRoomPresenter)()
		End Sub

		Public Sub OnRoomAdded( id As String ) Implements IInventory.OnRoomAdded
			Dim model = _getRoomDataQuery.Execute( id )
			If IsNothing( model ) Then Throw New EntryPointNotFoundException()

			For Each presenter As IRoomsListPresenter In _roomsListPresenters
				presenter.OnRoomAdded( model )
			Next
		End Sub

		Public Async Function OnRoomAddedAsync( id As String ) As Task Implements IInventory.OnRoomAddedAsync
			Dim model = Await _getRoomDataQuery.ExecuteAsync( id )
			If IsNothing( model ) Then Throw New EntryPointNotFoundException()

			For Each presenter As IRoomsListPresenter In _roomsListPresenters
				presenter.OnRoomAdded( model )
				Await Task.Delay( 25 )
			Next
		End Function

		Public Sub OnRoomUpdated( id As String ) Implements IInventory.OnRoomUpdated
			Dim model = _getRoomDataQuery.Execute( id )
			If IsNothing( model ) Then Throw New EntryPointNotFoundException()

			For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
				roomCollectionPresenter.OnRoomUpdated( model )
			Next

			For Each roomPresenter As IRoomPresenter In _roomPresenters
				roomPresenter.OnRoomUpdated( model )
			Next
		End Sub

		Public Async Function OnRoomUpdatedAsync( id As String ) As Task Implements IInventory.OnRoomUpdatedAsync
			Dim model = Await _getRoomDataQuery.ExecuteAsync( id )
			If IsNothing( model ) Then Throw New EntryPointNotFoundException()

			For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
				roomCollectionPresenter.OnRoomUpdated( model )
				Await Task.Delay( 25 )
			Next

			For Each roomPresenter As IRoomPresenter In _roomPresenters
				roomPresenter.OnRoomUpdated( model )
				Await Task.Delay( 25 )
			Next
		End Function

		Public Sub OnRoomRemoved( id As String ) Implements IInventory.OnRoomRemoved
			For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
				roomCollectionPresenter.OnRoomRemoved( id )
			Next
		End Sub

		Public Async Function OnRoomRemovedAsync( id As String ) As Task Implements IInventory.OnRoomRemovedAsync
			For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
				roomCollectionPresenter.OnRoomRemoved( id )
				Await Task.Delay( 25 )
			Next
		End Function

		Public Sub Track( childInventory As Object,
		                  code As Integer ) Implements IInventory.Track
			Select Case code
				Case ChildInventoryType.RoomsListPresenter
					_roomsListPresenters.Add( childInventory )
				Case ChildInventoryType.RoomPresenter
					_roomPresenters.Add( childInventory )
			End Select
		End Sub
	End Class

	Module InventoryExtensions
		< Extension >
		Public Sub RegisterInventory( roomsListPresenter As IRoomsListPresenter )
			IoC.Get(Of IInventory).Track( roomsListPresenter, ChildInventoryType.RoomsListPresenter )
		End Sub

		< Extension >
		Public Sub RegisterInventory( roomPresenter As IRoomPresenter )
			IoC.Get(Of IInventory).Track( roomPresenter, ChildInventoryType.RoomPresenter )
		End Sub
	End Module
End Namespace
