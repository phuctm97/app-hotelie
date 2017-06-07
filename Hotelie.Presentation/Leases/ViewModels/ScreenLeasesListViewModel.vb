Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Queries.GetLeaseData
Imports Hotelie.Application.Leases.Queries.GetLeaseDetailData
Imports Hotelie.Application.Leases.Queries.GetLeasesList
Imports Hotelie.Application.Rooms.Queries.GetRoomData
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Common.Infrastructure
Imports Hotelie.Presentation.Infrastructure

Namespace Leases.ViewModels
	Public Class ScreenLeasesListViewModel
		Implements INeedWindowModals
		Implements ILeasesListPresenter
		Implements IRoomPresenter

		' Dependencies
		Private ReadOnly _getLeasesListQuery As IGetLeasesListQuery

		' Bind
		Public ReadOnly Property Leases As IObservableCollection(Of LeasesListItemModel)

		Public Sub New( getLeasesListQuery As IGetLeasesListQuery )
			_getLeasesListQuery = getLeasesListQuery
			CType(Me, IRoomPresenter).RegisterInventory()
			CType(Me, ILeasesListPresenter).RegisterInventory()

			Leases = New BindableCollection(Of LeasesListItemModel)()
		End Sub

		Public Sub Init()
			InitLeases()
		End Sub

		Public Async Function InitAsync() As Task
			Await InitLeasesAsync()
		End Function

		Private Sub InitLeases()
			Leases.Clear()
			Leases.AddRange( _getLeasesListQuery.Execute() )
		End Sub

		Private Async Function InitLeasesAsync() As Task
			Leases.Clear()
			Leases.AddRange( Await _getLeasesListQuery.ExecuteAsync() )
		End Function

		' Infrastructure
		Public Sub OnLeaseAdded( model As LeaseModel ) Implements ILeasesListPresenter.OnLeaseAdded
			If Leases.Any( Function( l ) l.Id = model.Id )
				Throw New DuplicateWaitObjectException()
			End If

			' add new lease item
			Dim leaseListItem = New LeasesListItemModel With {
				    .Id = model.Id,
				    .CheckinDate = model.CheckinDate,
				    .ExpectedCheckoutDate = model.ExpectedCheckoutDate,
				    .RoomName = model.Room.Name,
				    .RoomCategoryName = model.Room.Category.Name,
				    .TotalExpense = 0}

			' add lease details
			For Each detailModel As LeaseDetailModel In model.Details
				leaseListItem.Details.Add( New LeasesListItemDetailModel With {
					                         .CustomerName = detailModel.CustomerName} )
			Next

			Leases.Add( leaseListItem )
		End Sub

		Public Sub OnLeaseUpdated( model As LeaseModel ) Implements ILeasesListPresenter.OnLeaseUpdated
			Dim leaseToUpdate = Leases.FirstOrDefault( Function( l ) l.Id = model.Id )
			If IsNothing( leaseToUpdate ) Then Throw New EntryPointNotFoundException()

			' update lease item
			leaseToUpdate.CheckinDate = model.CheckinDate
			leaseToUpdate.ExpectedCheckoutDate = model.ExpectedCheckoutDate
			leaseToUpdate.RoomName = model.Room.Name
			leaseToUpdate.RoomCategoryName = model.Room.Category.Name
			leaseToUpdate.TotalExpense = 0

			' update and add lease details
			For Each detailModel As LeaseDetailModel In model.Details
				Dim item = leaseToUpdate.Details.FirstOrDefault( Function( i ) i.Id = detailModel.Id )
				If IsNothing( item )
					item = New LeasesListItemDetailModel With {.Id = detailModel.Id, .CustomerName = detailModel.CustomerName}
					leaseToUpdate.Details.Add( item )
				Else
					item.CustomerName = detailModel.CustomerName
				End If
			Next

			' delete old details
			Dim itemDetailsToDelete = New List(Of LeasesListItemDetailModel)()
			For Each itemDetail As LeasesListItemDetailModel In leaseToUpdate.Details
				If Not model.Details.Any( Function( d ) d.Id = itemDetail.Id )
					itemDetailsToDelete.Add( itemDetail )
				End If
			Next
			For Each itemDetailToDelete As LeasesListItemDetailModel In itemDetailsToDelete
				leaseToUpdate.Details.Remove( itemDetailToDelete )
			Next
		End Sub

		Public Sub OnLeaseRemoved( id As String ) Implements ILeasesListPresenter.OnLeaseRemoved
			Dim leaseToRemove = Leases.FirstOrDefault( Function( l ) l.Id = id )
			If IsNothing( leaseToRemove ) Then Throw New EntryPointNotFoundException()

			Leases.Remove( leaseToRemove )
		End Sub

		Public Sub OnRoomUpdated( model As RoomModel ) Implements IRoomPresenter.OnRoomUpdated
			Dim itemToUpdate = Leases.FirstOrDefault( Function( l ) l.RoomId = model.Id )
			If IsNothing( itemToUpdate ) Then Return

			itemToUpdate.RoomName = model.Name
			itemToUpdate.RoomCategoryName = model.Category.Name
		End Sub
	End Class
End Namespace
