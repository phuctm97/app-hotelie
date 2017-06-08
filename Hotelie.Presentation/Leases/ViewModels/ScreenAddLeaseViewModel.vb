Imports System.Collections.Specialized
Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Factories
Imports Hotelie.Application.Leases.Models
Imports Hotelie.Application.Leases.Queries
Imports Hotelie.Application.Parameters.Queries
Imports Hotelie.Application.Rooms.Models
Imports Hotelie.Application.Rooms.Queries
Imports Hotelie.Application.Services.Infrastructure
Imports Hotelie.Presentation.Common
Imports Hotelie.Presentation.Common.Controls
Imports Hotelie.Presentation.Common.Infrastructure
Imports Hotelie.Presentation.Leases.Models
Imports Hotelie.Presentation.Start.MainWindow.Models

Namespace Leases.ViewModels
	Public Class ScreenAddLeaseViewModel
		Inherits AppScreenHasSaving
		Implements IChild(Of LeasesWorkspaceViewModel)
		Implements INeedWindowModals
		Implements IRoomsListPresenter

		' Dependencies
		Private ReadOnly _getAllRoomsQuery As IGetAllRoomsQuery
		Private ReadOnly _getAllCustomerCategoriesQuery As IGetAllCustomerCategoriesQuery
		Private ReadOnly _getParametersQuery As IGetParametersQuery
		Private ReadOnly _createLeaseFactory As ICreateLeaseFactory
		Private ReadOnly _inventory As IInventory

		' Backing fields
		Private _room As IRoomModel
		Private _expectedCheckoutDate As Date
		Private _roomCapacity As Integer

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As LeasesWorkspaceViewModel Implements IChild(Of LeasesWorkspaceViewModel).Parent
			Get
				Return TryCast(Parent, LeasesWorkspaceViewModel)
			End Get
			Set
				If IsNothing( Value ) OrElse Equals( Value, Parent ) Then Return
				Parent = value
				NotifyOfPropertyChange( Function() Parent )
				NotifyOfPropertyChange( Function() ParentWorkspace )
			End Set
		End Property

		' Binding models
		Public ReadOnly Property Lease As EditableLeaseModel

		Public ReadOnly Property CanAddDetail As Boolean
			Get
				Return IsNothing( Lease ) OrElse Lease.Details.Count < _roomCapacity
			End Get
		End Property

		Public ReadOnly Property CanDeleteDetail As Boolean
			Get
				Return IsNothing( Lease.Details ) OrElse Lease.Details.Count > 1
			End Get
		End Property

		' Binding data
		Public ReadOnly Property Rooms As IObservableCollection(Of IRoomModel)

		Public Shared ReadOnly Property CustomerCategories As IObservableCollection(Of ICustomerCategoryModel)

		' Initialization
		Shared Sub New()
			CustomerCategories = New BindableCollection(Of ICustomerCategoryModel)
		End Sub

		Public Sub New( workspace As LeasesWorkspaceViewModel,
		                getAllRoomsQuery As IGetAllRoomsQuery,
		                getAllCustomerCategoriesQuery As IGetAllCustomerCategoriesQuery,
		                getParametersQuery As IGetParametersQuery,
		                createLeaseFactory As ICreateLeaseFactory,
		                inventory As IInventory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ParentWorkspace = workspace
			_getAllRoomsQuery = getAllRoomsQuery
			_getAllCustomerCategoriesQuery = getAllCustomerCategoriesQuery
			_getParametersQuery = getParametersQuery
			_createLeaseFactory = createLeaseFactory
			_inventory = inventory
			RegisterInventory()

			Rooms = New BindableCollection(Of IRoomModel)
			Lease = New EditableLeaseModel()
			AddHandler Lease.Details.CollectionChanged, AddressOf OnDetailsUpdated
		End Sub

		Public Sub Init()
			Rooms.Clear()
			Rooms.AddRange( _getAllRoomsQuery.Execute().Where( Function( r ) r.State = 0 ) )

			CustomerCategories.Clear()
			CustomerCategories.AddRange( _getAllCustomerCategoriesQuery.Execute() )

			_roomCapacity = _getParametersQuery.Execute().RoomCapacity
			InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Rooms.Clear()
			Rooms.AddRange( (Await _getAllRoomsQuery.ExecuteAsync()).Where( Function( r ) r.State = 0 ) )

			CustomerCategories.Clear()
			CustomerCategories.AddRange( Await _getAllCustomerCategoriesQuery.ExecuteAsync() )

			_roomCapacity = (Await _getParametersQuery.ExecuteAsync()).RoomCapacity
			InitValues()
		End Function

		Private Sub InitValues()
			Lease.Id = String.Empty
			Lease.Room = Rooms.FirstOrDefault()
			Lease.CheckinDate = Today
			Lease.ExpectedCheckoutDate = Today
			Lease.Details.Clear()
		End Sub

		Private Sub ResetValues()
			Lease.Id = String.Empty
			Lease.Room = Rooms.FirstOrDefault()
			Lease.CheckinDate = Today
			Lease.ExpectedCheckoutDate = Today
			Lease.Details.Clear()
		End Sub

		' Domain actions

		Public Sub SetRoomId( id As String )
			Dim roomModel = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( roomModel )
				ShowStaticBottomNotification( StaticNotificationType.Error, "Lỗi! Không thể thuê phòng vừa chọn" )
				Return
			End If

			Lease.Room = roomModel
		End Sub

		Public Sub AddEmptyDetail()
			If CanAddDetail Then _
				Lease.Details.Add( New EditableLeaseDetailModel )
		End Sub

		Private Sub OnDetailsUpdated( sender As Object,
		                              e As NotifyCollectionChangedEventArgs )
			NotifyOfPropertyChange( Function() CanAddDetail )
			NotifyOfPropertyChange( Function() CanDeleteDetail )
		End Sub

		' Exit
		Public Overrides ReadOnly Property IsEdited As Boolean
			Get
				Return CheckForPendingChanges()
			End Get
		End Property

		Private Function CheckForPendingChanges() As Boolean
			If Lease.Details.Count > 0 Then Return True
			Return False
		End Function

		Public Overrides Function ActualExitAsync() As Task
			ResetValues()

			ParentWorkspace.NavigateToScreenLeasesList()
			Return MyBase.ActualExitAsync()
		End Function

		' Save
		Private Function ValidateData() As Boolean
			If Lease.ExpectedCheckoutDate.Date < Lease.CheckinDate.Date
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Ngày dự kiến trả phải sau ngày đăng ký thuê phòng" )
				Return False
			End If

			If IsNothing( Lease.Room ) OrElse String.IsNullOrWhiteSpace( Lease.Room.Id )
				ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng chọn phòng thuê" )
				Return False
			End If

			If Lease.Room.State = 1
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Phòng {Lease.Room.Name} đang thuê bởi khách hàng khác. Vui lòng chọn phòng khác" )
				Return False
			End If

			If Lease.Details.Count > _roomCapacity
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Vượt quá số khách quy định. Mỗi phòng chỉ có tối đa {_roomCapacity} người" )
				Return False
			End If

			If Lease.Details.Count <= 0
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Phải có ít nhất 1 khách thuê phòng" )
				Return False
			End If

			For Each detail As EditableLeaseDetailModel In Lease.Details
				If String.IsNullOrWhiteSpace( detail.CustomerName )
					ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng nhập đầy đủ tên khách hàng" )
					Return False
				End If

				If IsNothing( detail.CustomerCategory ) OrElse String.IsNullOrWhiteSpace( detail.CustomerCategory.Id )
					ShowStaticBottomNotification( StaticNotificationType.Information, "Vui chọn đầy đủ loại khách hàng" )
					Return False
				End If
			Next

			Return True
		End Function

		Public Overrides Function CanSave() As Task(Of Boolean)
			Return Task.Run( Function() ValidateData() )
		End Function

		Public Overrides Async Function ActualSaveAsync() As Task
			' try create lease
			Dim detailModels = New List(Of CreateLeaseDetailModel)
			For Each detail As EditableLeaseDetailModel In Lease.Details
				detailModels.Add( New CreateLeaseDetailModel With {
					                .CustomerName=detail.CustomerName,
					                .CustomerLicenseId=detail.CustomerLicenseId,
					                .CustomerAddress=detail.CustomerAddress,
					                .CustomerCategoryId=detail.CustomerCategory.Id} )
			Next

			Dim newId =
				    Await _
				    _createLeaseFactory.ExecuteAsync( Lease.Room.Id, Lease.CheckinDate, Lease.ExpectedCheckoutDate, detailModels )

			If String.IsNullOrEmpty( newId )
				OnSaveFail()
			Else
				Await OnSaveSuccessAsync( newId )
			End If
		End Function

		Private Async Function OnSaveSuccessAsync( newId As String ) As Task
			Await _inventory.OnLeaseAddedAsync( newId )
			Await ActualExitAsync()
		End Function

		Private Sub OnSaveFail()
			ShowStaticBottomNotification( StaticNotificationType.Error, "Gặp sự cố trong lúc tạo phiếu thuê phòng" )
		End Sub

		' Infrastructure
		Public Sub OnRoomAdded( model As IRoomModel ) Implements IRoomsListPresenter.OnRoomAdded
			If model.State <> 0 Then Return

			If Rooms.Any( Function( r ) r.Id = model.Id )
				ShowStaticBottomNotification( StaticNotificationType.Warning,
				                              "Tìm thấy phòng trùng với phòng cần thêm" )
				Return
			End If

			Rooms.Add( model )
		End Sub

		Public Sub OnRoomUpdated( model As IRoomModel ) Implements IRoomsListPresenter.OnRoomUpdated
			Dim roomToUpdate = Rooms.FirstOrDefault( Function( r ) r.Id = model.Id )
			If IsNothing( roomToUpdate )
				If model.State = 0
					Rooms.Add( model )
				End If
			Else
				If model.State <> 0
					Rooms.Remove( roomToUpdate )
					If Equals( Lease.Room, roomToUpdate )
						Lease.Room = Rooms.FirstOrDefault()
					End If
				Else
					Lease.Room = model
				End If
			End If
		End Sub

		Public Sub OnRoomRemoved( id As String ) Implements IRoomsListPresenter.OnRoomRemoved
			Dim roomToRemove = Rooms.FirstOrDefault( Function( r ) r.Id = id )
			If IsNothing( roomToRemove ) Then Return

			Rooms.Remove( roomToRemove )
			If Equals( Lease.Room, roomToRemove )
				Lease.Room = Rooms.FirstOrDefault()
			End If
		End Sub
	End Class
End Namespace
