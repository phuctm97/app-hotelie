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
Imports Hotelie.Presentation.Start.MainWindow.Models
Imports MaterialDesignThemes.Wpf

Namespace Bills.ViewModels
	Public Class ScreenAddBillViewModel
		' Dependencies
		Inherits AppScreenHasSaving
		Implements IChild(Of BillsWorkspaceViewModel)
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _getAllRoomsQuery As IGetAllRoomsQuery
		Private ReadOnly _getAllLeasesQuery As IGetAllLeasesQuery
		Private ReadOnly _createBillFactory As ICreateBillFactory
		Private ReadOnly _inventory As IInventory

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As BillsWorkspaceViewModel Implements IChild(Of BillsWorkspaceViewModel).Parent

		' Initialization
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

			Bill = New EditableBillModel
			AddHandler Bill.Details.CollectionChanged, AddressOf	OnDetailsUpdated
		End Sub

		' Binding models
		Public ReadOnly Property Bill As EditableBillModel

		' Binding data
		Public Shared ReadOnly Rooms As IObservableCollection(Of IRoomModel)

		Public Shared ReadOnly Leases As IObservableCollection(Of ILeaseModel)

		Public Sub Init()
			Rooms.Clear()
			Rooms.AddRange( _getAllRoomsQuery.Execute().Where( Function( r ) r.State = 1 ) )

			Leases.Clear()
			Leases.AddRange( _getAllLeasesQuery.Execute() )
			InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Rooms.Clear()
			Rooms.AddRange( (Await _getAllRoomsQuery.ExecuteAsync()).Where( Function( r ) r.State = 1 ) )

			Leases.Clear()
			Leases.AddRange( Await _getAllLeasesQuery.ExecuteAsync() )
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

		' Domain actions

		Public Sub InsertRoomId( id As String )
			Dim room = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( room ) Then Return

			Dim detail = New EditableBillDetailModel
			detail.Room = room

			Bill.Details.Add( detail )
		End Sub

		Public Sub InsertLeaseId( id As String )
			Dim lease = Leases.FirstOrDefault( Function( l ) l.Id = id )
			If IsNothing( lease ) Then Return

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
			Next
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
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Đã xóa các hóa đơn trùng, vui lòng kiểm tra lại trước khi lưu" )
				Bill.Details.Clear()
				For Each leaseId As String In uniqueLeaseIds
					Dim lease = Leases.FirstOrDefault( Function( l ) l.Id = leaseId )
					If IsNothing( lease ) Then Continue For

					Dim detail = New EditableBillDetailModel
					detail.Lease = lease
					Bill.Details.Add( detail )
				Next
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
			Await _inventory.OnBillAddedAsync( newId )
			Await ActualExitAsync()
		End Function

		Private Sub OnSaveFail()
			ShowStaticBottomNotification( StaticNotificationType.Error, "Gặp sự cố trong lúc lập hóa đơn" )
		End Sub
	End Class
End Namespace
