Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Models
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Common.Infrastructure
Imports Hotelie.Presentation.Leases.Models

Namespace Leases.ViewModels
	Public Class ScreenLeasesListViewModel
		Implements IChild(Of LeasesWorkspaceViewModel)
		Implements INeedWindowModals
		Implements ILeasesListPresenter
		Implements IRoomPresenter

		' Dependencies
		Private ReadOnly _getAllLeasesQuery As IGetAllLeasesQuery

		' Parent 
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As LeasesWorkspaceViewModel Implements IChild(Of LeasesWorkspaceViewModel).Parent
			get
				Return TryCast(Parent, LeasesWorkspaceViewModel)
			End Get
			Set
				Parent = value
			End Set
		End Property

		Public Sub New( workspace As LeasesWorkspaceViewModel,
		                getAllLeasesQuery As IGetAllLeasesQuery )
			ParentWorkspace = workspace
			_getAllLeasesQuery = getAllLeasesQuery
			TryCast(Me, IRoomPresenter).RegisterInventory()
			TryCast(Me, ILeasesListPresenter).RegisterInventory()

			Leases = New BindableCollection(Of ILeaseModel)()
		End Sub

		Public Sub Init()
			InitLeases()
		End Sub

		Private Sub InitLeases()
			Leases.Clear()

			Leases.AddRange(
				_getAllLeasesQuery.Execute().
				               Where( Function( l ) Not l.IsPaid ) )
		End Sub

		Private Async Function InitLeasesAsync() As Task
			Leases.Clear()
			Leases.AddRange(
				(Await _getAllLeasesQuery.ExecuteAsync()).
				               Where( Function( l ) Not l.IsPaid ) )
		End Function

		' Bind models

		Public ReadOnly Property Leases As IObservableCollection(Of ILeaseModel)

		' Business actions
		Public Sub DoLeaseAction( model As ILeaseModel )
			If IsNothing( model ) Then Return

			ParentWorkspace.ParentShell.NavigateToScreenAddBillWithLease( model.Id )
		End Sub

		' Infrastructure

		Public Sub OnLeaseAdded( model As ILeaseModel ) Implements ILeasesListPresenter.OnLeaseAdded
			If model.IsPaid Then Return

			Dim lease = Leases.FirstOrDefault( Function( l ) l.Id = model.Id )
			If lease IsNot Nothing
				ShowStaticTopNotification( Start.MainWindow.Models.StaticNotificationType.Warning,
				                           "Tìm thấy phiếu thuê phòng cùng id trong danh sách" )
				Leases.Remove( lease )
			End If

			' add new lease item
			Leases.Add( model )
		End Sub

		Public Sub OnLeaseUpdated( model As ILeaseModel ) Implements ILeasesListPresenter.OnLeaseUpdated
			Dim leaseToUpdate = Leases.FirstOrDefault( Function( l ) l.Id = model.Id )
			If IsNothing( leaseToUpdate )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Warning,
				                              "Không tìm thấy phiếu thuê phòng {model.Id} trong danh sách để cập nhật" )
				Return
			End If

			If model.IsPaid
				Leases.Remove( leaseToUpdate )
			Else
				Leases( Leases.IndexOf( leaseToUpdate ) ) = model
			End If
		End Sub

		Public Sub OnLeaseRemoved( id As String ) Implements ILeasesListPresenter.OnLeaseRemoved
			Dim leaseToRemove = Leases.FirstOrDefault( Function( l ) l.Id = id )
			If IsNothing( leaseToRemove )
				ShowStaticBottomNotification( Start.MainWindow.Models.StaticNotificationType.Warning,
				                              "Không tìm thấy phiếu thuê vừa xóa trong danh sách để cập nhật" )
				Return
			End If

			Leases.Remove( leaseToRemove )
		End Sub

		Public Sub OnRoomUpdated( model As IRoomModel ) Implements IRoomPresenter.OnRoomUpdated
			Dim leaseToUpdate = Leases.FirstOrDefault( Function( l ) l.Room.Id = model.Id )
			If IsNothing( leaseToUpdate ) Then Return

			' if lease is updatable, update it room
			Dim updatableLease = TryCast(leaseToUpdate, UpdatableLeaseModel)
			If (updatableLease IsNot Nothing)
				updatableLease.Room = model
				Return
			End If

			' create new lease with new room model
			Dim newLease = New UpdatableLeaseModel( leaseToUpdate )
			newLease.Room = model
			Leases( Leases.IndexOf( leaseToUpdate ) ) = newLease
		End Sub
	End Class
End Namespace
