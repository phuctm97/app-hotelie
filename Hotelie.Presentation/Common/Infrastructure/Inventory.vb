
Imports System.Runtime.CompilerServices
Imports Caliburn.Micro
Imports Hotelie.Application.Bills.Queries
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common.Controls

Namespace Common.Infrastructure
	Public Enum ChildInventoryType
		RoomsListPresenter
		RoomPresenter
		LeasesListPresenter
		LeasePresenter
		BillsListPresenter
	End Enum

	Public Class Inventory
		Implements IInventory

		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _getRoomQuery As IGetRoomQuery
		Private ReadOnly _getLeaseQuery As IGetLeaseQuery
		Private ReadOnly _getBillQuery As IGetBillQuery

		Private ReadOnly _roomsListPresenters As List(Of IRoomsListPresenter)

		Private ReadOnly _roomPresenters As List(Of IRoomPresenter)

		Private ReadOnly _leasesListPresenters As List(Of ILeasesListPresenter)

		Private ReadOnly _leasePresenters As List(Of ILeasePresenter)

		Private ReadOnly _billsListPresenters As List(Of IBillsListPresenter)

		Public Sub New( getRoomQuery As IGetRoomQuery,
		                getLeaseQuery As IGetLeaseQuery,
		                getBillQuery As IGetBillQuery )
			_getRoomQuery = getRoomQuery
			_getLeaseQuery = getLeaseQuery
			_getBillQuery = getBillQuery
			_roomsListPresenters = New List(Of IRoomsListPresenter)()
			_roomPresenters = New List(Of IRoomPresenter)()
			_leasesListPresenters = New List(Of ILeasesListPresenter)()
			_leasePresenters = new List(Of ILeasePresenter)()
			_billsListPresenters = new List(Of IBillsListPresenter)()
		End Sub

		Public Sub Track( childInventory As Object,
		                  code As Integer ) Implements IInventory.Track
			Select Case code
				Case ChildInventoryType.RoomsListPresenter
					_roomsListPresenters.Add( childInventory )
				Case ChildInventoryType.RoomPresenter
					_roomPresenters.Add( childInventory )
				Case ChildInventoryType.LeasesListPresenter
					_leasesListPresenters.Add( childInventory )
				Case ChildInventoryType.LeasePresenter
					_leasePresenters.Add( childInventory )
				Case ChildInventoryType.BillsListPresenter
					_billsListPresenters.Add( childInventory )
			End Select
		End Sub

		Public Sub OnRoomAdded( id As String ) Implements IInventory.OnRoomAdded
			Dim model = _getRoomQuery.Execute( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Cơ sở dữ liệu chưa được cập nhật!" )
				Return
			End If

			For Each presenter As IRoomsListPresenter In _roomsListPresenters
				presenter.OnRoomAdded( model )
			Next
		End Sub

		Public Async Function OnRoomAddedAsync( id As String ) As Task Implements IInventory.OnRoomAddedAsync
			Dim model = Await _getRoomQuery.ExecuteAsync( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Cơ sở dữ liệu chưa được cập nhật!" )
				Return
			End If

			For Each presenter As IRoomsListPresenter In _roomsListPresenters
				presenter.OnRoomAdded( model )
			Next
		End Function

		Public Sub OnRoomUpdated( id As String ) Implements IInventory.OnRoomUpdated
			Dim model = _getRoomQuery.Execute( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Không tìm thấy phòng vừa cập nhật trong cơ sở dữ liệu!" )
				Return
			End If

			For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
				roomCollectionPresenter.OnRoomUpdated( model )
			Next

			For Each roomPresenter As IRoomPresenter In _roomPresenters
				roomPresenter.OnRoomUpdated( model )
			Next
		End Sub

		Public Async Function OnRoomUpdatedAsync( id As String ) As Task Implements IInventory.OnRoomUpdatedAsync
			Dim model = Await _getRoomQuery.ExecuteAsync( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Không tìm thấy phòng vừa cập nhật trong cơ sở dữ liệu!" )
				Return
			End If

			For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
				roomCollectionPresenter.OnRoomUpdated( model )
			Next

			For Each roomPresenter As IRoomPresenter In _roomPresenters
				roomPresenter.OnRoomUpdated( model )
			Next
		End Function

		Public Sub OnRoomRemoved( id As String ) Implements IInventory.OnRoomRemoved
			For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
				roomCollectionPresenter.OnRoomRemoved( id )
			Next
		End Sub

		Public Function OnRoomRemovedAsync( id As String ) As Task Implements IInventory.OnRoomRemovedAsync
			For Each roomCollectionPresenter As IRoomsListPresenter In _roomsListPresenters
				roomCollectionPresenter.OnRoomRemoved( id )
			Next
			Return Task.FromResult( True )
		End Function

		Public Sub OnLeaseAdded( id As String ) Implements IInventory.OnLeaseAdded
			Dim model = _getLeaseQuery.Execute( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Cơ sở dữ liệu chưa được cập nhật!" )
				Return
			End If

			For Each presenter As ILeasesListPresenter In _leasesListPresenters
				presenter.OnLeaseAdded( model )
			Next
		End Sub

		Public Async Function OnLeaseAddedAsync( id As String ) As Task Implements IInventory.OnLeaseAddedAsync
			Dim model = Await _getLeaseQuery.ExecuteAsync( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Cơ sở dữ liệu chưa được cập nhật!" )
				Return
			End If

			For Each presenter As ILeasesListPresenter In _leasesListPresenters
				presenter.OnLeaseAdded( model )
			Next
		End Function

		Public Sub OnLeaseRemoved( id As String ) Implements IInventory.OnLeaseRemoved
			For Each presenter As ILeasesListPresenter In _leasesListPresenters
				presenter.OnLeaseRemoved( id )
			Next
		End Sub

		Public Function OnLeaseRemovedAsync( id As String ) As Task Implements IInventory.OnLeaseRemovedAsync
			For Each presenter As ILeasesListPresenter In _leasesListPresenters
				presenter.OnLeaseRemoved( id )
			Next
			Return Task.FromResult( True )
		End Function

		Public Sub OnLeaseUpdated( id As String ) Implements IInventory.OnLeaseUpdated
			Dim model = _getLeaseQuery.Execute( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Không tìm thấy phiếu thuê phòng trong cơ sở dữ liệu!" )
				Return
			End If

			For Each presenter As ILeasesListPresenter In _leasesListPresenters
				presenter.OnLeaseUpdated( model )
			Next

			For Each presenter As ILeasePresenter In _leasePresenters
				presenter.OnLeaseUpdated( model )
			Next
		End Sub

		Public Async Function OnLeaseUpdatedAsync( id As String ) As Task Implements IInventory.OnLeaseUpdatedAsync
			Dim model = Await _getLeaseQuery.ExecuteAsync( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Không tìm thấy phiếu thuê phòng trong cơ sở dữ liệu!" )
				Return
			End If

			For Each presenter As ILeasesListPresenter In _leasesListPresenters
				presenter.OnLeaseUpdated( model )
			Next

			For Each presenter As ILeasePresenter In _leasePresenters
				presenter.OnLeaseUpdated( model )
			Next
		End Function

		Public Sub OnBillAdded( id As String ) Implements IInventory.OnBillAdded
			Dim model = _getBillQuery.Execute( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Cơ sở dữ liệu chưa được cập nhật!" )
				Return
			End If

			For Each presenter As IBillsListPresenter In _billsListPresenters
				presenter.OnBillAdded( model )
			Next
		End Sub

		Public Async Function OnBillAddedAsync( id As String ) As Task Implements IInventory.OnBillAddedAsync
			Dim model = Await _getBillQuery.ExecuteAsync( id )
			If IsNothing( model )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Error,
				                              "Cơ sở dữ liệu chưa được cập nhật!" )
				Return
			End If

			For Each presenter As IBillsListPresenter In _billsListPresenters
				presenter.OnBillAdded( model )
			Next
		End Function

		Public Sub OnBillRemoved( id As String ) Implements IInventory.OnBillRemoved
			For Each presenter As IBillsListPresenter In _billsListPresenters
				presenter.OnBillRemoved( id )
			Next
		End Sub

		Public Function OnBillRemovedAsync( id As String ) As Task Implements IInventory.OnBillRemovedAsync
			For Each presenter As IBillsListPresenter In _billsListPresenters
				presenter.OnBillRemoved( id )
			Next

			Return Task.FromResult( True )
		End Function

		Public Sub Reload() Implements IInventory.Reload
			For Each presenter In _roomPresenters
				presenter.Reload()
			Next
			For Each presenter In _roomsListPresenters
				presenter.Reload()
			Next
			For Each presenter In _leasePresenters
				presenter.Reload()
			Next
			For Each presenter In _leasesListPresenters
				presenter.Reload()
			Next
			For Each presenter In _billsListPresenters
				presenter.Reload()
			Next
		End Sub

		Public Async Function ReloadAsync() As Task Implements IInventory.ReloadAsync
			For Each presenter In _roomPresenters
				Await presenter.ReloadAsync()
			Next
			For Each presenter In _roomsListPresenters
				Await presenter.ReloadAsync()
			Next
			For Each presenter In _leasePresenters
				Await presenter.ReloadAsync()
			Next
			For Each presenter In _leasesListPresenters
				Await presenter.ReloadAsync()
			Next
			For Each presenter In _billsListPresenters
				Await presenter.ReloadAsync()
			Next
		End Function
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

		< Extension >
		Public Sub RegisterInventory( leasesListPresenter As ILeasesListPresenter )
			IoC.Get(Of IInventory).Track( leasesListPresenter, ChildInventoryType.LeasesListPresenter )
		End Sub

		< Extension >
		Public Sub RegisterInventory( leasePresenter As ILeasePresenter )
			IoC.Get(Of IInventory).Track( leasePresenter, ChildInventoryType.LeasePresenter )
		End Sub

		< Extension >
		Public Sub RegisterInventory( billsListPresenter As IBillsListPresenter )
			IoC.Get(Of IInventory).Track( billsListPresenter, ChildInventoryType.BillsListPresenter )
		End Sub
	End Module
End Namespace
