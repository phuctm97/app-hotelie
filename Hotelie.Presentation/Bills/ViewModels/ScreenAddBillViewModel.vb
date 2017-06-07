Imports System.Collections.Specialized
Imports System.ComponentModel
Imports Caliburn.Micro
Imports Hotelie.Application.Bills.Factories.CreateBill
Imports Hotelie.Application.Leases.Queries.GetSimpleLeasesList
Imports Hotelie.Application.Rooms.Queries.GetSimpleRoomsList
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Bills.Models
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Bills.ViewModels
	Public Class ScreenAddBillViewModel
		' Dependencies
		Inherits PropertyChangedBase
		Implements IChild(Of BillsWorkspaceViewModel)
		Implements INeedWindowModals

		' Dependencies
		Private ReadOnly _inventory As IInventory
		Private ReadOnly _getSimpleRoomsListQuery As IGetSimpleRoomsListQuery
		Private ReadOnly _getSimpleLeasesListQuery As IGetSimpleLeasesListQuery
		Private ReadOnly _createBillFactory As ICreateBillFactory

		Private _payerName As String
		Private _payerAddress As String
		Private _createDate As Date
		Private _totalExpense As Decimal

		Public Property PayerName As String
			Get
				Return _payerName
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _payerName ) Then Return
				_payerName = value
				NotifyOfPropertyChange( Function() PayerName )
			End Set
		End Property

		Public Property PayerAddress As String
			Get
				Return _payerAddress
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _payerAddress ) Then Return
				_payerAddress = value
				NotifyOfPropertyChange( Function() PayerAddress )
			End Set
		End Property

		Public Property CreateDate As Date
			Get
				Return _createDate
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _createDate ) Then Return
				_createDate = value
				NotifyOfPropertyChange( Function() CreateDate )
			End Set
		End Property

		Public Property TotalExpense As Decimal
			Get
				Return _totalExpense
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, _totalExpense ) Then Return
				_totalExpense = value
				NotifyOfPropertyChange( Function() TotalExpense )
			End Set
		End Property

		Public Property Details As IObservableCollection(Of EditableBillDetailModel)

		Public Shared ReadOnly Property Rooms As IObservableCollection(Of SimpleRoomsListItemModel)

		Public Shared ReadOnly Property Leases As IObservableCollection(Of SimpleLeasesListItemModel)

		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As BillsWorkspaceViewModel Implements IChild(Of BillsWorkspaceViewModel).Parent

		' Initialization
		Shared Sub New()
			Rooms = New BindableCollection(Of SimpleRoomsListItemModel)
			Leases = New BindableCollection(Of SimpleLeasesListItemModel)
		End Sub

		Public Sub New( workspace As BillsWorkspaceViewModel,
		                inventory As IInventory,
		                getSimpleRoomsListQuery As IGetSimpleRoomsListQuery,
		                getSimpleLeasesListQuery As IGetSimpleLeasesListQuery,
		                createBillFactory As ICreateBillFactory )
			ParentWorkspace = workspace
			_inventory = inventory
			_getSimpleRoomsListQuery = getSimpleRoomsListQuery
			_getSimpleLeasesListQuery = getSimpleLeasesListQuery
			_createBillFactory = createBillFactory

			Details = New BindableCollection(Of EditableBillDetailModel)
			AddHandler Details.CollectionChanged, AddressOf	OnDetailsUpdated
		End Sub

		Private Sub OnDetailsUpdated( sender As Object,
		                              e As NotifyCollectionChangedEventArgs )
			If e.NewItems IsNot Nothing
				For Each obj As Object In e.NewItems
					Dim detail = CType(obj, EditableBillDetailModel)
					If IsNothing( detail ) Then Continue For

					AddHandler detail.PropertyChanged, AddressOf OnSingleDetailUpdated
				Next
			End If

			If e.OldItems IsNot Nothing
				For Each obj As Object In e.OldItems
					Dim detail = CType(obj, EditableBillDetailModel)
					If IsNothing( detail ) Then Continue For

					RemoveHandler detail.PropertyChanged, AddressOf OnSingleDetailUpdated
				Next
			End If

			Dim newExpense As Decimal = 0
			For Each detail As EditableBillDetailModel In Details
				newExpense += detail.TotalExpense
			Next
			TotalExpense = newExpense
		End Sub

		Private Sub OnSingleDetailUpdated( sender As Object,
		                                   e As PropertyChangedEventArgs )
			If Not String.Equals( e.PropertyName, "TotalExpense" ) Then Return

			Dim newExpense As Decimal = 0
			For Each detail As EditableBillDetailModel In Details
				newExpense += detail.TotalExpense
			Next
			TotalExpense = newExpense
		End Sub

		Public Sub Init()
			Rooms.Clear()
			Rooms.AddRange( _getSimpleRoomsListQuery.Execute().Where( Function( r ) r.State = 0 ) )

			Leases.Clear()
			Leases.AddRange( _getSimpleLeasesListQuery.Execute() )
			InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Rooms.Clear()
			Rooms.AddRange( (Await _getSimpleRoomsListQuery.ExecuteAsync()).Where( Function( r ) r.State = 1 ) )

			Leases.Clear()
			Leases.AddRange( Await _getSimpleLeasesListQuery.ExecuteAsync() )
			InitValues()
		End Function

		Private Sub InitValues()
			PayerName = String.Empty
			PayerAddress = String.Empty
			CreateDate = Today
			TotalExpense = 0
			Details.Clear()
		End Sub

		Private Sub ResetValues()
			PayerName = String.Empty
			PayerAddress = String.Empty
			CreateDate = Today
			TotalExpense = 0
			Details.Clear()
		End Sub

		Public Sub InsertRoomId( id As String )
		End Sub

		Public Sub InsertLeaseId( id As String )
		End Sub

		Public Sub AddEmptyDetail()
			Details.Add( New EditableBillDetailModel )
		End Sub

		' Exit
		Public Async Sub PreviewExit()
			If CheckForPendingChanges()
				Dim result = Await ConfirmExit()

				If Equals( result, 1 )
					PreviewSave()
					Return
				ElseIf Equals( result, 2 )
					Return
				End If
			End If

			[Exit]()
		End Sub

		Public Async Sub PreviewExitAsync()
			If CheckForPendingChanges()
				Dim result = Await ConfirmExit()

				If Equals( result, 1 )
					PreviewSaveAsync()
					Return
				ElseIf Equals( result, 2 )
					Return
				End If
			End If

			[Exit]()
		End Sub

		Private Sub [Exit]()
			ParentWorkspace.NavigateToScreenBillsList()
			ResetValues()
		End Sub

		Private Function CheckForPendingChanges() As Boolean
			If Details.Count > 0 Then Return True
			Return False
		End Function

		Private Async Function ConfirmExit() As Task(Of Integer)
			' show dialog
			Dim dialog = New ThreeButtonDialog( "Thoát mà không lưu các thay đổi?",
			                                    "THOÁT",
			                                    "LƯU & THOÁT",
			                                    "HỦY",
			                                    False,
			                                    True,
			                                    False )
			Dim result = Await ShowDynamicWindowDialog( dialog )

			If String.Equals( result, "THOÁT" ) Then Return 0
			If String.Equals( result, "HỦY" ) Then Return 2
			Return 1
		End Function

		' Save
		Public Sub PreviewSave()
			If ValidateData()
				Save()
			End If
		End Sub

		Public Sub PreviewSaveAsync()
			If ValidateData()
				SaveAsync()
			End If
		End Sub

		Private Sub Save()
			' try create bill
			Dim listOfLeaseIds = New List(Of String)
			For Each detail As EditableBillDetailModel In Details
				listOfLeaseIds.Add( detail.Lease.Id )
			Next

			Dim newId = _createBillFactory.Execute( PayerName, PayerAddress, listOfLeaseIds, TotalExpense )

			If String.IsNullOrEmpty( newId )
				OnSaveFail()
			Else
				OnSaveSuccess( newId )
			End If
		End Sub

		Private Sub OnSaveSuccess( newId As String )
			[Exit]()
		End Sub

		Private Async Sub SaveAsync()
			' try create bill
			Dim listOfLeaseIds = New List(Of String)
			For Each detail As EditableBillDetailModel In Details
				listOfLeaseIds.Add( detail.Lease.Id )
			Next

			Dim newId = Await _createBillFactory.ExecuteAsync( PayerName, PayerAddress, listOfLeaseIds, TotalExpense )

			If String.IsNullOrEmpty( newId )
				OnSaveFail()
			Else
				Await OnSaveSuccessAsync( newId )
			End If
		End Sub

		Private Function OnSaveSuccessAsync( newId As String ) As Task
			[Exit]()
		End Function

		Private Sub OnSaveFail()
			ShowStaticBottomNotification( StaticNotificationType.Error, "Gặp sự cố trong lúc lập hóa đơn" )
		End Sub

		Private Function ValidateData() As Boolean
			If String.IsNullOrWhiteSpace( PayerName )
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Vui lòng nhập tên đơn vị thanh toán" )
				Return False
			End If

			If Details.Count = 0
				ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng chọn ít nhất 1 phòng cần thanh toán" )
				Return False
			End If

			' Remove invalid detail
			Dim detailsToRemove = New List(Of EditableBillDetailModel)
			For Each detail As EditableBillDetailModel In Details
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
			Details.RemoveRange( detailsToRemove )

			' Remove duplicate detail
			Dim uniqueLeaseIds = New List(Of String)
			For Each detailModel As EditableBillDetailModel In Details
				If IsNothing( detailModel ) OrElse IsNothing( detailModel.Lease ) Then Continue For
				If Not uniqueLeaseIds.Contains( detailModel.Lease.Id )
					uniqueLeaseIds.Add( detailModel.Lease.Id )
				End If
			Next

			If uniqueLeaseIds.Count < Details.Count
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Đã xóa các hóa đơn trùng, vui lòng kiểm tra lại trước khi lưu" )
				Details.Clear()
				For Each leaseId As String In uniqueLeaseIds
					Dim lease = Leases.FirstOrDefault( Function( l ) l.Id = leaseId )
					If IsNothing( lease ) Then Continue For

					Dim detail = New EditableBillDetailModel
					detail.Lease = lease
					Details.Add( detail )
				Next
				Return False
			End If

			Return True
		End Function
	End Class
End Namespace
