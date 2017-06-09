Imports System.Collections.Specialized
Imports System.ComponentModel
Imports Caliburn.Micro
Imports Hotelie.Application.Bills.Factories
Imports Hotelie.Application.Leases.Models
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Bills.Models
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Common.Infrastructure
Imports Hotelie.Presentation.Start.MainWindow.Models
Imports MaterialDesignThemes.Wpf

Namespace Bills.ViewModels
	Public Class ScreenAddBillViewModel
		' Dependencies
		Inherits AppScreenHasSaving
		Implements IChild(Of BillsWorkspaceViewModel)
		Implements INeedWindowModals
		Implements IRoomsListPresenter
		Implements ILeasesListPresenter

		' Dependencies
		Private ReadOnly _getAllRoomsQuery As IGetAllRoomsQuery
		Private ReadOnly _getAllLeasesQuery As IGetAllLeasesQuery
		Private ReadOnly _createBillFactory As ICreateBillFactory
		Private ReadOnly _inventory As IInventory

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As BillsWorkspaceViewModel Implements IChild(Of BillsWorkspaceViewModel).Parent

		' Initializations
		Shared Sub New()
			Rooms = New BindableCollection(Of IRoomModel)
			Leases = New BindableCollection(Of ILeaseModel)
		End Sub

		Public Sub New( workspace As BillsWorkspaceViewModel,
		                getAllRoomsQuery As IGetAllRoomsQuery,
		                getAllLeasesQuery As IGetAllLeasesQuery,
		                createBillFactory As ICreateBillFactory,
		                inventory As IInventory )
			MyBase.New( ColorZoneMode.PrimaryDark )

			ParentWorkspace = workspace
			_getAllRoomsQuery = getAllRoomsQuery
			_getAllLeasesQuery = getAllLeasesQuery
			_createBillFactory = createBillFactory
			_inventory = inventory
			TryCast(Me, IRoomsListPresenter).RegisterInventory()
			TryCast(Me, ILeasesListPresenter).RegisterInventory()

			Bill = New EditableBillModel
			AddHandler Bill.Details.CollectionChanged, AddressOf	OnDetailsUpdated
		End Sub

		' Binding models
		Public ReadOnly Property Bill As EditableBillModel

		' Binding data
		Public Shared ReadOnly Rooms As IObservableCollection(Of IRoomModel)

		Public Shared ReadOnly Leases As IObservableCollection(Of ILeaseModel)

		Public Sub Init()
			'Rooms.Clear()
			'Rooms.AddRange( _getAllRoomsQuery.Execute().Where( Function( r ) r.State = 1 ) )

			'Leases.Clear()
			'Leases.AddRange( _getAllLeasesQuery.Execute().Where( Function( l ) Not l.IsPaid ) )
			'InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Rooms.Clear()
			Rooms.AddRange( (Await _getAllRoomsQuery.ExecuteAsync()).Where( Function( r ) r.State = 1 ) )

			Leases.Clear()
			Leases.AddRange( (Await _getAllLeasesQuery.ExecuteAsync()).Where( Function( l ) Not l.IsPaid ) )
			InitValues()
		End Function

		Private Sub InitValues()
			Bill.CustomerName = String.Empty
			Bill.CustomerAddress = String.Empty
			Bill.CreateDate = Today
			Bill.TotalExpense = 0
			Bill.Details.Clear()
		End Sub

		Private Sub ResetValues()
			Bill.CustomerName = String.Empty
			Bill.CustomerAddress = String.Empty
			Bill.CreateDate = Today
			Bill.TotalExpense = 0
			Bill.Details.Clear()
		End Sub

		' Loading
		Public Sub Reload() Implements IRoomsListPresenter.Reload
			Throw New NotImplementedException()
		End Sub

		Private Sub Reload_() Implements ILeasesListPresenter.Reload
		End Sub

		Public Function ReloadAsync_() As Task Implements ILeasesListPresenter.ReloadAsync
			Return Task.FromResult( True )
		End Function

		Public Async Function ReloadAsync() As Task Implements IRoomsListPresenter.ReloadAsync
			Rooms.Clear()
			Rooms.AddRange( (Await _getAllRoomsQuery.ExecuteAsync()).Where( Function( r ) r.State = 1 ) )

			Leases.Clear()
			Leases.AddRange( (Await _getAllLeasesQuery.ExecuteAsync()).Where( Function( l ) Not l.IsPaid ) )
			ReloadValues()
		End Function

		Private Sub ReloadValues()
			Bill.CustomerName = String.Empty
			Bill.CustomerAddress = String.Empty
			Bill.CreateDate = Today
			Bill.TotalExpense = 0
			Bill.Details.Clear()
		End Sub

		' Domain actions

		Public Sub InsertRoomId( id As String )
			Dim room = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( room ) Then Return
			If Bill.Details.Any( Function( d ) d.Room?.Id = room.Id ) Then Return

			Dim detail = New EditableBillDetailModel
			detail.Room = room

			Bill.Details.Add( detail )
		End Sub

		Public Sub InsertLeaseId( id As String )
			Dim lease = Leases.FirstOrDefault( Function( l ) l.Id = id )
			If IsNothing( lease ) Then Return
			If Bill.Details.Any( Function( d ) d.Lease?.Id = lease.Id ) Then Return

			Dim detail = New EditableBillDetailModel
			detail.Lease = lease

			Bill.Details.Add( detail )
		End Sub

		Public Sub AddEmptyDetail()
			Bill.Details.Add( New EditableBillDetailModel )
		End Sub

		Private Sub OnDetailsUpdated( sender As Object,
		                              e As NotifyCollectionChangedEventArgs )
			If e.NewItems IsNot Nothing
				For Each obj As Object In e.NewItems
					Dim detail = TryCast(obj, EditableBillDetailModel)
					If IsNothing( detail ) Then Continue For

					AddHandler detail.PropertyChanged, AddressOf OnSingleDetailUpdated
				Next
			End If

			If e.OldItems IsNot Nothing
				For Each obj As Object In e.OldItems
					Dim detail = TryCast(obj, EditableBillDetailModel)
					If IsNothing( detail ) Then Continue For

					RemoveHandler detail.PropertyChanged, AddressOf OnSingleDetailUpdated
				Next
			End If

			Dim newExpense As Decimal = 0
			For Each detail As EditableBillDetailModel In Bill.Details
				newExpense += detail.TotalExpense
			Next
			Bill.TotalExpense = newExpense
		End Sub

		Private Sub OnSingleDetailUpdated( sender As Object,
		                                   e As PropertyChangedEventArgs )
			If Not String.Equals( e.PropertyName, "TotalExpense" ) Then Return

			Dim newExpense As Decimal = 0
			For Each detail As EditableBillDetailModel In Bill.Details
				newExpense += detail.TotalExpense
			Next
			Bill.TotalExpense = newExpense
		End Sub

		' Exit
		Public Overrides ReadOnly Property IsEdited As Boolean
			Get
				Return CheckForPendingChanges()
			End Get
		End Property

		Private Function CheckForPendingChanges() As Boolean
			If Bill.Details.Count > 0 Then Return True
			Return False
		End Function

		Public Overrides Function ActualExitAsync() As Task
			ResetValues()

			ParentWorkspace.NavigateToScreenBillsList()
			Return MyBase.ActualExitAsync()
		End Function

		' Save
		Public Overrides Async Function CanSave() As Task(Of Boolean)
			Return Await Task.Run( Function() ValidateData() )
		End Function

		Private Function ValidateData() As Boolean
			If String.IsNullOrWhiteSpace( Bill.CustomerName )
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Vui lòng nhập tên đơn vị thanh toán" )
				Return False
			End If

			If Bill.Details.Count = 0
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Vui lòng chọn ít nhất 1 phòng cần thanh toán" )
				Return False
			End If

			Dim removeMessage = String.Empty

			' Remove invalid detail
			Dim detailsToRemove = New List(Of EditableBillDetailModel)
			For Each detail As EditableBillDetailModel In Bill.Details
				If IsNothing( detail )
					detailsToRemove.Add( detail )
					Continue For
				End If

				If IsNothing( detail.Room ) OrElse String.IsNullOrWhiteSpace( detail.Room.Id )
					detailsToRemove.Add( detail )
					Continue For
				End If

				If IsNothing( detail.Lease ) OrElse String.IsNullOrWhiteSpace( detail.Lease.Id )
					detailsToRemove.Add( detail )
					Continue For
				End If

				If detail.Lease.IsPaid
					detailsToRemove.Add( detail )
					Continue For
				End If
			Next

			If detailsToRemove.Count > 0
				removeMessage += "Đã xóa một số phòng không hợp lệ (đã thanh toán,.. ). "
			End If
			Bill.Details.RemoveRange( detailsToRemove )

			' Remove duplicate detail
			Dim uniqueLeaseIds = New List(Of String)
			For Each detailModel As EditableBillDetailModel In Bill.Details
				If IsNothing( detailModel ) OrElse IsNothing( detailModel.Lease ) Then Continue For
				If Not uniqueLeaseIds.Contains( detailModel.Lease.Id )
					uniqueLeaseIds.Add( detailModel.Lease.Id )
				End If
			Next

			If uniqueLeaseIds.Count < Bill.Details.Count
				removeMessage += "Đã xóa các hóa đơn trùng. "
			End If

			If uniqueLeaseIds.Count < Bill.Details.Count
				Bill.Details.Clear()
				For Each leaseId As String In uniqueLeaseIds
					Dim lease = Leases.FirstOrDefault( Function( l ) l.Id = leaseId )
					If IsNothing( lease ) Then Continue For

					Dim detail = New EditableBillDetailModel
					detail.Lease = lease
					Bill.Details.Add( detail )
				Next
			End If

			If Not String.IsNullOrEmpty( removeMessage )
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"{removeMessage}Vui lòng kiểm tra lại!" )
				Return False
			End If

			Return True
		End Function

		Public Overrides Async Function ActualSaveAsync() As Task
			' try create bill
			Dim listOfLeaseIds = New List(Of String)
			For Each detail As EditableBillDetailModel In Bill.Details
				listOfLeaseIds.Add( detail.Lease.Id )
			Next

			Dim newId = Await _createBillFactory.ExecuteAsync( Bill.CustomerName,
			                                                   Bill.CustomerAddress,
			                                                   listOfLeaseIds,
			                                                   Bill.TotalExpense,
			                                                   "" )

			If String.IsNullOrEmpty( newId )
				OnSaveFail()
			Else
				Await OnSaveSuccessAsync( newId )
			End If
		End Function

		Private Async Function OnSaveSuccessAsync( newId As String ) As Task
			Dim leaseIds = New List(Of String)
			Dim roomIds = New List(Of String)

			If Bill.Details IsNot Nothing
				For Each detail As EditableBillDetailModel In Bill.Details
					If IsNothing( detail ) Then Continue For

					If detail.Lease IsNot Nothing AndAlso Not String.IsNullOrEmpty( detail.Lease.Id )
						leaseIds.Add( detail.Lease.Id )
					End If
					If detail.Room IsNot Nothing AndAlso Not String.IsNullOrEmpty( detail.Room.Id )
						roomIds.Add( detail.Room.Id )
					End If
				Next
			End If

			Await _inventory.OnBillAddedAsync( newId )
			For Each leaseId As String In leaseIds
				Await _inventory.OnLeaseUpdatedAsync( leaseId )
			Next
			For Each roomId As String In roomIds
				Await _inventory.OnRoomUpdatedAsync( roomId )
			Next

			Await ActualExitAsync()
		End Function

		Private Sub OnSaveFail()
			ShowStaticBottomNotification( StaticNotificationType.Error, "Gặp sự cố trong lúc lập hóa đơn" )
		End Sub

		' Infrastructure
		Public Sub OnLeaseAdded( model As ILeaseModel ) Implements ILeasesListPresenter.OnLeaseAdded
			If IsNothing( model ) OrElse String.IsNullOrEmpty( model.Id ) Then Return
			If model.IsPaid Then Return

			Dim lease = Leases.FirstOrDefault( Function( l ) l.Id = model.Id )
			If lease IsNot Nothing
				Leases( Leases.IndexOf( lease ) ) = model
				if Bill.Details IsNot Nothing
					For Each detail As EditableBillDetailModel In Bill.Details
						If detail.Lease?.Id = model.Id Then detail.Lease = model
					Next
				End If
			Else
				Leases.Add( model )
			End If
		End Sub

		Public Sub OnLeaseUpdated( model As ILeaseModel ) Implements ILeasesListPresenter.OnLeaseUpdated
			If IsNothing( model ) OrElse String.IsNullOrEmpty( model.Id ) Then Return

			If Not model.IsPaid
				Dim leaseToUpdate = Leases.FirstOrDefault( Function( l ) l.Id = model.Id )
				If IsNothing( leaseToUpdate )
					Leases.Add( model )
				Else
					Leases( Leases.IndexOf( leaseToUpdate ) ) = model
					if Bill.Details IsNot Nothing
						For Each detail As EditableBillDetailModel In Bill.Details
							If detail.Lease?.Id = model.Id Then detail.Lease = model
						Next
					End If
				End If

			Else
				Dim leaseToRemove = Leases.FirstOrDefault( Function( l ) l.Id = model.Id )
				If IsNothing( leaseToRemove ) Then Return

				Leases.Remove( leaseToRemove )
				if Bill.Details IsNot Nothing
					For Each detail As EditableBillDetailModel In Bill.Details
						If detail.Lease?.Id = leaseToRemove.Id Then detail.Lease = Leases.FirstOrDefault()
					Next
				End If
			End If
		End Sub

		Public Sub OnLeaseRemoved( id As String ) Implements ILeasesListPresenter.OnLeaseRemoved
			Dim leaseToRemove = Leases.FirstOrDefault( Function( l ) l.Id = id )
			If IsNothing( leaseToRemove ) Then Return

			Leases.Remove( leaseToRemove )
			if Bill.Details IsNot Nothing
				For Each detail As EditableBillDetailModel In Bill.Details
					If detail.Lease?.Id = leaseToRemove.Id Then detail.Lease = Leases.FirstOrDefault()
				Next
			End If
		End Sub

		Public Sub OnRoomAdded( model As IRoomModel ) Implements IRoomsListPresenter.OnRoomAdded
			If IsNothing( model ) OrElse String.IsNullOrEmpty( model.Id ) Then Return
			If model.State = 0 Then Return

			Dim room = Rooms.FirstOrDefault( Function( r ) r.Id = model.Id )
			If room IsNot Nothing
				Rooms( Rooms.IndexOf( room ) ) = model
				if Bill.Details IsNot Nothing
					For Each detail As EditableBillDetailModel In Bill.Details
						If detail.Room?.Id = model.Id Then detail.Room = model
					Next
				End If
			Else
				Rooms.Add( model )
			End If
		End Sub

		Public Sub OnRoomUpdated( model As IRoomModel ) Implements IRoomsListPresenter.OnRoomUpdated
			If IsNothing( model ) OrElse String.IsNullOrEmpty( model.Id ) Then Return

			If model.State = 1
				Dim roomToUpdate = Rooms.FirstOrDefault( Function( r ) r.Id = model.Id )
				If IsNothing( roomToUpdate )
					Rooms.Add( model )
				Else
					Rooms( Rooms.IndexOf( roomToUpdate ) ) = model
					if Bill.Details IsNot Nothing
						For Each detail As EditableBillDetailModel In Bill.Details
							If detail.Room?.Id = model.Id Then detail.Room = model
						Next
					End If
				End If

			Else
				Dim roomToRemove = Rooms.FirstOrDefault( Function( r ) r.Id = model.Id )
				If IsNothing( roomToRemove ) Then Return

				Rooms.Remove( roomToRemove )
				if Bill.Details IsNot Nothing
					For Each detail As EditableBillDetailModel In Bill.Details
						If detail.Room?.Id = roomToRemove.Id Then detail.Room = Rooms.FirstOrDefault()
					Next
				End If
			End If
		End Sub

		Public Sub OnRoomRemoved( id As String ) Implements IRoomsListPresenter.OnRoomRemoved
			Dim roomToRemove = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( roomToRemove ) Then Return

			Rooms.Remove( roomToRemove )
			if Bill.Details IsNot Nothing
				For Each detail As EditableBillDetailModel In Bill.Details
					If detail.Room?.Id = roomToRemove.Id Then detail.Room = Rooms.FirstOrDefault()
				Next
			End If
		End Sub
	End Class
End Namespace
