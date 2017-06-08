
Imports System.Runtime.CompilerServices
Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Infrastructure

Namespace Common.Infrastructure
    Public Enum ChildInventoryType
        RoomsListPresenter
        RoomPresenter
        LeasesListPresenter
    End Enum

    Public Class Inventory
        Implements IInventory

        ' Dependencies
        Private ReadOnly _getRoomQuery As IGetRoomQuery
        Private ReadOnly _getLeaseQuery As IGetLeaseQuery

        Private ReadOnly _roomsListPresenters As List(Of IRoomsListPresenter)

        Private ReadOnly _roomPresenters As List(Of IRoomPresenter)

        Private ReadOnly _leasesListPresenter As List(Of ILeasesListPresenter)

        Public Sub New(getRoomQuery As IGetRoomQuery,
                       getLeaseQuery As IGetLeaseQuery)
            _getRoomQuery = getRoomQuery
            _getLeaseQuery = getLeaseQuery
            _roomsListPresenters = New List(Of IRoomsListPresenter)()
            _roomPresenters = New List(Of IRoomPresenter)()
            _leasesListPresenter = New List(Of ILeasesListPresenter)()
        End Sub

        Public Sub Track(childInventory As Object,
                         code As Integer) Implements IInventory.Track
            Select Case code
                Case ChildInventoryType.RoomsListPresenter
                    _roomsListPresenters.Add(childInventory)
                Case ChildInventoryType.RoomPresenter
                    _roomPresenters.Add(childInventory)
                Case ChildInventoryType.LeasesListPresenter
                    _leasesListPresenter.Add(childInventory)
            End Select
        End Sub

        Public Sub OnRoomAdded(id As String) Implements IInventory.OnRoomAdded
            Dim model = _getRoomQuery.Execute(id)
            If IsNothing(model) Then Throw New EntryPointNotFoundException()

            For Each presenter As IRoomsListPresenter In _roomsListPresenters
                presenter.OnRoomAdded(model)
            Next
        End Sub

        Public Async Function OnRoomAddedAsync(id As String) As Task Implements IInventory.OnRoomAddedAsync
            Dim model = Await _getRoomQuery.ExecuteAsync(id)
            If IsNothing(model) Then Throw New EntryPointNotFoundException()

            For Each presenter As IRoomsListPresenter In _roomsListPresenters
                presenter.OnRoomAdded(model)
                Await Task.Delay(25)
            Next
        End Function

        Public Sub OnRoomUpdated(id As String) Implements IInventory.OnRoomUpdated
            Dim model = _getRoomQuery.Execute(id)
            If IsNothing(model) Then Throw New EntryPointNotFoundException()

            For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
                roomCollectionPresenter.OnRoomUpdated(model)
            Next

            For Each roomPresenter As IRoomPresenter In _roomPresenters
                roomPresenter.OnRoomUpdated(model)
            Next
        End Sub

        Public Async Function OnRoomUpdatedAsync(id As String) As Task Implements IInventory.OnRoomUpdatedAsync
            Dim model = Await _getRoomQuery.ExecuteAsync(id)
            If IsNothing(model) Then Throw New EntryPointNotFoundException()

            For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
                roomCollectionPresenter.OnRoomUpdated(model)
                Await Task.Delay(25)
            Next

            For Each roomPresenter As IRoomPresenter In _roomPresenters
                roomPresenter.OnRoomUpdated(model)
                Await Task.Delay(25)
            Next
        End Function

        Public Sub OnRoomRemoved(id As String) Implements IInventory.OnRoomRemoved
            For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
                roomCollectionPresenter.OnRoomRemoved(id)
            Next
        End Sub

        Public Async Function OnRoomRemovedAsync(id As String) As Task Implements IInventory.OnRoomRemovedAsync
            For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
                roomCollectionPresenter.OnRoomRemoved(id)
                Await Task.Delay(25)
            Next
        End Function

        Public Sub OnLeaseAdded(id As String) Implements IInventory.OnLeaseAdded
            Dim model = _getLeaseQuery.Execute(id)
            If IsNothing(model) Then Throw New EntryPointNotFoundException()

            For Each presenter As ILeasesListPresenter In _leasesListPresenter
                presenter.OnLeaseAdded(model)
            Next
        End Sub

        Public Async Function OnLeaseAddedAsync(id As String) As Task Implements IInventory.OnLeaseAddedAsync
            Dim model = Await _getLeaseQuery.ExecuteAsync(id)
            If IsNothing(model) Then Throw New EntryPointNotFoundException()

            For Each presenter As ILeasesListPresenter In _leasesListPresenter
                presenter.OnLeaseAdded(model)
                Await Task.Delay(25)
            Next
        End Function

        Public Sub OnLeaseRemoved(id As String) Implements IInventory.OnLeaseRemoved
            For Each presenter As ILeasesListPresenter In _leasesListPresenter
                presenter.OnLeaseRemoved(id)
            Next
        End Sub

        Public Async Function OnLeaseRemovedAsync(id As String) As Task Implements IInventory.OnLeaseRemovedAsync
            For Each presenter As ILeasesListPresenter In _leasesListPresenter
                presenter.OnLeaseRemoved(id)
                Await Task.Delay(25)
            Next
        End Function

        Public Sub OnLeaseUpdated(id As String) Implements IInventory.OnLeaseUpdated
            Dim model = _getLeaseQuery.Execute(id)
            If IsNothing(model) Then Throw New EntryPointNotFoundException()

            For Each presenter As ILeasesListPresenter In _leasesListPresenter
                presenter.OnLeaseUpdated(model)
            Next
        End Sub

        Public Async Function OnLeaseUpdatedAsync(id As String) As Task Implements IInventory.OnLeaseUpdatedAsync
            Dim model = Await _getLeaseQuery.ExecuteAsync(id)
            If IsNothing(model) Then Throw New EntryPointNotFoundException()

            For Each presenter As ILeasesListPresenter In _leasesListPresenter
                presenter.OnLeaseUpdated(model)
                Await Task.Delay(25)
            Next
        End Function
    End Class

    Module InventoryExtensions
        <Extension>
        Public Sub RegisterInventory(roomsListPresenter As IRoomsListPresenter)
            IoC.Get (Of IInventory).Track(roomsListPresenter, ChildInventoryType.RoomsListPresenter)
        End Sub

        <Extension>
        Public Sub RegisterInventory(roomPresenter As IRoomPresenter)
            IoC.Get (Of IInventory).Track(roomPresenter, ChildInventoryType.RoomPresenter)
        End Sub

        <Extension>
        Public Sub RegisterInventory(leasesListPresenter As ILeasesListPresenter)
            IoC.Get (Of IInventory).Track(leasesListPresenter, ChildInventoryType.LeasesListPresenter)
        End Sub
    End Module
End Namespace
