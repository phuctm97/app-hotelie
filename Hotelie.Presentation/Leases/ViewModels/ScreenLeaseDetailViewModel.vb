Imports System.Collections.Specialized
Imports System.ComponentModel
Imports Caliburn.Micro
Imports Hotelie.Application.Leases.Commands
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
	Public Class ScreenLeaseDetailViewModel
		Inherits AppScreenHasSavingAndDeleting
		Implements IChild(Of LeasesWorkspaceViewModel)
		Implements INeedWindowModals
		Implements IRoomsListPresenter

		' Dependencies
		Private ReadOnly _getLeaseQuery As IGetLeaseQuery
		Private ReadOnly _getAllRoomsQuery As IGetAllRoomsQuery
		Private ReadOnly _getAllCustomerCategoriesQuery As IGetAllCustomerCategoriesQuery
		Private ReadOnly _getParametersQuery As IGetParametersQuery
		Private ReadOnly _updateLeaseCommand As IUpdateLeaseCommand
		Private ReadOnly _removeLeaseCommand As IRemoveLeaseCommand
		Private ReadOnly _updateLeaseDetailCommand As IUpdateLeaseDetailCommand
		Private ReadOnly _removeLeaseDetailCommand As IRemoveLeaseDetailCommand
		Private ReadOnly _createLeaseDetailFactory As ICreateLeaseDetailFactory
		Private ReadOnly _inventory As IInventory

		' Backing fields
		Private _originalroomUnitPrice As Decimal
		Private _originalcheckinDate As Date
		Private _originalroomId As String
		Private _originalexpectedCheckoutDate As Date
		Private ReadOnly _originalDetails As List(Of EditableLeaseDetailModel)

		Private _isEdited As Boolean
		Private _roomCapacity As Integer

		' Parent
		Public Property Parent As Object Implements IChild.Parent

		Public Property ParentWorkspace As LeasesWorkspaceViewModel Implements IChild(Of LeasesWorkspaceViewModel).Parent
			Get
				Return TryCast(Parent, LeasesWorkspaceViewModel)
			End Get
			Set
				Parent = value
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
				Return IsNothing( Lease ) OrElse Lease.Details.Count > 1
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
		                getLeaseQuery As IGetLeaseQuery,
		                getAllRoomsQuery As IGetAllRoomsQuery,
		                getAllCustomerCategoriesQuery As IGetAllCustomerCategoriesQuery,
		                getParametersQuery As IGetParametersQuery,
		                updateLeaseCommand As IUpdateLeaseCommand,
		                removeLeaseCommand As IRemoveLeaseCommand,
		                updateLeaseDetailCommand As IUpdateLeaseDetailCommand,
		                removeLeaseDetailCommand As IRemoveLeaseDetailCommand,
		                createLeaseDetailFactory As ICreateLeaseDetailFactory,
		                inventory As IInventory )
			MyBase.New( MaterialDesignThemes.Wpf.ColorZoneMode.PrimaryDark )

			ParentWorkspace = workspace
			_getLeaseQuery = getLeaseQuery
			_getAllRoomsQuery = getAllRoomsQuery
			_getAllCustomerCategoriesQuery = getAllCustomerCategoriesQuery
			_getParametersQuery = getParametersQuery
			_updateLeaseCommand = updateLeaseCommand
			_removeLeaseCommand = removeLeaseCommand
			_updateLeaseDetailCommand = updateLeaseDetailCommand
			_removeLeaseDetailCommand = removeLeaseDetailCommand
			_createLeaseDetailFactory = createLeaseDetailFactory
			_inventory = inventory
			RegisterInventory()

			Rooms = New BindableCollection(Of IRoomModel)
			Lease = New EditableLeaseModel
			AddHandler Lease.Details.CollectionChanged, AddressOf OnDetailsUpdated

			_originalDetails = New List(Of EditableLeaseDetailModel)
		End Sub

		Public Sub Init()
			Rooms.Clear()
			Rooms.AddRange( _getAllRoomsQuery.Execute() )

			CustomerCategories.Clear()
			CustomerCategories.AddRange( _getAllCustomerCategoriesQuery.Execute() )

			_roomCapacity = _getParametersQuery.Execute().RoomCapacity
			InitValues()
		End Sub

		Public Async Function InitAsync() As Task
			Rooms.Clear()
			Rooms.AddRange( (Await _getAllRoomsQuery.ExecuteAsync()) )

			CustomerCategories.Clear()
			CustomerCategories.AddRange( Await _getAllCustomerCategoriesQuery.ExecuteAsync() )

			_roomCapacity = (Await _getParametersQuery.ExecuteAsync()).RoomCapacity
			InitValues()
		End Function

		Private Sub InitValues()
			Lease.Id = String.Empty
			Lease.Room = Rooms.FirstOrDefault()
			Lease.RoomUnitPrice = 0
			Lease.CheckinDate = Today
			Lease.ExpectedCheckoutDate = Today

			_originalcheckinDate = Lease.CheckinDate
			_originalexpectedCheckoutDate = Lease.ExpectedCheckoutDate
			_originalroomUnitPrice = Lease.RoomUnitPrice
			_originalroomId = Lease.Room.Id
			_originalDetails.Clear()
			_isEdited = False
		End Sub

		Private Sub ResetValues()
			Lease.Id = String.Empty
			Lease.Room = Rooms.FirstOrDefault()
			Lease.RoomUnitPrice = 0
			Lease.CheckinDate = Today
			Lease.ExpectedCheckoutDate = Today

			_originalcheckinDate = Lease.CheckinDate
			_originalexpectedCheckoutDate = Lease.ExpectedCheckoutDate
			_originalroomUnitPrice = Lease.RoomUnitPrice
			_originalroomId = Lease.Room.Id
			_originalDetails.Clear()
			_isEdited = False
		End Sub

		' Domain actions

		Public Sub SetLease( id As String )
			Dim model = _getLeaseQuery.Execute( id )
			If IsNothing( model ) Then Return

			' add selected room to list
			Dim roomItem = Rooms.FirstOrDefault( Function( r ) r.Id = model.Room.Id )
			If IsNothing( roomItem )
				ShowStaticBottomNotification( StaticNotificationType.Warning,
				                              $"Không tìm thấy phiếu thuê phòng {id}" )
				Return
			End If

			Lease.Id = id
			Lease.Room = roomItem
			Lease.RoomUnitPrice = model.RoomUnitPrice
			Lease.CheckinDate = model.CheckinDate
			Lease.ExpectedCheckoutDate = model.ExpectedCheckoutDate

			_originalcheckinDate = Lease.CheckinDate
			_originalexpectedCheckoutDate = Lease.ExpectedCheckoutDate
			_originalroomUnitPrice = Lease.RoomUnitPrice
			_originalroomId = Lease.Room.Id

			Lease.Details.Clear()
			_originalDetails.Clear()
			For Each leaseDetailModel As LeaseDetailModel In model.Details
				Dim editableModel = New EditableLeaseDetailModel()
				editableModel.Id = leaseDetailModel.Id
				editableModel.CustomerName = leaseDetailModel.CustomerName
				editableModel.CustomerLicenseId = leaseDetailModel.CustomerLicenseId
				editableModel.CustomerAddress = leaseDetailModel.CustomerAddress

				Dim customerCategory = CustomerCategories.FirstOrDefault( Function( c ) c.Id = leaseDetailModel.CustomerCategory.Id )
				If IsNothing( customerCategory )
					ShowStaticBottomNotification( StaticNotificationType.Warning,
					                              "Không tìm thấy loại khách hàng tương ứng" )
					Continue For
				End If

				editableModel.CustomerCategory = customerCategory
				Lease.Details.Add( editableModel )

				' backup details for saving
				Dim originalModel = New EditableLeaseDetailModel()
				originalModel.Id = leaseDetailModel.Id
				originalModel.CustomerName = leaseDetailModel.CustomerName
				originalModel.CustomerLicenseId = leaseDetailModel.CustomerLicenseId
				originalModel.CustomerAddress = leaseDetailModel.CustomerAddress
				originalModel.CustomerCategory = customerCategory
				_originalDetails.Add( originalModel )
			Next

			_isEdited = False
		End Sub

		Public Async Function SetLeaseAsync( id As String ) As Task
			ShowStaticWindowLoadingDialog()
			Dim model = Await _getLeaseQuery.ExecuteAsync( id )
			CloseStaticWindowDialog()

			If IsNothing( model ) Then Return

			' add selected room to list
			Dim roomItem = Rooms.FirstOrDefault( Function( r ) r.Id = model.Room.Id )
			If IsNothing( roomItem )
				ShowStaticBottomNotification( StaticNotificationType.Warning,
				                              $"Không tìm thấy phiếu thuê phòng {id}" )
				Return
			End If

			Lease.Id = id
			Lease.Room = roomItem
			Lease.RoomUnitPrice = model.RoomUnitPrice
			Lease.CheckinDate = model.CheckinDate
			Lease.ExpectedCheckoutDate = model.ExpectedCheckoutDate

			_originalcheckinDate = Lease.CheckinDate
			_originalexpectedCheckoutDate = Lease.ExpectedCheckoutDate
			_originalroomUnitPrice = Lease.RoomUnitPrice
			_originalroomId = Lease.Room.Id

			Lease.Details.Clear()
			_originalDetails.Clear()
			For Each leaseDetailModel As LeaseDetailModel In model.Details
				Dim editableModel = New EditableLeaseDetailModel()
				editableModel.Id = leaseDetailModel.Id
				editableModel.CustomerName = leaseDetailModel.CustomerName
				editableModel.CustomerLicenseId = leaseDetailModel.CustomerLicenseId
				editableModel.CustomerAddress = leaseDetailModel.CustomerAddress

				Dim customerCategory = CustomerCategories.FirstOrDefault( Function( c ) c.Id = leaseDetailModel.CustomerCategory.Id )
				If IsNothing( customerCategory )
					ShowStaticBottomNotification( StaticNotificationType.Warning,
					                              "Không tìm thấy loại khách hàng tương ứng" )
					Continue For
				End If

				editableModel.CustomerCategory = customerCategory
				Lease.Details.Add( editableModel )

				' backup details for saving
				Dim originalModel = New EditableLeaseDetailModel()
				originalModel.Id = leaseDetailModel.Id
				originalModel.CustomerName = leaseDetailModel.CustomerName
				originalModel.CustomerLicenseId = leaseDetailModel.CustomerLicenseId
				originalModel.CustomerAddress = leaseDetailModel.CustomerAddress
				originalModel.CustomerCategory = customerCategory
				_originalDetails.Add( originalModel )
			Next

			_isEdited = False
		End Function

		Public Sub AddEmptyDetail()
			If CanAddDetail Then _
				Lease.Details.Add( New EditableLeaseDetailModel )
		End Sub

		Private Sub OnDetailsUpdated( sender As Object,
		                              e As NotifyCollectionChangedEventArgs )
			_isEdited = True
			NotifyOfPropertyChange( Function() CanAddDetail )
			NotifyOfPropertyChange( Function() CanDeleteDetail )

			' add handler
			If e.NewItems IsNot Nothing
				For Each newItem As Object In e.NewItems
					Dim detail = TryCast(newItem, EditableLeaseDetailModel)
					If IsNothing( detail ) Then Continue For

					AddHandler detail.PropertyChanged, AddressOf OnSingleDetailChanged
				Next
			End If

			' remove handler
			If e.OldItems IsNot Nothing
				For Each oldItem As Object In e.OldItems
					Dim detail = TryCast(oldItem, EditableLeaseDetailModel)
					If IsNothing( detail ) Then Continue For

					RemoveHandler detail.PropertyChanged, AddressOf OnSingleDetailChanged
				Next
			End If
		End Sub

		Private Sub OnSingleDetailChanged( sender As Object,
		                                   e As PropertyChangedEventArgs )
			_isEdited = True
		End Sub

		Public Sub PreviewOrder()
		End Sub

		' Exit

		Public Overrides ReadOnly Property IsEdited As Boolean
			Get
				Return _isEdited OrElse CheckForPendingChanges()
			End Get
		End Property

		Private Function CheckForPendingChanges() As Boolean
			If Not Equals( _originalcheckinDate.Date, Lease.CheckinDate.Date ) Then Return True
			If Not Equals( _originalexpectedCheckoutDate.Date, Lease.ExpectedCheckoutDate.Date ) Then Return True
			If Not Equals( _originalroomUnitPrice, Lease.RoomUnitPrice ) Then Return True
			If Not Equals( _originalroomId, Lease.Room.Id ) Then Return True
			If Not Equals( _originalDetails.Count, Lease.Details.Count ) Then Return True
			Return False
		End Function

		Public Overrides Function ActualExitAsync() As Task
			ResetValues()

			ParentWorkspace.NavigateToScreenLeasesList()
			Return MyBase.ActualExitAsync()
		End Function

		' Save
		Public Overrides Function CanSave() As Task(Of Boolean)
			If IsNothing( Lease ) OrElse String.IsNullOrWhiteSpace( Lease.Id ) Then Return Task.FromResult( False )

			If Lease.ExpectedCheckoutDate.Date < Lease.CheckinDate.Date
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              "Ngày dự kiến trả phải sau ngày đăng ký thuê phòng" )
				Return Task.FromResult( False )
			End If

			If IsNothing( Lease.Room ) OrElse String.IsNullOrWhiteSpace( Lease.Room.Id )
				ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng chọn phòng thuê" )
				Return Task.FromResult( False )
			End If

			If (Not String.Equals( Lease.Room.Id, _originalroomId )) And Lease.Room.State = 1
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Phòng {Lease.Room.Name} đang thuê bởi khách hàng khác. Vui lòng chọn phòng khác" )
				Return Task.FromResult( False )
			End If

			If Lease.Details.Count > _roomCapacity
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Vượt quá số khách quy định. Mỗi phòng chỉ có tối đa {_roomCapacity} người" )
				Return Task.FromResult( False )
			End If

			If Lease.Details.Count <= 0
				ShowStaticBottomNotification( StaticNotificationType.Information,
				                              $"Phải có ít nhất 1 khách thuê phòng" )
				Return Task.FromResult( False )
			End If

			For Each detail As EditableLeaseDetailModel In Lease.Details
				If String.IsNullOrWhiteSpace( detail.CustomerName )
					ShowStaticBottomNotification( StaticNotificationType.Information, "Vui lòng nhập đầy đủ tên khách hàng" )
					Return Task.FromResult( False )
				End If

				If IsNothing( detail.CustomerCategory ) OrElse String.IsNullOrWhiteSpace( detail.CustomerCategory.Id )
					ShowStaticBottomNotification( StaticNotificationType.Information, "Vui chọn đầy đủ loại khách hàng" )
					Return Task.FromResult( False )
				End If
			Next

			Return Task.FromResult( True )
		End Function

		Public Overrides Async Function ActualSaveAsync() As Task
			' try update
			ShowStaticWindowLoadingDialog()

			' try update lease details
			Dim hasError = False

			For Each detail As EditableLeaseDetailModel In Lease.Details
				If Not String.IsNullOrWhiteSpace( detail.Id )
					' update lease detail
					If Not Await UpdateLeaseDetailAsync( detail )
						hasError = True
						Exit For
					End If
				Else
					' create lease detail
					If Not Await CreateLeaseDetailAsync( detail )
						hasError = True
						Exit For
					End If
				End If
			Next

			' remove lease detail
			For Each originalDetail As EditableLeaseDetailModel In _originalDetails
				If Not Lease.Details.Any( Function( d ) d.Id = originalDetail.Id )
					If Not Await RemoveLeaseDetailAsync( originalDetail )
						hasError = True
						Exit For
					End If
				End If
			Next

			' try update lease
			If hasError
				CloseStaticWindowDialog()
				Return
			End If

			Dim err = Await _updateLeaseCommand.ExecuteAsync( Lease.Id, Lease.Room.Id, Lease.ExpectedCheckoutDate )

			If String.IsNullOrEmpty( err )
				Await OnSaveSuccessAsync()
			Else
				OnSaveFail( err )
			End If
			CloseStaticWindowDialog()
		End Function

		Private Async Function UpdateLeaseDetailAsync( detail As EditableLeaseDetailModel ) As Task(Of Boolean)
			Dim err = Await _updateLeaseDetailCommand.ExecuteAsync( detail.Id,
			                                                        detail.CustomerName,
			                                                        detail.CustomerLicenseId,
			                                                        detail.CustomerAddress,
			                                                        detail.CustomerCategory.Id )
			' error
			If Not String.IsNullOrEmpty( err )
				OnSaveFail( err )
				Return False
			End If

			Return True
		End Function

		Private Async Function CreateLeaseDetailAsync( detail As EditableLeaseDetailModel ) As Task(Of Boolean)
			' create lease detail
			Dim newId = Await _createLeaseDetailFactory.ExecuteAsync( Lease.Id,
			                                                          detail.CustomerName,
			                                                          detail.CustomerLicenseId,
			                                                          detail.CustomerAddress,
			                                                          detail.CustomerCategory.Id )
			If String.IsNullOrEmpty( newId )
				OnSaveFail( $"Gặp sự cố trong lúc tạo chi tiết thuê phòng cho khách hàng {detail.CustomerName}" )
				Return False
			End If

			Return True
		End Function

		Private Async Function RemoveLeaseDetailAsync( detail As EditableLeaseDetailModel ) As Task(Of Boolean)
			Dim err = Await _removeLeaseDetailCommand.ExecuteAsync( detail.Id )
			' error
			If Not String.IsNullOrEmpty( err )
				OnSaveFail( err )
				Return False
			End If

			Return True
		End Function

		Private Async Function OnSaveSuccessAsync() As Task
			Await _inventory.OnLeaseUpdatedAsync( Lease.Id )
			Await ActualExitAsync()
		End Function

		Private Sub OnSaveFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		' Delete
		Public Overrides Async Function ActualDeleteAsync() As Task
			' try update
			ShowStaticWindowLoadingDialog()
			Dim err = Await _removeLeaseCommand.ExecuteAsync( Lease.Id )

			If String.IsNullOrEmpty( err )
				Await OnDeleteSuccessAsync()
			Else
				OnDeleteFail( err )
			End If

			CloseStaticWindowDialog()
		End Function

		Private Async Function OnDeleteSuccessAsync() As Task
			Await _inventory.OnLeaseRemovedAsync( Lease.Id )
			Await ActualExitAsync()
		End Function

		Private Sub OnDeleteFail( err As String )
			ShowStaticBottomNotification( StaticNotificationType.Error, err )
		End Sub

		Public Overrides Function CanDelete() As Task(Of Boolean)
			If IsNothing( Lease ) OrElse String.IsNullOrWhiteSpace( Lease.Id ) Then Return Task.FromResult( False )

			Return MyBase.CanDelete()
		End Function

		' Infrastructure
		Public Sub OnRoomAdded( model As IRoomModel ) Implements IRoomsListPresenter.OnRoomAdded
			If Rooms.Any( Function( r ) r.Id = model.Id )
				ShowStaticBottomNotification( StaticNotificationType.Warning,
				                              "Tìm thấy phòng trùng nhau trong danh sách" )
				Return
			End If

			Rooms.Add( model )
		End Sub

		Public Sub OnRoomUpdated( model As IRoomModel ) Implements IRoomsListPresenter.OnRoomUpdated
			Dim roomToUpdate = Rooms.FirstOrDefault( Function( r ) r.Id = model.Id )
			If IsNothing( roomToUpdate )
				ShowStaticBottomNotification( StaticNotificationType.Warning,
				                              "Không tìm thấy phòng trong danh sách để cập nhật" )
				Return
			End If

			Rooms( Rooms.IndexOf( roomToUpdate ) ) = model
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
