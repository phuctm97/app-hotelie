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
			If IsNothing( model ) OrElse String.IsNullOrEmpty( model.Id ) Then Return
			If model.IsPaid Then Return

			Dim lease = Leases.FirstOrDefault( Function( l ) l.Id = model.Id )
			If lease IsNot Nothing
				ShowStaticTopNotification( Start.MainWindow.Models.StaticNotificationType.Warning,
				                           $"Tìm thấy phiếu thuê phòng cùng mã {model.IdEx} trong danh sách" )
				Leases( Leases.IndexOf( lease ) ) = model
			Else
				Leases.Add( model )
			End If
		End Sub

		Public Sub OnLeaseUpdated( model As ILeaseModel ) Implements ILeasesListPresenter.OnLeaseUpdated
			If IsNothing( model ) OrElse String.IsNullOrEmpty( model.Id ) Then Return

			If model.IsPaid
				Dim leaseToRemove = Leases.FirstOrDefault( Function( l ) l.Id = model.Id )
				If leaseToRemove IsNot Nothing
					Leases.Remove( leaseToRemove )
				End If

			Else
				Dim leaseToUpdate = Leases.FirstOrDefault( Function( l ) l.Id = model.Id )
				If leaseToUpdate IsNot Nothing
					Leases( Leases.IndexOf( leaseToUpdate ) ) = model
				Else
					Leases.Add( model )
				End If
			End If
		End Sub

		Public Sub OnLeaseRemoved( id As String ) Implements ILeasesListPresenter.OnLeaseRemoved
			Dim leaseToRemove = Leases.FirstOrDefault( Function( l ) l.Id = id )
			If IsNothing( leaseToRemove ) Then Return

			Leases.Remove( leaseToRemove )
		End Sub

		Public Sub OnRoomUpdated( model As IRoomModel ) Implements IRoomPresenter.OnRoomUpdated
			If IsNothing( model ) OrElse String.IsNullOrEmpty( model.Id ) Then Return

			Dim leaseToUpdate = Leases.FirstOrDefault( Function( l ) l.Room?.Id = model.Id )
			If IsNothing( leaseToUpdate ) Then Return

			' if lease is updatable, update it room
			Dim updatableLease = TryCast(leaseToUpdate, UpdatableLeaseModel)
			If (updatableLease IsNot Nothing)
				updatableLease.Room = model
			Else
				Leases( Leases.IndexOf( leaseToUpdate ) ) = new UpdatableLeaseModel( leaseToUpdate ) With {.Room=model}
			End If
		End Sub
	End Class
End Namespace
